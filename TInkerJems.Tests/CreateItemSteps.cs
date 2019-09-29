using Moq;
using NUnit.Framework;
using Prism.Regions;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TinkerJems.Core.Models;
using TinkerJems.Wpf;

namespace TInkerJems.Tests
{
    [Binding]
    public class CreateItemSteps
    {
        private readonly Dictionary<int, JewelryItem> MyObject;
        private readonly ScenarioContext context;

        public CreateItemSteps(ScenarioContext context)
        {
            var mockRegionManager = new Mock<IRegionManager>();
            this.context = context;
            context.Add("mockRegionManager", mockRegionManager);
            this.MyObject = new Dictionary<int, JewelryItem>();
        }

        [Given(@"I am at the start page")]
        public void GivenIAmAtTheStartPage()
        {
            var mockRegion = context.Get<Mock<IRegionManager>>("mockRegionManager");
            var app = new MainWindowViewModel(mockRegion.Object);

            context.Add("mainWindowViewModel", app);
        }

        [When(@"I click on Add Item button")]
        public void WhenIClickOnAddItemButton()
        {
            var mockRegion = context.Get<Mock<IRegionManager>>("mockRegionManager");
            var app = new MainWindowViewModel(mockRegion.Object);

            context.Add("mainWindowViewModel", app);
        }

        [Then(@"I can see the add item page")]
        public void ThenICanSeeTheAddItemPage()
        {
            var mockRegion = context.Get<Mock<IRegionManager>>("mockRegionManager");
            mockRegion.Verify();
        }

        //-----------------------------------------------------------------------------------//


        [Given(@"I entered the following data into boxes")]
        public void GivenIEnteredTheFollowingDataIntoBoxes(Table table)
        {
            var rows = table.CreateSet<JewelryItem>();
            //Store each row and use id as key
            foreach( JewelryItem i in rows)
            {
                this.MyObject.Add(i.Id, i);
            }
        }

        [Then(@"My binding should have the following data")]
        public void ThenMyBindingShouldHaveTheFollowingData(Table table)
        {
            var Expected = table.CreateSet<JewelryItem>();

            //foreach loops check to see if object is actually present
            foreach(JewelryItem expectedItem in Expected)
            {
                JewelryItem actual = this.MyObject[expectedItem.Id];
                if(false == actual.Name.Equals(expectedItem.Name))
                {
                    Assert.Fail(String.Format(
                        "Expected name '{0}', actual text was {1}",
                        expectedItem.Name,
                        actual.Name));
                }

                if (false == actual.Price.Equals(expectedItem.Name))
                {
                    Assert.Fail(String.Format(
                        "Expected price '{0}', actual text was {1}",
                        expectedItem.Price,
                        actual.Price));
                }

                if (false == actual.Description.Equals(expectedItem.Name))
                {
                    Assert.Fail(String.Format(
                        "Expected description '{0}', actual text was {1}",
                        expectedItem.Description,
                        actual.Description));
                }

                if (false == actual.Name.Equals(expectedItem.Name))
                {
                    Assert.Fail(String.Format(
                        "Expected long description '{0}', actual text was {1}",
                        expectedItem.LongDescription,
                        actual.LongDescription));
                }

            }
        }

        //---------------------------------------------------------------------------------//

        [Given(@"the user is at the Add Item page")]
        public void GivenTheUserIsAtTheAddItemPage()
        {
            throw new PendingStepException();
        }

        [When(@"the user enters valid credentials")]
        public void WhenTheUserEntersValidCredentials()
        {
            throw new PendingStepException();
        }

        [When(@"clicks the AddItem button")]
        public void WhenClicksTheAddItemButton()
        {
            throw new PendingStepException();
        }

        [Then(@"Item should be added")]
        public void ThenItemShouldBeAdded()
        {
            throw new PendingStepException();
        }


    }
}
