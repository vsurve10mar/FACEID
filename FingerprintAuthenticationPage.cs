using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace App1
{
    public class FingerprintAuthenticationPage : PopupPage
    {
        ContentView contentView;
        ScaleAnimation scaleAnimation;
        Button Cancel_Button;
        Label FailAuthenLabel, unLocklabel, useBioMetricToUnlock;
        Image FingerprintButton;
        StackLayout stackLayout;
        public FingerprintAuthenticationPage(int androidSdk)
        {
            scaleAnimation = new ScaleAnimation()
            {
                PositionIn = MoveAnimationOptions.Center,
                PositionOut = MoveAnimationOptions.Center,
                ScaleIn = 1.2,
                ScaleOut = 0.8,
                DurationIn = 400,
                DurationOut = 300,
                EasingIn = Easing.SinInOut,
                EasingOut = Easing.SinIn,
                HasBackgroundAnimation = true
            };
            this.Animation = scaleAnimation;

            contentView = new ContentView()
            {
                Margin = new Thickness(20, 0, 20, 0),
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            unLocklabel = new Label()
            {
                Text = "Unlock",
                HorizontalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                TextColor = Color.Black,
                FontSize = 14,
                Margin = new Thickness(20, 20, 20, 0)
            };

            useBioMetricToUnlock = new Label()
            {
                Text = "Use your fingerprint to unlock.",
                HorizontalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(20, 0, 20, 0)
            };

            FingerprintButton = new Image()
            {
                Margin = new Thickness(0, 30, 0, 0),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 50,
                HeightRequest = 50,
                Source = "ic_fingerprint.png"
            };

            FailAuthenLabel = new Label()
            {
                VerticalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                TextColor = Color.Red,
                IsVisible = false
            };
            Cancel_Button = new Button()
            {
                Text = "Cancel",
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(0, 10, 0, 0),
                BackgroundColor = Color.White,
                BorderWidth = 2,
                BorderColor = Color.White,
                BorderRadius = 0,
                TextColor = Color.Orange
            };
            Cancel_Button.Clicked += Cancel_Button_Clicked;

            stackLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    unLocklabel,
                    FingerprintButton,
                    FailAuthenLabel,
                    Cancel_Button
                }
            };

        }
        public void SetFailLabelText(string text)
        {
            FailAuthenLabel.Text = text;
            FailAuthenLabel.IsVisible = true;
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        protected override bool OnBackgroundClicked()
        {
            return false;
        }


        private void Cancel_Button_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<Interface.IAuthentication>().CancelCurrentAuthentication();
        }
    }
}