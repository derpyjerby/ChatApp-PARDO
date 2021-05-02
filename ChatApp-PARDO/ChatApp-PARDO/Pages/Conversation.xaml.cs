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
    public partial class Conversation : ContentPage
    {
        ObservableCollection<ConversationModel> conversationList = new ObservableCollection<ConversationModel>();
        DataClass dataClass = DataClass.GetInstance;

        public Conversation(ContactModel contact)
        {
            this.BindingContext = contact;
            InitializeComponent();
            displayConversation(contact);
        }

        private void closeConvoPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }
        private void displayConversation(ContactModel contact)
        {
            isLoading();
            CrossCloudFirestore.Current
                .Instance
                .GetCollection("contacts")
                .GetDocument(contact.id)
                .GetCollection("conversations")
                .OrderBy("created_at", false)
                .AddSnapshotListener(async (snapshot, error) =>
                {
                    conversationsListView.ItemsSource = conversationList;
                    if (snapshot != null)
                    {
                        foreach (var documentChange in snapshot.DocumentChanges)
                        {
                            //var obj = new ConversationModel
                            //{
                            //    id = JsonConvert.DeserializeObject<string>(JsonConvert.SerializeObject(documentChange.Document.Data["id"])),
                            //    message = JsonConvert.DeserializeObject<string>(JsonConvert.SerializeObject(documentChange.Document.Data["message"])),
                            //    converseeID = JsonConvert.DeserializeObject<string>(JsonConvert.SerializeObject(documentChange.Document.Data["converseeID"])),
                            //    created_at = DateTime.UtcNow.ToString()
                            //};
                            var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                            var obj = JsonConvert.DeserializeObject<ConversationModel>(json);
                            switch (documentChange.Type)
                            {
                                case DocumentChangeType.Added:
                                    conversationList.Add(obj);
                                    break;
                                case DocumentChangeType.Modified:
                                    if (conversationList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = conversationList.Where(c => c.id == obj.id).FirstOrDefault();
                                        item = obj;
                                    }
                                    break;
                                case DocumentChangeType.Removed:
                                    if (conversationList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = conversationList.Where(c => c.id == obj.id).FirstOrDefault();
                                        conversationList.Remove(item);
                                    }
                                    break;
                            }
                            var conv = conversationsListView.ItemsSource.Cast<object>().LastOrDefault();
                            conversationsListView.ScrollTo(conv, ScrollToPosition.End, false);
                        }
                    }
                    emptyListLabel.IsVisible = conversationList.Count == 0;
                    conversationsListView.IsVisible = !(conversationList.Count == 0);
                });
            stopLoading();
        }
        async private void sendBtn_Clicked(object sender, EventArgs e)
        {
            var contact = BindingContext as ContactModel;
            string ID = IDGenerator.generateID();
            ConversationModel conversation = new ConversationModel()
            {
                id = ID,
                converseeID = dataClass.loggedInUser.uid,
                message = messageEditor.Text,
                created_at = DateTime.UtcNow.ToString()
            };
            await CrossCloudFirestore.Current
                .Instance
                .GetCollection("contacts")
                .GetDocument(contact.id)
                .GetCollection("conversations")
                .GetDocument(ID)
                .SetDataAsync(conversation);
            messageEditor.Text = "";
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