using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;


namespace XFormsPerformance.Android
{
    [Activity(Label = "XFormsPerformance.Android.Android", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            FindViewById<Button>(Resource.Id.myButton).Click += delegate {
                App.StartTime = DateTime.Now;
                StartActivity(new Intent(this, typeof(Stop)));
            };
        }
    }


    [Activity(Label = "Stop")]            
    public class Stop : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Stop);
            var list = FindViewById<LinearLayout>(Resource.Id.list);
            for (var i = 0; i < 40; i++)
                list.AddView(new TextView(this){ Text = "Label " + i });
        }

        protected override void OnResume()
        {
            base.OnResume();
            (FindViewById<LinearLayout>(Resource.Id.list).GetChildAt(0) as TextView).Text = "Stop after " + (DateTime.Now - App.StartTime).TotalMilliseconds + " ms";

        }
    }

}

