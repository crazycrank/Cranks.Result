using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cranks.Result.Generator
{
    internal class ResultSyntaxReceiver : ISyntaxReceiver
    {
        public HashSet<ClassDeclarationSyntax> ResultExtensionDeclarations { get; } = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            switch (syntaxNode)
            {
                case MethodDeclarationSyntax
                     {
                         Parent: ClassDeclarationSyntax { Identifier: { ValueText: "ResultExtensions" } } resultExtensionsDeclaration,
                         Modifiers: var modifiers,
                         ParameterList: { Parameters: var parameters },
                     }:
                    if (modifiers.Any(t => t is { Text: "public" })
                        && modifiers.Any(t => t is { Text: "static" })
                        && parameters[0].Modifiers.Any(t => t is { Text: "this" }))
                    {
                        ResultExtensionDeclarations.Add(resultExtensionsDeclaration);
                    }

                    break;
            }
        }
    }
}
