using System;
using Android.App;
using Android.Content;
using Android.OS;

namespace iNAV
{
    [Activity(Label = "iNAV", MainLauncher = true, Theme = "@style/Theme.Splash", NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Start our real activity
            
            StartActivity(typeof(MainActivity));
        }
    }
}