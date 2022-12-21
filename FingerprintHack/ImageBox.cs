using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace FingerprintHack
{
    public class ImageBox : ContentView
    {
        protected Image img;
        private bool Checked;
        private int id;
        public ImageBox()
        {
            var TapGesture = new TapGestureRecognizer();
            TapGesture.Tapped += TapGestureOnTapped;
            GestureRecognizers.Add(TapGesture);

            img = new Image
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };
            img.HeightRequest = 100;
            img.WidthRequest = 100;
            img.Source = "p0_0.PNG";
            Padding = 5;
            Content = img;
        }
        public void setSource(string s, int i)
        {
            img.Source = s;
            id = i;
        }

        private void TapGestureOnTapped(object sender, EventArgs eventArgs)
        {
            if (IsEnabled)
            {
                Checked = !Checked;
            }
            if (Checked)
            {
                BackgroundColor = Color.Red;
            }
            else
            {
                BackgroundColor = Color.White;
            }
        }
        public bool isChecked()
        {
            return Checked;
        }
        public void uncheck()
        {
            Checked = false;
            BackgroundColor = Color.White;
        }
        public int getId()
        {
            return id;
        }
    }
}
