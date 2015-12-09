using System;
using NLog.Interface;

namespace TypeCalculator.Views.Home
{
	public class HomeEndpoint
	{
	    private readonly ILogger _logger;

	    public HomeEndpoint(ILogger logger)
	    {
	        _logger = logger;
	    }

		public HomeViewModel Index(HomeInputModel input)
		{
		    try
		    {
		        throw new Exception("Problem Happened");
		    }
		    catch (Exception e)
		    {
                _logger.Fatal("Problem Happened in Home Input Model", e);
		    }
		    return new HomeViewModel();
		}
	}
}