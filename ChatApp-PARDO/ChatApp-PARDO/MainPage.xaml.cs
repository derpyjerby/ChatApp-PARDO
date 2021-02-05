using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChatApp_PARDO
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(NameEntry.Text) && !string.IsNullOrEmpty(EmailEntry.Text) && !string.IsNullOrEmpty(PasswordEntry.Text))
            {
                Application.Current.Properties["name"] = NameEntry.Text;
                Application.Current.Properties["email"] = EmailEntry.Text;
                Application.Current.SavePropertiesAsync();

                Application.Current.MainPage = new SampleTabbedPage(NameEntry.Text, EmailEntry.Text);
                /*
                //Use this if you want the master detail page
                Application.Current.MainPage = new MainMasterDetailPage(NameEntry.Text, EmailEntry.Text); 
                */
            }
            else
            {
                bool retryBool = await DisplayAlert("Error", "Please fill in all fields. Retry?", "Yes", "No");
                if (retryBool)
                {
                    NameEntry.Text = string.Empty;
                    EmailEntry.Text = string.Empty;
                    PasswordEntry.Text = string.Empty;
                    NameEntry.Focus();
                }
            }
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            NameEntry.Text = string.Empty;
            EmailEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
        }

        private async void SignUp_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SignupPage(), true);
        }
    }
}
