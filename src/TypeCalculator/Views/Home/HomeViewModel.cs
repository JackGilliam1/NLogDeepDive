using NLog;

namespace TypeCalculator.Views.Home
{
    public class HomeViewModel
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public HomeViewModel()
        {
            _logger.Trace("View Model was created");
        }
    }
}