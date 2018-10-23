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
using Speech2TextApp.Data;

namespace Speech2TextApp.Droid.Pages
{
    [Activity(Label = "Page3AddMemberActivity")]
    public class Page3AddMemberActivity : BaseActivity
    {
        EditText title;
        EditText name;

        RadioButton liveTogetherY;
        RadioButton liveTogetherN;

        RadioButton healthStatusN;
        RadioButton healthStatusSick;
        RadioButton healthStatusSick2;
        RadioButton healthStatusSick3;

        RadioButton workStatusFix;
        RadioButton workStatusTemp;
        RadioButton workStatusStree;
        RadioButton workStatusNo;

        RadioButton referralsY;
        RadioButton referralsN;

        RadioButton isInNursingHomeY;
        RadioButton isInNursingHomeN;

        MemberDetail member;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Page3AddMemberActivity);
            this.Title = "訪視紀錄";
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            member = new MemberDetail();
            liveTogetherY = FindViewById<RadioButton>(Resource.Id.liveTogetherY);
            liveTogetherN = FindViewById<RadioButton>(Resource.Id.liveTogetherN);
            InitLiveTogether(liveTogetherY);
            InitLiveTogether(liveTogetherN);

            referralsY = FindViewById<RadioButton>(Resource.Id.referralsY);
            referralsN = FindViewById<RadioButton>(Resource.Id.referralsN);
            InitReferrals(referralsY);
            InitReferrals(referralsN);

            healthStatusN = FindViewById<RadioButton>(Resource.Id.healthStatusN);
            healthStatusSick = FindViewById<RadioButton>(Resource.Id.healthStatusSick);
            healthStatusSick2 = FindViewById<RadioButton>(Resource.Id.healthStatusSick2);
            healthStatusSick3 = FindViewById<RadioButton>(Resource.Id.healthStatusSick3);
            InitHealthStatus(healthStatusN);
            InitHealthStatus(healthStatusSick);
            InitHealthStatus(healthStatusSick2);
            InitHealthStatus(healthStatusSick3);

            workStatusFix = FindViewById<RadioButton>(Resource.Id.workStatusFix);
            workStatusNo = FindViewById<RadioButton>(Resource.Id.workStatusNo);
            workStatusStree = FindViewById<RadioButton>(Resource.Id.workStatusStree);
            workStatusNo = FindViewById<RadioButton>(Resource.Id.workStatusNo);
            InitWorkStatus(workStatusFix);
            InitWorkStatus(workStatusNo);
            InitWorkStatus(workStatusStree);
            InitWorkStatus(workStatusNo);

            isInNursingHomeY = FindViewById<RadioButton>(Resource.Id.isInNursingHomeY);
            isInNursingHomeN = FindViewById<RadioButton>(Resource.Id.isInNursingHomeN);
            InitIsInNursingHome(isInNursingHomeY);
            InitIsInNursingHome(isInNursingHomeN);

            var cancel = FindViewById<Button>(Resource.Id.btn_page_3_cancel);
            cancel.Click += delegate
            {
                StartActivity(typeof(Page3Activity));
            };

            var addMember = FindViewById<Button>(Resource.Id.btn_page_3_add);
            addMember.Click += delegate
            {
                if (MainActivity.dataCurrent.VisitDetail.Members == null) {
                    MainActivity.dataCurrent.VisitDetail.Members = new List<MemberDetail>();
                }
                member.Name = FindViewById<EditText>(Resource.Id.name).Text;

                member.Title = FindViewById<EditText>(Resource.Id.title).Text;
                MainActivity.dataCurrent.VisitDetail.Members.Add(member);
                StartActivity(typeof(Page3Activity));
            };
        }

        private void InitLiveTogether(RadioButton rb) {
            rb.Click += delegate
            {
                member.LiveTogether = rb.Text;
            };
            if (rb.Text == member.LiveTogether) {
                rb.PerformClick();
            }
        }

        private void InitHealthStatus(RadioButton rb)
        {
            rb.Click += delegate
            {
                member.HealthStatus = rb.Text;
            };
            if (rb.Text == member.HealthStatus)
            {
                rb.PerformClick();
            }
        }

        private void InitWorkStatus(RadioButton rb)
        {
            rb.Click += delegate
            {
                member.WorkStatus = rb.Text;
            };
            if (rb.Text == member.WorkStatus)
            {
                rb.PerformClick();
            }
        }

        private void InitReferrals(RadioButton rb)
        {
            rb.Click += delegate
            {
                member.Referrals = rb.Text;
            };
            if (rb.Text == member.Referrals)
            {
                rb.PerformClick();
            }
        }

        private void InitIsInNursingHome(RadioButton rb)
        {
            rb.Click += delegate
            {
                member.IsInNursingHome = rb.Text;
            };
            if (rb.Text == member.IsInNursingHome)
            {
                rb.PerformClick();
            }
        }

        private void InitRadioButton(RadioButton rb, string defaultName)
        {
            if (rb.Text == defaultName)
            {
                rb.Checked = true;
            }
        }
    }
}