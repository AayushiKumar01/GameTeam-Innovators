using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using Game.Engine.EngineInterfaces;
using Game.Engine.EngineModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public partial class BattlePage : ContentPage
    {
        // HTML Formatting for message output box
        public HtmlWebViewSource htmlSource = new HtmlWebViewSource();

        // Wait time before proceeding
        public int WaitTime = 1500;

        // Maximum wait time setting before button disabled
        public int MAX_WAIT_TIME = 4096;
        
        // Minimum wait time setting before button disabled
        public int MIN_WAIT_TIME = 32;
        
        // Hold the Map Objects, for easy access to update them
        public Dictionary<string, object> MapLocationObject = new Dictionary<string, object>();


        // Empty Constructor for UTs
        bool UnitTestSetting;
        public BattlePage(bool UnitTest) { UnitTestSetting = UnitTest; }

        // Flag to forge the game to end with 'Finish Game' Button
        public bool forceGameOver = false;

        // Flag for setting autoplay
        public bool AutoplayEnabled = false;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public BattlePage()
        {
            InitializeComponent();

            // Set initial State to Starting
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Starting;

            // Set up the UI to Defaults
            BindingContext = BattleEngineViewModel.Instance;

            // Create and Draw the Map
            _ = InitializeMapGrid();

            // Start the Battle Engine
            _ = BattleEngineViewModel.Instance.Engine.StartBattle(false);

            // Populate the UI Map
            DrawMapGridInitialState();

            // Ask the Game engine to select who goes first
            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(null);

            // Add Players to Display
            DrawGameAttackerDefenderBoard();
            
            //Clear messages
            _ = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.ClearMessages();
            
            // Set the Battle Mode
            ShowBattleMode();
        }

        /// <summary>
        /// Dray the Player Boxes
        /// </summary>
        public void DrawPlayerBoxes()
        {
            var CharacterBoxList = CharacterBox.Children.ToList();
            foreach (var data in CharacterBoxList)
            {
                _ = CharacterBox.Children.Remove(data);
            }

            // Draw the Characters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Character).ToList())
            {
                CharacterBox.Children.Add(PlayerInfoDisplayBox(data));
            }

            var MonsterBoxList = MonsterBox.Children.ToList();
            foreach (var data in MonsterBoxList)
            {
                _ = MonsterBox.Children.Remove(data);
            }

            // Draw the Monsters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Monster).ToList())
            {
                MonsterBox.Children.Add(PlayerInfoDisplayBox(data));
            }

            // Add one black PlayerInfoDisplayBox to hold space in case the list is empty
            CharacterBox.Children.Add(PlayerInfoDisplayBox(null));

            // Add one black PlayerInfoDisplayBox to hold space incase the list is empty
            MonsterBox.Children.Add(PlayerInfoDisplayBox(null));

        }

        /// <summary>
        /// Put the Player into a Display Box
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout PlayerInfoDisplayBox(PlayerInfoModel data)
        {
            if (data == null)
            {
                data = new PlayerInfoModel
                {
                    ImageURI = ""
                };
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["PlayerBattleMediumStyle"],
                Source = data.ImageURI
            };

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["PlayerBattleDisplayBox"],
                Children = {
                    PlayerImage,
                },
            };

            return PlayerStack;
        }

        #region BattleMapMode

        /// <summary>
        /// Create the Initial Map Grid
        /// 
        /// All lcoations are empty
        /// </summary>
        /// <returns></returns>
        public bool InitializeMapGrid()
        {
            _ = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.ClearMapGrid();

            return true;
        }

        /// <summary>
        /// Draw the Map Grid
        /// Add the Players to the Map
        /// 
        /// Need to have Players in the Engine first, to then put on the Map
        /// 
        /// The Map Objects are all created with the map background image first
        /// 
        /// Then the actual characters are added to the map
        /// </summary>
        public void DrawMapGridInitialState()
        {
            // Create the Map in the Game Engine
            _ = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.PopulateMapModel(BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList);

            _ = CreateMapGridObjects();

            _ = UpdateMapGrid();
        }

        /// <summary>
        /// Walk the current grid
        /// check each cell to see if it matches the engine map
        /// Update only those that need change
        /// </summary>
        /// <returns></returns>
        public bool UpdateMapGrid()
        {
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation)
            {
                // Use the ImageButton from the dictionary because that represents the player object
                var MapObject = GetMapGridObject(GetDictionaryImageButtonName(data));
                if (MapObject == null)
                {
                    return false;
                }

                var imageObject = (ImageButton)MapObject;

                // Check automation ID on the Image, That should match the Player, if not a match, the cell is now different need to update
                if (!imageObject.AutomationId.Equals(data.Player.Guid))
                {
                    // The Image is different, so need to re-create the Image Object and add it to the Stack
                    // That way the correct monster is in the box.

                    MapObject = GetMapGridObject(GetDictionaryStackName(data));
                    if (MapObject == null)
                    {
                        return false;
                    }

                    var stackObject = (StackLayout)MapObject;

                    // Remove the ImageButton
                    stackObject.Children.RemoveAt(0);

                    var PlayerImageButton = DetermineMapImageButton(data);

                    stackObject.Children.Add(PlayerImageButton);

                    // Update the Image in the Datastructure
                    _ = MapGridObjectAddImage(PlayerImageButton, data);

                    stackObject.BackgroundColor = DetermineMapBackgroundColor(data);
                }
            }

            return true;
        }

        /// <summary>
        /// Convert the Stack to a name for the dictionary to lookup
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetDictionaryFrameName(MapModelLocation data)
        {
            return string.Format("MapR{0}C{1}Frame", data.Row, data.Column);
        }

        /// <summary>
        /// Convert the Stack to a name for the dictionary to lookup
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetDictionaryStackName(MapModelLocation data)
        {
            return string.Format("MapR{0}C{1}Stack", data.Row, data.Column);
        }

        /// <summary>
        /// Covert the player map location to a name for the dictionary to lookup
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetDictionaryImageButtonName(MapModelLocation data)
        {
            // Look up the Frame in the Dictionary
            return string.Format("MapR{0}C{1}ImageButton", data.Row, data.Column);
        }

        /// <summary>
        /// Populate the Map
        /// 
        /// For each map position in the Engine
        /// Create a grid object to hold the Stack for that grid cell.
        /// </summary>
        /// <returns></returns>
        public bool CreateMapGridObjects()
        {
            // Make a frame for each location on the map
            // Populate it with a new Frame Object that is unique
            // Then updating will be easier

            foreach (var location in BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation)
            {
                var data = MakeMapGridBox(location);

                // Add the Box to the UI

                MapGrid.Children.Add(data, location.Column, location.Row);
            }

            // Set the Height for the MapGrid based on the number of rows * the height of the BattleMapFrame

            var height = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapXAxiesCount * 120;

            BattleMapDisplay.MinimumHeightRequest = height;
            BattleMapDisplay.HeightRequest = height;

            return true;
        }

        /// <summary>
        /// Get the Frame from the Dictionary
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetMapGridObject(string name)
        {
            _ = MapLocationObject.TryGetValue(name, out var data);
            return data;
        }

        /// <summary>
        /// Make the Game Map Frame 
        /// Place the Character or Monster on the frame
        /// If empty, place Empty
        /// </summary>
        /// <param name="mapLocationModel"></param>
        /// <returns></returns>
        public Frame MakeMapGridBox(MapModelLocation mapLocationModel)
        {
            if (mapLocationModel.Player == null)
            {
                mapLocationModel.Player = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.EmptySquare;
            }

            var PlayerImageButton = DetermineMapImageButton(mapLocationModel);

            var PlayerStack = new StackLayout
            {
                Padding = 0,
                Style = (Style)Application.Current.Resources["BattleMapImageBox"],
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = DetermineMapBackgroundColor(mapLocationModel),
                Children = {
                    PlayerImageButton
                },
            };

            _ = MapGridObjectAddImage(PlayerImageButton, mapLocationModel);
            _ = MapGridObjectAddStack(PlayerStack, mapLocationModel);

            var MapFrame = new Frame
            {
                Style = (Style)Application.Current.Resources["BattleMapFrame"],
                Content = PlayerStack,
                AutomationId = GetDictionaryFrameName(mapLocationModel)
            };

            return MapFrame;
        }

        /// <summary>
        /// This add the ImageButton to the stack to kep track of
        /// </summary>
        /// <param name="data"></param>
        /// <param name="MapModel"></param>
        /// <returns></returns>
        public bool MapGridObjectAddImage(ImageButton data, MapModelLocation MapModel)
        {
            var name = GetDictionaryImageButtonName(MapModel);

            // First check to see if it has data, if so update rather than add
            if (MapLocationObject.ContainsKey(name))
            {
                // Update it
                MapLocationObject[name] = data;
                return true;
            }

            MapLocationObject.Add(name, data);

            return true;
        }

        /// <summary>
        /// This adds the Stack into the Dictionary to keep track of
        /// </summary>
        /// <param name="data"></param>
        /// <param name="MapModel"></param>
        /// <returns></returns>
        public bool MapGridObjectAddStack(StackLayout data, MapModelLocation MapModel)
        {
            var name = GetDictionaryStackName(MapModel);

            // First check to see if it has data, if so update rather than add
            if (MapLocationObject.ContainsKey(name))
            {
                // Update it
                MapLocationObject[name] = data;
                return true;
            }

            MapLocationObject.Add(name, data);
            return true;
        }

        /// <summary>
        /// Set the Image onto the map
        /// The Image represents the player
        /// 
        /// So a charcter is the character Image for that character
        /// 
        /// The Automation ID equals the guid for the player
        /// This makes it easier to identify when checking the map to update thigns
        /// 
        /// The button action is set per the type, so Characters events are differnt than monster events
        /// </summary>
        /// <param name="MapLocationModel"></param>
        /// <returns></returns>
        public ImageButton DetermineMapImageButton(MapModelLocation MapLocationModel)
        {
            var data = new ImageButton
            {
                Style = (Style)Application.Current.Resources["BattleMapPlayerSmallStyle"],
                Source = MapLocationModel.Player.ImageURI,

                // Store the guid to identify this button
                AutomationId = MapLocationModel.Player.Guid
            };

            switch (MapLocationModel.Player.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    data.Clicked += (sender, args) => SetSelectedCharacter(MapLocationModel);
                    break;
                case PlayerTypeEnum.Monster:
                    data.Clicked += (sender, args) => SetSelectedMonster(MapLocationModel);
                    break;
                case PlayerTypeEnum.Unknown:
                default:
                    data.Clicked += (sender, args) => SetSelectedEmpty(MapLocationModel);

                    // Use the blank cell
                    data.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.EmptySquare.ImageURI;
                    break;
            }

            return data;
        }

        /// <summary>
        /// Set the Background color for the tile.
        /// Monsters and Characters have different colors
        /// Empty cells are transparent
        /// </summary>
        /// <param name="MapModel"></param>
        /// <returns></returns>
        public Color DetermineMapBackgroundColor(MapModelLocation MapModel)
        {
            string BattleMapBackgroundColor;
            switch (MapModel.Player.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    BattleMapBackgroundColor = "BattleMapCharacterColor";
                    break;
                case PlayerTypeEnum.Monster:
                    BattleMapBackgroundColor = "BattleMapMonsterColor";
                    break;
                case PlayerTypeEnum.Unknown:
                default:
                    BattleMapBackgroundColor = "BattleMapTransparentColor";
                    break;
            }

            var result = (Color)Application.Current.Resources[BattleMapBackgroundColor];
            return result;
        }

        #region MapEvents
        /// <summary>
        /// Event when an empty location is clicked on
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetSelectedEmpty(MapModelLocation data)
        {
            EngineSettingsModel engineEngineSettings = BattleEngineViewModel.Instance.Engine.EngineSettings;
            if (!engineEngineSettings.EnableMapClick)
            {
                return false;
            }

            // Set action to move when clicking empty square
            engineEngineSettings.CurrentAction = ActionEnum.Move;
            
            // Copy map location to CordinatesModel to save in settings
            CordinatesModel cords = new CordinatesModel(data);
            
            // Save to coordinates to try to move towards in EngineSettings
            engineEngineSettings.MoveMapLocation = cords;
            
            // Finish processing the turn 
            FinishTurnProcess();
            return true;
        }

        /// <summary>
        /// Event when a Monster is clicked on
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public RoundEnum SetSelectedMonster(MapModelLocation data)
        {
            IBattleEngineInterface instanceEngine = BattleEngineViewModel.Instance.Engine;
            if (!instanceEngine.EngineSettings.EnableMapClick)
            {
                return RoundEnum.Unknown;
            }
            _ = instanceEngine.Round.SetCurrentDefender(data.Player);
            PlayerInfoModel attacker = instanceEngine.EngineSettings.CurrentAttacker;
            if (instanceEngine.EngineSettings.MapModel.IsTargetInRange(attacker,data.Player))
            {
                instanceEngine.EngineSettings.CurrentAction = ActionEnum.Attack;    
            }
            else
            {
                BattleMessages.Text = string.Format("{0} tried to attack {1} but is out of range, moving closer\n{2}", attacker.Name,
                    data.Player.Name, BattleMessages.Text);

                CordinatesModel cordinatesModel = new CordinatesModel(data);
                BattleEngineViewModel.Instance.Engine.EngineSettings.MoveMapLocation = cordinatesModel;
                instanceEngine.EngineSettings.CurrentAction = ActionEnum.Move;
            }

            data.IsSelectedTarget = true;
            return FinishTurnProcess();
        }

        /// <summary>
        /// Event when a Character is clicked on
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetSelectedCharacter(MapModelLocation data)
        {
            EngineSettingsModel EngineSettings = BattleEngineViewModel.Instance.Engine.EngineSettings;
            if (!EngineSettings.EnableMapClick)
            {
                return false;
            }
            PlayerInfoModel playerAtLocation = EngineSettings.MapModel.GetPlayerAtLocation(data.Column, data.Row);
            if (EngineSettings.CurrentAttacker == playerAtLocation)
            {
                BattleMessages.Text = string.Format("{0} this {1} is playing this turn. \n{2}", playerAtLocation.Name, playerAtLocation.Job, BattleMessages.Text);
            }
            else
            {
                BattleMessages.Text = string.Format("{0} the {1}, it's not their turn. \n{2}", playerAtLocation.Name, playerAtLocation.Job, BattleMessages.Text);
            }

            return true;
        }
        #endregion MapEvents

        #endregion BattleMapMode

        #region BasicBattleMode

        /// <summary>
        /// Draw the UI for
        ///
        /// Attacker vs Defender Mode
        /// 
        /// </summary>
        public void DrawGameAttackerDefenderBoard()
        {
            // Clear the current UI
            DrawGameBoardClear();

            // Show Characters across the Top
            DrawPlayerBoxes();

            // Draw the Map
            _ = UpdateMapGrid();

            // Show the Attacker and Defender
            DrawGameBoardAttackerDefenderSection();
        }

        /// <summary>
        /// Draws the Game Board Attacker and Defender
        /// </summary>
        public void DrawGameBoardAttackerDefenderSection()
        {
            BattlePlayerBoxVersus.Text = "";

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker == null)
            {
                return;
            }

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender == null)
            {
                return;
            }

            AttackerImage.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.ImageURI;
            AttackerName.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.Name;
            AttackerHealth.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.GetCurrentHealthTotal.ToString() + " / " + BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.GetMaxHealthTotal.ToString();

            // Show what action the Attacker used
            AttackerAttack.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.PreviousAction.ToImageURI();

            var item = ItemIndexViewModel.Instance.GetItem(BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PrimaryHand);
            if (item != null)
            {
                AttackerAttack.Source = item.ImageURI;
            }

            DefenderImage.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.ImageURI;
            DefenderName.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.Name;
            DefenderHealth.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.GetCurrentHealthTotal.ToString() + " / " + BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.GetMaxHealthTotal.ToString();

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.Alive == false)
            {
                _ = UpdateMapGrid();
                DefenderImage.BackgroundColor = Color.Red;
            }

            BattlePlayerBoxVersus.Text = "vs";
        }

        /// <summary>
        /// Draws the Game Board Attacker and Defender areas to be null
        /// </summary>
        public void DrawGameBoardClear()
        {
            AttackerImage.Source = string.Empty;
            AttackerName.Text = string.Empty;
            AttackerHealth.Text = string.Empty;

            DefenderImage.Source = string.Empty;
            DefenderName.Text = string.Empty;
            DefenderHealth.Text = string.Empty;
            DefenderImage.BackgroundColor = Color.Transparent;

            BattlePlayerBoxVersus.Text = string.Empty;
        }

        /// <summary>
        /// Attack Action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RestButton_Clicked(object sender, EventArgs e)
        {
            Rest();
        }

        /// <summary>
        /// Ability
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AbilityButton_Clicked(object sender, EventArgs e)
        {
            Ability();
            //BattleMessages.Text = string.Format("{0} \n{1}", "Needs to be implemented!", BattleMessages.Text);
        }

        /// <summary>
        /// Settings Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Setttings_Clicked(object sender, EventArgs e)
        {
            await ShowBattleSettingsPage();
        }

        
        
        /// <summary>
        /// Temporary button handler to force the game to end early
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FinishButton_Clicked(object sender, EventArgs e)
        {
            forceGameOver = true;
            NextAttackExample();
        }
        
        /// <summary>
        /// Button that starts recursive method for autoplaying the game showing UI updates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AutoplayButton_Clicked(object sender, EventArgs e)
        {
            AutoplayEnabled = true;
            // Lower the wait time so each turn does not take too long
            WaitTime = 256;
            // Show speed buttons
            AutoplayStack.IsVisible = true;
            // Recursive method that runs the game in new threads
            AutoPlayNext();
            // Unneeded buttons invisible
            AutoplayButton.IsVisible = false;
            AbilityButton.IsVisible = false;
        }

        /// <summary>
        /// Auto play the rest of the game showing UI updates
        /// </summary>
        private void AutoPlayNext()
        {
            // Start new thread to give UI a chance to refresh
            Task.Run(async () =>
            {
                var RoundCondition = RoundEnum.Unknown;
                Task.Delay(WaitTime).Wait();
                
                // Run in Main thread so that UI update is allowed
                Device.BeginInvokeOnMainThread(async () =>
                {
                    // Check if the game is over and exit early if so
                    if (forceGameOver || !AutoplayEnabled)
                        return;

                    PlayerInfoModel closestPlayer = null;
                    EngineSettingsModel engineSettings = BattleEngineViewModel.Instance.Engine.EngineSettings;
                    
                    // Find closest monster to current player to attack
                    closestPlayer = engineSettings.MapModel.FindClosestPlayerToPlayers(engineSettings.CurrentAttacker, engineSettings.MonsterList);
                    
                    // Assume new round if closest player is empty
                    RoundCondition = RoundEnum.NewRound;
                    if (closestPlayer != null)
                    {
                        // Simulate click on monster closest to current player 
                        RoundCondition = SetSelectedMonster(engineSettings.MapModel.GetLocationForPlayer(closestPlayer));
                    }

                    if (RoundCondition == RoundEnum.NextTurn)
                    {
                        // Rerun this method to handle next turn
                        AutoPlayNext();
                    }
                    // Just finished a round so need to click through screens
                    if (RoundCondition == RoundEnum.NewRound)
                    {
                        _ = BattleEngineViewModel.Instance.Engine.Round.NewRound();

                        // Exit the items page
                        _ = await Navigation.PopModalAsync();

                        // Click next round
                        NextRoundButton_Clicked(null, null);

                        // Click Begin on next round
                        _ = await Navigation.PopModalAsync();
                        
                        AutoPlayNext();
                    }
                    // If we get here without calling AutoPlayNext, finished the game so exit
                });
            });
        }
        
        
        /// <summary>
        /// Next Attack Example
        /// 
        /// This code example follows the rule of
        /// 
        /// Auto Select Attacker
        /// Auto Select Defender
        /// 
        /// Do the Attack and show the result
        /// 
        /// So the pattern is Click Next, Next, Next until game is over
        /// 
        /// </summary>
        public RoundEnum NextAttackExample()
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            // Get the turn, set the current player and attacker to match
            SetAttackerAndDefender();

            // Let the player make a choice if there is no defender picked
            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender != null || BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Count < 1)
            {
                return FinishTurnProcess();
            }


            return RoundEnum.Unknown;
        }

        private RoundEnum FinishTurnProcess()
        {
            // Hold the current state
            var RoundCondition = BattleEngineViewModel.Instance.Engine.Round.RoundNextTurn();
            if (forceGameOver)
            {
                RoundCondition = RoundEnum.GameOver;
            }

            // Output the Message of what happened.
            GameMessage();

            // Show the outcome on the Board
            DrawGameAttackerDefenderBoard();

            if (RoundCondition == RoundEnum.NewRound)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.NewRound;
                _ = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.ClearMessages();
                
                // Pause
                _ = Task.Delay(WaitTime);

                Debug.WriteLine("New Round");

                // Show the Round Over, after that is cleared, it will show the New Round Dialog
                ShowModalRoundOverPage();
            }
            else if (RoundCondition == RoundEnum.GameOver)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.GameOver;

                // Wrap up
                _ = BattleEngineViewModel.Instance.Engine.EndBattle();

                // Pause
                _ = Task.Delay(WaitTime);

                Debug.WriteLine("Game Over");

                GameOver();
                AutoplayEnabled = false;
            }
            else
            {
                NextAttackExample();
            }

            return RoundCondition;
        }

        /// <summary>
        /// Highlight the current players background to show whose turn it is
        /// </summary>
        public void ColorBackground()
        {
            EngineSettingsModel enginesettings = BattleEngineViewModel.Instance.Engine.EngineSettings;
            foreach (PlayerInfoModel player in enginesettings.CharacterList)
            {
                MapModelLocation locationForPlayer = enginesettings.MapModel.GetLocationForPlayer(player);
                if (locationForPlayer != null)
                {
                    var MapObject = GetMapGridObject(GetDictionaryStackName(locationForPlayer));
                
                    var stackObject = (StackLayout) MapObject;
                    stackObject.BackgroundColor = DetermineMapBackgroundColor(locationForPlayer);
                    if (enginesettings.CurrentAttacker.Guid == player.Guid)
                    {
                        stackObject.BackgroundColor = (Color) Application.Current.Resources["BattleMapActiveColor"];
                    }
                }
            }
        }

        /// <summary>
        /// Decide The Turn and who to Attack
        /// </summary>
        public void SetAttackerAndDefender()
        {
            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn());

            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    // User would select who to attack
                    PlayerInfoModel attacker = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;
                    ColorBackground();
                    BattleMessages.Text = string.Format("{0} \n{1}", attacker.Name +" the " + attacker.Job + "s turn, select map or action.", BattleMessages.Text);
                    
                    // Leaving empty to let user pick a target
                    _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(null);
                    break;

                case PlayerTypeEnum.Monster:
                default:

                    // Monsters turn, so auto pick a Character to Attack
                    _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(BattleEngineViewModel.Instance.Engine.Round.Turn.AttackChoice(BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker));
                    break;
            }
        }

        public void Rest()
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker);
            PlayerInfoModel attacker = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;

            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Rest;

            _ = BattleEngineViewModel.Instance.Engine.Round.Turn.TakeTurn(attacker);


            FinishTurnProcess();

            BattleMessages.Text = string.Format("{0} has emergency repairs", attacker.Name, BattleMessages.Text);


        }

        public void Ability()
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker);
            PlayerInfoModel attacker = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;

            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Ability;

            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentActionAbility = attacker.SelectAbilityToUse();

            _ = BattleEngineViewModel.Instance.Engine.Round.Turn.TakeTurn(attacker);

            var abilitytxt = AbilityEnumExtensions.ToMessage(attacker.SelectAbilityToUse());

            FinishTurnProcess();

            BattleMessages.Text = string.Format("{0} is using ability {1}\n{2}", attacker.Name, abilitytxt, BattleMessages.Text);


        }

        /// <summary>
        /// Game is over
        /// 
        /// Show Buttons
        /// 
        /// Clean up the Engine
        /// 
        /// Show the Score
        /// 
        /// Clear the Board
        /// 
        /// </summary>
        public async void GameOver()
        {
            // Save the Score to the Score View Model, by sending a message to it.
            var Score = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore;
            MessagingCenter.Send(this, "AddData", Score);

            ShowBattleMode();
            await Navigation.PushModalAsync(new ScorePage());
        }
        #endregion BasicBattleMode

        #region MessageHandelers

        /// <summary>
        /// Builds up the output message
        /// </summary>
        /// <param name="message"></param>
        public void GameMessage()
        {
            // Output The Message that happened.
            BattleMessages.Text = string.Format("{0} \n{1}", BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.TurnMessage, BattleMessages.Text);
            
            Debug.WriteLine(BattleMessages.Text);
            
            if (!string.IsNullOrEmpty(BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.LevelUpMessage))
            {
                BattleMessages.Text = string.Format("{0} \n{1}", BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.LevelUpMessage, BattleMessages.Text);
            }

            String source = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel
                .GetHTMLFormattedTurnMessage();
            if (!source.Contains("Unknown"))
            {
                htmlSource.Html = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.GetHTMLFormattedTurnMessage();
                HtmlBox.Source = htmlSource;
            }
        }

        /// <summary>
        ///  Clears the messages on the UX
        /// </summary>
        public void ClearMessages()
        {
            BattleMessages.Text = "";
            htmlSource.Html = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.GetHTMLBlankMessage();
            HtmlBox.Source = htmlSource;
        }

        #endregion MessageHandelers

        #region PageHandelers

        /// <summary>
        /// Battle Over, so Exit Button
        /// Need to show this for the user to click on.
        /// The Quit does a prompt, exit just exits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ExitButton_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// The Next Round Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void NextRoundButton_Clicked(object sender, EventArgs e)
        {
            EngineSettingsModel engineSettings = BattleEngineViewModel.Instance.Engine.EngineSettings;
            engineSettings.BattleStateEnum = BattleStateEnum.Battling;
            ShowBattleMode();
            await Navigation.PushModalAsync(new NewRoundPage());
            engineSettings.EnableMapClick = true;
            if (Device.RuntimePlatform != Device.Android)
            {
                AutoplayButton.IsVisible = true;
            }
        }

        /// <summary>
        /// The Start Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void StartButton_Clicked(object sender, EventArgs e)
        {
            EngineSettingsModel engineSettings = BattleEngineViewModel.Instance.Engine.EngineSettings;
            engineSettings.BattleStateEnum = BattleStateEnum.Battling;

            ShowBattleMode();
            await Navigation.PushModalAsync(new NewRoundPage());
            engineSettings.EnableMapClick = true;
        }

        /// <summary>
        /// Show the Game Over Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void ShowScoreButton_Clicked(object sender, EventArgs args)
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new ScorePage());
        }

        /// <summary>
        /// Show the Round Over page
        /// 
        /// Round Over is where characters get items
        /// 
        /// </summary>
        public async void ShowModalRoundOverPage()
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new RoundOverPage());
        }

        /// <summary>
        /// Show Settings
        /// </summary>
        public async Task ShowBattleSettingsPage()
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new BattleSettingsPage());
        }
        #endregion PageHandelers

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ShowBattleMode();
            SetAttackerAndDefender();
        }

        /// <summary>
        /// 
        /// Hide the differnt button states
        /// 
        /// Hide the message display box
        /// 
        /// </summary>
        public void HideUIElements()
        {
            NextRoundButton.IsVisible = false;
            StartBattleButton.IsVisible = false;
            AbilityButton.IsVisible = false;
            FinishButton.IsVisible = false;
            RestButton.IsVisible = false;
            MessageDisplayBox.IsVisible = false;
            BattlePlayerInformationBox.IsVisible = false;
            HtmlBox.IsVisible = false;
            BattleMessagesFrame.IsVisible = false;
        }

        /// <summary>
        /// Show the proper Battle Mode
        /// </summary>
        public void ShowBattleMode()
        {
            // If running in UT mode, 
            if (UnitTestSetting)
            {
                return;
            }

            HideUIElements();

            ClearMessages();

            DrawPlayerBoxes();

            // Update the Mode
            BattleModeValue.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum.ToMessage();

            ShowBattleModeDisplay();

            ShowBattleModeUIElements();
        }

        /// <summary>
        /// Control the UI Elements to display
        /// </summary>
        public void ShowBattleModeUIElements()
        {
            EngineSettingsModel engineSettings = BattleEngineViewModel.Instance.Engine.EngineSettings;
            switch (engineSettings.BattleStateEnum)
            {
                case BattleStateEnum.Starting:
                    //GameUIDisplay.IsVisible = false;
                    AttackerAttack.Source = ActionEnum.Unknown.ToImageURI();
                    StartBattleButton.IsVisible = true;
                    engineSettings.EnableMapClick = false;
                    break;

                case BattleStateEnum.NewRound:
                    _ = UpdateMapGrid();
                    AttackerAttack.Source = ActionEnum.Unknown.ToImageURI();
                    NextRoundButton.IsVisible = true;
                    AutoplayButton.IsVisible = false;
                    engineSettings.EnableMapClick = false;
                    break;

                case BattleStateEnum.GameOver:
                    // Hide the Game Board
                    GameUIDisplay.IsVisible = false;
                    AttackerAttack.Source = ActionEnum.Unknown.ToImageURI();
                    engineSettings.EnableMapClick = false;

                break;

                case BattleStateEnum.RoundOver:
                case BattleStateEnum.Battling:
                    GameUIDisplay.IsVisible = true;
                    BattlePlayerInformationBox.IsVisible = true;
                    MessageDisplayBox.IsVisible = true;
                    HtmlBox.IsVisible = true;
                    BattleMessagesFrame.IsVisible = true;
                    FinishButton.IsVisible = false;
                    if (!AutoplayEnabled)
                    {
                        if (Device.RuntimePlatform != Device.Android)
                        {
                            AutoplayButton.IsVisible = true;
                        }
                        AbilityButton.IsVisible = true;
                    }
                    RestButton.IsVisible = true;
                    break;

                // Based on the State disable buttons
                case BattleStateEnum.Unknown:
                default:
                    break;
            }
        }

        /// <summary>
        /// Control the Map Mode or Simple
        /// </summary>
        public void ShowBattleModeDisplay()
        {
            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum)
            {
                case BattleModeEnum.MapAbility:
                case BattleModeEnum.MapFull:
                case BattleModeEnum.MapNext:
                    GamePlayersTopDisplay.IsVisible = false;
                    BattleMapDisplay.IsVisible = true;
                    break;

                case BattleModeEnum.SimpleAbility:
                case BattleModeEnum.SimpleNext:
                case BattleModeEnum.Unknown:
                default:
                    GamePlayersTopDisplay.IsVisible = true;
                    BattleMapDisplay.IsVisible = false;
                    break;
            }
        }

        public void DelaySlower_Button(object sender, EventArgs e)
        {
            WaitTime *= 2;
            FasterButton.IsEnabled = true;
            if (WaitTime >= MAX_WAIT_TIME)
            {
                SlowerButton.IsEnabled = false;
                WaitTime = MAX_WAIT_TIME;
            }
        }

        public void DelayFaster_Button(object sender, EventArgs e)
        {
            WaitTime /= 2;
            SlowerButton.IsEnabled = true;
            if (WaitTime <= MIN_WAIT_TIME)
            {
                FasterButton.IsEnabled = false;
                WaitTime = MIN_WAIT_TIME;
            }
        }

        public void AutoplayStop_Button(object sender, EventArgs e)
        {
            AutoplayEnabled = false;
            AutoplayStack.IsVisible = false;
            AbilityButton.IsVisible = true;
            if (Device.RuntimePlatform != Device.Android)
            {
                AutoplayButton.IsVisible = true;
            }
        }
    }
}