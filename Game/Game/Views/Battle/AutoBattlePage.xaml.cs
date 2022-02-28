﻿using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.Models;
using Game.ViewModels;
using Game.Engine.EngineInterfaces;
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

        //hold audio file
        public ISimpleAudioPlayer BattleStartAudio;

        /// <summary>
        /// Constructor
        /// </summary>
        public AutoBattlePage()
        {
            InitializeComponent();

            //Initialzing and loading audio file
            BattleStartAudio = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            BattleStartAudio.Load("autobattle.mp3");
        }

        /// <summary>
        /// start auto battle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void AutobattleButton_Clicked(object sender, EventArgs e)
        {
            //playing background audio when auto battle begins
            BattleStartAudio.Play();
            
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

            var BattleMessage = string.Format("Traveling to planet 1. Quest completed with {0} Rounds", AutoBattle.Battle.EngineSettings.BattleScore.RoundCount);

            BattleMessageValue.Text = BattleMessage;

        }
    }
}