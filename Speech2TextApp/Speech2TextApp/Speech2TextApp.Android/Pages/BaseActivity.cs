using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Speech2TextApp.Droid.Pages
{
    public abstract class BaseActivity : Activity
    {
        public Android.Graphics.Color TextColor = Android.Graphics.Color.ParseColor("#4A90E2");
        public float titleSize = 14f;
        public float dataSize = 16f;

        public bool IsEdit { get; set; }
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

            var visitedCount = new TextView(this) { Text = $"訪視次數  {MainActivity.dataCurrent.VisitDetails.Count}" };
            visitedCount.SetTextColor(TextColor);
            visitedCount.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            layout.AddView(visitedCount);

            var applyName = new TextView(this) { Text = $"申請人  {MainActivity.dataCurrent.ApplyName}" };
            applyName.SetTextColor(TextColor);
            applyName.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            layout.AddView(applyName);

            var visitName = new TextView(this) { Text = $"受訪人  {MainActivity.dataCurrent.VisitName}" };
            visitName.SetTextColor(TextColor);
            visitName.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            layout.AddView(visitName);

            var relatoinship = new TextView(this) { Text = $"申請人與受訪人關係  {MainActivity.dataCurrent.Relatoinship}" };
            relatoinship.SetTextColor(TextColor);
            relatoinship.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            layout.AddView(relatoinship);

            var phone = new TextView(this) { Text = $"連絡電話  {MainActivity.dataCurrent.Phone}" };
            phone.SetTextColor(TextColor);
            phone.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            layout.AddView(phone);

            var addressTitle = new TextView(this) { Text = "訪視地址" };
            addressTitle.SetTextColor(TextColor);
            addressTitle.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            layout.AddView(addressTitle);
            var addressType = new TextView(this) { Text = $"{MainActivity.dataCurrent.AddressType}" };
            addressType.SetTextColor(TextColor);
            addressType.SetTextSize(Android.Util.ComplexUnitType.Sp, dataSize);
            layout.AddView(addressType);
            var address = new TextView(this) { Text = $"{MainActivity.dataCurrent.Address1}{MainActivity.dataCurrent.Address2}{MainActivity.dataCurrent.Address3}" };
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

            var liveStatus = new TextView(this) { Text = $"✔ {MainActivity.dataCurrent.VisitDetail.LiveStatus}" };
            liveStatus.SetTextColor(TextColor);
            liveStatus.SetTextSize(Android.Util.ComplexUnitType.Sp, dataSize);
            layout.AddView(liveStatus);

            var liveCityStatusTitle = new TextView(this) { Text = "申請人是否實際居住本市" };
            liveCityStatusTitle.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            liveCityStatusTitle.SetTextColor(TextColor);
            layout.AddView(liveCityStatusTitle);

            var liveCityStatusText = MainActivity.dataCurrent.VisitDetail.LiveStatus == "Y" ? "是" : "否";
            var liveCityStatus = new TextView(this) { Text = $"✔ {liveCityStatusText}" };
            liveCityStatus.SetTextSize(Android.Util.ComplexUnitType.Sp, dataSize);
            liveCityStatus.SetTextColor(TextColor);
            layout.AddView(liveCityStatus);

            var applyTypeTitle = new TextView(this) { Text = "申請項目" };
            applyTypeTitle.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            applyTypeTitle.SetTextColor(TextColor);
            layout.AddView(applyTypeTitle);
            foreach (var item in MainActivity.dataCurrent.VisitDetail.ApplyType)
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

            var applyReason = new TextView(this) { Text = $"✔ {MainActivity.dataCurrent.VisitDetail.ApplyReason}" };
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

            foreach (var item in MainActivity.dataCurrent.VisitDetail.Members)
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
                borderLayout.SetBackgroundResource(Resource.Drawable.main_bottom_border);

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
                button.SetTextColor(TextColor);
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

            var otherPeopleTitle = new TextView(this) { Text = "有無人口之外其他共同居住人口" };
            otherPeopleTitle.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            otherPeopleTitle.SetTextColor(TextColor);
            layout.AddView(otherPeopleTitle);

            var otherPeopleText = MainActivity.dataCurrent.VisitDetail.OtherPeople;
            var liveCityStatus = new TextView(this) { Text = $"✔ {otherPeopleText}" };
            liveCityStatus.SetTextSize(Android.Util.ComplexUnitType.Sp, dataSize);
            liveCityStatus.SetTextColor(TextColor);
            layout.AddView(liveCityStatus);

            var otherDescTitle = new TextView(this) { Text = "其他家戶概述" };
            otherDescTitle.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
            otherDescTitle.SetTextColor(TextColor);
            layout.AddView(otherDescTitle);

            var otherDescText = MainActivity.dataCurrent.VisitDetail.OtherDesc;
            var otherDesc = new TextView(this) { Text = $"{otherDescText}" };
            otherDesc.SetTextSize(Android.Util.ComplexUnitType.Sp, dataSize);
            otherDesc.SetTextColor(TextColor);
            layout.AddView(otherDesc);
            return layout;
        }
    }
}