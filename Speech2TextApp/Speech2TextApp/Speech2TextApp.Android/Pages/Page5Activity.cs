using Android.OS;
using Android.App;
using Android.Widget;
using System;
using Newtonsoft.Json;
using System.IO;

namespace Speech2TextApp.Droid.Pages
{
    [Activity(Label = "Page5Activity")]
    public class Page5Activity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Page5Activity);

            var submit = FindViewById<Button>(Resource.Id.btn_page_5_next);
            submit.Click += NextButtonEvent;
        }

        public void NextButtonEvent(object sender, EventArgs e)
        {
            MainActivity.dataCurrent.VisitDetails.Add(MainActivity.dataCurrent.VisitDetail);
            MainActivity.dataCurrent.Status = "Y";
            var descDocument = GetExternalFilesDir(null).AbsolutePath;
            string json = JsonConvert.SerializeObject(MainActivity.dataCurrent);
            string destPath = Path.Combine(descDocument, "data4.json");
            File.WriteAllText(destPath, json);
            StartActivity(typeof(MainActivity));
        }
    }
}