using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Serenity;
using Serenity.Fixtures;
using StoryTeller;
using StoryTeller.Engine;

namespace TypeCalculator.StoryTeller.Fixtures
{
    public class TypeFixture : ScreenFixture
    {
        public TypeFixture()
        {
            var types = new [] {
                "None", "Normal", "Fire", "Water", "Electric", "Grass", "Ice",
                "Fighting", "Poison", "Ground", "Flying", "Psychic", "Bug", "Rock",
                "Ghost", "Dragon", "Dark", "Steel", "Fairy"
            };
            AddSelectionValues("Types", types);
            AddSelectionValues("SelectTypes", types);
            AddSelectionValues("BadgeStatus", new [] {
                "Updating", "Updated"
            });
        }

        [FormatAs("Select Type One: {selectType}")]
        public void SelectType([SelectionValues("SelectTypes")] string selectType)
        {
            Driver.SetData(Driver.FindElement(By.Id("typeSelectOne")), selectType.ToLower());
            WaitForStatus("Updated");
        }

        [FormatAs("Select Type Two: {selectType}")]
        public void SelectTypeTwo([SelectionValues("SelectTypes")] string selectType)
        {
            Driver.SetData(Driver.FindElement(By.Id("typeSelectTwo")), selectType.ToLower());
            WaitForStatus("Updated");
        }

        [FormatAs("Wait for Status {status}")]
        public void WaitForStatus([SelectionValues("BadgeStatus")] string status)
        {
            Wait.Until(() =>
            {
                try
                {
                    return Driver.FindElement(By.Id("#statusBadge")).Text == status;
                }
                catch (Exception e)
                {
                }
                return false;
            }, timeoutInMilliseconds: 2000);
        }

        public IGrammar StrengthsShouldContain()
        {
            return TypesShouldContain("attackSection", "strong", "Strong Attack");
        }

        public IGrammar WeakAttackShouldContain()
        {
            return TypesShouldContain("attackSection", "weak", "Weak Attack");
        }

        public IGrammar WeaknessesShouldContain()
        {
            return TypesShouldContain("defenseSection", "weak", "Weak Defense");
        }

        public IGrammar ImmunitiesShouldContain()
        {
            return TypesShouldContain("defenseSection", "immune", "Immune Defense");
        }

        public IGrammar StrongAgainstShouldContain()
        {
            return TypesShouldContain("defenseSection", "strong", "Strong Defense");
        }

        private IGrammar TypesShouldContain(string section, string type, string listName)
        {
            return VerifyStringList(() => GetTypes(section, type)
                .Select(x => x.Text)
                .ToList())
                .Ordered()
                .Titled("The List " + listName + " Should Contain")
                .Grammar();
        }

        private IEnumerable<IWebElement> GetTypes(string sectionId, string type)
        {
            var columnTypes = Driver.FindElement(By.Id(sectionId))
                .FindElement(By.ClassName(type));
            return columnTypes.FindElements(By.ClassName("type"));
        }
    }
}
