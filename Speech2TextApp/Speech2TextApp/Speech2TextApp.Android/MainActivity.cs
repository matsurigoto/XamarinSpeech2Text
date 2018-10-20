using Android.App;
using Android.Content.PM;
using Android.Widget;
using Android.OS;
using Android.Content;
using Speech2TextApp.Interface;
using Speech2TextApp.Data;
using System.Collections.Generic;
using Speech2TextApp.Service;
using System;
using System.Linq;
using Android.Graphics;
using Speech2TextApp.Droid.Pages;

namespace Speech2TextApp.Droid
{
    [Activity(Label = "Speech2TextApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private LinearLayout dataLayout;
        private IData dataService;
        private Button nonVisitedButton;
        private Button visitedButton;
        public List<ApplyResult> datas { get; set; }
        public List<ApplyResult> datasInStatus { get; set; }
        public static ApplyResult dataCurrent { get; set; }
        TextView dataCount;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            var path = GetExternalFilesDir(null).AbsolutePath; 
            dataService = new DataService();
            datas = dataService.GetDatas(path);
            
            if (datas.Count == 0) {
                dataService.GenData(path);
                datas = dataService.GetDatas(path);
            }
    
            nonVisitedButton = FindViewById<Button>(Resource.Id.visit_status_n);
            visitedButton = FindViewById<Button>(Resource.Id.visit_status_Y);
            dataCount = FindViewById<TextView>(Resource.Id.data_count);
            dataLayout = FindViewById<LinearLayout>(Resource.Id.data_layout);

            nonVisitedButton.Click += GetVisitData;
            visitedButton.Click += GetVisitData;

            nonVisitedButton.PerformClick();
        }

        private void GetVisitData(object sender, EventArgs e)
        {
            var rb = (Button)sender;
            var status = (rb.Id == Resource.Id.visit_status_Y) ? "Y" : "N";

            nonVisitedButton.SetBackgroundResource(Resource.Drawable.blue_button);
            visitedButton.SetBackgroundResource(Resource.Drawable.blue_button);
            if (status == "Y")
            {
                visitedButton.SetBackgroundResource(Resource.Drawable.blue_button_activity);
            }
            else
            {
                nonVisitedButton.SetBackgroundResource(Resource.Drawable.blue_button_activity);
            }

            var countDesc = (rb.Id == Resource.Id.visit_status_Y) ? "送出資料" : "訪視資料";
            datasInStatus = datas.Where(x => x.Status == status).ToList();
            dataCount.Text = $"共 {datasInStatus.Count().ToString()} 筆 {countDesc}";
            dataLayout.RemoveAllViews();
            foreach (var data in datasInStatus)
            {
                var layoutParameter = new LinearLayout.LayoutParams(Android.Views.ViewGroup.LayoutParams.MatchParent,
                    Android.Views.ViewGroup.LayoutParams.WrapContent, 1.0f);
                layoutParameter.SetMargins(20,5,0,0);
                // first layout
                var firstLayout = new LinearLayout(this)
                {
                    Orientation = Orientation.Horizontal,
                    WeightSum = 2,
                    LayoutParameters = layoutParameter
                };

                var name = new TextView(this)
                {
                    Text = data.ApplyName,
                };
                name.SetTextColor(Color.ParseColor("#4A90E2"));
                name.SetTextSize(Android.Util.ComplexUnitType.Sp, 20);
                var textViewTitleParams = new LinearLayout.LayoutParams(250, Android.Views.ViewGroup.LayoutParams.WrapContent);
                name.LayoutParameters = textViewTitleParams;


                var count = new TextView(this)
                {
                    Text = string.Format("探訪次數 : {0}次",data.VisitDetails.Count())
                };
                count.SetTextColor(Color.ParseColor("#4A90E2"));
                count.SetTextSize(Android.Util.ComplexUnitType.Sp, 14);

                firstLayout.AddView(name);
                firstLayout.AddView(count);

                // second layout
                var secondLayout = new LinearLayout(this)
                {
                    Orientation = Orientation.Horizontal,
                    LayoutParameters = layoutParameter
                };
             
                var dateTitle = new TextView(this)
                {
                    Text = "探訪時間"
                };
                dateTitle.SetTextColor(Color.ParseColor("#4A90E2"));
                dateTitle.SetTextSize(Android.Util.ComplexUnitType.Sp, 22);
                dateTitle.LayoutParameters = textViewTitleParams;

                var date = new TextView(this)
                {
                    Text = "2018 年 10月 12日 下午 1:30"
                };
                date.SetTextColor(Color.ParseColor("#4A90E2"));
                date.SetTextSize(Android.Util.ComplexUnitType.Sp, 16);

                secondLayout.AddView(dateTitle);
                secondLayout.AddView(date);

                // main layout
                var mainLayout = new LinearLayout(this)
                {
                    Orientation = Orientation.Vertical,
                    WeightSum = 2,
                    LayoutParameters = new LinearLayout.LayoutParams(Android.Views.ViewGroup.LayoutParams.MatchParent, Android.Views.ViewGroup.LayoutParams.WrapContent)
                };
                mainLayout.AddView(firstLayout);
                mainLayout.AddView(secondLayout);
                mainLayout.Click += delegate
                {
                    var intent = new Intent(this, typeof(Page1Activity));
                    if (data.VisitDetail == null)
                    {
                        data.VisitDetail = new ApplyDetail()
                        {
                            Status = "Y"
                        };
                    }
                    dataCurrent = data;
                   
                    StartActivity(intent);
                };
                mainLayout.SetBackgroundResource(Resource.Drawable.main_bottom_border);
                dataLayout.AddView(mainLayout);
            }
        }
    }
}