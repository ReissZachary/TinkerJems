using FluentAssertions;
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

        [Given(@"the name textbox contains a ""([^""]*)""")]
        public void GivenTheNameTextboxContainsA(string name)
        {
            //var model = new AddItemViewModel();
            //var name = model.Name;
            context.Add("name", name);

        }

        [When(@"the user clicks the add item button")]
        public void WhenTheUserClicksTheAddItemButton()
        {
            var name = context.Get<string>("name");
            context.Add("result", name);
        }

        [Then(@"The item with ""([^""]*)"" is added")]
        public void ThenTheItemWithIsAdded(string name)
        {
            var actual = context.Get<string>("name");
            actual.Should().Be(name);
        }


    }
}
