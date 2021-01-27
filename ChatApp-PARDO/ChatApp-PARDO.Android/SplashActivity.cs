using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Support.V7.App;
using ChatApp_PARDO.Droid;

namespace ChatApp_PARDO.Droid
{
    [Activity(Label = "ChatApp_PARDO", Icon = "@mipmap/ic_launcher", Theme = "@style/splesh", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(typeof(MainActivity));
        }
    }

}