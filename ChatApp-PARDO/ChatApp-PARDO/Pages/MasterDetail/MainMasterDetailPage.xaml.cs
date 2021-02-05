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
		public MainMasterDetailPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            masterPage.ListView.ItemSelected += OnItemSelected;
        }

        public MainMasterDetailPage(string name, string email)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            masterPage.GreetingLabel.Text = "Welcome "+name+"!";
            masterPage.EmailLabel.Text = email;
            masterPage.ListView.ItemSelected += OnItemSelected;
            
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