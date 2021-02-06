using Plugin.CloudFirestore;
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
    public partial class SignupPage : ContentPage
    {
        DataClass dataClass = DataClass.GetInstance;
        public SignupPage()
        {
            InitializeComponent();
        }

        private async void SignIn_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }

        private async void SignUp_Clicked(object sender, EventArgs e)
        {
            if (EmailEntry.Text.Length == 0 || PasswordEntry.Text.Length == 0 || ConfirmPasswordEntry.Text.Length == 0)
            {
                await DisplayAlert("Error", "Missing field/s", "Okay");
            }
            else
            {
                if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
                {
                    await DisplayAlert("Error", "Passwords don't match.", "Okay");
                    ConfirmPasswordEntry.Text = string.Empty;
                    ConfirmPasswordEntry.Focus();
                }
                else
                {
                    loading.IsVisible = true;
                    FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
                    res = await DependencyService.Get<iFirebaseAuth>().SignUpWithEmailPassword(NameEntry.Text, EmailEntry.Text, PasswordEntry.Text);

                    if (res.Status == true)
                    {
                        try
                        {
                            await CrossCloudFirestore.Current
                             .Instance
                             .GetCollection("users")
                             .GetDocument(dataClass.loggedInUser.uid)
                             .SetDataAsync(dataClass.loggedInUser);

                            await DisplayAlert("Success", res.Response, "Okay");
                            await Navigation.PopModalAsync(true);
                        }
                        catch (Exception ex)
                        {
                            await DisplayAlert("Error", ex.Message, "Okay");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", res.Response, "Okay");
                    }
                    loading.IsVisible = false;
                }
            }
        }
    }
}