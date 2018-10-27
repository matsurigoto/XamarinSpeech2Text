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
using Android.Views;
using Android.Graphics.Drawables;
using Android.Support.V4.Graphics.Drawable;
using Android.Util;

namespace Speech2TextApp.Droid
{
    [Activity(Label = "Speech2TextApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : BaseActivity
    {
        private LinearLayout dataLayout;
        private IData dataService;
        private Button nonVisitedButton;
        private Button visitedButton;
        public List<ApplyResult> datas { get; set; }
        public List<ApplyResult> datasInStatus { get; set; }
        public static ApplyResult dataCurrent { get; set; }
        TextView dataCount;

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater inflater = this.MenuInflater;
            inflater.Inflate(Resource.Menu.main_menu, menu);
            return true;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            var path = GetExternalFilesDir(null).AbsolutePath;
            dataService = new DataService();
            datas = dataService.GetDatas(path);

            if (datas.Count == 0)
            {
                dataService.GenData(path);
                datas = dataService.GetDatas(path);
            }
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
           
            
            toolbar.Title = "台北市社會輔助訪視調查表";
            toolbar.InflateMenu(Resource.Menu.main_menu);

            toolbar.SetNavigationIcon(Resource.Mipmap.baseline_keyboard_arrow_left_black_18dp);
            SetActionBar(toolbar);
            toolbar.MenuItemClick += (sender, e2) => {
                switch (e2.Item.TitleFormatted.ToString())
                {
                    case "Import":
                        dataService = new DataService();
                        datas = dataService.GetDatas(path);

                        if (datas.Count == 0)
                        {
                            dataService.GenData(path);
                            datas = dataService.GetDatas(path);
                        }
                        break;
                    case "Export":
                        string filePath = System.IO.Path.Combine(path, "doswData.csv");
                        var csv = new StringBuilder();
                        var titleLine = @"申請人,受訪者,申請者與受訪者關係,連絡電話,訪視地點,訪視時間,訪視概述,申請人是否實際居住本市,住宅狀況,申請項目,申請低收入主要原因,有無人口之外其他共同居住之人口";
                        csv.AppendLine(titleLine);
                        foreach (var v in datasInStatus)
                        {
                            string address = v.AddressType;
                            if (v.AddressType == "戶籍地址")
                            {
                                address += " " + v.Address1;
                            }
                            else if (v.AddressType == "居住地址")
                            {
                                address += " " + v.Address2;
                            }
                            else
                            {
                                address += " " + v.Address3;
                            }
                            if (v.VisitDetail == null)
                            {
                                v.VisitDetail = new ApplyDetail()
                                {
                                    ApplyType = new List<string>(),
                                    VisitDesc = string.Empty,
                                    LiveCityStatus = string.Empty,
                                    LiveStatus = string.Empty,
                                    ApplyReason = string.Empty,
                                    OtherDesc = string.Empty,
                                    OtherPeople = string.Empty
                                };
                            }
                            string date = string.Empty;
                            if (v.VisitDetail.VisitDate != null)
                            {
                                date = v.VisitDetail.VisitDate.GetValueOrDefault().ToString("yyyy/MM/dd HH:mm:ss");
                            }
                            try
                            {
                                var newLine = $"{v.ApplyName},{v.VisitName},{v.Relatoinship},{v.Phone},{address},{date},{v.VisitDetail.VisitDesc ?? string.Empty},{v.VisitDetail.LiveCityStatus ?? string.Empty},{v.VisitDetail.LiveStatus ?? string.Empty},{string.Join(";", v.VisitDetail.ApplyType)},{v.VisitDetail.ApplyReason ?? string.Empty},{v.VisitDetail.OtherPeople ?? string.Empty},{v.VisitDetail.OtherDesc ?? string.Empty}";
                                csv.AppendLine(newLine);
                            }
                            catch (Exception e) { }
                        }
                        try
                        {
                            File.WriteAllText(filePath, csv.ToString());
                            Toast.MakeText(this, "匯出完畢", ToastLength.Long).Show();
                        }
                        catch (Exception e)
                        {
                            Toast.MakeText(this, "匯出失敗", ToastLength.Long).Show();
                        }
                        break;
                }
            };

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
            nonVisitedButton.SetTextColor(TextColor);
            visitedButton.SetBackgroundResource(Resource.Drawable.blue_button);
            visitedButton.SetTextColor(TextColor);
            if (status == "Y")
            {
                visitedButton.SetBackgroundResource(Resource.Drawable.blue_button_activity);
                visitedButton.SetTextColor(Color.White);
            }
            else
            {
                nonVisitedButton.SetBackgroundResource(Resource.Drawable.blue_button_activity);
                nonVisitedButton.SetTextColor(Color.White);
            }

            var countDesc = (rb.Id == Resource.Id.visit_status_Y) ? "送出資料" : "訪視資料";
            datasInStatus = datas.Where(x => x.Status == status).ToList();
            dataCount.Text = $"共 {datasInStatus.Count().ToString()} 筆 {countDesc}";
            dataLayout.RemoveAllViews();
            foreach (var data in datasInStatus)
            {
                var layoutParameter = new LinearLayout.LayoutParams(Android.Views.ViewGroup.LayoutParams.MatchParent,
                    Android.Views.ViewGroup.LayoutParams.MatchParent, 1.0f);
                layoutParameter.SetMargins(20,5,0,0);

                if (data.VisitDetail == null)
                {
                    data.VisitDetail = new ApplyDetail()
                    {
                        Status = "Y"
                    };
                }
                if (data.Message == null) {
                    data.Message = new List<string>();
                }



                // first layout
                var firstLayout = new LinearLayout(this)
                {
                    Orientation = Orientation.Horizontal,
                    LayoutParameters = layoutParameter
                };

                var name = new TextView(this)
                {
                    Text = data.ApplyName,
                };
                name.SetTextColor(TextColor);
                name.SetTextSize(Android.Util.ComplexUnitType.Sp, dataSize);
                var textViewTitleParams = new LinearLayout.LayoutParams(250, Android.Views.ViewGroup.LayoutParams.WrapContent);
                name.LayoutParameters = textViewTitleParams;


                var count = new TextView(this)
                {
                    Text = string.Format("探訪次數 : {0}次",data.VisitDetails.Count())
                };
                count.SetTextColor(TextColor);
                count.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);

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
                    Text = "最後訪視"
                };
                dateTitle.SetTextColor(TextColor);
                dateTitle.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
                dateTitle.LayoutParameters = textViewTitleParams;
                if (data.VisitDetail == null)
                {
                    data.VisitDetail = new ApplyDetail()
                    {
                        Status = "Y"
                    };
                }
                var lastVisit = data.VisitDetails.OrderByDescending(x => x.VisitDate).FirstOrDefault();
                var date = new TextView(this);
                date.Text = "-";
                if (lastVisit != null && lastVisit.VisitDate != null)
                {
                    date.Text = lastVisit.VisitDate.Value.ToString("yyyy/MM/dd HH:mm:ss");
                }
                date.SetTextColor(TextColor);
                date.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);

                secondLayout.AddView(dateTitle);
                secondLayout.AddView(date);

                var layP = new LinearLayout.LayoutParams(Android.Views.ViewGroup.LayoutParams.MatchParent, Android.Views.ViewGroup.LayoutParams.MatchParent);
                layP.SetMargins(20, 0, 20, 50);
                // main layout
                var mainLayout = new LinearLayout(this)
                {
                    Orientation = Orientation.Vertical,

                    LayoutParameters = layP
                };
                //mainLayout.SetPadding(20, 20, 20, 20);
                
                mainLayout.AddView(firstLayout);
                if (status == "N")
                {

                    secondLayout.SetBackgroundResource(Resource.Drawable.main_bottom_border);
                }
                mainLayout.AddView(secondLayout);
                mainLayout.Click += delegate

                {

                    data.IsLast = false;  
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
                

                if (status == "N")
                {
                    layoutParameter.Gravity = GravityFlags.Center;
                    layoutParameter.SetMargins(layoutParameter.LeftMargin, 30, layoutParameter.RightMargin, 30);
                    var record = new LinearLayout(this)
                    {
                        Orientation = Orientation.Horizontal,
                        LayoutParameters = layoutParameter
                    };
                    

                    ImageView imageView = new ImageView(this);
                    imageView.SetColorFilter(TextColor);
                    imageView.SetImageResource(Resource.Drawable.baseline_event_note_24);
                    record.AddView(imageView);

                    
                    TextView tv = new TextView(this);
                    tv.SetTextColor(TextColor);
                    tv.Text = "語音備忘錄";
                    record.AddView(tv);
                   
                    if (data.Message.Count() > 0) {
                        TextView ig = new TextView(this);
                        ig.SetTextColor(Color.White);
                        ig.LayoutParameters = layoutParameter;
                        ig.SetBackgroundResource(Resource.Drawable.circle_activity);
                        ig.Text = data.Message.Count().ToString();
                        ig.Gravity = GravityFlags.Center;
                        record.AddView(ig);
                    }
                    record.Click += delegate {
                        
                        dataCurrent = data;
                        var intent = new Intent(this, typeof(MessageActivity));
                        StartActivity(intent);
                       
                    };
                    mainLayout.AddView(record);
                    //preDataLayout.AddView(record);
                }
                mainLayout.SetBackgroundResource(Resource.Drawable.main_border);
                mainLayout.SetPadding(30, 50, 30, 50);
                dataLayout.AddView(mainLayout);
            }
        }

        private int ConvertDp2Px(int dp)
        {
            int pixel = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, Resources.DisplayMetrics);
            return pixel;
        }

      
    }
}