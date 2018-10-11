using System;
using Android.OS;
using Android.App;
using Android.Widget;

namespace Speech2TextApp.Droid.Pages
{
    [Activity(Label = "Page4Activity")]
    public class Page4Activity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Page4Activity);

            var next = FindViewById<Button>(Resource.Id.btn_page_2_next);
            next.Click += NextButtonEvent;
        }

        public void NextButtonEvent(object sender, EventArgs e)
        {
            StartActivity(typeof(Page5Activity));
        }
    }
}