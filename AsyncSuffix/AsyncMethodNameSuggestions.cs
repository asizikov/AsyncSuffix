using JetBrains.Annotations;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using System;
using System.Collections.Generic;
using JetBrains.Util;
using JetBrains.Util.Extension;

namespace Sizikov.AsyncSuffix
{
    public class AsyncMethodNameSuggestions
    {
        private const string Async = "Async";
        private static readonly List<string> typos = new List<string>
                {
                    "Asyn", "Asinc", "Asin", "Asynch", "async", "asyn", "asinc", "asynch"
                };

        [NotNull]
        public static List<string> Get([NotNull] IMethodDeclaration method)
        {
            if (method == null) throw new ArgumentNullException("method");
            if (!method.IsValid())
            {
                return (List<string>) EmptyList<string>.InstanceList;
            }
            var declared = method.DeclaredElement;
            if (declared != null)
            {
                var shortName = declared.ShortName;
                var newName = shortName + Async;
                var names = new List<string> { newName };
                
                foreach (var pattern in typos)
                {
                    if (shortName.EndsWith(pattern))
                    {
                        var result = shortName.SubstringBefore(pattern) + Async;
                        names.Add(result);
                    }
                }
                return names;
            }
            return (List<string>)EmptyList<string>.InstanceList;
        }
    }
}
