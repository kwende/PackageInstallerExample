using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.App.Admin;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PackageInstallerExample.Droid
{
    [BroadcastReceiver(Permission = "android.permission.BIND_DEVICE_ADMIN")]
    [MetaData("android.app.device_admin", Resource = "@xml/device_admin_privs")]
    [IntentFilter(new[] { "android.app.action.DEVICE_ADMIN_ENABLED", Intent.ActionMain })]
    public class DeviceAdminBroadcastReceiver : DeviceAdminReceiver
    {
        public override void OnEnabled(Context context, Intent intent)
        {
            base.OnEnabled(context, intent);
            Toast.MakeText(context, "DeviceAdmin", ToastLength.Short).Show();
        }
        public override void OnDisabled(Context context, Intent intent)
        {
            base.OnDisabled(context, intent);
        }

        public override void OnPasswordChanged(Context context, Intent intent)
        {
            base.OnPasswordChanged(context, intent);
        }

        public override void OnPasswordFailed(Context context, Intent intent)
        {
            base.OnPasswordFailed(context, intent);
        }
        public override void OnPasswordSucceeded(Context context, Intent intent)
        {
            base.OnPasswordSucceeded(context, intent);
        }

        public override void OnReceive(Context context, Intent intent)
        {
            base.OnReceive(context, intent);
        }
    }
}