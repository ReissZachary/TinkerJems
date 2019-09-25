using Moq;
using System;
using TechTalk.SpecFlow;
using TinkerJems.Wpf.ViewModels;

namespace TInkerJems.Tests
{
    [Binding]
    public class AddItemEntriesSteps
    {

        private readonly ScenarioContext context;

        public AddItemEntriesSteps(ScenarioContext context)
        {
            this.context = context;
        }

        [Given(@"the name textbox contains text")]
        public void GivenTheNameTextboxContainsText()
        {
            var model = new AddItemViewModel();
            var name = model.Name;

        }

        [When(@"the user clicks the add item button")]
        public void WhenTheUserClicksTheAddItemButton()
        {
            throw new PendingStepException();
        }

        [Then(@"The item is added")]
        public void ThenTheItemIsAdded()
        {
            throw new PendingStepException();
        }
    }
   
}
