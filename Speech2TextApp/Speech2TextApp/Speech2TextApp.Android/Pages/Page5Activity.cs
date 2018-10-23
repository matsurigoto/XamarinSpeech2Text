using Android.OS;
using Android.App;
using Android.Widget;
using System;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using Android.Views;

namespace Speech2TextApp.Droid.Pages
{
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
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            MainActivity.dataCurrent.IsLast = true;
            this.IsEdit = true;
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
            string destPath = Path.Combine(descDocument, MainActivity.dataCurrent.Id);
            File.WriteAllText(destPath, json);
            StartActivity(typeof(MainActivity));
        }

        //private LinearLayout GetFirstPage()
        //{
        //    var layoutParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,                
        //        ViewGroup.LayoutParams.WrapContent, 1.0f);
        //    layoutParameter.SetMargins(20, 5, 0, 0);
        //    var layout = new LinearLayout(this)
        //    {
        //        Orientation = Orientation.Vertical,
        //        WeightSum = 2,
        //        LayoutParameters = layoutParameter
        //    };

        //    var title = new TextView(this) { Text = "1" };
        //    layout.AddView(title);

        //    var line = new View(this);
        //    line.SetBackgroundColor(TextColor);
        //    var lineParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 1);
        //    line.LayoutParameters = lineParameter;
        //    layout.AddView(line);

        //    var visitedCount = new TextView(this) {Text = $"訪視次數  {MainActivity.dataCurrent.VisitCount}"};
        //    visitedCount.SetTextColor(TextColor);
        //    layout.AddView(visitedCount);

        //    var applyName = new TextView(this) { Text = $"申請人  {MainActivity.dataCurrent.ApplyName}"};
        //    applyName.SetTextColor(TextColor);
        //    layout.AddView(applyName);

        //    var visitName = new TextView(this) { Text = $"受訪人  {MainActivity.dataCurrent.VisitName}" };
        //    visitName.SetTextColor(TextColor);
        //    layout.AddView(visitName);

        //    var relatoinship = new TextView(this) {Text = $"申請人與受訪人關係  {MainActivity.dataCurrent.Relatoinship}"};
        //    relatoinship.SetTextColor(TextColor);
        //    layout.AddView(relatoinship);

        //    var phone = new TextView(this) { Text = $"連絡電話  {MainActivity.dataCurrent.Phone}" };
        //    phone.SetTextColor(TextColor);
        //    layout.AddView(phone);

        //    var addressTitle = new TextView(this) { Text = "訪視地址" };
        //    addressTitle.SetTextColor(TextColor);
        //    layout.AddView(addressTitle);
        //    var addressType = new TextView(this) { Text = $"{MainActivity.dataCurrent.AddressType}" };
        //    phone.SetTextColor(TextColor);
        //    layout.AddView(addressType);
        //    var address = new TextView(this) { Text = $"{MainActivity.dataCurrent.Address1}{MainActivity.dataCurrent.Address2}{MainActivity.dataCurrent.Address3}" };
        //    address.SetTextColor(TextColor);
        //    layout.AddView(address);

        //    var visitTimeTitle = new TextView(this) { Text = "訪視時間" };
        //    visitTimeTitle.SetTextColor(TextColor);
        //    layout.AddView(visitTimeTitle);
        //    //var visitTime = new TextView(this) {   };
        //    //visitTime.SetTextColor(TextColor);
        //    //layout.AddView(visitTime);

        //    return layout;
        //}

        //private LinearLayout GetSecondPage()
        //{
        //    var layoutParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
        //        ViewGroup.LayoutParams.WrapContent, 1.0f);
        //    layoutParameter.SetMargins(20, 5, 0, 0);
        //    var layout = new LinearLayout(this)
        //    {
        //        Orientation = Orientation.Vertical,
        //        WeightSum = 2,
        //        LayoutParameters = layoutParameter
        //    };

        //    var title = new TextView(this) { Text = "2" };
        //    layout.AddView(title);

        //    var line = new View(this);
        //    line.SetBackgroundColor(TextColor);
        //    var lineParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 1);
        //    line.LayoutParameters = lineParameter;
        //    layout.AddView(line);

        //    var liveStatus = new TextView(this) { Text = $"✔ {MainActivity.dataCurrent.VisitDetail.LiveStatus}" }; 
        //    liveStatus.SetBackgroundColor(TextColor);
        //    layout.AddView(liveStatus);

        //    var liveCityStatusTitle = new TextView(this) { Text = "申請人是否實際居住本市" };
        //    liveCityStatusTitle.SetBackgroundColor(TextColor);
        //    layout.AddView(liveCityStatusTitle);

        //    var liveCityStatusText = MainActivity.dataCurrent.VisitDetail.LiveStatus == "Y" ? "是" : "否";
        //    var liveCityStatus = new TextView(this) { Text = $"✔ {liveCityStatusText}" };
        //    liveCityStatus.SetBackgroundColor(TextColor);
        //    layout.AddView(liveCityStatus);

        //    var applyTypeTitle = new TextView(this) { Text = "申請項目" };
        //    applyTypeTitle.SetBackgroundColor(TextColor);
        //    layout.AddView(applyTypeTitle);
        //    foreach (var item in MainActivity.dataCurrent.VisitDetail.ApplyType)
        //    {
        //        var applyType = new TextView(this) { Text = $"✔ {item}" };
        //        applyType.SetBackgroundColor(TextColor);
        //        layout.AddView(applyType);
        //    }

        //    var applyReasonTitle = new TextView(this) { Text = "申請低收入主要原因" };
        //    applyReasonTitle.SetBackgroundColor(TextColor);
        //    layout.AddView(applyReasonTitle);

        //    var applyReason = new TextView(this) { Text = $"✔ {MainActivity.dataCurrent.VisitDetail.ApplyReason}"  };
        //    applyReason.SetBackgroundColor(TextColor);
        //    layout.AddView(applyReason);
        //    return layout;
        //}

        //private LinearLayout GetThirdPage()
        //{
        //    var layoutParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
        //        ViewGroup.LayoutParams.WrapContent, 1.0f);
        //    layoutParameter.SetMargins(20, 5, 0, 0);
        //    var layout = new LinearLayout(this)
        //    {
        //        Orientation = Orientation.Vertical,
        //        WeightSum = 2,
        //        LayoutParameters = layoutParameter
        //    };

        //    var title = new TextView(this) { Text = "3" };
        //    layout.AddView(title);

        //    var line = new View(this);
        //    line.SetBackgroundColor(TextColor);
        //    var lineParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 1);
        //    line.LayoutParameters = lineParameter;
        //    layout.AddView(line);

        //    foreach (var item in MainActivity.dataCurrent.VisitDetail.Members)
        //    {
        //        var borderLayoutParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
        //            ViewGroup.LayoutParams.WrapContent, 1.0f);
        //        layoutParameter.SetMargins(20, 5, 0, 0);
        //        var borderLayout = new LinearLayout(this)
        //        {
        //            Orientation = Orientation.Vertical,
        //            WeightSum = 2,
        //            LayoutParameters = borderLayoutParameter
        //        };
        //        borderLayout.SetBackgroundResource(Resource.Drawable.main_bottom_border);

        //        var name = new TextView(this) { Text = item.Name };
        //        name.SetBackgroundColor(TextColor);
        //        borderLayout.AddView(name);

        //        var memberTitle = new TextView(this) { Text = $"稱    謂   {item.Title}" };
        //        memberTitle.SetBackgroundColor(TextColor);
        //        borderLayout.AddView(memberTitle);

        //        var liveTogether = new TextView(this) { Text = $"是否同住   {item.LiveTogether}" };
        //        liveTogether.SetBackgroundColor(TextColor);
        //        borderLayout.AddView(liveTogether);

        //        var healthStatus = new TextView(this) { Text = $"健康狀況   {item.HealthStatus}" };
        //        healthStatus.SetBackgroundColor(TextColor);
        //        borderLayout.AddView(healthStatus);

        //        var workStatus = new TextView(this) { Text = $"就業狀況   {item.WorkStatus}" };
        //        workStatus.SetBackgroundColor(TextColor);
        //        borderLayout.AddView(workStatus);

        //        var isInNursingHome = new TextView(this) { Text = $"是否安置療養院所 {item.IsInNursingHome}" };
        //        isInNursingHome.SetBackgroundColor(TextColor);
        //        borderLayout.AddView(isInNursingHome);

        //        layout.AddView(borderLayout);
        //    }

        //    return layout;

        //}

        //private LinearLayout GetForthPage()
        //{
        //    var layoutParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
        //        ViewGroup.LayoutParams.WrapContent, 1.0f);
        //    layoutParameter.SetMargins(20, 5, 0, 0);
        //    var layout = new LinearLayout(this)
        //    {
        //        Orientation = Orientation.Vertical,
        //        WeightSum = 2,
        //        LayoutParameters = layoutParameter
        //    };

        //    var title = new TextView(this) { Text = "4" };
        //    layout.AddView(title);

        //    var line = new View(this);
        //    line.SetBackgroundColor(TextColor);
        //    var lineParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 1);
        //    line.LayoutParameters = lineParameter;
        //    layout.AddView(line);
        //    return layout;
        //}
    }
}