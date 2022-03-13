using NUnit.Framework;
using System.Linq;

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
    public class CharacterCreatePageTests : CharacterCreatePage
    {
        App app;
        CharacterCreatePage page;

        public CharacterCreatePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new CharacterCreatePage(new GenericViewModel<CharacterModel>(new CharacterModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void CharacterCreatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CharacterCreatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_Save_Clicked_Empty_Name_Shows_Error_Should_Pass()
        {
            // Arrange
            ((Entry)page.FindByName("CharacterName")).Text = "";

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_Save_Clicked_Empty_Description_Shows_Error_Should_Pass()
        {
            // Arrange
            ((Entry)page.FindByName("CharacterDescription")).Text = "";

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
        
        [Test]
        public void CharacterCreatePage_Save_Clicked_Null_Image_Should_Pass()
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
        public void CharacterCreatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            _ = OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_Attack_OnStepperAttackChanged_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterModel();
            var ViewModel = new GenericViewModel<CharacterModel>(data);

            page = new CharacterCreatePage(ViewModel);
            var oldAttack = 0.0;
            var newAttack = 1.0;

            var args = new ValueChangedEventArgs(oldAttack, newAttack);

            // Act
            page.Attack_OnSliderValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_Speed_OnStepperValueChanged_Default_Should_Pass()
        {
            // ArSpeed
            var data = new CharacterModel();
            var ViewModel = new GenericViewModel<CharacterModel>(data);

            page = new CharacterCreatePage(ViewModel);
            var oldSpeed = 0.0;
            var newSpeed = 1.0;

            var args = new ValueChangedEventArgs(oldSpeed, newSpeed);

            // Act
            page.Speed_OnSliderValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_Defense_OnStepperDefenseChanged_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterModel();
            var ViewModel = new GenericViewModel<CharacterModel>(data);

            page = new CharacterCreatePage(ViewModel);
            var oldDefense = 0.0;
            var newDefense = 1.0;

            var args = new ValueChangedEventArgs(oldDefense, newDefense);

            // Act
            page.Defense_OnSliderValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_RollDice_Clicked_Default_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data = new CharacterModel();

            // Act
            page.RollDice_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_ClosePopup_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.ClosePopup();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_ClosePopup_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.ClosePopup_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_ShowPopup_Default_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data = new CharacterModel();

            // Act
            _ = page.ShowPopup(ItemLocationEnum.PrimaryHand);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_OnPopupItemSelected_Clicked_Default_Should_Pass()
        {
            // Arrange

            var data = new ItemModel();

            var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(data, 0);

            // Act
            page.OnPopupItemSelected(null, selectedCharacterChangedEventArgs);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_OnPopupItemSelected_Clicked_Null_Should_Fail()
        {
            // Arrange

            var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(null, 0);

            // Act
            page.OnPopupItemSelected(null, selectedCharacterChangedEventArgs);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_Item_ShowPopup_Default_Should_Pass()
        {
            // Arrange

            var item = page.GetItemToDisplay(ItemLocationEnum.PrimaryHand);

            // Act
            var itemButton = item.Children.FirstOrDefault(m => m.GetType().Name.Equals("Button"));

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCratePage_GetItemToDisplay_Click_Button_Valid_Should_Pass()
        {
            // Arrange
            var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.PrimaryHand);
            page.ViewModel.Data.Head = item.Id;
            var StackItem = page.GetItemToDisplay(ItemLocationEnum.PrimaryHand);
            var dataImage = StackItem.Children[0];

            // Act
            ((ImageButton)dataImage).PropagateUpClicked();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_LeftArrow_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.LeftArrow_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_RightArrow_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.RightArrow_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_ChangeImageByIncrement_Valid_Increment_Should_Pass()
        {
            // Arrange
            page.ViewModel = new GenericViewModel<CharacterModel>() { Data = new CharacterModel() { ImageURI = "char3.gif" } };

            var indexCurrent = RandomPlayerHelper.CharacterImageList.IndexOf(page.ViewModel.Data.ImageURI);

            // Act
            var result = page.ChangeImageByIncrement(1);

            // Reset

            // Assert
            Assert.AreEqual(true, indexCurrent == result - 1);
        }

        [Test]
        public void CharacterCreatePage_ChangeImageByIncrement_Valid_Decrement_Should_Pass()
        {
            // Arrange
            page.ViewModel = new GenericViewModel<CharacterModel>() { Data = new CharacterModel() { ImageURI = "char3.gif" } };

            var indexCurrent = RandomPlayerHelper.CharacterImageList.IndexOf(page.ViewModel.Data.ImageURI);

            // Act
            var result = page.ChangeImageByIncrement(-1);

            // Reset

            // Assert
            Assert.AreEqual(true, indexCurrent == result + 1);
        }

        [Test]
        public void CharacterCreatePage_ChangeImageByIncrement_Invalid_Decrement_Zero_Should_Pass()
        {
            // Arrange

            // set to the first in the list, zero
            page.ViewModel = new GenericViewModel<CharacterModel>() { Data = new CharacterModel() { ImageURI = "char1.gif" } };

            // Act
            var result = page.ChangeImageByIncrement(-1);

            // Reset

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void CharacterCreatePage_ChangeImageByIncrement_Invalid_Increment_Overflow_Should_Pass()
        {
            // Arrange

            // set to the last in the list
            page.ViewModel = new GenericViewModel<CharacterModel>() { Data = new CharacterModel() { ImageURI = "ship8.png" } };

            // Act
            var result = page.ChangeImageByIncrement(1);

            // Reset

            // Assert
            Assert.AreEqual(6, result);
        }

        [Test]
        public void CharacterCreatePage_EnableImageArrowButtons_Valid_Image1_Should_Disable_Left()
        {
            // Arrange
            var LeftButton = (Button)page.FindByName("LeftArrowButton");
            var RightButton = (Button)page.FindByName("RightArrowButton");

            // Set List to Left most
            page.ViewModel.Data.ImageURI = RandomPlayerHelper.CharacterImageList.First();

            // Act
            var result = page.EnableImageArrowButtons();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(false, LeftButton.IsEnabled);
            Assert.AreEqual(true, RightButton.IsEnabled);
        }

        [Test]
        public void CharacterCreatePage_EnableImageArrowButtons_Valid_Image2_Should_Enable_Both()
        {
            // Arrange
            var LeftButton = (Button)page.FindByName("LeftArrowButton");
            var RightButton = (Button)page.FindByName("RightArrowButton");

            // Set List to middle
            page.ViewModel.Data.ImageURI = RandomPlayerHelper.CharacterImageList.ElementAt(2);

            // Act
            var result = page.EnableImageArrowButtons();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, LeftButton.IsEnabled);
            Assert.AreEqual(true, RightButton.IsEnabled);
        }

    }
}