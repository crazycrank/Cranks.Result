using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ResultZ.Generator
{
    internal class ResultSyntaxReceiver : ISyntaxReceiver
    {
        public List<MethodDeclarationSyntax> MethodDeclarations { get; } = new();

        public ClassDeclarationSyntax ResultDeclaration { get; private set; }

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {

            switch (syntaxNode)
            {
                case MethodDeclarationSyntax
                     {
                         Parent: ClassDeclarationSyntax { Identifier: { ValueText: "ResultExtensions" } },
                         Modifiers: var modifiers,
                         ParameterList: { Parameters: var parameters },
                     } methodDeclarationSyntax:
                    if (modifiers.Any(t => t is { Text: "public" })
                        && modifiers.Any(t => t is { Text: "static" })
                        && parameters[0].Modifiers.Any(t => t is { Text: "this" }))
                    {
                        MethodDeclarations.Add(methodDeclarationSyntax);
                    }

                    break;

                case ClassDeclarationSyntax { Identifier: { ValueText: "Results" } } resultsDeclaration:
                    ResultDeclaration = resultsDeclaration;
                    break;
            }
        }
    }
}
