using Android.App;
using Android.Content;
using FEI_News.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEI_News
{
    [Activity(Label = "FEI News", Icon = "@drawable/icon", Theme = "@style/splashscreen", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnResume()
        {
            base.OnResume();
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}
