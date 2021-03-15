using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ResultZ.Generator
{
    internal class ResultMethodRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        private TypeSyntax? _resultType;
        private ParameterListSyntax? _parameters;

        public ResultMethodRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitParameterList(ParameterListSyntax node)
        {
            _resultType = node.Parameters[0].Type!;

            var newNode = ParameterList(node.Parameters.RemoveAt(0));
            _parameters = newNode;

            return node.ReplaceNode(node, newNode);
        }

        public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            VisitParameterList(node.ParameterList);

            if (node.Body is not null)
            {
                node = node.RemoveNode(node.Body, SyntaxRemoveOptions.KeepNoTrivia)!;
            }

            if (node.ExpressionBody is not null)
            {
                node = node.RemoveNode(node.ExpressionBody, SyntaxRemoveOptions.KeepNoTrivia)!;
            }

            var arrowExpression = ArrowExpressionClause(InvocationExpression(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                                                                                    ObjectCreationExpression(ParseTypeName(_resultType is GenericNameSyntax
                                                                                                        ? "Passed<TValue>"
                                                                                                        : "Passed")),
                                                                                                    IdentifierName(node.Identifier)),
                                                                             ArgumentList(new SeparatedSyntaxList<ArgumentSyntax>()
                                                                                              .AddRange(_parameters!.Parameters.Select(p => Argument(IdentifierName(p.Identifier)))))));


            return node.WithExpressionBody(arrowExpression);
        }
    }
}
