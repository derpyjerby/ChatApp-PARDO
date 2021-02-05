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
	public partial class SampleTabbedPage : TabbedPage
	{
		public SampleTabbedPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public SampleTabbedPage(string name, string email)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            profilePage.Name = name;
            profilePage.Email = email;
        }
    }
}