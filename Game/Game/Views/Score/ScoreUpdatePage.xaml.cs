using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// Score Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreUpdatePage : ContentPage
    {
        // View Model for Score
        public readonly GenericViewModel<ScoreModel> ViewModel;

        //to hold original score data for cancel update
        public ScoreModel ScoreCopy;

        // Constructor for Unit Testing
        public ScoreUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor that takes and existing data Score
        /// </summary>
        public ScoreUpdatePage(GenericViewModel<ScoreModel> data)
        {
            InitializeComponent();

            //copying original score data to ScoreCopy for cancel update
            ScoreCopy = new ScoreModel(data.Data);

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
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
            //restoring original score data
            ViewModel.Data.Update(ScoreCopy);
            
            _ = await Navigation.PopModalAsync();
        }
    }
}