using FubuMVC.Core;
using FubuMVC.StructureMap;

namespace TypeCalculator
{
	public class TypeCalculatorApplication : IApplicationSource
	{
	    public FubuApplication BuildApplication()
	    {
	        return FubuApplication.For<TypeCalculatorFubuRegistry>()
	            .StructureMap<TypeCalculatorRegistry>();
	    }
	}
}