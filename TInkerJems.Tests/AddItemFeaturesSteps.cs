using System;
using TechTalk.SpecFlow;

namespace TInkerJems.Tests
{
    [Binding]
    public class AddItemFeaturesSteps
    {
        private readonly ScenarioContext context;

        public AddItemFeaturesSteps(ScenarioContext context)
        {
            this.context = context;
        }

        [Given(@"the name box is not empty")]
        public void GivenTheNameBoxIsNotEmpty()
        {
            throw new PendingStepException();
        }

        [When(@"they click ""([^""]*)"" button")]
        public void WhenTheyClickButton(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"the item is added")]
        public void ThenTheItemIsAdded()
        {
            throw new PendingStepException();
        }
    }
}
