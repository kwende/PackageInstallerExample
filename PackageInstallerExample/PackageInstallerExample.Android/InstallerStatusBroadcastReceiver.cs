using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PackageInstallerExample.Droid
{
    [BroadcastReceiver]
    public class InstallerStatusBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            int status = intent.GetIntExtra(PackageInstaller.ExtraStatus, -1);
            string moreMessage = intent.GetStringExtra(PackageInstaller.ExtraStatusMessage);

            // error codes and whatnot pulled from //https://developer.android.com/reference/android/content/pm/PackageInstaller#STATUS_FAILURE
            switch (status)
            {
                // STATUS_FAILURE_INVALID
                case 4:
                    Toast.MakeText(context, "STATUS_FAILURE_INVALID!", ToastLength.Short).Show();
                    //The operation failed because one or more of the APKs was invalid. For example, they might be malformed, corrupt, incorrectly signed, mismatched, etc.
                    break;
            }

            return;
        }
    }
}
