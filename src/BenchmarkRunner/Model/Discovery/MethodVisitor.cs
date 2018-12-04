using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace BenchmarkRunner.Model
{
    public class MethodVisitor : CSharpSyntaxRewriter
    {
        public List<MethodDeclarationSyntax> BenchmarkMethods { get; } = new List<MethodDeclarationSyntax>();
                       
        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (IsBenchmark(node))
                BenchmarkMethods.Add(node);

            return base.VisitMethodDeclaration(node);
        }

        private bool IsBenchmark(MethodDeclarationSyntax node)
        {
            if (node.AttributeLists == null || node.AttributeLists.Count == 0)
                return false;

            foreach (var attributeList in node.AttributeLists)
            {
                foreach (var attribute in attributeList.Attributes)
                {
                    if (attribute.Name is IdentifierNameSyntax identifier && identifier.Identifier.ValueText == "Benchmark")
                        return true;
                }
            }
            return false;
        }
    }
}
