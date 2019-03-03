using System;
using Android.OS;
using Android.App;
using Android.Widget;
using Android.Graphics;
using Speech2TextApp.Service;
using Speech2TextApp.AndroidIna.Helper;
using Android.Support.V7.Widget;
using Android.Support.Design.Widget;

namespace Speech2TextApp.AndroidIna
{
    /// <summary>
    /// 第三頁:家戶人口資料
    /// </summary>
    [Activity(Label = "Page3Activity")]
    public class Page3Activity : BaseActivity
    {
        TextView memberCount;

        private MemberAdapter adapter;
        private RecyclerView.LayoutManager layoutManager;
        private RecyclerView recycleView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Page3Activity);
            this.Title = "訪視紀錄";
            this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            if (DataService.dataCurrent.VisitDetails.Count > 0)
            {
                this.Title = $"訪視紀錄({DataService.dataCurrent.VisitDetails.Count})";
            }

            var circle = FindViewById<TextView>(Resource.Id.circle3);
            circle.SetBackgroundResource(Resource.Drawable.circle_activity);
            circle.SetTextColor(Color.White);

            InitRecordButton();
            //ActionBar.SetDisplayHomeAsUpEnabled(true);
            recycleView = FindViewById<RecyclerView>(Resource.Id.members);
            memberCount = FindViewById<TextView>(Resource.Id.member_count);
            if (DataService.dataCurrent.VisitDetail.Members == null) {
                DataService.dataCurrent.VisitDetail.Members = new System.Collections.Generic.List<Data.MemberDetail>();
            }
            memberCount.Text = string.Format("共 {0} 筆  列表人口",DataService.dataCurrent.VisitDetail.Members.Count);

            InitMembers();

            var addMember = FindViewById<FloatingActionButton>(Resource.Id.addMember);
            addMember.Click += AddMemberEvent;

            var next = FindViewById<Button>(Resource.Id.btn_page_3_next);
            next.Click += NextButtonEvent;
        }

        /// <summary>
        /// 帶入家戶人口
        /// </summary>
        private void InitMembers() {
            recycleView.RemoveAllViews();
            recycleView.HasFixedSize = true;
            layoutManager = new LinearLayoutManager(this);
            recycleView.SetLayoutManager(layoutManager);
            adapter = new MemberAdapter(this.ApplicationContext, Resource.Layout.member, DataService.dataCurrent.VisitDetail.Members);
            recycleView.SetAdapter(adapter);
        }

        /// <summary>
        /// 新增家戶人口資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddMemberEvent(object sender, EventArgs e)
        {
            StartActivity(typeof(Page3AddMemberActivity));
        }

        /// <summary>
        /// 下一頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NextButtonEvent(object sender, EventArgs e)
        {
            if (DataService.dataCurrent.IsLast)
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