using System.Collections.Generic;

namespace TypeCalculator.Views.Types
{
    public class TypesResponse
    {
        public TypesResponse()
        {
            StrongAttack = new List<string>();
            WeakAttack = new List<string>();
            WeakDefense = new List<string>();
            ImmuneDefense = new List<string>();
            StrongDefense = new List<string>();
        }

        public IEnumerable<string> StrongAttack { get; set; }
        public IEnumerable<string> WeakAttack { get; set; }
        public IEnumerable<string> WeakDefense { get; set; }
        public IEnumerable<string> ImmuneDefense { get; set; }
        public IEnumerable<string> StrongDefense { get; set; }
    }
}