using OpenQA.Selenium;
using Serenity.Fixtures;
using StoryTeller.Engine;
using TypeCalculator.Views.Home;

namespace TypeCalculator.StoryTeller.Fixtures
{
    public class NavigationFixture : ScreenFixture
    {
        [FormatAs("Navigate to Home Page")]
        public void NavigateHome()
        {
            Navigation.NavigateTo(new HomeInputModel());
        }

        [FormatAs("Should see type input box")]
        public bool ShouldSeeTypeInputBox()
        {
            return Driver.FindElement(By.Id("typeDropdown")).Displayed;
        }
    }
}
