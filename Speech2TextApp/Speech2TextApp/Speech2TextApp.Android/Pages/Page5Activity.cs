using Android.OS;
using Android.App;
using Android.Widget;
using System;
using Newtonsoft.Json;
using System.IO;
using Android.Views;

namespace Speech2TextApp.Droid.Pages
{
    [Activity(Label = "Page5Activity")]
    public class Page5Activity : Activity
    {
        private  LinearLayout _pageLayout;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Page5Activity);

            var submit = FindViewById<Button>(Resource.Id.btn_page_5_next);
            submit.Click += NextButtonEvent;

            _pageLayout = FindViewById<LinearLayout>(Resource.Id.page_5_layout);
            // put MainActivity.dataCurrent to linear layout
            _pageLayout.AddView(GetFirstPage());
            _pageLayout.AddView(GetSecondPage());
            _pageLayout.AddView(GetThirdPage());
            _pageLayout.AddView(GetForthPage());
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

        private LinearLayout GetFirstPage()
        {
            var layoutParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,                
                ViewGroup.LayoutParams.WrapContent, 1.0f);
            layoutParameter.SetMargins(20, 5, 0, 0);
            var layout = new LinearLayout(this)
            {
                Orientation = Orientation.Vertical,
                WeightSum = 2,
                LayoutParameters = layoutParameter
            };

            var title = new TextView(this) { Text = "1" };
            layout.AddView(title);

            var line = new View(this);
            line.SetBackgroundColor(Android.Graphics.Color.ParseColor("#4A90E2"));
            var lineParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 1);
            line.LayoutParameters = lineParameter;
            layout.AddView(line);

            var visitedCount = new TextView(this);
            visitedCount.Text = $"訪視次數:{MainActivity.dataCurrent.VisitCount}";
            visitedCount.SetTextColor(Android.Graphics.Color.ParseColor("#4A90E2"));

            return layout;
        }

        private LinearLayout GetSecondPage()
        {
            var layoutParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent, 1.0f);
            layoutParameter.SetMargins(20, 5, 0, 0);
            var layout = new LinearLayout(this)
            {
                Orientation = Orientation.Vertical,
                WeightSum = 2,
                LayoutParameters = layoutParameter
            };

            var title = new TextView(this) { Text = "2" };
            layout.AddView(title);

            var line = new View(this);
            line.SetBackgroundColor(Android.Graphics.Color.ParseColor("#4A90E2"));
            var lineParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 2);
            line.LayoutParameters = lineParameter;
            layout.AddView(line);
            return layout;
        }

        private LinearLayout GetThirdPage()
        {
            var layoutParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent, 1.0f);
            layoutParameter.SetMargins(20, 5, 0, 0);
            var layout = new LinearLayout(this)
            {
                Orientation = Orientation.Vertical,
                WeightSum = 2,
                LayoutParameters = layoutParameter
            };

            var title = new TextView(this) { Text = "3" };
            layout.AddView(title);

            var line = new View(this);
            line.SetBackgroundColor(Android.Graphics.Color.ParseColor("#4A90E2"));
            var lineParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 1);
            line.LayoutParameters = lineParameter;
            layout.AddView(line);
            return layout;
        }

        private LinearLayout GetForthPage()
        {
            var layoutParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent, 1.0f);
            layoutParameter.SetMargins(20, 5, 0, 0);
            var layout = new LinearLayout(this)
            {
                Orientation = Orientation.Vertical,
                WeightSum = 2,
                LayoutParameters = layoutParameter
            };

            var title = new TextView(this) { Text = "4" };
            layout.AddView(title);

            var line = new View(this);
            line.SetBackgroundColor(Android.Graphics.Color.ParseColor("#4A90E2"));
            var lineParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 1);
            line.LayoutParameters = lineParameter;
            layout.AddView(line);
            return layout;
        }
    }
}