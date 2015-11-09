using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sizikov.AsyncSuffix
{
    public class AsyncMethodNameSuggestions
    {
        public IEnumerable<string> Get(IMethodDeclaration method)
        {
            if (!method.IsValid())
            {
                return Enumerable.Empty<string>();
            }
            var declared = method.DeclaredElement;
            if (declared != null)
            {
                var newName = declared.ShortName + "Async";
                return new List<string> { newName };
            }
            return Enumerable.Empty<string>();
        }
    }
}
