using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cranks.Result.Generator
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
                Debugger.Launch();
            }
#endif

            context.RegisterForSyntaxNotifications(() => new ResultSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var syntaxReceiver = (ResultSyntaxReceiver)context.SyntaxReceiver!;

            foreach (var resultExtensionDeclaration in syntaxReceiver.ResultExtensionDeclarations)
            {
                var rewriter = new ResultMethodRewriter();
                var modifiedNode = rewriter.Visit(resultExtensionDeclaration.SyntaxTree.GetRoot());

                context.AddSource(GetHintName(resultExtensionDeclaration), modifiedNode.GetText(Encoding.UTF8));
            }
        }

        private static string GetHintName(ClassDeclarationSyntax classDeclaration)
        {
            var compilationUnit = FindCompilationUnit(classDeclaration) as CompilationUnitSyntax;
            var filePath = compilationUnit!.SyntaxTree.FilePath;

            return $"Result.{Path.GetFileNameWithoutExtension(filePath).Split('.').Last()}";

            static SyntaxNode FindCompilationUnit(SyntaxNode node)
            {
                return node is CompilationUnitSyntax ? node : FindCompilationUnit(node.Parent!);
            }
        }
    }
}
