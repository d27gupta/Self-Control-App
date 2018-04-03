﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace SelfControl.Helpers
{
    public class PracticeImageView : Image
    {

        public static readonly BindableProperty ImageByteProperty =
        BindableProperty.Create("ImageByte", typeof(byte[]), typeof(PracticeImageView), null, propertyChanged: (bindable, oldValue, newValue) =>
        {
        });

        public static readonly BindableProperty IncreaseSaturationProperty =
        BindableProperty.Create("IncreaseSaturation", typeof(int), typeof(PracticeImageView), 0, propertyChanged: (bindable, oldValue, newValue) =>
        {
        });

        public byte[] ImageByte
        {
            get { return (byte[])GetValue(ImageByteProperty); }
            set { SetValue(ImageByteProperty, value); }
        }

        public event EventHandler<int> SaturationIncreased;

        public int IncreaseSaturation
        {
            get { return (int)GetValue(IncreaseSaturationProperty); }
            set
            {
                Console.WriteLine("Increase Saturation Set" + System.Environment.NewLine);
                SetValue(IncreaseSaturationProperty, value);
                var eventHandler = this.SaturationIncreased;
                if (eventHandler != null)
                {
                    eventHandler.Invoke(this, value);
                }
            }
        }
    }
}
