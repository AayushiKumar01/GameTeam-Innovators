using NUnit.Framework;

using Game;
using Game.Views;
using Game.ViewModels;
using Game.Models;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using Game.GameRules;

namespace UnitTests.Views
{
    [TestFixture]
    public class MonsterUpdatePageTests : MonsterUpdatePage
    {
        App app;
        MonsterUpdatePage page;

        public MonsterUpdatePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new MonsterUpdatePage(new GenericViewModel<MonsterModel>(new MonsterModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void MonsterUpdatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void MonsterUpdatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_Save_Clicked_Empty_Description_Should_Pass()
        {
            // Arrange
            ((Entry)page.FindByName("MonsterDescription")).Text = "";

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_Save_Clicked_Empty_Name_Should_Pass()
        {
            // Arrange
            ((Entry)page.FindByName("MonsterName")).Text = "";

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_Save_Clicked_Null_Image_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.ImageURI = null;

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            _ = OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_Attack_OnStepperValueChanged_Default_Should_Pass()
        {
            // Arrange
            var data = new MonsterModel();
            var ViewModel = new GenericViewModel<MonsterModel>(data);

            page = new MonsterUpdatePage(ViewModel);
            var oldValue = 0.0;
            var newValue = 1.0;

            var args = new ValueChangedEventArgs(oldValue, newValue);

            // Act
            page.Attack_OnSliderValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_Defense_OnStepperValueChanged_Default_Should_Pass()
        {
            // Arrange
            var data = new MonsterModel();
            var ViewModel = new GenericViewModel<MonsterModel>(data);

            page = new MonsterUpdatePage(ViewModel);
            var oldRange = 0.0;
            var newRange = 1.0;

            var args = new ValueChangedEventArgs(oldRange, newRange);

            // Act
            page.Defense_OnSliderValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_Speed_OnStepperDamageChanged_Default_Should_Pass()
        {
            // Arrange
            var data = new MonsterModel();
            var ViewModel = new GenericViewModel<MonsterModel>(data);

            page = new MonsterUpdatePage(ViewModel);
            var oldDamage = 0.0;
            var newDamage = 1.0;

            var args = new ValueChangedEventArgs(oldDamage, newDamage);

            // Act
            page.Speed_OnSliderValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_LeftArrow_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.LeftArrow_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_RightArrow_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.RightArrow_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_ChangeImageByIncrement_Valid_Increment_Should_Pass()
        {
            // Arrange
            page.ViewModel = new GenericViewModel<MonsterModel>() { Data = new MonsterModel() { ImageURI = "monster3.gif" } };

            var indexCurrent = RandomPlayerHelper.MonsterImageList.IndexOf(page.ViewModel.Data.ImageURI);

            // Act
            var result = page.ChangeImageByIncrement(1);

            // Reset

            // Assert
            Assert.AreEqual(true, indexCurrent == result - 1);
        }

        [Test]
        public void MonsterUpdatePage_ChangeImageByIncrement_Invalid_Decrement_Zero_Should_Pass()
        {
            // Arrange

            // set to the first in the list, zero
            page.ViewModel = new GenericViewModel<MonsterModel>() { Data = new MonsterModel() { ImageURI = "monster1.gif" } };

            // Act
            var result = page.ChangeImageByIncrement(-1);

            // Reset

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void MonsterUpdatePage_ChangeImageByIncrement_Valid_Decrement_Should_Pass()
        {
            // Arrange
            page.ViewModel = new GenericViewModel<MonsterModel>() { Data = new MonsterModel() { ImageURI = "monster3.gif" } };

            var indexCurrent = RandomPlayerHelper.MonsterImageList.IndexOf(page.ViewModel.Data.ImageURI);

            // Act
            var result = page.ChangeImageByIncrement(-1);

            // Reset

            // Assert
            Assert.AreEqual(true, indexCurrent == result + 1);
        }

        [Test]
        public void MonsterUpdatePage_ChangeImageByIncrement_Invalid_Increment_Overflow_Should_Pass()
        {
            // Arrange

            // set to the last in the list
            page.ViewModel = new GenericViewModel<MonsterModel>() { Data = new MonsterModel() { ImageURI = "monsterdrafts8.png" } };

            // Act
            var result = page.ChangeImageByIncrement(1);

            // Reset

            // Assert
            Assert.AreEqual(7, result);
        }

    }
}