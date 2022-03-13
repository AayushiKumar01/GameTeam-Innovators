﻿using NUnit.Framework;

using Game;
using Game.Views;
using Game.ViewModels;
using Game.Models;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using Game.GameRules;
using System.Linq;

namespace UnitTests.Views
{
    [TestFixture]
    public class MonsterCreatePageTests : MonsterCreatePage
    {
        App app;
        MonsterCreatePage page;

        public MonsterCreatePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new MonsterCreatePage(new GenericViewModel<MonsterModel>(new MonsterModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void MonsterCreatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void MonsterCreatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterCreatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterCreatePage_Save_Clicked_Null_Image_Should_Pass()
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
        public void MonsterCreatePage_Save_Clicked_Empty_Name_Shows_Error_Should_Pass()
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
        public void MonsterCreatePage_Save_Clicked_Empty_Description_Shows_Error_Should_Pass()
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
        public void MonsterCreatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            _ = OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterCreatePage_Attack_OnStepperAttackChanged_Default_Should_Pass()
        {
            // Arrange
            var data = new MonsterModel();
            var ViewModel = new GenericViewModel<MonsterModel>(data);

            page = new MonsterCreatePage(ViewModel);
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
        public void MonsterCreatePage_Speed_OnStepperValueChanged_Default_Should_Pass()
        {
            // ArSpeed
            var data = new MonsterModel();
            var ViewModel = new GenericViewModel<MonsterModel>(data);

            page = new MonsterCreatePage(ViewModel);
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
        public void MonsterCreatePage_Defense_OnStepperDefenseChanged_Default_Should_Pass()
        {
            // Arrange
            var data = new MonsterModel();
            var ViewModel = new GenericViewModel<MonsterModel>(data);

            page = new MonsterCreatePage(ViewModel);
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
        public void MonsterCreatePage_RollDice_Clicked_Default_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data = new MonsterModel();

            // Act
            page.RollDice_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterCreatePage_LeftArrow_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.LeftArrow_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterCreatePage_RightArrow_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.RightArrow_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterCreatePage_ChangeImageByIncrement_Valid_Increment_Should_Pass()
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
        public void MonsterCreatePage_ChangeImageByIncrement_Valid_Decrement_Should_Pass()
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
        public void MonsterCreatePage_ChangeImageByIncrement_Invalid_Decrement_Zero_Should_Pass()
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
        public void MonsterCreatePage_ChangeImageByIncrement_Invalid_Increment_Overflow_Should_Pass()
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

        [Test]
        public void MonsterCreatePage_EnableImageArrowButtons_Valid_Image1_Should_Disable_Left()
        {
            // Arrange
            var LeftButton = (Button)page.FindByName("LeftArrowButton");
            var RightButton = (Button)page.FindByName("RightArrowButton");

            // Set List to Left most
            page.ViewModel.Data.ImageURI = RandomPlayerHelper.MonsterImageList.First();

            // Act
            var result = page.EnableImageArrowButtons();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(false, LeftButton.IsEnabled);
            Assert.AreEqual(true, RightButton.IsEnabled);
        }

        [Test]
        public void MonsterCreatePage_EnableImageArrowButtons_Valid_Image2_Should_Enable_Both()
        {
            // Arrange
            var LeftButton = (Button)page.FindByName("LeftArrowButton");
            var RightButton = (Button)page.FindByName("RightArrowButton");

            // Set List to middle
            page.ViewModel.Data.ImageURI = RandomPlayerHelper.MonsterImageList.ElementAt(2);

            // Act
            var result = page.EnableImageArrowButtons();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, LeftButton.IsEnabled);
            Assert.AreEqual(true, RightButton.IsEnabled);
        }

        [Test]
        public void MonsterCreatePage_EnableImageArrowButtons_Valid_Image8_Should_Disable_Right()
        {
            // Arrange
            var LeftButton = (Button)page.FindByName("LeftArrowButton");
            var RightButton = (Button)page.FindByName("RightArrowButton");

            // Set List to Right most
            page.ViewModel.Data.ImageURI = RandomPlayerHelper.MonsterImageList.Last();

            // Act
            var result = page.EnableImageArrowButtons();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, LeftButton.IsEnabled);
            Assert.AreEqual(false, RightButton.IsEnabled);
        }

    }
}