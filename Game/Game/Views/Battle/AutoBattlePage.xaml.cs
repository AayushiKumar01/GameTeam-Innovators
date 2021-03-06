using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.Models;
using Game.ViewModels;
using Game.Engine.EngineInterfaces;
using Game.Helpers;
using Plugin.SimpleAudioPlayer;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AutoBattlePage : ContentPage
    {
        // Hold the Engine, so it can be swapped out for unit testing
        public IAutoBattleInterface AutoBattle = BattleEngineViewModel.Instance.AutoBattleEngine;

        private AudioHelper audio;

        /// <summary>
        /// Constructor
        /// </summary>
        public AutoBattlePage()
        {
            InitializeComponent();
            audio = new AudioHelper();
        }

        /// <summary>
        /// start auto battle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void AutobattleButton_Clicked(object sender, EventArgs e)
        {
            //playing background audio when auto battle begins
            audio.playBattleStart();

            // Call into Auto Battle from here to do the Battle...

            // To See Level UP happening, a character needs to be close to the next level
            var Character = new CharacterModel
            {
                ExperienceTotal = 300,    // Enough for next level
                Name = "Mike Level Example",
                Speed = 100,    // Go first
            };

            var CharacterPlayer = new PlayerInfoModel(Character);

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            _ = await AutoBattle.RunAutoBattle();

            //playing background audio when auto battle is over
            audio.stopStartAudio();
            audio.playGameOverAudio();

            var BattleMessage = string.Format("Returned from Andromeda. Quest completed with {0} Rounds", AutoBattle.Battle.EngineSettings.BattleScore.RoundCount);

            BattleMessageValue.Text = BattleMessage;

        }
    }
}