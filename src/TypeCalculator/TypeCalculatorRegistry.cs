using NLog.Interface;
using StructureMap.Configuration.DSL;
using TypeCalculator.Views;
using TypeCalculator.Views.Home;

namespace TypeCalculator
{
	public class TypeCalculatorRegistry : Registry
	{
		public TypeCalculatorRegistry()
		{
			// make any StructureMap configuration here
			
            // Sets up the default "IFoo is Foo" naming convention
            // for auto-registration within this assembly
            Scan(x => {
                x.AssemblyContainingType<HomeInputModel>();
                x.WithDefaultConventions();
            });

		    For<ITypesDictionary>().Use<TypesDictionary>();


		    For<ILogger>().Use<TypeCalculatorLogger>();
		}
	}
}