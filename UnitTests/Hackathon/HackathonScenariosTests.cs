using System.Threading.Tasks;
using Game;
using Game.Engine.EngineGame;
using Game.Engine.EngineModels;
using Game.Helpers;
using Game.Models;
using Game.ViewModels;
using Game.Views;
using NUnit.Framework;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace Scenario
{
    [TestFixture]
    public class HackathonScenarioTests
    {
        #region TestSetup
        readonly BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;
        BattleEngine Engine;

        [SetUp]
        public void Setup()
        {
            // Put seed data into the system for all tests
            _ = BattleEngineViewModel.Instance.Engine.Round.ClearLists();

            //Start the Engine in AutoBattle Mode
            _ = BattleEngineViewModel.Instance.Engine.StartBattle(false);

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowCriticalHit = false;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowCriticalMiss = false;

            Engine = new BattleEngine();
            _ = Engine.StartBattle(true);   // Start engine in auto battle mode
        }

        [TearDown]
        public void TearDown()
        {
        }
        #endregion TestSetup

        #region Scenario0
        [Test]
        public void HakathonScenario_Scenario_0_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      #
            *      
            * Description: 
            *      <Describe the scenario>
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      <List Files Changed>
            *      <List Classes Changed>
            *      <List Methods Changed>
            * 
            * Test Algrorithm:
            *      <Step by step how to validate this change>
            * 
            * Test Conditions:
            *      <List the different test conditions to make>
            * 
            * Validation:
            *      <List how to validate this change>
            *  
            */

            // Arrange

            // Act

            // Assert


            // Act
            var result = EngineViewModel;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion Scenario0

        #region Scenario1
        [Test]
        public async Task HackathonScenario_Scenario_1_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      1
            *      
            * Description: 
            *      Make a Character called Mike, who dies in the first round
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes requied 
            * 
            * Test Algrorithm:
            *      Create Character named Mike
            *      Set speed to -1 so he is really slow
            *      Set Max health to 1 so he is weak
            *      Set Current Health to 1 so he is weak
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Mike is not in the Player List
            *      Verify Round Count is 1
            *  
            */

            //Arrange

            // Set Character Conditions

            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1, // Will go last...
                                Level = 1,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                            });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerMike);

            // Set Monster Conditions

            // Auto Battle will add the monsters

            // Monsters always hit
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;

            //Act
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

            //Reset
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(null, EngineViewModel.Engine.EngineSettings.PlayerList.Find(m => m.Name.Equals("Mike")));
            //Assert.AreEqual(1, EngineViewModel.Engine.EngineSettings.BattleScore.RoundCount);
        }
        #endregion Scenario1

        /* 
        #region Scenario2
        [Test]
        public void HackathonScenario_Scenario_2_Valid_Default_WithDoug_Should_Pass()
        {
            
            * Scenario Number:  
            *      2
            *      
            * Description: 
            *      Make a Character called Doug
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      TurnEngine.cs - Added condition for Doug to miss in TurnAsAttack
            * 
            * Test Algrorithm:
            *      Create Character named Doug
            *      call TurnAsAttack
            *      check is hit status is miss
            *  
            *      
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify TurnAsAttack Returned True
            *      Verify Hit Staus is miss
            *  
           

        //Arrange

        // Set Character Conditions

        EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayerDoug = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1,
                                Level = 1,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Doug",
                            });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerDoug);

            // Set Monster Conditions

            var Monster = new MonsterModel();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            //Act
            var result = EngineViewModel.Engine.Round.Turn.TurnAsAttack(CharacterPlayerDoug, MonsterPlayer);

            //Reset


            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, EngineViewModel.Engine.EngineSettings.BattleMessagesModel.HitStatus == HitStatusEnum.Miss);

        }
         */


        #region Scenario4
        [Test]
        public void HackathonScenario_Scenario_4_Valid_Default_Should_Pass()
        {
            // Arrange
            var AttackScore = 1;
            var DefenseScore = 100;

            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(20);

            var oldSeting = EngineSettingsModel.Instance.BattleSettingsModel.AllowCriticalHit;
            EngineSettingsModel.Instance.BattleSettingsModel.AllowCriticalHit = true;

            // Act
            var result = Engine.Round.Turn.RollToHitTarget(AttackScore, DefenseScore);

            // Reset
            _ = DiceHelper.DisableForcedRolls();
            EngineSettingsModel.Instance.BattleSettingsModel.AllowCriticalHit = oldSeting;

            // Assert
            Assert.AreEqual(HitStatusEnum.CriticalHit, result);
        }
        #endregion Scenario4

        #region Scenario12
        [Test]
        public void HackathonScenario_Scenario_12_Valid_AutoPlayClick_Should_Pass_And_Play_5_Turns()
        {
            /* 
            * Scenario Number:  
            *      12
            *      
            * Description:
            *       Add Autoplay button to normal battle screen
            *       Clicking this button will autoplay through all rounds showing UI until all characters have died
            *       Screens between rounds are clicked through to continue until the game is over  
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *       BattlePage.xaml - Add Autoplay button
            *       BattlePage.xaml.cs - Add button click handler and logic for auto playing the game
            * 
            Test Algorithm:
            *       Setup BattlePage with 3 Characters and Monsters
            *       Start autoplay and wait for 1 second for a few turns to pass
            *       Click the 'Finish Early' button
            *       Verify that we played at least 5 turns
            *      
            * Test Conditions:
            *      We have enough Characters and Monsters to play at least 5 turns in the round
            * 
            * Validation:
            *      Verify that we played at least 5 turns in 1 second
            */


            // Arrange
            MockForms.Init();
            App app = new App();
            Application.Current = app;

            BattlePage page = new BattlePage();
            
            Engine.EngineSettings.MonsterList.Clear();
            
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Character",
                ListOrder = 1,
            };

            // Add each model here to warm up and load it.
            _ = DataSetsHelper.WarmUp();

            Engine.EngineSettings.CharacterList.Clear();

            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));
            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));
            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();
            
            // Add to Monster list
            var Monster = new MonsterModel();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);
            // Act
            page.NextAttackExample();
            page.AutoplayButton_Clicked(null, null);
            Task.Delay(1500).Wait();
            
            // Reset
            // Finish the game early
            page.FinishButton_Clicked(null, null);
            page.GameOver();
            
            // Assert
            Assert.True(EngineViewModel.Engine.EngineSettings.BattleScore.TurnCount >= 2);
        }
        #endregion Scenario12
        
        #region Scenario33
        [Test]
        public void HackathonScenario_Scenario_33_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      33
            *      
            * Description: 
            *      Lets a character choose to rest
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *     TurnEngineBase.cs - ChooseToRest and Rest functions
            *     ITurnEngineInterface.cs - Rest and ChooseToRest bools
            *     ActionEnum.cs - Added Rest enum
            * 
            Test Algrorithm:
            *      Create Character
            *      Set Max Health
            *      Set Current Health
            *      Call Rest
            *      Check HP
            *      
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Current HP increased by 2
            */
            // Arrange
            var PlayerInfo = new PlayerInfoModel();
            PlayerInfo.MaxHealth = 100;
            PlayerInfo.CurrentHealth = 98;

            // Act
            var result = Engine.Round.Turn.Rest(PlayerInfo);
            
            // Assert
            Assert.AreEqual(PlayerInfo.CurrentHealth, PlayerInfo.MaxHealth);
        }

        [Test]
        public void HackathonScenario_Scenario_33_Valid_Health_Larger_Than_Rest_Threshold_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      33
            *      
            * Description: 
            *      Lets a character choose to rest
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *     TurnEngineBase.cs - ChooseToRest and Rest functions
            *     ITurnEngineInterface.cs - Rest and ChooseToRest bools
            *     ActionEnum.cs - Added Rest enum
            * 
            Test Algrorithm:
            *      Create Character
            *      Set Max Health
            *      Set Current Health
            *      Call Rest
            *      Check HP
            *      
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Current HP increased by 2
            */
            // Arrange
            var PlayerInfo = new PlayerInfoModel();
            PlayerInfo.MaxHealth = 100;
            PlayerInfo.CurrentHealth = 99;

            // Act
            var result = Engine.Round.Turn.Rest(PlayerInfo);

            // Assert
            Assert.AreEqual(PlayerInfo.CurrentHealth, PlayerInfo.MaxHealth);
        }

        [Test]
        public void HackathonScenario_Scenario_33_TurnEngine_ChooseToRest_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      33
            *      
            * Description: 
            *      Lets a character choose to rest
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *     TurnEngineBase.cs - ChooseToRest and Rest functions
            *     ITurnEngineInterface.cs - Rest and ChooseToRest bools
            *     ActionEnum.cs - Added Rest enum
            * 
            Test Algrorithm:
            *      Create Character
            *      Set Max Health
            *      Set Current Health
            *      Add Character to PlayerList
            *      Set ActionEnum to Unknown
            *      Set AutoBattle to True
            *      Call ChooseToRest
            *      Check HP
            *      
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Character chose to rest
            */
            // Arrange

            var CharacterPlayer = new PlayerInfoModel(new CharacterModel());

            CharacterPlayer.CurrentHealth = 1;
            CharacterPlayer.MaxHealth = 100;

            Engine.EngineSettings.PlayerList.Add(CharacterPlayer);

            _ = Engine.EngineSettings.MapModel.PopulateMapModel(Engine.EngineSettings.PlayerList);

            Engine.EngineSettings.CurrentAction = ActionEnum.Unknown;
            Engine.EngineSettings.BattleScore.AutoBattle = true;

            // Act
            var result = Engine.Round.Turn.ChooseToRest(CharacterPlayer);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion Scenario33
        
        #region Scenario37
        [Test]
        public void HackathonScenario_Scenario_37_Valid_AllowSeattleIce_Causes_IceSlip_Previous_Action()
        {
            /* 
            * Scenario Number:  
            *      37
            *      
            * Description:
            *       Turn on AllowSeattleIce and set probability to 100%
            *       Make a Character and Monster, play one turn and it should result in ice slip action 
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *       BattleSettingsModel.cs - Added settings to turn this action on
            *       TurnEngine.cs - Set action based on settings probability
            *       ActionEnum.cs - Update enum to have slip action  
            * 
            Test Algorithm:
            *       Turn on AllowSeattleIce and set probability to 100%
            *       Make a Character and Monster, play one turn
            *       it should result in ice slip as last action 
            *  
            *      
            * Test Conditions:
            *      AllowSeattleIce is turned on and SeattleIcePercentage is 100
            * 
            * Validation:
            *      Verify that last action was ice slip
            */


            // Arrange
            Engine.EngineSettings.MonsterList.Clear();
            Engine.EngineSettings.BattleSettingsModel.AllowSkips = true;
            Engine.EngineSettings.BattleSettingsModel.SkipPercentage = 100;
            
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Character",
                ListOrder = 1,
            };

            // Add each model here to warm up and load it.
            _ = DataSetsHelper.WarmUp();

            Engine.EngineSettings.CharacterList.Clear();

            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();
            
            // Add to Monster list
            var Monster = new MonsterModel();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);
            
            // Act
            var result = Engine.Round.RoundNextTurn();

            // Reset
            // Disable for the next test
            Engine.EngineSettings.BattleSettingsModel.AllowSkips = false;
            
            // Assert
            Assert.AreEqual(ActionEnum.SkipTurn, Engine.EngineSettings.PreviousAction);
        }
        #endregion Scenario37
        

    }
}