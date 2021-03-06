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
    /// Monster Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterUpdatePage : ContentPage
    {
        // The Monster to create
        public GenericViewModel<MonsterModel> ViewModel { get; set; }

        // Hold the current location selected
        public ItemLocationEnum PopupLocationEnum = ItemLocationEnum.Unknown;

        // Current count of the Images in the Monster Image List
        public int ImageListCount = RandomPlayerHelper.MonsterImageList.Count;

        //hold monster original data for cancel update
        public MonsterModel MonsterCopy;

        // Empty Constructor for UTs
        public MonsterUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public MonsterUpdatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            //copy original monster data to MonsterCopy to restore values in case update is canceled
            MonsterCopy = new MonsterModel(data.Data);

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;


            _ = UpdatePageBindingContext();
        }

        /// <summary>
        /// Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
        public bool UpdatePageBindingContext()
        {
            // Temp store off the Difficulty
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
            //restoring original monster data
            ViewModel.Data.Update(MonsterCopy);

            _ = await Navigation.PopModalAsync();
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
        /// Catch the change to the Stepper for Speed
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