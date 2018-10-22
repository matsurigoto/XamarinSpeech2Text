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
using System.Text;
using System.IO;

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

            var exportButton = FindViewById<Button>(Resource.Id.export);
            exportButton.Click += delegate {
                string filePath = System.IO.Path.Combine(path, "doswData.csv");
                var csv = new StringBuilder();
                var titleLine = @"申請人,受訪者,申請者與受訪者關係,連絡電話,訪視地點,訪視時間,訪視概述,申請人是否實際居住本市,住宅狀況,申請項目,申請低收入主要原因,有無人口之外其他共同居住之人口";
                csv.AppendLine(titleLine);
                foreach (var v in datasInStatus) {
                    string address = v.AddressType;
                    if (v.AddressType == "戶籍地址")
                    {
                        address += " "+ v.Address1;
                    }
                    else if (v.AddressType == "居住地址")
                    {
                        address += " " + v.Address2;
                    }
                    else
                    {
                        address += " " + v.Address3;
                    }
                    if (v.VisitDetail == null) {
                        v.VisitDetail = new ApplyDetail() {
                            ApplyType = new List<string>()
                        };
                    }
                    string date = string.Empty;
                    if (v.VisitDetail.VisitDate != null) {
                        date = v.VisitDetail.VisitDate.ToString("yyyy/MM/dd HH:mm:ss");
                    }
                    var newLine = $"{v.ApplyName},{v.VisitName},{v.Relatoinship},{v.Phone},{address},{date},{v.VisitDetail.VisitDesc},{v.VisitDetail.LiveCityStatus},{v.VisitDetail.LiveStatus},{string.Join(";",v.VisitDetail.ApplyType)},{v.VisitDetail.ApplyReason},{v.VisitDetail.OtherPeople},{v.VisitDetail.OtherDesc}";
                    csv.AppendLine(newLine);
                }
                try
                {
                    File.WriteAllText(filePath, csv.ToString());
                    Toast.MakeText(this, "匯出完畢", ToastLength.Long).Show();
                }
                catch (Exception e) {
                    Toast.MakeText(this, "匯出失敗", ToastLength.Long).Show();
                }
            };
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
                   
                    if (data.VisitDetail == null)
                    {
                        data.VisitDetail = new ApplyDetail()
                        {
                            Status = "Y"
                        };
                    }
                    dataCurrent = data;
                    if (status == "N")
                    {
                        var intent = new Intent(this, typeof(Page1Activity));
                        StartActivity(intent);
                    }
                    else if (status == "Y") {
                        var intent = new Intent(this, typeof(DetailActivity));
                        StartActivity(intent);
                    }
                   
                };
                mainLayout.SetBackgroundResource(Resource.Drawable.main_bottom_border);
                dataLayout.AddView(mainLayout);
            }
        }
    }
}