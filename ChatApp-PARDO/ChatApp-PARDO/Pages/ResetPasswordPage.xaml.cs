using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp_PARDO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPasswordPage : ContentPage
    {
        public ResetPasswordPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void SendEmail_Clicked(object sender, EventArgs e)
        {
            if (EmailEntry.Text.Length == 0)
            {
                await DisplayAlert("Error", "Missing fields", "Okay");
            }
            else
            {
                loading.IsVisible = true;
                FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
                res = await DependencyService.Get<iFirebaseAuth>().ResetPassword(EmailEntry.Text);

                if (res.Status == true)
                {
                    await DisplayAlert("Success", res.Response, "Okay");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error", res.Response, "Okay");
                }
                loading.IsVisible = false;
            }
        }

        private async void Back_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}