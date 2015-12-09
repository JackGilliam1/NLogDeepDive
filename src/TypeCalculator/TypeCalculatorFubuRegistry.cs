using FubuMVC.Core;
using FubuMVC.Spark;
using TypeCalculator.Views.Home;

namespace TypeCalculator
{
	public class TypeCalculatorFubuRegistry : FubuRegistry
	{
		public TypeCalculatorFubuRegistry()
		{
            Routes.HomeIs<HomeInputModel>();
		    // Register any custom FubuMVC policies, inclusions, or 
		    // other FubuMVC configuration here
		    // Or leave as is to use the default conventions unchanged
		}
	}
}