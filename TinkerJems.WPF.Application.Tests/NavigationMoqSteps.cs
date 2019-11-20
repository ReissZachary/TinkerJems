using Moq;
using Prism.Regions;
using System;
using TechTalk.SpecFlow;
using TinkerJems.Wpf.Application.ViewModels;

namespace TinkerJems.WPF.Application.Tests
{
    [Binding]
    public class NavigationMoqSteps
    {
        private readonly ScenarioContext context;
        public NavigationMoqSteps(ScenarioContext context)
        {
            this.context = context;
            var regionManagerMock = new Mock<IRegionManager>();
            context.Add("regionManagerMock", regionManagerMock);
        }

        [Given(@"the app runs and the start page is seen")]
        public void GivenTheAppRunsAndTheStartPageIsSeen()
        {
            var mockRegion = context.Get<Mock<IRegionManager>>("regionManagerMock");
            var app = new MainWindowViewModel(mockRegion.Object);

            context.Add("mainWindowViewModel", app);
        }

        [When(@"the shop now button is pushed")]
        public void WhenTheShopNowButtonIsPushed()
        {
            var mockRegion = context.Get<Mock<IRegionManager>>("regionManagerMock");
            var app = new SearchViewModel(mockRegion.Object);

            context.Add("searchViewModel", app);
        }

        [Then(@"the search view can be seen")]
        public void ThenTheSearchViewCanBeSeen()
        {
            var mockRegion = context.Get<Mock<IRegionManager>>("regionManagerMock");
            mockRegion.Verify();
        }
    }
}
