using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace BenchmarkRunner
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class BenchmarkFinder : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "BenchmarkFinder";
        internal static readonly LocalizableString Title = "BenchmarkFinder Title";
        internal static readonly LocalizableString MessageFormat = "BenchmarkFinder '{0}'";
        internal const string Category = "BenchmarkFinder Category";

        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSymbolAction(AnalyzeMethod, SymbolKind.Method);
        }

        private void AnalyzeMethod(SymbolAnalysisContext context)
        {
            IMethodSymbol methodSymbol = context.Symbol as IMethodSymbol;
            if (methodSymbol == null)
                return;

            ImmutableArray<AttributeData> attributes = methodSymbol.GetAttributes();
            if (attributes == null || attributes.Length == 0)
                return;

            
        }
    }
}
