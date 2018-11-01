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
    public abstract class BaseActivity : AppCompatActivity
    {
        public Android.Graphics.Color TextColor = Android.Graphics.Color.ParseColor("#4A90E2");
        public float titleSize = 14f;
        public float dataSize = 16f;

        public bool IsEdit { get; set; }

        protected void InitRecordButton() {
            var button = FindViewById<FloatingActionButton>(Resource.Id.fab);
            button.Click += delegate {
                var intent = new Intent(this, typeof(MessageActivity));
                StartActivity(intent);
            };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.bar, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        

        public override bool OnContextItemSelected(IMenuItem item)
        {
            switch (item.ItemId) {
                case Resource.Id.action_record:
                    break;
                case Resource.Id.action_delete:
                    break;
                default:
                    OnBackPressed();
                    break;
            }
            return true;
        }

        protected LinearLayout GetFirstPage()
        {
            var layoutParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent, 1.0f);
            layoutParameter.SetMargins(100, 0, 100, 100);
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

            var visitedCount = new TextView(this) { Text = $"訪視次數  {DataService.dataCurrent.VisitDetails.Count}" };
            visitedCount.SetTextColor(TextColor);
            visitedCount.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            layout.AddView(visitedCount);

            var applyName = new TextView(this) { Text = $"申請人  {DataService.dataCurrent.ApplyName}" };
            applyName.SetTextColor(TextColor);
            applyName.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            layout.AddView(applyName);

            var visitName = new TextView(this) { Text = $"受訪人  {DataService.dataCurrent.VisitName}" };
            visitName.SetTextColor(TextColor);
            visitName.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            layout.AddView(visitName);

            var relatoinship = new TextView(this) { Text = $"申請人與受訪人關係  {DataService.dataCurrent.Relatoinship}" };
            relatoinship.SetTextColor(TextColor);
            relatoinship.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            layout.AddView(relatoinship);

            var phone = new TextView(this) { Text = $"連絡電話  {DataService.dataCurrent.Phone}" };
            phone.SetTextColor(TextColor);
            phone.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            layout.AddView(phone);

            var addressTitle = new TextView(this) { Text = "訪視地址" };
            addressTitle.SetTextColor(TextColor);
            addressTitle.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            layout.AddView(addressTitle);
            var addressType = new TextView(this) { Text = $"{DataService.dataCurrent.AddressType}" };
            addressType.SetTextColor(TextColor);
            addressType.SetTextSize(Android.Util.ComplexUnitType.Sp, dataSize);
            layout.AddView(addressType);
            var address = new TextView(this) { Text = $"{DataService.dataCurrent.Address1}{DataService.dataCurrent.Address2}{DataService.dataCurrent.Address3}" };
            address.SetTextColor(TextColor);
            address.SetTextSize(Android.Util.ComplexUnitType.Sp, dataSize);
            layout.AddView(address);

            var visitTimeTitle = new TextView(this) { Text = "訪視時間" };
            visitTimeTitle.SetTextColor(TextColor);
            layout.AddView(visitTimeTitle);
            //var visitTime = new TextView(this) {   };
            //visitTime.SetTextColor(TextColor);
            //layout.AddView(visitTime);

            return layout;
        }

        protected LinearLayout GetSecondPage()
        {
            var layoutParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent, 1.0f);
            layoutParameter.SetMargins(100, 0, 100, 100);
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

            var liveStatus = new TextView(this) { Text = $"✔ {DataService.dataCurrent.VisitDetail.LiveStatus}" };
            liveStatus.SetTextColor(TextColor);
            liveStatus.SetTextSize(Android.Util.ComplexUnitType.Sp, dataSize);
            layout.AddView(liveStatus);

            var liveCityStatusTitle = new TextView(this) { Text = "申請人是否實際居住本市" };
            liveCityStatusTitle.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            liveCityStatusTitle.SetTextColor(TextColor);
            layout.AddView(liveCityStatusTitle);

            var liveCityStatusText = DataService.dataCurrent.VisitDetail.LiveStatus == "Y" ? "是" : "否";
            var liveCityStatus = new TextView(this) { Text = $"✔ {liveCityStatusText}" };
            liveCityStatus.SetTextSize(Android.Util.ComplexUnitType.Sp, dataSize);
            liveCityStatus.SetTextColor(TextColor);
            layout.AddView(liveCityStatus);

            var applyTypeTitle = new TextView(this) { Text = "申請項目" };
            applyTypeTitle.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            applyTypeTitle.SetTextColor(TextColor);
            layout.AddView(applyTypeTitle);
            foreach (var item in DataService.dataCurrent.VisitDetail.ApplyType)
            {
                var applyType = new TextView(this) { Text = $"✔ {item}" };
                applyType.SetTextSize(Android.Util.ComplexUnitType.Sp, dataSize);
                applyType.SetTextColor(TextColor);
                layout.AddView(applyType);
            }

            var applyReasonTitle = new TextView(this) { Text = "申請低收入主要原因" };
            applyReasonTitle.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            applyReasonTitle.SetTextColor(TextColor);
            layout.AddView(applyReasonTitle);

            var applyReason = new TextView(this) { Text = $"✔ {DataService.dataCurrent.VisitDetail.ApplyReason}" };
            applyReason.SetTextSize(Android.Util.ComplexUnitType.Sp, dataSize);
            applyReason.SetTextColor(TextColor);
            layout.AddView(applyReason);
            return layout;
        }

        protected LinearLayout GetThirdPage()
        {
            var layoutParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent, 1.0f);
            layoutParameter.SetMargins(100, 0, 100, 100);
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
                layoutParameter.SetMargins(100, 0, 100, 0);
                borderLayout.SetBackgroundResource(Resource.Drawable.main_border);

                var name = new TextView(this) { Text = item.Name };
                name.SetTextColor(TextColor);
                name.SetTextSize(Android.Util.ComplexUnitType.Sp, dataSize);
                borderLayout.AddView(name);

                var memberTitle = new TextView(this) { Text = $"稱    謂   {item.Title}" };
                memberTitle.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
                memberTitle.SetTextColor(TextColor);
                borderLayout.AddView(memberTitle);

                var liveTogether = new TextView(this) { Text = $"是否同住   {item.LiveTogether}" };
                liveTogether.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
                liveTogether.SetTextColor(TextColor);
                borderLayout.AddView(liveTogether);

                var healthStatus = new TextView(this) { Text = $"健康狀況   {item.HealthStatus}" };
                healthStatus.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
                healthStatus.SetTextColor(TextColor);
                borderLayout.AddView(healthStatus);

                var workStatus = new TextView(this) { Text = $"就業狀況   {item.WorkStatus}" };
                workStatus.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
                workStatus.SetTextColor(TextColor);
                borderLayout.AddView(workStatus);

                var isInNursingHome = new TextView(this) { Text = $"是否安置療養院所 {item.IsInNursingHome}" };
                isInNursingHome.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
                isInNursingHome.SetTextColor(TextColor);
                borderLayout.AddView(isInNursingHome);

                layout.AddView(borderLayout);
            }

            return layout;

        }

        protected LinearLayout GetForthPage()
        {
            var layoutParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent, 1.0f);
            layoutParameter.SetMargins(100, 0, 100, 100);
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

            var otherPeopleTitle = new TextView(this) { Text = "有無人口之外其他共同居住人口" };
            otherPeopleTitle.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            otherPeopleTitle.SetTextColor(TextColor);
            layout.AddView(otherPeopleTitle);

            var otherPeopleText = DataService.dataCurrent.VisitDetail.OtherPeople;
            var liveCityStatus = new TextView(this) { Text = $"✔ {otherPeopleText}" };
            liveCityStatus.SetTextSize(Android.Util.ComplexUnitType.Sp, dataSize);
            liveCityStatus.SetTextColor(TextColor);
            layout.AddView(liveCityStatus);

            var otherDescTitle = new TextView(this) { Text = "其他家戶概述" };
            otherDescTitle.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            otherDescTitle.SetTextColor(TextColor);
            layout.AddView(otherDescTitle);

            var otherDescText = DataService.dataCurrent.VisitDetail.OtherDesc;
            var otherDesc = new TextView(this) { Text = $"{otherDescText}" };
            otherDesc.SetTextSize(Android.Util.ComplexUnitType.Sp, dataSize);
            otherDesc.SetTextColor(TextColor);
            layout.AddView(otherDesc);
            return layout;
        }
    }
}