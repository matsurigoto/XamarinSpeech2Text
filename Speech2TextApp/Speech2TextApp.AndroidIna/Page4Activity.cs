using System;
using Android.OS;
using Android.App;
using Android.Widget;
using Speech2TextApp.Service;
using Android.Graphics;

namespace Speech2TextApp.AndroidIna
{
    /// <summary>
    /// 第四頁:備註資料
    /// </summary>
    [Activity(Label = "Page4Activity")]
    public class Page4Activity : BaseActivity
    {
        RadioButton liveTogetherY;
        RadioButton liveTogetherN;

        EditText otherDesc;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Page4Activity);

            this.Title = "訪視紀錄";
            this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            if (DataService.dataCurrent.VisitDetails.Count > 0)
            {
                this.Title = $"訪視紀錄({DataService.dataCurrent.VisitDetails.Count})";
            }

            var circle = FindViewById<TextView>(Resource.Id.circle4);
            circle.SetBackgroundResource(Resource.Drawable.circle_activity);
            circle.SetTextColor(Color.White);

            InitRecordButton();
            liveTogetherY = FindViewById<RadioButton>(Resource.Id.liveTogetherY);
            liveTogetherN = FindViewById<RadioButton>(Resource.Id.liveTogetherN);
            otherDesc = FindViewById<EditText>(Resource.Id.otherDesc);

            InitLiveTogethe(liveTogetherY);
            InitLiveTogethe(liveTogetherY);
            InitOtherDesc();
            var next = FindViewById<Button>(Resource.Id.btn_page_4_next);
            next.Click += NextButtonEvent;

        }

        /// <summary>
        /// 初始化是否同住
        /// </summary>
        /// <param name="rb"></param>
        private void InitLiveTogethe(RadioButton rb) {
            if (rb.Text == DataService.dataCurrent.VisitDetail.OtherPeople) {
                rb.Checked = true;
            }
            rb.Click += delegate
            {
                DataService.dataCurrent.VisitDetail.OtherPeople = rb.Text;
            };
        }

        /// <summary>
        /// 初始化其他備註
        /// </summary>
        private void InitOtherDesc() {
            if (string.IsNullOrEmpty(DataService.dataCurrent.VisitDetail.OtherDesc)) {
                DataService.dataCurrent.VisitDetail.OtherDesc = string.Join(@"。", DataService.dataCurrent.Message);
            }
            otherDesc.Text = DataService.dataCurrent.VisitDetail.OtherDesc;
        }

        /// <summary>
        /// 下一頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NextButtonEvent(object sender, EventArgs e)
        {
            DataService.dataCurrent.VisitDetail.OtherDesc = otherDesc.Text;
            StartActivity(typeof(Page5Activity));
        }
    }
}