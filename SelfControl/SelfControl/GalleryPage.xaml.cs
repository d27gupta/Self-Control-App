﻿using SelfControl.DatabaseManager;
using SelfControl.Helpers;
using SelfControl.Helpers.Pages;
using SelfControl.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SelfControl.Helpers.GlobalVariables;

namespace SelfControl
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GalleryPage : ContentPage
	{ 
        StackLayout view;
        ScrollView scrollView;
        Dictionary<DateTime,List<FoodItem>> foodDiary;
        public GalleryMode mode = GalleryMode.Normal;
        public List<ImageDisplay> selectedItems;
        ToolbarItem deleteOption;

        public GalleryPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            selectedItems = new List<ImageDisplay>();
            mode = GalleryMode.Normal;
            //Title = "Gallery";
            deleteOption = new ToolbarItem("Delete", "", new Action(() => { DeleteSelectedImage(); }), ToolbarItemOrder.Primary, 0);

            
            view = new StackLayout();
            scrollView = new ScrollView
            {
                Orientation = ScrollOrientation.Vertical
            };
            Update();
            this.InitialLoad();
        }

        async void InitialLoad()
        {
            foodDiary = await GlobalVariables.UpdateDateDiary();
            SetView();
            Update();
        }

        async void OnReviewButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WeeklyReviewPage(), true);
        }

        private void SetView()
        {
            view.Children.Clear();

            foreach (var i in foodDiary)
            {
                DateTime date = i.Key;
                List<FoodItem> foods = i.Value;

                Label dateLabel = new Label
                {
                    Text = date.ToLongDateString(),
                    TextColor = Color.Gray,
                    FontSize = 15,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    Margin = new Thickness(5, 10, 0, 0)
                };
                Grid imageGrid = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(App.ScreenWidth / 4, GridUnitType.Absolute) },
                        new ColumnDefinition { Width = new GridLength(App.ScreenWidth / 4, GridUnitType.Absolute) },
                        new ColumnDefinition { Width = new GridLength(App.ScreenWidth / 4, GridUnitType.Absolute) },
                        new ColumnDefinition { Width = new GridLength(App.ScreenWidth / 4, GridUnitType.Absolute) }
                    },
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.Start
                };
                List<ImageDisplay> images = new List<ImageDisplay>();
                foreach (var food in foods)
                {
                    byte[] thumbnail = DeserializeStringToByteArray(food.IMGBYTES);
                    if (thumbnail != null)
                    {
                        ImageDisplay bmp = new ImageDisplay
                        {
                            ImageByte = thumbnail,
                            BackgroundColor = Color.White,
                            WidthRequest = App.ScreenWidth / 4,
                            HeightRequest = App.ScreenWidth / 4,
                            Margin = new Thickness(0,0,0,0),
                            Aspect = Aspect.AspectFill,
                            DatabaseItem = food
                        };
                        bmp.OnTouch = new Command((p) =>
                        {
                            Console.WriteLine("LongClick");
                            if (mode == SelfControl.Helpers.GlobalVariables.GalleryMode.Normal)
                            {
                                mode = SelfControl.Helpers.GlobalVariables.GalleryMode.Selection;
                                MakeOptionsVisible();
                            }
                            bmp.IsSelected = !bmp.IsSelected;
                            if (bmp.IsSelected)
                            {
                                selectedItems.Add(bmp);
                                UpdateTitle();
                            }
                            else
                            {
                                selectedItems.Remove(bmp);
                                UpdateTitle();
                            }
                        });
                        bmp.OnClick = new Command((p) =>
                        {
                            if (mode == SelfControl.Helpers.GlobalVariables.GalleryMode.Selection)
                            {
                                bmp.IsSelected = !bmp.IsSelected;
                                if (bmp.IsSelected)
                                {
                                    selectedItems.Add(bmp);
                                    UpdateTitle();
                                }
                                else
                                {
                                    selectedItems.Remove(bmp);
                                    UpdateTitle();
                                }
                            }
                            else
                            {
                                NagivateImageViewer(bmp.DatabaseItem);
                            }
                        });
                        images.Add(bmp);
                    }
                }
                fillGrid(imageGrid, images);
                view.Children.Add(dateLabel);
                view.Children.Add(imageGrid);


            }
            var reviewButton = new Button
            {
                Text = "Review Your Meals",
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            reviewButton.Clicked += OnReviewButtonClicked;
            view.Children.Add(reviewButton);
        }

        public void UpdateTitle()
        {
            if (mode == GalleryMode.Normal)
            {
                Title = "Gallery";
            }
            else if (mode == GalleryMode.Selection)
            {
                Title = "Selected " + selectedItems.Count.ToString() + " " +"items";
            }
        }

        public void MakeOptionsVisible()
        {
            if (mode == GalleryMode.Normal)
            {
                ToolbarItems.Remove(deleteOption);
            }
            else if (mode == GalleryMode.Selection)
            {
                ToolbarItems.Add(deleteOption);
            }
        }

        public void Update()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                scrollView.ClearValue(ContentProperty);
                scrollView.Content = view;
                this.Content = scrollView;
            });
        }

        private void fillGrid(Grid imageGrid, List<ImageDisplay> images)
        {
            int colNum = 0;
            int rowNum = 0;
            foreach (var bmp in images)
            {
                if (colNum == 4)
                {
                    rowNum++;
                    colNum = 0;
                }
                imageGrid.Children.Add(bmp, colNum, rowNum);
                colNum++;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if(mode == GalleryMode.Selection)
            {
                foreach(var img in selectedItems)
                {
                    img.IsSelected = false;
                }
                selectedItems.Clear();
                mode = GalleryMode.Normal;
                MakeOptionsVisible();
                UpdateTitle();
                return true;
            }
            return false;
        }

        private void DeleteSelectedImage()
        {
            foreach (var i in selectedItems)
            {
                RemoveFromDateDiary(i.DatabaseItem);
                Task.Run(async () => {
                    if (foodItemsDatabase != null)
                    {
                        await foodItemsDatabase.DeleteItemAsync(i.DatabaseItem);
                    }
                });
            }
            foodDiary = getDateDiary();
            selectedItems.Clear();
            UpdateTitle();
            SetView();
            Update();
        }

        async public void NagivateImageViewer(FoodItem id)
        {
            await Navigation.PushAsync(new ImageViewer(id), true);
        }
    }
}