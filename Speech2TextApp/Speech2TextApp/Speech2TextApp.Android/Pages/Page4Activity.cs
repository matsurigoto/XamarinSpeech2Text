﻿using System;
using Android.OS;
using Android.App;
using Android.Widget;

namespace Speech2TextApp.Droid.Pages
{
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
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            liveTogetherY = FindViewById<RadioButton>(Resource.Id.liveTogetherY);
            liveTogetherN = FindViewById<RadioButton>(Resource.Id.liveTogetherN);
            otherDesc = FindViewById<EditText>(Resource.Id.otherDesc);

            InitLiveTogethe(liveTogetherY);
            InitLiveTogethe(liveTogetherY);
            InitOtherDesc();
            var next = FindViewById<Button>(Resource.Id.btn_page_4_next);
            next.Click += NextButtonEvent;

        }

        private void InitLiveTogethe(RadioButton rb) {
            if (rb.Text == MainActivity.dataCurrent.VisitDetail.OtherPeople) {
                rb.Checked = true;
            }
            rb.Click += delegate
            {
                MainActivity.dataCurrent.VisitDetail.OtherPeople = rb.Text;
            };
        }

        private void InitOtherDesc() {
            if (string.IsNullOrEmpty(MainActivity.dataCurrent.VisitDetail.OtherDesc)) {
                MainActivity.dataCurrent.VisitDetail.OtherDesc = string.Join(@"。", MainActivity.dataCurrent.Message);
            }
            otherDesc.Text = MainActivity.dataCurrent.VisitDetail.OtherDesc;
        }

        public void NextButtonEvent(object sender, EventArgs e)
        {
            MainActivity.dataCurrent.VisitDetail.OtherDesc = otherDesc.Text;
            StartActivity(typeof(Page5Activity));
        }
    }
}