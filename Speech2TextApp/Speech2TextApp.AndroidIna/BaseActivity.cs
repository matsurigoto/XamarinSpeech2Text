using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Speech2TextApp.Service;

namespace Speech2TextApp.AndroidIna
{
    /// <summary>
    /// 共通頁面
    /// </summary>
    public abstract class BaseActivity : AppCompatActivity
    {
        public Android.Graphics.Color TextColor = Android.Graphics.Color.ParseColor("#4A90E2");
        public Android.Graphics.Color BlackColor = Android.Graphics.Color.ParseColor("#000000");
        public Android.Graphics.Color WhiteColor = Android.Graphics.Color.ParseColor("#FFFFFF");
        public float titleSize = 14f;
        public float dataSize = 16f;

        /// <summary>
        /// 能否編輯註記
        /// </summary>
        public bool IsEdit { get; set; }

        /// <summary>
        /// 語音備忘錄起始按鈕
        /// </summary>
        protected void InitRecordButton() {
            var button = FindViewById<FloatingActionButton>(Resource.Id.fab);
            button.Click += delegate {
                var intent = new Intent(this, typeof(MessageActivity));
                StartActivity(intent);
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.bar, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId) {
                case Resource.Id.action_record:
                    StartActivity(typeof(HistoryActivity));
                    break;
                case Resource.Id.action_delete:
                    StartActivity(typeof(VisitListActivity));
                    break;
                default:
                    OnBackPressed();
                    break;
            }
            return true;
        }

        /// <summary>
        /// 設定資料
        /// </summary>
        /// <param name="title">標題</param>
        /// <param name="data">資料</param>
        /// <param name="layout">設計</param>
        private void SetData(string title, string data, LinearLayout layout) {
           var layoutParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
               ViewGroup.LayoutParams.WrapContent, 1.0f);
            layoutParameter.SetMargins(0, 0, 0, 8);
            LinearLayout h = new LinearLayout(this) {
                Orientation = Orientation.Horizontal,
                LayoutParameters = layoutParameter
            };
            h.Orientation = Orientation.Horizontal;

            var visitedCount = new TextView(this) { Text = title };
            visitedCount.TextAlignment = TextAlignment.Center;
            visitedCount.SetTextColor(WhiteColor);
            visitedCount.SetHeight(100);
            visitedCount.SetWidth(500);
            visitedCount.SetBackgroundColor(TextColor);
            visitedCount.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            h.AddView(visitedCount);

            var dataInput = new TextView(this) { Text = data };
            dataInput.TextAlignment = TextAlignment.Center;
            dataInput.SetTextColor(TextColor);
            dataInput.SetTextSize(Android.Util.ComplexUnitType.Sp, dataSize);
            h.AddView(dataInput);

            layout.AddView(h);
        }

        /// <summary>
        /// 第一頁:基本資料
        /// </summary>
        /// <returns></returns>
        protected LinearLayout GetFirstPage()
        {
            var layoutParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent, 1.0f);
            //layoutParameter.SetMargins(100, 0, 100, 100);
            var layout = new LinearLayout(this)
            {
                Orientation = Orientation.Vertical,
                WeightSum = 2,
                LayoutParameters = layoutParameter
            };

            var layoutTitle = new LinearLayout(this)
            {
                Orientation = Orientation.Horizontal
            };
            var title = new TextView(this) { Text = "1" };
            title.SetTextColor(TextColor);
            title.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            layoutTitle.AddView(title);
            
            if (IsEdit) {
                var button = new TextView(this) { Text = "編輯" };
                button.SetTextColor(TextColor);
                LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                lp.Gravity = GravityFlags.Right;
                button.LayoutParameters = lp;
                button.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
               
                button.Click += delegate {
                    var intent = new Intent(this, typeof(Page1Activity));
                    StartActivity(intent);
                };
                layoutTitle.AddView(button);
            }
            layout.AddView(layoutTitle);

            var line = new View(this);
            line.SetBackgroundColor(TextColor);
            var lineParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 1);
            line.LayoutParameters = lineParameter;
            layout.AddView(line);
            SetData("訪視次數", DataService.dataCurrent.VisitDetails.Count.ToString(), layout);
            SetData("申請人", DataService.dataCurrent.ApplyName, layout);
            SetData("受訪人", DataService.dataCurrent.VisitName, layout);
            SetData("申請人與受訪人關係", DataService.dataCurrent.Relatoinship, layout);
            SetData("連絡電話", DataService.dataCurrent.Phone, layout);
            SetData("訪視地址", DataService.dataCurrent.Address1, layout);
            return layout;
        }

        /// <summary>
        /// 第二頁:申請資料
        /// </summary>
        /// <returns></returns>
        protected LinearLayout GetSecondPage()
        {
            var layoutParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent, 1.0f);
            //layoutParameter.SetMargins(100, 0, 100, 100);
            var layout = new LinearLayout(this)
            {
                Orientation = Orientation.Vertical,
                WeightSum = 2,
                LayoutParameters = layoutParameter
            };


            var layoutTitle = new LinearLayout(this)
            {
                Orientation = Orientation.Horizontal
            };
            var title = new TextView(this) { Text = "2" };
            title.SetTextColor(TextColor);
            title.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            layoutTitle.AddView(title);

            if (IsEdit)
            {
                var button = new TextView(this) { Text = "編輯" };
                button.SetTextColor(TextColor);
                LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                lp.Gravity = GravityFlags.Right;
                button.LayoutParameters = lp;
                button.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);

                button.Click += delegate {
                    var intent = new Intent(this, typeof(Page2Activity));
                    StartActivity(intent);
                };
                layoutTitle.AddView(button);
            }
            layout.AddView(layoutTitle);


            var line = new View(this);
            line.SetBackgroundColor(TextColor);
            var lineParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 1);
            line.LayoutParameters = lineParameter;
            layout.AddView(line);

            SetData("住宅狀況", DataService.dataCurrent.VisitDetail.LiveStatus, layout);
            SetData("申請人是否實際居住本市", DataService.dataCurrent.VisitDetail.LiveStatus == "Y" ? "是" : "否", layout);
            SetData("申請項目", string.Join(",", DataService.dataCurrent.VisitDetail.ApplyType), layout);
            SetData("申請低收入主要原因", DataService.dataCurrent.VisitDetail.ApplyReason, layout);
            return layout;
        }

        /// <summary>
        /// 第三頁:家戶人口資料
        /// </summary>
        /// <returns></returns>
        protected LinearLayout GetThirdPage()
        {
            var layoutParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent, 1.0f);
            //layoutParameter.SetMargins(100, 0, 100, 100);
            var layout = new LinearLayout(this)
            {
                Orientation = Orientation.Vertical,
                WeightSum = 2,
                LayoutParameters = layoutParameter
            };


            var layoutTitle = new LinearLayout(this)
            {
                Orientation = Orientation.Horizontal
            };
            var title = new TextView(this) { Text = "3" };
            title.SetTextColor(TextColor);
            title.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            layoutTitle.AddView(title);

            if (IsEdit)
            {
                var button = new TextView(this) { Text = "編輯" };
                LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                lp.Gravity = GravityFlags.Right;
                button.LayoutParameters = lp;
                button.SetTextColor(TextColor);
                button.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);

                button.Click += delegate {
                    var intent = new Intent(this, typeof(Page3Activity));
                    StartActivity(intent);
                };
                layoutTitle.AddView(button);
            }
            layout.AddView(layoutTitle);

            var line = new View(this);
            line.SetBackgroundColor(TextColor);
            var lineParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 1);
            line.LayoutParameters = lineParameter;
            layout.AddView(line);

            foreach (var item in DataService.dataCurrent.VisitDetail.Members)
            {
                var borderLayout = new LinearLayout(this)
                {
                    Orientation = Orientation.Vertical,
                    WeightSum = 2,
                    LayoutParameters = new LinearLayout.LayoutParams(Android.Views.ViewGroup.LayoutParams.MatchParent, Android.Views.ViewGroup.LayoutParams.WrapContent)
                };
                var borderLayoutParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.WrapContent, 1.0f);
                //layoutParameter.SetMargins(100, 0, 100, 0);
                borderLayout.SetBackgroundResource(Resource.Drawable.main_border);

                var name = new TextView(this) { Text = item.Name };
                name.SetTextColor(TextColor);
                name.SetTextSize(Android.Util.ComplexUnitType.Sp, dataSize);
                borderLayout.AddView(name);

                SetData("稱    謂", item.Title, borderLayout);
                SetData("是否同住", item.LiveTogether, borderLayout);
                SetData("健康狀況", item.HealthStatus, borderLayout);
                SetData("就業狀況", item.WorkStatus, borderLayout);
                SetData("是否安置療養院所", item.IsInNursingHome, borderLayout);
                layout.AddView(borderLayout);
            }

            return layout;

        }

        /// <summary>
        /// 第四頁:備註資料
        /// </summary>
        /// <returns></returns>
        protected LinearLayout GetForthPage()
        {
            var layoutParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent, 1.0f);
            //layoutParameter.SetMargins(100, 0, 100, 100);
            var layout = new LinearLayout(this)
            {
                Orientation = Orientation.Vertical,
                WeightSum = 2,
                LayoutParameters = layoutParameter
            };


            var layoutTitle = new LinearLayout(this)
            {
                Orientation = Orientation.Horizontal
            };
            var title = new TextView(this) { Text = "4" };
            title.SetTextColor(TextColor);
           
            title.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            layoutTitle.AddView(title);

            if (IsEdit)
            {
                var button = new TextView(this) { Text = "編輯" };
                LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                lp.Gravity = GravityFlags.Right;
                button.LayoutParameters = lp;
                button.SetTextColor(TextColor);
                button.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);

                button.Click += delegate {
                    var intent = new Intent(this, typeof(Page4Activity));
                    StartActivity(intent);
                };
                layoutTitle.AddView(button);
            }
            layout.AddView(layoutTitle);

            var line = new View(this);
            line.SetBackgroundColor(TextColor);
            var lineParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 1);
            line.LayoutParameters = lineParameter;
            layout.AddView(line);

            SetData("有無人口之外其他共同居住人口", DataService.dataCurrent.VisitDetail.OtherPeople, layout);
            SetData("其他家戶概述", DataService.dataCurrent.VisitDetail.OtherDesc, layout);
            
            return layout;
        }
    }
}