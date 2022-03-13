using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;
using Game.GameRules;

namespace Game.Views
{
    /// <summary>
    /// Character Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterUpdatePage : ContentPage
    {
        // The Character to create
        public GenericViewModel<CharacterModel> ViewModel { get; set; }

        // Hold the current location selected
        public ItemLocationEnum PopupLocationEnum = ItemLocationEnum.Unknown;

        // Current count of the Images in the Character Image List
        public int ImageListCount = RandomPlayerHelper.CharacterImageList.Count;

        // to hold original character data for cancel update
        public CharacterModel CharacterCopy;

        // Empty Constructor for UTs
        public CharacterUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public CharacterUpdatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            //copying character data to CharacterCopy to restore values in case update is canceled
            CharacterCopy = new CharacterModel(data.Data);

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;

            SpaceshipPicker.SelectedItem = data.Data.Job.ToMessage();

            // Load the values for the Level into the Picker
            for (var i = 1; i <= LevelTableHelper.MaxLevel; i++)
            {
                LevelPicker.Items.Add(i.ToString());
            }

            _ = UpdatePageBindingContext();
        }

        /// <summary>
        /// Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
        public bool UpdatePageBindingContext()
        {
            // Temp store off the Level
            var level = this.ViewModel.Data.Level;

            // Clear the Binding and reset it
            BindingContext = null;
            BindingContext = this.ViewModel;

            // This resets the Picker to -1 index, need to reset it back
            ViewModel.Data.Level = level;
            LevelPicker.SelectedIndex = ViewModel.Data.Level - 1;

            ManageHealth();

            AddItemsToDisplay();

            return true;
        }

        /// <summary>
        /// The Level selected from the list
        /// Need to recalculate Max Health
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Level_Changed(object sender, EventArgs args)
        {
            // Change the Level
            ViewModel.Data.Level = LevelPicker.SelectedIndex + 1;

            ManageHealth();
        }

        /// <summary>
        /// Change the Level Picker
        /// </summary>
        public void ManageHealth()
        {
            // Roll for new HP
            ViewModel.Data.MaxHealth = RandomPlayerHelper.GetHealth(ViewModel.Data.Level);

            // Show the Result
            MaxHealthValue.Text = ViewModel.Data.MaxHealth.ToString();
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // Abort save if name is empty
            if (string.IsNullOrEmpty(CharacterName.Text))
            {
                ErrorMessage.Text = "Mandatory fields can not be blank.";
                return;
            }

            // Abort save if description is empty
            if (string.IsNullOrEmpty(CharacterDescription.Text))
            {
                ErrorMessage.Text = "Mandatory fields can not be blank.";
                return;
            }

            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = new CharacterModel().ImageURI;
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
            //restore original character data
            ViewModel.Data.Update(CharacterCopy);
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
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnPopupItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ItemModel data = args.SelectedItem as ItemModel;
            if (data == null)
            {
                return;
            }

            _ = ViewModel.Data.AddItem(PopupLocationEnum, data.Id);

            AddItemsToDisplay();

            ClosePopup();
        }

        /// <summary>
        /// Show the Popup for Selecting Items
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool ShowPopup(ItemLocationEnum location)
        {
            PopupItemSelector.IsVisible = true;

            PopupLocationLabel.Text = "Items for :";
            PopupLocationValue.Text = location.ToMessage();

            // Make a fake item for None
            var NoneItem = new ItemModel
            {
                Id = null, // will use null to clear the item
                Guid = "None", // how to find this item amoung all of them
                ImageURI = "icon_cancel.png",
                Name = "None",
                Description = "None"
            };

            List<ItemModel> itemList = new List<ItemModel>
            {
                NoneItem
            };

            // Add the rest of the items to the list
            itemList.AddRange(ItemIndexViewModel.Instance.GetLocationItems(location));

            // Populate the list with the items
            PopupLocationItemListView.ItemsSource = itemList;

            // Remember the location for this popup
            PopupLocationEnum = location;

            return true;
        }

        /// <summary>
        /// When the user clicks the close in the Popup
        /// hide the view
        /// show the scroll view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClosePopup_Clicked(object sender, EventArgs e)
        {
            ClosePopup();
        }

        /// <summary>
        /// Close the popup
        /// </summary>
        public void ClosePopup()
        {
            PopupItemSelector.IsVisible = false;
        }

        /// <summary>
        /// Show the Items the Character has
        /// </summary>
        public void AddItemsToDisplay()
        {
            var FlexList = ItemBox.Children.ToList();
            foreach (var data in FlexList)
            {
                _ = ItemBox.Children.Remove(data);
            }

            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Head));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Necklass));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.PrimaryHand));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.OffHand));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.RightFinger));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.LeftFinger));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Feet));
        }

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public StackLayout GetItemToDisplay(ItemLocationEnum location)
        {
            // Get the Item, if it exist show the info
            // If it does not exist, show a Plus Icon for the location

            // Defualt Image is the item
            var ImageSource = "item.png";

            var data = ViewModel.Data.GetItemByLocation(location);
            if (data == null)
            {
                data = new ItemModel { Location = location, ImageURI = ImageSource };
            }

            // Hookup the Image Button to show the Item picture
            var ItemButton = new ImageButton
            {
                Style = (Style)Application.Current.Resources["ImageMediumStyle"],
                Source = data.ImageURI
            };

            // Add a event to the user can click the item and see more
            ItemButton.Clicked += (sender, args) => ShowPopup(location);

            // Add the Display Text for the item
            var ItemLabel = new Label
            {
                Text = location.ToMessage(),
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            // Put the Image Button and Text inside a layout
            var ItemStack = new StackLayout
            {
                Padding = 3,
                Style = (Style)Application.Current.Resources["ItemImageLabelBox"],
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    ItemButton,
                    ItemLabel
                },
            };

            return ItemStack;
        }

        /// <summary>
        /// Randomize Character Values and Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RollDice_Clicked(object sender, EventArgs e)
        {
            _ = DiceAnimationHandeler();

            _ = RandomizeCharacter();

            return;
        }

        /// <summary>
        /// Randomize the Character, keep the level the same
        /// </summary>
        /// <returns></returns>
        public bool RandomizeCharacter()
        {
            // Randomize Name
            ViewModel.Data.Name = RandomPlayerHelper.GetCharacterName();
            ViewModel.Data.Description = RandomPlayerHelper.GetCharacterDescription();

            // Randomize the Attributes
            ViewModel.Data.Attack = RandomPlayerHelper.GetAbilityValue();
            ViewModel.Data.Speed = RandomPlayerHelper.GetAbilityValue();
            ViewModel.Data.Defense = RandomPlayerHelper.GetAbilityValue();


            //Randomize the Job (Spaceship Type)
            ViewModel.Data.Job = RandomPlayerHelper.GetCharacterJob();

            // Randomize an Item for Location
            ViewModel.Data.Head = RandomPlayerHelper.GetItem(ItemLocationEnum.Head);
            ViewModel.Data.Necklass = RandomPlayerHelper.GetItem(ItemLocationEnum.Necklass);
            ViewModel.Data.PrimaryHand = RandomPlayerHelper.GetItem(ItemLocationEnum.PrimaryHand);
            ViewModel.Data.OffHand = RandomPlayerHelper.GetItem(ItemLocationEnum.OffHand);
            ViewModel.Data.RightFinger = RandomPlayerHelper.GetItem(ItemLocationEnum.Finger);
            ViewModel.Data.LeftFinger = RandomPlayerHelper.GetItem(ItemLocationEnum.Finger);
            ViewModel.Data.Feet = RandomPlayerHelper.GetItem(ItemLocationEnum.Feet);

            ViewModel.Data.MaxHealth = RandomPlayerHelper.GetHealth(ViewModel.Data.Level);

            ViewModel.Data.ImageURI = RandomPlayerHelper.GetCharacterImage();

            _ = UpdatePageBindingContext();

            return true;
        }

        /// <summary>
        /// Setup the Dice Animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool DiceAnimationHandeler()
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
            var ImageIndexCurrent = RandomPlayerHelper.CharacterImageList.IndexOf(ViewModel.Data.ImageURI);

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
            ViewModel.Data.ImageURI = RandomPlayerHelper.CharacterImageList.ElementAt(indexNew);

            _ = UpdatePageBindingContext();

            return indexNew;
        }
    }
}