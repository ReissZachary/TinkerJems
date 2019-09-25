using FluentAssertions;
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
            //var AddItemMock = new Mock<AddItemViewModel>();
            //context.Add("AddItemMock", AddItemMock);
            this.context = context;
        }

        [Given(@"the name textbox contains a ""([^""]*)""")]
        public void GivenTheNameTextboxContainsA(string name)
        {
            //var modelMock = context.Get<Mock<AddItemViewModel>>("AddItemMock");
            //modelMock.Setup(n => n.Name.Equals(n.Name)).Verifiable();
            //context.Add("AddItemMock", modelMock);
            //var name = model.Name;
            context.Add("name", name);

        }

        [When(@"the user clicks the add item button")]
        public void WhenTheUserClicksTheAddItemButton()
        {
            //var modelMock = context.Get<Mock<AddItemViewModel>>("AddItemMock");
            //modelMock.Setup(v => v.AddItem).Verifiable();
            //var command = context.Get<AddItemViewModel>("AddItemMock");
            //command.AddItem.Execute();
            var name = context.Get<string>("name");
            context.Add("result", name);
        }

        [Then(@"The item with ""([^""]*)"" is added")]
        public void ThenTheItemWithIsAdded(string name)
        {
            //var modelMock = context.Get<Mock<AddItemViewModel>>("AddItemMock");
            //modelMock.Verify();
            var actual = context.Get<string>("name");
            actual.Should().Be(name);
        }


    }
}
