using System.Diagnostics;
using System.Text;

using Microsoft.CodeAnalysis;

namespace ResultZ.Generator
{
    [Generator]
    internal class ResultGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            if (!Debugger.IsAttached)
            {
                // uncomment to debug
                ////Debugger.Launch();
            }
#endif

            context.RegisterForSyntaxNotifications(() => new ResultSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var syntaxReceiver = (ResultSyntaxReceiver)context.SyntaxReceiver!;

            var resultDeclaration = syntaxReceiver.ResultDeclaration;

            var generatedCode = new StringBuilder(@"
namespace ResultZ
{
    public static partial class Result
    {");

            foreach (var methodDeclaration in syntaxReceiver.MethodDeclarations)
            {
                var syntaxTree = methodDeclaration.SyntaxTree;
                var semanticModel = context.Compilation.GetSemanticModel(syntaxTree);
                var rewriter = new ResultMethodRewriter(semanticModel);
                var modifiedNode = rewriter.Visit(syntaxTree.GetRoot());
                generatedCode.Append(@$"
{modifiedNode}");
            }

            generatedCode.Append(@"
    }
}");
            context.AddSource(nameof(ResultGenerator), generatedCode.ToString());
        }
    }
}
