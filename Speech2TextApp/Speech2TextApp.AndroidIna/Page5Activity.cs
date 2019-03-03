using Android.OS;
using Android.App;
using Android.Widget;
using System;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using Android.Views;
using Speech2TextApp.Service;
using Android.Graphics;

namespace Speech2TextApp.AndroidIna
{
    /// <summary>
    /// 第五頁:送出資料檢視
    /// </summary>
    [Activity(Label = "Page5Activity")]
    public class Page5Activity : BaseActivity
    {
        private  LinearLayout _pageLayout;
        //public Android.Graphics.Color TextColor = Android.Graphics.Color.ParseColor("#4A90E2");


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Page5Activity);
            this.Title = "訪視紀錄";
            this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            if (DataService.dataCurrent.VisitDetails.Count > 0)
            {
                this.Title = $"訪視紀錄({DataService.dataCurrent.VisitDetails.Count})";
            }

            var circle = FindViewById<TextView>(Resource.Id.circle5);
            circle.SetBackgroundResource(Resource.Drawable.circle_activity);
            circle.SetTextColor(Color.White);

            DataService.dataCurrent.IsLast = true;
            this.IsEdit = true;
            var submit = FindViewById<Button>(Resource.Id.btn_page_5_next);
            submit.Click += NextButtonEvent;


            _pageLayout = FindViewById<LinearLayout>(Resource.Id.page_5_layout);
            // put DataService.dataCurrent to linear layout
            _pageLayout.AddView(GetFirstPage());
            _pageLayout.AddView(GetSecondPage());
            _pageLayout.AddView(GetThirdPage());
            _pageLayout.AddView(GetForthPage());
        }

        /// <summary>
        /// 下一頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NextButtonEvent(object sender, EventArgs e)
        {
            DataService.dataCurrent.VisitDetails.Add(DataService.dataCurrent.VisitDetail);
            DataService.dataCurrent.Status = "Y";
            var descDocument = GetExternalFilesDir(null).AbsolutePath;
            string json = JsonConvert.SerializeObject(DataService.dataCurrent);
            string destPath = System.IO.Path.Combine(descDocument, DataService.dataCurrent.Id);
            File.WriteAllText(destPath, json);
            StartActivity(typeof(VisitListActivity));
        }

    }
}