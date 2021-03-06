﻿using FFImageLoading.Forms;
using SelfControl.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SelfControl.Helpers
{
    public class ImageDisplay : Image
    {
        public static readonly BindableProperty ImageFileProperty =
        BindableProperty.Create("ImageFile", typeof(string), typeof(ImageDisplay), default(string), propertyChanged: (bindable, oldValue, newValue) =>
        {
            if (Device.RuntimePlatform != Device.Android)
            {
                var image = (ImageDisplay)bindable;

                var baseImage = (Image)bindable;
                baseImage.Source = image.ImageFile;
            }
        });

        public static readonly BindableProperty ImageIdProperty =
        BindableProperty.Create("DataBaseItem", typeof(FoodItem), typeof(ImageDisplay), null, propertyChanged: (bindable, oldValue, newValue) =>
        {
        });

        public static readonly BindableProperty IsSelectedProperty =
        BindableProperty.Create("IsSelected", typeof(bool), typeof(ImageDisplay), false, propertyChanged: (bindable, oldValue, newValue) =>
        {
        });

        public static readonly BindableProperty ImageByteProperty =
        BindableProperty.Create("ImageByte", typeof(byte[]), typeof(ImageDisplay), null, propertyChanged: (bindable, oldValue, newValue) =>
        {
            if (Device.RuntimePlatform != Device.Android)
            {
                var image = (ImageDisplay)bindable;

                var baseImage = (Image)bindable;
                baseImage.Source = ImageSource.FromStream(() => new MemoryStream(image.ImageByte));
            }
        });

        public static readonly BindableProperty OnClickProperty =
        BindableProperty.Create("OnClick", typeof(ICommand), typeof(ImageDisplay), null);

        public static readonly BindableProperty OnTouchProperty =
        BindableProperty.Create("OnTouch", typeof(ICommand), typeof(ImageDisplay), null);

        public string ImageFile
        {
            get { return GetValue(ImageFileProperty) as string; }
            set { SetValue(ImageFileProperty, value); }
        }

        public byte[] ImageByte
        {
            get { return (byte[])GetValue(ImageByteProperty); }
            set { SetValue(ImageByteProperty, value); }
        }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public FoodItem DatabaseItem
        {
            get { return (FoodItem)GetValue(ImageIdProperty); }
            set { SetValue(ImageIdProperty, value); }
        }

        public ICommand OnClick
        {
            get { return (ICommand)GetValue(OnClickProperty); }
            set { SetValue(OnClickProperty, value); }
        }

        public ICommand OnTouch
        {
            get { return (ICommand)GetValue(OnTouchProperty); }
            set { SetValue(OnTouchProperty, value); }
        }
    }
}
