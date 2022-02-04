using System;
using System.Collections.Generic;
using System.ComponentModel;
using Game.GameRules;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.Models;
using Game.ViewModels;

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

        // Empty Constructor for UTs
        public MonsterUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public MonsterUpdatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;


            // Load the values for the Difficulty into the Picker
            foreach (string diff in DifficultyEnumHelper.GetListMonster)
            {
                DifficultyPicker.Items.Add(diff);
            }

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

            return true;
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // Abort save if name or description is empty
            if (string.IsNullOrEmpty(MonsterName.Text) || string.IsNullOrEmpty(MonsterDescription.Text))
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

        private void Difficulty_Changed(object sender, EventArgs e)
        {
            // Find the Difficulty enum
            string stringValue = "Unknown";
            if (DifficultyPicker.SelectedItem != null)
            {
                // Get the value if it exists, otherwise leave as 'Unknown'
                stringValue = DifficultyPicker.SelectedItem.ToString();
            }
            ViewModel.Data.Difficulty = DifficultyEnumHelper.ConvertStringToEnum(stringValue);
        }

    }
}