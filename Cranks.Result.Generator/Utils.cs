using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cranks.Result.Generator
{
    internal static class Utils
    {
        internal static SyntaxNode FindCompilationUnit(this SyntaxNode node)
        {
            return node is CompilationUnitSyntax
                       ? node
                       : node.Parent!.FindCompilationUnit();
        }
    }
}
