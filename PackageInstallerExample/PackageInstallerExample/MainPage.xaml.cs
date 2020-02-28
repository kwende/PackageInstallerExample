using Android.App;
using Android.Content;
using Android.Content.PM;
using System;
using System.ComponentModel;
using System.IO;
using Xamarin.Forms;

namespace PackageInstallerExample
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnUpdateButtonClicked(object sender, EventArgs args)
        {
            // path to the APK to be installed. This could be something downloaded, or it could a file on the SD card. 
            const string localPath = "/storage/emulated/0/Download/test/HelloWorld_v1.0_apkpure.com.apk";

            // the following code was largely inspired from 
            // here: https://github.com/nagamanojv/android-kiosk-example/blob/master/KioskExample/app/src/main/java/com/sureshjoshi/android/kioskexample/MainActivity.java. 
            // here: https://forums.xamarin.com/discussion/73589/does-anyone-try-to-install-package-with-packageinstaller-i-get-files-still-open-exception
            // here: https://forums.xamarin.com/discussion/170925/xamarin-android-10-how-to-install-3rd-party-apk

            //instantiate a package installer and its parameters.
            PackageInstaller installer = Android.App.Application.Context.PackageManager.PackageInstaller;
            PackageInstaller.SessionParams sessionParams = new PackageInstaller.SessionParams(PackageInstallMode.FullInstall);

            int sessionId = installer.CreateSession(sessionParams);
            PackageInstaller.Session session = installer.OpenSession(sessionId);

            using (var input = new FileStream(localPath, FileMode.Open, FileAccess.Read))
            {
                using (var packageInSession = session.OpenWrite("package", 0, -1))
                {
                    input.CopyTo(packageInSession);
                }
            }

            //That this is necessary could be a Xamarin bug.
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, sessionId, new Intent("ANY_UNIQUE_NAME_WILL_DO"), 0);
            session.Commit(pendingIntent.IntentSender);

            return; 
        }
    }
}
