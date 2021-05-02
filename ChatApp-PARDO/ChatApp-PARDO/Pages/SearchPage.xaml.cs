using Newtonsoft.Json;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp_PARDO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        ObservableCollection<UserModel> result = new ObservableCollection<UserModel>();
        DataClass dataClass = DataClass.GetInstance;
        
        public SearchPage(string param)
        {
            InitializeComponent();
            displaySearchResultAsync(param);
        }

        private void closeSearchPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }

        [Obsolete]
        private async Task displaySearchResultAsync(string param)
        {
            isLoading();
            var documents = await CrossCloudFirestore.Current
                                .Instance
                                .GetCollection("users")
                                .WhereEqualsTo("email", param)
                                .GetAsync();

            foreach (var documentChange in documents.Documents)
            {
                //var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                var obj = documentChange.ToObject<UserModel>();
                result.Add(obj);
            }
            
            resultsList.ItemsSource = result;
            resultsList.IsVisible = true;
            if (result.Count == 0)
            {
                await DisplayAlert("", "User not found.", "Okay");
                await Navigation.PopModalAsync(true);
            }
            stopLoading();
        }

        [Obsolete]
        async private void resultsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as UserModel;


            if (dataClass.loggedInUser.uid == item.uid)
            {
                await DisplayAlert("Error", "You are not allowed to add your own self", "OKAY");
            }
            else
            {
                var res = await DisplayAlert("Add contact", "Would you like to add " + item.name + "?", "Yes", "No");
                if (res)
                {
                    isLoading();
                    ContactModel contact = new ContactModel()
                    {
                        id = Guid.NewGuid().ToString(),
                        contactID = new string[] { DataClass.GetInstance.loggedInUser.uid, item.uid },
                        contactEmail = new string[] { DataClass.GetInstance.loggedInUser.email, item.email },
                        contactName = new string[] { DataClass.GetInstance.loggedInUser.uid, item.name },
                        created_at = DateTime.UtcNow.ToString()
                    };
                    //users(owner)->contacts
                    if (dataClass.loggedInUser.contacts == null)
                    {
                        dataClass.loggedInUser.contacts = new List<string>();
                    }
                    if (!(dataClass.loggedInUser.contacts.Contains(item.uid)))
                    {
                        //contacts
                        await CrossCloudFirestore.Current
                            .Instance
                            .GetCollection("contacts")
                            .GetDocument(contact.id)
                            .SetDataAsync(contact);

                        dataClass.loggedInUser.contacts.Add(item.uid);
                        await CrossCloudFirestore.Current
                        .Instance
                        .GetCollection("users")
                        .GetDocument(dataClass.loggedInUser.uid)
                        .UpdateDataAsync(new { contacts = dataClass.loggedInUser.contacts });

                        //users(addedContact)->contacts
                        if (item.contacts == null)
                        {
                            item.contacts = new List<string>();
                        }
                        item.contacts.Add(dataClass.loggedInUser.uid);
                        await CrossCloudFirestore.Current
                            .Instance
                            .GetCollection("users")
                            .GetDocument(item.uid)
                            .UpdateDataAsync(new { contacts = item.contacts });
                        await DisplayAlert("Success", "Contact Added!", "Okay");
                    }
                    else
                    {
                        await DisplayAlert("Failed", "You both already have a connection", "Okay");
                    }
                    await Navigation.PopModalAsync(true);
                    stopLoading();
                }
                else
                {
                    await Navigation.PopModalAsync(true);
                }
            }
        }
        private void isLoading()
        {
            ai.IsRunning = true;
            aiLayout.BackgroundColor = Color.FromRgba(0, 0, 0, 0.50);
            aiLayout.IsVisible = true;
        }
        private void stopLoading()
        {
            aiLayout.IsVisible = false;
            ai.IsRunning = false;
        }
    }
}