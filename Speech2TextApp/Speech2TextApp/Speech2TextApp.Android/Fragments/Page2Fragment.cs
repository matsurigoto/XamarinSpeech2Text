using Android.Support.V4.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using System;

namespace Speech2TextApp.Droid.Fragments
{
    public class Page2Fragment : Fragment, Page2LiveStatusDialog.NoticeDialogListener
    {
        TextView circle;
        RadioButton liveStatusSelf;
        RadioButton liveStatusRant;
        RadioButton liveStatusAssign;
        RadioButton liveStatusBorrow;
        RadioButton liveStatusOrg;
        RadioButton liveStatusE;
        public Page2Fragment()
        {

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.Page2Fragment, container, false);
            #region live status
            liveStatusSelf = view.FindViewById<RadioButton>(Resource.Id.liveStatusSelf);
            liveStatusRant = view.FindViewById<RadioButton>(Resource.Id.liveStatusRant);
            liveStatusAssign = view.FindViewById<RadioButton>(Resource.Id.liveStatusAssign);
            liveStatusBorrow = view.FindViewById<RadioButton>(Resource.Id.liveStatusBorrow);
            liveStatusOrg = view.FindViewById<RadioButton>(Resource.Id.liveStatusOrg);
            liveStatusE = view.FindViewById<RadioButton>(Resource.Id.liveStatusE);
            liveStatusSelf.Click += LiveStatusClick;
            liveStatusRant.Click += LiveStatusClick;
            liveStatusAssign.Click += LiveStatusClick;
            liveStatusBorrow.Click += LiveStatusClick;
            liveStatusOrg.Click += LiveStatusClick;
            liveStatusE.Click += LiveStatusClick;
            #endregion
            return view;
        }

        public void OnDialogPositiveClick(DialogFragment dialog)
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
                newFragment.Show(this.FragmentManager, "dialog");
            }
            else
            {
                liveStatusRant.Text = "租賃";
            }

        }
    }
}