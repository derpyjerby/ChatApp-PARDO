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
    public partial class MainMasterDetailPage : MasterDetailPage
    {
        DataClass dataClass = DataClass.GetInstance;
        public MainMasterDetailPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            masterPage.ListView.ItemSelected += OnItemSelected;
            masterPage.GreetingLabel.Text = "Welcome " + dataClass.loggedInUser.name + "!";
            masterPage.EmailLabel.Text = dataClass.loggedInUser.email;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterDetailItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}