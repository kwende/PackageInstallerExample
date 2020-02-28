using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.App.Admin;
using Android.Content;
using Android.Support.V4.Content;
using Android;
using Android.Support.V4.App;

namespace PackageInstallerExample.Droid
{
    [Activity(Label = "PackageInstallerExample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Looks like permissions to read and write external storage must be explicit. 
            //https://stackoverflow.com/questions/31746787/xamarin-android-system-unauthorizedaccessexception-access-to-the-path-is-de
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.WriteExternalStorage }, 0);
            }

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage }, 0);
            }

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            // This will prompt the user to allow/disallow the elevation of this app as "device owner". 
            DevicePolicyManager devicePolicyManager = (DevicePolicyManager)GetSystemService(Context.DevicePolicyService);
            ComponentName demoDeviceAdmin = new ComponentName(this, Java.Lang.Class.FromType(typeof(DeviceAdminBroadcastReceiver)));
            Intent intent = new Intent(DevicePolicyManager.ActionAddDeviceAdmin);
            intent.PutExtra(DevicePolicyManager.ExtraDeviceAdmin, demoDeviceAdmin);
            intent.PutExtra(DevicePolicyManager.ExtraAddExplanation, "Device administrator");
            StartActivity(intent);

            IntentFilter filter = new IntentFilter();
            filter.AddAction("ANY_UNIQUE_NAME_WILL_DO");
            RegisterReceiver(new InstallerStatusBroadcastReceiver(), filter);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}