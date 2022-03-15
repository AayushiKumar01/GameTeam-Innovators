
using Xamarin.Forms;
using Xamarin.Forms.Mocks;

using NUnit.Framework;
using Game.Views;
using Game;

namespace Scenario
{
    [TestFixture]
    public class BattlePageScenarioTests
    {
        App app;
        BattlePage page;

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void BattlePage_Constructor_Default_Should_Pass()
        {
            // Arrange
            page = new BattlePage();

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
    }
}