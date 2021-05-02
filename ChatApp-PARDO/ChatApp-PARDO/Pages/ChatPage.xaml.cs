using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.CloudFirestore;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp_PARDO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        DataClass dataClass = DataClass.GetInstance;
        ObservableCollection<ContactModel> contactList = new ObservableCollection<ContactModel>();
        public ChatPage()
        {
            InitializeComponent();
            displayContacts();

        }

        private void clearEntry_Clicked(object sender, EventArgs e)
        {
            searchEntry.Text = "";
            clearEntry.IsVisible = false;
        }

        private void contactsList_ItemTapped (object sender, ItemTappedEventArgs e)
        {
            var contact = e.Item as ContactModel;
            Navigation.PushModalAsync(new Conversation(contact));
        }
        private void searchEntry_TextChanged (object sender, TextChangedEventArgs e)
        {
            clearEntry.IsVisible = true;
        }

        async private void displayContacts()
        {
            CrossCloudFirestore.Current
                .Instance
                .GetCollection("contacts")
                .WhereArrayContains("contactID", dataClass.loggedInUser.uid)
                .AddSnapshotListener(async (snapshot, error) =>
               {
                   isLoading();

                   if (snapshot != null)
                   {
                       foreach (var documentChange in snapshot.DocumentChanges)
                       {
                           var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                           var obj = JsonConvert.DeserializeObject<ContactModel>(json);

                           switch (documentChange.Type)
                           {
                               case DocumentChangeType.Added:
                                   contactList.Add(obj);
                                   break;

                               case DocumentChangeType.Modified:
                                   if (contactList.Where(c => c.id == obj.id).Any())
                                   {
                                       var item = contactList.Where(c => c.id == obj.id).FirstOrDefault();
                                       item = obj;
                                   }
                                   break;
                               case DocumentChangeType.Removed:
                                   if (contactList.Where(c => c.id == obj.id).Any())
                                   {
                                       var item = contactList.Where(c => c.id == obj.id).FirstOrDefault();
                                       string idOfRemoved = item.contactID[0] == dataClass.loggedInUser.uid ? item.contactID[1] : item.contactID[0];
                                       bool test = dataClass.loggedInUser.contacts.Remove(idOfRemoved);
                                       contactList.Remove(item);
                                   }
                                   break;
                           }
                       }
                   }
                   emptyListLabel.IsVisible = contactList.Count == 0;
                   contactsList.IsVisible = !(contactList.Count == 0);
                   contactsList.ItemsSource = contactList;
                   stopLoading();
               });
        }

        private void isLoading()
        {
            ai.IsRunning = true;
            aiLayout.BackgroundColor = Color.FromRgba(0, 0, 0, 0.5);
            aiLayout.IsVisible = true;
        }

        private void stopLoading()
        {
            aiLayout.IsVisible = false;
            ai.IsRunning = false;
        }

        private void searchEntry_Completed(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SearchPage(searchEntry.Text), true);
        }
    }
}