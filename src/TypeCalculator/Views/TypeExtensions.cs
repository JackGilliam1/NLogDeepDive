using System.Collections.Generic;
using System.Linq;
using NLog;
using TypeCalculator.Core;

namespace TypeCalculator.Views
{
    public static class TypeExtensions
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Add(this IDictionary<ElementType, IList<ElementType>> elementDictionary, ElementType element, params ElementType[] elementTypes)
        {
            Logger.Debug("Adding type {0} to dictionary", element);
            elementDictionary.Add(element, elementTypes.ToList());
        }
    }
}