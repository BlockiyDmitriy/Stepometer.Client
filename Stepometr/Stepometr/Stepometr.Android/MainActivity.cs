using System;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using AndroidX.Core.Content;
using Stepometer.Droid.Service;
using Stepometer.Service;
using Xamarin.Forms;

namespace Stepometer.Droid
{
    [Activity(Label = "Stepometer", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                               ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity Instance;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Instance = this;
            Rg.Plugins.Popup.Popup.Init(this);
            global::Xamarin.Forms.Forms.SetFlags("Shell_UWP_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());


            DoBindService();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }
        private async void DoBindService()
        {
            Log.Info("MyApp", "DoBindService");

            //InitService();

            var channel = new NotificationChannel("com.companyname.Stepometer", "com.companyname.Stepometer",
                NotificationImportance.Default)
            {
                Description = "com.companyname.Stepometer"
            };
            var notificationManager = (NotificationManager) GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);

            var res = await CheckAndRequestPermission();

        }

        private async Task<Permission> CheckAndRequestPermission()
        {
            var status = ContextCompat.CheckSelfPermission(this, Manifest.Permission.ActivityRecognition);
            if (status != Permission.Granted)
            {
                // Permission is not granted
                ActivityCompat.RequestPermissions(this, new string[]
                {
                   Manifest.Permission.ActivityRecognition
                }, 0);
                status = Permission.Granted;
            }
            return status;
        }
    }
}