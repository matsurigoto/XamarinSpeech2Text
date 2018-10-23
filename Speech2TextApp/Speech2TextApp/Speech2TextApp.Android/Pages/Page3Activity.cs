using System;
using Android.OS;
using Android.App;
using Android.Widget;
using Android.Graphics;

namespace Speech2TextApp.Droid.Pages
{
    [Activity(Label = "Page3Activity")]
    public class Page3Activity : BaseActivity
    {
        TextView memberCount;
        LinearLayout members;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Page3Activity);
            this.Title = "訪視紀錄";
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            memberCount = FindViewById<TextView>(Resource.Id.member_count);
            members = FindViewById<LinearLayout>(Resource.Id.members);
            if (MainActivity.dataCurrent.VisitDetail.Members == null) {
                MainActivity.dataCurrent.VisitDetail.Members = new System.Collections.Generic.List<Data.MemberDetail>();
            }
            memberCount.Text = string.Format("共 {0} 筆  列表人口",MainActivity.dataCurrent.VisitDetail.Members.Count);

            InitMembers();

            var addMember = FindViewById<TextView>(Resource.Id.addMember);
            addMember.Click += AddMemberEvent;

            var next = FindViewById<Button>(Resource.Id.btn_page_3_next);
            next.Click += NextButtonEvent;
        }

        private void InitMembers() {
            members.RemoveAllViews();
            foreach (var member in MainActivity.dataCurrent.VisitDetail.Members) {
                var layoutParameter = new LinearLayout.LayoutParams(Android.Views.ViewGroup.LayoutParams.MatchParent,
                  Android.Views.ViewGroup.LayoutParams.WrapContent, 1.0f);
                layoutParameter.SetMargins(20, 5, 0, 0);
                // first layout
                var firstLayout = new LinearLayout(this)
                {
                    Orientation = Orientation.Vertical,
                    WeightSum = 2,
                    LayoutParameters = layoutParameter
                };

                var name = new TextView(this)
                {
                    Text = member.Name,
                };
                name.SetTextColor(Color.ParseColor("#4A90E2"));
                name.SetTextSize(Android.Util.ComplexUnitType.Sp, 20);
                var textViewTitleParams = new LinearLayout.LayoutParams(250, Android.Views.ViewGroup.LayoutParams.WrapContent);
                name.LayoutParameters = textViewTitleParams;
                firstLayout.AddView(name);


                var title = new TextView(this)
                {
                    Text = string.Format("稱    謂 {0}", member.Title)
                };
                title.SetTextColor(Color.ParseColor("#4A90E2"));
                title.SetTextSize(Android.Util.ComplexUnitType.Sp, 14);
                firstLayout.AddView(title);

                var liveTogether = new TextView(this)
                {
                    Text = string.Format("是否同住 {0}", member.LiveTogether)
                };
                liveTogether.SetTextColor(Color.ParseColor("#4A90E2"));
                liveTogether.SetTextSize(Android.Util.ComplexUnitType.Sp, 14);
                firstLayout.AddView(liveTogether);

                var healthStatus = new TextView(this)
                {
                    Text = string.Format("健康狀況 {0}", member.HealthStatus)
                };
                healthStatus.SetTextColor(Color.ParseColor("#4A90E2"));
                healthStatus.SetTextSize(Android.Util.ComplexUnitType.Sp, 14);
                firstLayout.AddView(healthStatus);

                var workStatus = new TextView(this)
                {
                    Text = string.Format("就業狀況 {0}", member.WorkStatus)
                };
                workStatus.SetTextColor(Color.ParseColor("#4A90E2"));
                workStatus.SetTextSize(Android.Util.ComplexUnitType.Sp, 14);
                firstLayout.AddView(workStatus);

                var isInNursingHome = new TextView(this)
                {
                    Text = string.Format("是否安置療養院所 {0}", member.IsInNursingHome)
                };
                isInNursingHome.SetTextColor(Color.ParseColor("#4A90E2"));
                isInNursingHome.SetTextSize(Android.Util.ComplexUnitType.Sp, 14);
                firstLayout.AddView(isInNursingHome);

                members.AddView(firstLayout);
            }
        }

        public void AddMemberEvent(object sender, EventArgs e)
        {
            StartActivity(typeof(Page3AddMemberActivity));
        }

        public void NextButtonEvent(object sender, EventArgs e)
        {
            if (MainActivity.dataCurrent.IsLast)
            {
                StartActivity(typeof(Page5Activity));
            }
            else
            {
                StartActivity(typeof(Page4Activity));
            }
        }
    }
}