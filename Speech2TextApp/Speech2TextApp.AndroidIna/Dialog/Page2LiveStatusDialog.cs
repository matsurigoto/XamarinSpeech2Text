using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Speech2TextApp.Service;

namespace Speech2TextApp.AndroidIna.Dialog
{
    public class Page2LiveStatusDialog : DialogFragment
    {
        NoticeDialogListener mListener;
        public override void OnAttach(Context context)
        {
            base.OnAttach(context);
            try
            {
                // Instantiate the NoticeDialogListener so we can send events to the host
                mListener = (NoticeDialogListener)this.Activity;
            }
            catch (Exception e)
            {
            }
        }






        public static Page2LiveStatusDialog NewInstance(Bundle bundle)
        {
            Page2LiveStatusDialog fragment = new Page2LiveStatusDialog();
            fragment.Arguments = bundle;
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public RadioButton LiveStatusType { get; set; }
        public string LiveStatusMoney { get; set; }

        RadioButton liveStatusRant1;
        RadioButton liveStatusRant2;
        RadioButton liveStatusRant3;
        RadioButton liveStatusRant4;
        EditText liveStatusRantCost;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Page2LiveStatusDialog, container, false);
            Dialog.SetTitle("住宅狀況:租賃");
            liveStatusRant1 = view.FindViewById<RadioButton>(Resource.Id.liveStatusRant1);
            liveStatusRant2 = view.FindViewById<RadioButton>(Resource.Id.liveStatusRant2);
            liveStatusRant3 = view.FindViewById<RadioButton>(Resource.Id.liveStatusRant3);
            liveStatusRant4 = view.FindViewById<RadioButton>(Resource.Id.liveStatusRant4);
            if (string.IsNullOrEmpty(DataService.dataCurrent.VisitDetail.LiveRentStatus)) {
                DataService.dataCurrent.VisitDetail.LiveRentStatus = "一般租屋";
            }
            InirLiveStatus(liveStatusRant1,DataService.dataCurrent.VisitDetail.LiveRentStatus);
            InirLiveStatus(liveStatusRant2, DataService.dataCurrent.VisitDetail.LiveRentStatus);
            InirLiveStatus(liveStatusRant3, DataService.dataCurrent.VisitDetail.LiveRentStatus);
            InirLiveStatus(liveStatusRant4, DataService.dataCurrent.VisitDetail.LiveRentStatus);
          
            liveStatusRantCost = view.FindViewById<EditText>(Resource.Id.liveStatusRantCost);
            liveStatusRantCost.Text = DataService.dataCurrent.VisitDetail.LiveRentMoney ?? string.Empty;
            Button button = view.FindViewById<Button>(Resource.Id.liveStatusOK);
            button.Click += delegate
            {
                this.LiveStatusMoney = liveStatusRantCost.Text;
                mListener.OnDialogPositiveClick(this);

            };
            return view;
        }

        private void InirLiveStatus(RadioButton rb,string value)
        {
            rb.Click += LiveStatusClick;
            if (rb.Text == value) {
                rb.PerformClick();
            }
           
        }

        private void LiveStatusClick(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            this.LiveStatusType = rb;

        }
    }
}