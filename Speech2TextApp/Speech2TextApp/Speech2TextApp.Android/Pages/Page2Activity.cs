using Android.OS;
using Android.Widget;
using System;
using Android.App;

namespace Speech2TextApp.Droid.Pages
{
    [Activity(Label = "Page2Activity")]
    public class Page2Activity : Activity, Page2LiveStatusDialog.NoticeDialogListener
    {
        TextView circle;
        RadioButton liveStatusSelf;
        RadioButton liveStatusRant;
        RadioButton liveStatusAssign;
        RadioButton liveStatusBorrow;
        RadioButton liveStatusOrg;
        RadioButton liveStatusE;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Page2Activity);
            #region live status
            liveStatusSelf = FindViewById<RadioButton>(Resource.Id.liveStatusSelf);
            liveStatusRant = FindViewById<RadioButton>(Resource.Id.liveStatusRant);
            liveStatusAssign = FindViewById<RadioButton>(Resource.Id.liveStatusAssign);
            liveStatusBorrow = FindViewById<RadioButton>(Resource.Id.liveStatusBorrow);
            liveStatusOrg = FindViewById<RadioButton>(Resource.Id.liveStatusOrg);
            liveStatusE = FindViewById<RadioButton>(Resource.Id.liveStatusE);
            liveStatusSelf.Click += LiveStatusClick;
            liveStatusRant.Click += LiveStatusClick;
            liveStatusAssign.Click += LiveStatusClick;
            liveStatusBorrow.Click += LiveStatusClick;
            liveStatusOrg.Click += LiveStatusClick;
            liveStatusE.Click += LiveStatusClick;
            #endregion

            var next = FindViewById<Button>(Resource.Id.btn_page_2_next);
            next.Click += NextButtonEvent;
        }

        public void OnDialogPositiveClick(Android.Support.V4.App.DialogFragment dialog)
        {
            liveStatusRant.Text = string.Format("{0}({1}:{2})",
                (dialog as Page2LiveStatusDialog).LiveStatusType.Text,
                (dialog as Page2LiveStatusDialog).LiveStatusMoney);
           
        }

        private void LiveStatusClick(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Id == Resource.Id.liveStatusRant)
            {
                // Create and show the dialog.
                Page2LiveStatusDialog newFragment = Page2LiveStatusDialog.NewInstance(null);
                //Add fragment
                //newFragment.Show(this.FragmentManager, "dialog");
            }
            else
            {
                liveStatusRant.Text = "租賃";
            }
        }

        public void NextButtonEvent(object sender, EventArgs e)
        {
            StartActivity(typeof(Page3Activity));
        }
    }
}