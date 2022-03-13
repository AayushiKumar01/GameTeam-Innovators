using System;
using System.Collections.Generic;
using System.ComponentModel;
using Game.GameRules;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.Models;
using Game.ViewModels;
using System.Linq;

namespace Game.Views
{
    /// <summary>
    /// Create Monster
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterCreatePage : ContentPage
    {
        // The Monster to create
        public GenericViewModel<MonsterModel> ViewModel { get; set; }

        // Hold the current location selected
        public ItemLocationEnum PopupLocationEnum = ItemLocationEnum.Unknown;

        // Current count of the Images in the Monster Image List
        public int ImageListCount = RandomPlayerHelper.MonsterImageList.Count;
        // Empty Constructor for UTs
        public MonsterCreatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public MonsterCreatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            data.Data = new MonsterModel();
            this.ViewModel = data;

            this.ViewModel.Title = "Create Alien Craft";

            _ = UpdatePageBindingContext();
        }

        /// <summary>
        /// Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
        public bool UpdatePageBindingContext()
        {

            // Temp store off the difficulty
            var difficulty = this.ViewModel.Data.Difficulty;

            // Clear the Binding and reset it
            BindingContext = null;
            BindingContext = this.ViewModel;

            ViewModel.Data.Difficulty = difficulty;

            _ = EnableImageArrowButtons();

            return true;
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // Abort save if name is empty
            if (string.IsNullOrEmpty(MonsterName.Text))
            { 
                ErrorMessage.Text = "Mandatory fields can not be blank.";
                return;
            }

            // Abort save if description is empty
            if (string.IsNullOrEmpty(MonsterDescription.Text))
            {
                ErrorMessage.Text = "Mandatory fields can not be blank.";
                return;
            }

            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = new MonsterModel().ImageURI;
            }

            MessagingCenter.Send(this, "Create", ViewModel.Data);
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopModalAsync();
        }

        ///// <summary>
        ///// 
        ///// Randomize the Monster
        ///// Keep the Level the Same
        ///// 
        ///// </summary>
        ///// <returns></returns>
        public bool RandomizeMonster()
        {
            // Randomize Name
            ViewModel.Data.Name = RandomPlayerHelper.GetMonsterName();
            ViewModel.Data.Description = RandomPlayerHelper.GetMonsterDescription();
            
            //Randomize the Job (Spaceship Type)
            ViewModel.Data.Job = RandomPlayerHelper.GetCharacterJob();

            // Randomize the Attributes
            ViewModel.Data.Attack = RandomPlayerHelper.GetAbilityValue();
            ViewModel.Data.Speed = RandomPlayerHelper.GetAbilityValue();
            ViewModel.Data.Defense = RandomPlayerHelper.GetAbilityValue();

            ViewModel.Data.Difficulty = RandomPlayerHelper.GetMonsterDifficultyValue();

            ViewModel.Data.ImageURI = RandomPlayerHelper.GetMonsterImage();

            ViewModel.Data.UniqueItem = RandomPlayerHelper.GetMonsterUniqueItem();

            _ = UpdatePageBindingContext();

            return true;
        }

        /// <summary>
        /// Randomize Monster Values and Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RollDice_Clicked(object sender, EventArgs e)
        {
            _ = DiceAnimationHandeler();

            _ = RandomizeMonster();

            return;
        }
    
        /// <summary>
        /// Catch the change to the Slider for Attack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Attack_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            double value = e.NewValue;
            value = Math.Round(value, MidpointRounding.AwayFromZero);
            AttackValue.Text = string.Format("{0}", (int)value);
        }
       
        /// <summary>
        /// Catch the change to the Slider for Defense
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Defense_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            double value = e.NewValue;
            value = Math.Round(value, MidpointRounding.AwayFromZero);
            DefenseValue.Text = string.Format("{0}", (int)value);
        }
        
        /// <summary>
        /// Catch the change to the Slider for Speed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Speed_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            double value = e.NewValue;
            value = Math.Round(value, MidpointRounding.AwayFromZero);
            SpeedValue.Text = string.Format("{0}", (int)value);
        }
        
    
        /// <summary>
        /// Setup the Dice Animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        bool DiceAnimationHandeler()
        {
            // Animate the Rolling of the Dice
            var image = RollDice;
            uint duration = 1000;

            var parentAnimation = new Animation();

            // Grow the image Size
            var scaleUpAnimation = new Animation(v => image.Scale = v, 1, 2, Easing.SpringIn);

            // Spin the Image
            var rotateAnimation = new Animation(v => image.Rotation = v, 0, 360);

            // Shrink the Image
            var scaleDownAnimation = new Animation(v => image.Scale = v, 2, 1, Easing.SpringOut);

            parentAnimation.Add(0, 0.5, scaleUpAnimation);
            parentAnimation.Add(0, 1, rotateAnimation);
            parentAnimation.Add(0.5, 1, scaleDownAnimation);

            parentAnimation.Commit(this, "ChildAnimations", 16, duration, null, null);

            return true;
        }

        /// <summary>
        /// Enable True of False the Image Arrows
        /// Based on the image in the list
        /// </summary>
        /// <returns></returns>
        public bool EnableImageArrowButtons()
        {
            LeftArrowButton.IsEnabled = true;
            RightArrowButton.IsEnabled = true;

            var ImageIndexCurrent = RandomPlayerHelper.MonsterImageList.IndexOf(ViewModel.Data.ImageURI);

            if (ImageIndexCurrent < 1)
            {
                LeftArrowButton.IsEnabled = false;
            }

            if (ImageIndexCurrent >= ImageListCount - 1)
            {
                RightArrowButton.IsEnabled = false;
            }

            return true;
        }


        /// <summary>
        /// Shift Image to the Left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LeftArrow_Clicked(object sender, EventArgs e)
        {
            _ = ChangeImageByIncrement(-1);
        }

        /// <summary>
        /// Shift Image to the Right
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RightArrow_Clicked(object sender, EventArgs e)
        {
            _ = ChangeImageByIncrement(1);
        }

        /// <summary>
        /// Move the Image left or Right
        /// </summary>
        /// <param name="increment"></param>
        public int ChangeImageByIncrement(int increment)
        {
            // Find the Index for the current Image
            var ImageIndexCurrent = RandomPlayerHelper.MonsterImageList.IndexOf(ViewModel.Data.ImageURI);

            // Amount to move
            var indexNew = ImageIndexCurrent + increment;

            if (indexNew >= ImageListCount)
            {
                indexNew = ImageListCount - 1;
            }

            if (indexNew <= 0)
            {
                indexNew = 0;
            }

            // Increment or Decrement to change the to a different image
            ViewModel.Data.ImageURI = RandomPlayerHelper.MonsterImageList.ElementAt(indexNew);

            _ = UpdatePageBindingContext();

            return indexNew;
        }

    }
}