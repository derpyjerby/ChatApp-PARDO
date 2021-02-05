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
	public partial class MasterPage : ContentPage
	{
		public MasterPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
		}

		private void LogoutButton_Clicked(object sender, EventArgs e)
		{
			//Remove all saved properties
			Application.Current.Properties.Clear();
			//Remove specific properties
			Application.Current.Properties.Remove("email");
			Application.Current.Properties.Remove("name");
			Application.Current.SavePropertiesAsync();

			Application.Current.MainPage = new MainPage();
		}
	}
}