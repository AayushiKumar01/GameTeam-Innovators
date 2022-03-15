
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;

using NUnit.Framework;
using Game.Views;
using Game;
using Game.Engine.EngineModels;
using Game.Models;
using Game.ViewModels;

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

        [Test]
        public void BattlePage_SetSelectedMonster_Scenerio_Should_Pass()
        {
            
            // Arrange
            page = new BattlePage();
            BattleEngineViewModel.Instance.Engine.EngineSettings.EnableMapClick = true;
            EngineSettingsModel engineSettings = BattleEngineViewModel.Instance.Engine.EngineSettings;
            PlayerInfoModel character = new PlayerInfoModel(new CharacterModel());
            character.PlayerType = PlayerTypeEnum.Character;
            PlayerInfoModel monster = new PlayerInfoModel(new MonsterModel());
            monster.PlayerType = PlayerTypeEnum.Monster;
            
            MapModelLocation characterLocation = new MapModelLocation();
            characterLocation.Player = character;
            characterLocation.Column = 1;
            characterLocation.Row = 1;
            
            MapModelLocation monsterLocation = new MapModelLocation();
            monsterLocation.Player = monster;
            monsterLocation.Column = 2;
            monsterLocation.Row = 2;
            
            //Switch them since NextAttack with switch them again
            engineSettings.CurrentAttacker = monster;
            engineSettings.CurrentDefender = character;
            
            engineSettings.CharacterList.Clear();
            engineSettings.MonsterList.Clear();
            engineSettings.PlayerList.Clear();
            engineSettings.CharacterList.Add(character);
            engineSettings.MonsterList.Add(monster);
            engineSettings.PlayerList.Add(character);
            engineSettings.PlayerList.Add(monster);
            engineSettings.MapModel.MapGridLocation[1, 1] = characterLocation;
            engineSettings.MapModel.MapGridLocation[2, 2] = monsterLocation;
            page.NextAttackExample();
            
            // Act
            var result = page.SetSelectedMonster(monsterLocation);

            // Reset
            engineSettings.CharacterList.Clear();
            engineSettings.MonsterList.Clear();
            engineSettings.PlayerList.Clear();
            
            // Assert
            Assert.AreEqual(RoundEnum.NextTurn, result); // Got to here, so it happened...
        }
        
                [Test]
        public void BattlePage_SetSelectedCharacter_Scenerio_Should_Pass()
        {
            // Arrange
            page = new BattlePage();
            BattleEngineViewModel.Instance.Engine.EngineSettings.EnableMapClick = true;
            EngineSettingsModel engineSettings = BattleEngineViewModel.Instance.Engine.EngineSettings;
            PlayerInfoModel character = new PlayerInfoModel(new CharacterModel());
            character.PlayerType = PlayerTypeEnum.Character;
            PlayerInfoModel monster = new PlayerInfoModel(new MonsterModel());
            monster.PlayerType = PlayerTypeEnum.Monster;
            
            MapModelLocation characterLocation = new MapModelLocation();
            characterLocation.Player = character;
            characterLocation.Column = 1;
            characterLocation.Row = 1;
            
            MapModelLocation monsterLocation = new MapModelLocation();
            monsterLocation.Player = monster;
            monsterLocation.Column = 2;
            monsterLocation.Row = 2;
            
            //Switch them since NextAttack with switch them again
            engineSettings.CurrentAttacker = monster;
            engineSettings.CurrentDefender = character;
            
            engineSettings.CharacterList.Clear();
            engineSettings.MonsterList.Clear();
            engineSettings.PlayerList.Clear();
            engineSettings.CharacterList.Add(character);
            engineSettings.MonsterList.Add(monster);
            engineSettings.PlayerList.Add(character);
            engineSettings.PlayerList.Add(monster);
            engineSettings.MapModel.MapGridLocation[1, 1] = characterLocation;
            engineSettings.MapModel.MapGridLocation[2, 2] = monsterLocation;
            page.NextAttackExample();
            
            // Act
            var result = page.SetSelectedCharacter(characterLocation);

            // Reset
            engineSettings.CharacterList.Clear();
            engineSettings.MonsterList.Clear();
            engineSettings.PlayerList.Clear();
            
            // Assert
            Assert.AreEqual(true, result); // Got to here, so it happened...
        }
        
        
    }
}