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
        DataClass dataClass = DataClass.GetInstance;
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            EmailEntry.Text = dataClass.loggedInUser != null ? dataClass.loggedInUser.email : "";
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EmailEntry.Text) && string.IsNullOrEmpty(PasswordEntry.Text))
            {
                 DisplayAlert("Error", "Missing fields", "Okay");
            }
            else
            {
                loading.IsVisible = true;
                FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
                res = await DependencyService.Get<iFirebaseAuth>().LoginWithEmailPassword(EmailEntry.Text, PasswordEntry.Text);

                if (res.Status == true)
                {
                    Application.Current.MainPage = new SampleTabbedPage();
                    /*
                    //Use this if you want the master detail page
                    Application.Current.MainPage = new MainMasterDetailPage(); 
                    */
                }
                else
                {
                    bool retryBool = await DisplayAlert("Error", res.Response + " Retry?", "Yes", "No");
                    if (retryBool)
                    {
                        EmailEntry.Text = string.Empty;
                        PasswordEntry.Text = string.Empty;
                        EmailEntry.Focus();
                    }
                }
                loading.IsVisible = false;
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();


            //logo.Rotation = 90;//animation without time
            //await logo.RotateTo(90, 5000);//animation with time

            //logo.Scale = 2;//animation without time
            //await logo.ScaleTo(2, 5000);//animation with time

            //logo.TranslationX = 50;//animation without time
            //logo.TranslationY = 50;//animation without time
            //await logo.TranslateTo(50, 50, 5000);//animation with time

            //logo.Opacity = 0.5;//animation without time
            //await logo.FadeTo(0.1, 5000);//animation with time
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            EmailEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
        }

        private async void SignUp_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SignupPage(), true);
        }

        private async void ForgotPassword_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ResetPasswordPage(), true);
        }
    }
}
