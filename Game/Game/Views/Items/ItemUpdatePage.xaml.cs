using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// Item Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemUpdatePage : ContentPage
    {
        // View Model for Item
        public readonly GenericViewModel<ItemModel> ViewModel;

        // Empty Constructor for Tests
        public ItemUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public ItemUpdatePage(GenericViewModel<ItemModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;

            //Need to make the SelectedItem a string, so it can select the correct item.
            LocationPicker.SelectedItem = data.Data.Location.ToMessage();
            AttributePicker.SelectedItem = data.Data.Attribute.ToString();
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            //Abort save it name or description or Location or Attribute is empty
            if (string.IsNullOrEmpty(ItemName.Text) || string.IsNullOrEmpty(ItemDescription.Text))
            {
                ErrorMessage.Text = "Mandatory fields can not be blank.";
                return;
            }
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
            }

            MessagingCenter.Send(this, "Update", ViewModel.Data);
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Catch the change to the slider for Range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Range_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            double value = e.NewValue;
            value = Math.Round(value, MidpointRounding.AwayFromZero);
            RangeValue.Text = string.Format("{0}", (int)value);

        }

        /// <summary>
        /// Catch the change to the slider for Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Value_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            double value = e.NewValue;
            value = Math.Round(value, MidpointRounding.AwayFromZero);
            ValueValue.Text = string.Format("{0}", (int)value);
        }

        /// <summary>
        /// Catch the change to the stepper for Damage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Damage_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            double value = e.NewValue;
            value = Math.Round(value, MidpointRounding.AwayFromZero);
            DamageValue.Text = string.Format("{0}", (int)value);
        }
    }
}