using App1.Common;
using App1.Interface;
using App1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace App1
{
    public class LoginPage : ContentPage
    {
        private Grid _loginGridView;
        private Label _loginScreenLabel;
        private Entry _userNameEntry;
        private Entry _passwordEntry;
        private Button _loginButton;
        private Label _biometrickTextLabel;
        public LoginPage()
        {
            BindingContext = App.Locator.Main;
            GlobalObject.curMainPage = this;
            _loginGridView = new Grid()
            {
                Style = (Style)App.Current.Resources[ConfigConstants.GRID_VIEW_LAYOUT],
                RowDefinitions =
                {
                    new RowDefinition{Height = new GridLength(1,GridUnitType.Auto) },
                    new RowDefinition{Height = new GridLength(1,GridUnitType.Auto) },
                    new RowDefinition{Height = new GridLength(1,GridUnitType.Auto) },
                    new RowDefinition{Height = new GridLength(1,GridUnitType.Auto) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition{Width = new GridLength(1,GridUnitType.Star)}
                }
            };
            _loginScreenLabel = new Label()
            {
                Style = (Style)App.Current.Resources[ConfigConstants.LABEL_VIEW_LAYOUT],
                Text = "Welcome Biometric Authentication",
                TextColor = Color.Orange
            };
            _userNameEntry = new Entry()
            {
                Style = (Style)App.Current.Resources[ConfigConstants.TEXTFIELD_VIEW_LAYOUT],
                Placeholder = "Username",
                PlaceholderColor = Color.DimGray
            };
            _userNameEntry.SetBinding(Entry.TextProperty, ConfigConstants.USERNAME);

            _passwordEntry = new Entry()
            {
                Style = (Style)App.Current.Resources[ConfigConstants.TEXTFIELD_VIEW_LAYOUT],
                Placeholder = "Password",
                IsPassword = true,
                PlaceholderColor = Color.DimGray
            };
            _userNameEntry.SetBinding(Entry.TextProperty, ConfigConstants.PASSWORD);


            _loginButton = new Button()
            {
                Style = (Style)App.Current.Resources[ConfigConstants.BUTTON_VIEW_LAYOUT]
            };
            
            _biometrickTextLabel = new Label()
            {
                IsVisible = false,
                Style = (Style)App.Current.Resources[ConfigConstants.LABEL_VIEW_LAYOUT]
            };
            var tgr = new TapGestureRecognizer();
            tgr.Tapped += Tgr_Tapped;
            _biometrickTextLabel.GestureRecognizers.Add(tgr);

            _loginGridView.Children.Add(_loginScreenLabel, 0, 0);
            _loginGridView.Children.Add(_userNameEntry, 0, 1);
            _loginGridView.Children.Add(_passwordEntry, 0, 2);
            _loginGridView.Children.Add(_loginButton, 0, 3);
            _loginGridView.Children.Add(_biometrickTextLabel, 0, 5);

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                   _loginGridView
                }
            };
        }

        private void Tgr_Tapped(object sender, EventArgs e)
        {
            bool canFingerprint = DependencyService.Get<Interface.IAuthentication>().IsFingerprintAuthenticationPossible();
            if (canFingerprint)
            {
                DependencyService.Get<Interface.IAuthentication>().Authenticate();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var AuthType = DependencyService.Get<Interface.IAuthentication>().GetLocalAuthType();
            if (AuthType == LocalAuthType.FaceId)
            {
                _biometrickTextLabel.IsVisible = true;
                _biometrickTextLabel.Text = $"Login with Face ID";
            }
            else if (AuthType == LocalAuthType.TouchId)
            {
                _biometrickTextLabel.IsVisible = true;
                _biometrickTextLabel.Text = $"Login with Touch ID";
            }
            else if (AuthType == LocalAuthType.Passcode)
            {
                _biometrickTextLabel.IsVisible = true;
                _biometrickTextLabel.Text = $"Login with Password";
            }
            else
            {
                _biometrickTextLabel.IsVisible = false;
            }
        }
    }
}