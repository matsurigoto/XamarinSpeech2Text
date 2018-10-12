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
using Speech2TextApp.Droid.Pages;

namespace Speech2TextApp.Droid
{
    [Activity(Label = "Speech2TextApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private LinearLayout dataLayout;
        private IData dataService;
        public List<ApplyResult> datas { get; set; }
        public List<ApplyResult> datasInStatus { get; set; }
        public static ApplyResult dataCurrent { get; set; }
        Button visitStatusN;
        Button visitStatusY;
        TextView dataCount;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
            string path = this.GetExternalFilesDir(null).AbsolutePath; 
            dataService = new DataService();
            datas = dataService.GetDatas(path);
            
            if (datas.Count == 0) {
                dataService.GenData(path);
                this.datas = dataService.GetDatas(path);
            }

           

            visitStatusN = FindViewById<Button>(Resource.Id.visit_status_n);
            visitStatusY = FindViewById<Button>(Resource.Id.visit_status_Y);
            dataCount = FindViewById<TextView>(Resource.Id.data_count);
            dataLayout = FindViewById<LinearLayout>(Resource.Id.data_layout);


            visitStatusN.Click += LoadVisitStatusClick;
            visitStatusY.Click += LoadVisitStatusClick;
        }


        private void LoadVisitStatusClick(object sender, EventArgs e)
        {
            var rb = (Button)sender;
            var status = (rb.Id == Resource.Id.visit_status_Y) ? "Y" : "N";
            var countDesc = (rb.Id == Resource.Id.visit_status_Y) ? "送出資料" : "訪視資料";
            datasInStatus = datas.Where(x => x.Status == status).ToList();
            dataCount.Text = $"共 {datasInStatus.Count().ToString()} 筆 {countDesc}";
            dataLayout.RemoveAllViews();
            foreach (var data in datasInStatus)
            {
                var layout = new LinearLayout(this);
                var name = new TextView(this) {Text = data.ApplyName};
                layout.AddView(name);
                layout.Click += delegate
                {
                    var intent = new Intent(this, typeof(Page1Activity));
                    dataCurrent = data;
                    this.StartActivity(intent);
                };
                dataLayout.AddView(layout);
            }


        }
        private void LinearLayout1_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}