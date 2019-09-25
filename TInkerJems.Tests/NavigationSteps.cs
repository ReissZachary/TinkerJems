using Moq;
using Prism.Regions;
using System;
using TechTalk.SpecFlow;
using TinkerJems.Wpf;

namespace TInkerJems.Tests
{
    [Binding]
    public class NavigationSteps
    {
        private readonly ScenarioContext context;

        public NavigationSteps(ScenarioContext context)
        {
            var mockRegionManager = new Mock<IRegionManager>();
            this.context = context;
            context.Add("mockRegionManager", mockRegionManager);

        }

        [Given(@"the apps runs")]
        public void GivenTheAppsRuns()
        {
            var mockRegion = context.Get<Mock<IRegionManager>>("mockRegionManager");
            var app = new MainWindowViewModel(mockRegion.Object);

            context.Add("mainWindowViewModel", app);
        }

        [When(@"the add item button is clicked")]
        public void WhenTheAddItemButtonIsClicked()
        {
            var mockRegion = context.Get<Mock<IRegionManager>>("mockRegionManager");
            mockRegion.Setup(rm => rm.RequestNavigate("ContentRegion", "AddItemView")).Verifiable();

            var view = context.Get<MainWindowViewModel>("mainWindowViewModel");
            view.AddItemCommand.Execute("AddItemView");
        }

        [Then(@"add item view can be seen")]
        public void ThenAddItemViewCanBeSeen()
        {
            var mockRegion = context.Get<Mock<IRegionManager>>("mockRegionManager");
            mockRegion.Verify();
        }

        [Given(@"the app runs")]
        public void GivenTheAppRuns()
        {
            var mockRegion = context.Get<Mock<IRegionManager>>("mockRegionManager");
            var app = new MainWindowViewModel(mockRegion.Object);

            context.Add("mainWindowViewModel", app);
        }

        [When(@"the edit item button is clicked")]
        public void WhenTheEditItemButtonIsClicked()
        {
            var mockRegion = context.Get<Mock<IRegionManager>>("mockRegionManager");
            mockRegion.Setup(x => x.RequestNavigate("ContentRegion", "EditItemView")).Verifiable();

            var view = context.Get<MainWindowViewModel>("mainWindowViewModel");
            view.EditItemCommand.Execute("EditItemView");
        }

        [Then(@"edit item view can be seen")]
        public void ThenEditItemViewCanBeSeen()
        {
            var mockRegion = context.Get<Mock<IRegionManager>>("mockRegionManager");
            mockRegion.Verify();
        }
    }
}
