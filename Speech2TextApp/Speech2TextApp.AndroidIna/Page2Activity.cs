﻿using Android.OS;
using Android.Widget;
using System;
using Android.App;
using System.Collections.Generic;
using Speech2TextApp.Service;
using Speech2TextApp.AndroidIna.Dialog;
using Android.Graphics;

namespace Speech2TextApp.AndroidIna
{
    [Android.App.Activity(Label = "Page2Activity")]
    public class Page2Activity : BaseActivity, NoticeDialogListener
    {
        Button next;

        RadioButton liveCityY;
        RadioButton liveCityN;
        RadioButton liveCityE;


        RadioButton liveStatusSelf;
        RadioButton liveStatusRant;
        RadioButton liveStatusAssign;
        RadioButton liveStatusBorrow;
        RadioButton liveStatusOrg;
        RadioButton liveStatusE;


        CheckBox type1;
        CheckBox type2;
        CheckBox type3;
        CheckBox type4;


        RadioButton reasonNoWork;
        RadioButton reasonDie;
        RadioButton reasonJail;
        RadioButton reasonDivorce;
        RadioButton reasonMuch;
        RadioButton reasonSick;
        RadioButton reasonSick2;
        RadioButton reasonE;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Page2Activity);
            this.Title = "訪視紀錄";
            this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            if (DataService.dataCurrent.VisitDetails.Count > 0)
            {
                this.Title = $"訪視紀錄({DataService.dataCurrent.VisitDetails.Count})";
            }
            var circle = FindViewById<TextView>(Resource.Id.circle2);
            circle.SetBackgroundResource(Resource.Drawable.circle_activity);
            circle.SetTextColor(Color.White);

            //ActionBar.SetDisplayHomeAsUpEnabled(true);
            InitRecordButton();
            #region live status
            liveStatusSelf = FindViewById<RadioButton>(Resource.Id.liveStatusSelf);
            liveStatusRant = FindViewById<RadioButton>(Resource.Id.liveStatusRant);
            liveStatusAssign = FindViewById<RadioButton>(Resource.Id.liveStatusAssign);
            liveStatusBorrow = FindViewById<RadioButton>(Resource.Id.liveStatusBorrow);
            liveStatusOrg = FindViewById<RadioButton>(Resource.Id.liveStatusOrg);
            liveStatusE = FindViewById<RadioButton>(Resource.Id.liveStatusE);
            InitLiveStatus(liveStatusSelf);
            InitLiveStatus(liveStatusRant);
            InitLiveStatus(liveStatusAssign);
            InitLiveStatus(liveStatusBorrow);
            InitLiveStatus(liveStatusOrg);
            InitLiveStatus(liveStatusE);

            #endregion


            liveCityY = FindViewById<RadioButton>(Resource.Id.liveCityY);
            liveCityN = FindViewById<RadioButton>(Resource.Id.liveCityN);
            liveCityE = FindViewById<RadioButton>(Resource.Id.liveCityE);
            InitLiveCity(liveCityY);
            InitLiveCity(liveCityN);
            InitLiveCity(liveCityE);


            type1 = FindViewById<CheckBox>(Resource.Id.type1);
            type2 = FindViewById<CheckBox>(Resource.Id.type2);
            type3 = FindViewById<CheckBox>(Resource.Id.type3);
            type4 = FindViewById<CheckBox>(Resource.Id.type4);
            InitCheckbox(type1, DataService.dataCurrent.VisitDetail.ApplyType);
            InitCheckbox(type2, DataService.dataCurrent.VisitDetail.ApplyType);
            InitCheckbox(type3, DataService.dataCurrent.VisitDetail.ApplyType);
            InitCheckbox(type4, DataService.dataCurrent.VisitDetail.ApplyType);

            reasonNoWork = FindViewById<RadioButton>(Resource.Id.reasonNoWork);
            reasonDie = FindViewById<RadioButton>(Resource.Id.reasonDie);
            reasonJail = FindViewById<RadioButton>(Resource.Id.reasonJail);
            reasonDivorce = FindViewById<RadioButton>(Resource.Id.reasonDivorce);
            reasonMuch = FindViewById<RadioButton>(Resource.Id.reasonMuch);
            reasonSick = FindViewById<RadioButton>(Resource.Id.reasonSick);
            reasonSick2 = FindViewById<RadioButton>(Resource.Id.reasonSick);
            reasonE = FindViewById<RadioButton>(Resource.Id.reasonE);
            InitApplyReason(reasonNoWork);
            InitApplyReason(reasonDie);
            InitApplyReason(reasonJail);
            InitApplyReason(reasonDivorce);
            InitApplyReason(reasonMuch);
            InitApplyReason(reasonSick);
            InitApplyReason(reasonSick2);
            InitApplyReason(reasonE);

            var next = FindViewById<Button>(Resource.Id.btn_page_2_next);
            next.Click += NextButtonEvent;
        }

        public void OnDialogPositiveClick(DialogFragment dialog)
        {
            liveStatusRant.Text = string.Format("{0}({1}:{2})",
                 "租賃",
                (dialog as Page2LiveStatusDialog).LiveStatusType.Text,
                (dialog as Page2LiveStatusDialog).LiveStatusMoney);
            dialog.Dismiss();


        }

        private void InitCheckbox(CheckBox rb, IEnumerable<string> defaultName)
        {
            foreach (var val in defaultName)
            {
                if (rb.Text == val)
                {
                    rb.Checked = true;
                    break;
                }
            }
            rb.Click += delegate
            {
                if (rb.Checked)
                {
                    DataService.dataCurrent.VisitDetail.ApplyType.Add(rb.Text);
                }
                else
                {
                    if (DataService.dataCurrent.VisitDetail.ApplyType.Contains(rb.Text))
                    {
                        DataService.dataCurrent.VisitDetail.ApplyType.Remove(rb.Text);
                    }
                }
            };
        }

        private void InitRadioButton(RadioButton rb, string defaultName)
        {
            if (rb.Text == defaultName)
            {
                rb.Checked = true;
            }
        }

        private void InitApplyReason(RadioButton rb)
        {
            InitRadioButton(rb, DataService.dataCurrent.VisitDetail.ApplyReason);
            rb.Click += delegate
            {
                DataService.dataCurrent.VisitDetail.ApplyReason = rb.Text;
            };
        }

        private void InitLiveCity(RadioButton rb)
        {
            InitRadioButton(rb, DataService.dataCurrent.VisitDetail.LiveCityStatus);
            rb.Click += delegate
            {
                DataService.dataCurrent.VisitDetail.LiveCityStatus = rb.Text;
            };
        }

        private void InitLiveStatus(RadioButton rb)
        {
            InitRadioButton(rb, DataService.dataCurrent.VisitDetail.LiveCityStatus);
            rb.Click += LiveStatusClick;
        }

       

        private void LiveStatusClick(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Id == Resource.Id.liveStatusRant)
            {
                FragmentTransaction transcation = FragmentManager.BeginTransaction();
                Page2LiveStatusDialog dialog = new Page2LiveStatusDialog();
                dialog.Show(transcation, "Dialog Fragment");
            }
            else
            {
                liveStatusRant.Text = "租賃";
            }
        }

        public void NextButtonEvent(object sender, EventArgs e)
        {
            if (DataService.dataCurrent.IsLast)
            {
                StartActivity(typeof(Page5Activity));
            }
            else
            {
                StartActivity(typeof(Page3Activity));
            }
        }
    }
}