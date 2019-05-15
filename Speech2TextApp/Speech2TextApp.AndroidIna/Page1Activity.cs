using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Speech;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Util;
using Speech2TextApp.Service;

namespace Speech2TextApp.AndroidIna
{
    /// <summary>
    /// 第一頁:基本資料
    /// </summary>
    [Activity(Label = "Page1Activity")]
    public class Page1Activity : BaseActivity
    {

        
        TextView times;
        TextView applyName;
        EditText visitName;
        EditText relatoinship;
        TextView phone;
        LinearLayout addressLayout;
        public string AddressType { get; set; }
        Button address1;
        Button address2;
        Button address3;
        Button next;
        //Button record;
        EditText visitDate;
        EditText _description;
        RadioButton radoiAtHomeY;
        RadioButton radoiAtHomeN;
        LinearLayout descLayout;
        private readonly int VOICE = 10;
        EditText address;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Page1Activity);
            this.Title = "訪視紀錄";
            this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            if (DataService.dataCurrent.VisitDetails.Count > 0) {
                this.Title = $"訪視紀錄({DataService.dataCurrent.VisitDetails.Count})";
            }

            var circle = FindViewById<TextView>(Resource.Id.circle1);
            circle.SetBackgroundResource(Resource.Drawable.circle_activity);
            circle.SetTextColor(Color.White);

            InitRecordButton();
            //times = FindViewById<TextView>(Resource.Id.times);
            applyName = FindViewById<TextView>(Resource.Id.apply_name);
            visitName = FindViewById<EditText>(Resource.Id.visit_name);
            relatoinship = FindViewById<EditText>(Resource.Id.relatoinship);
            phone = FindViewById<TextView>(Resource.Id.phone);

           // times.Text = DataService.dataCurrent.VisitDetails.Count.ToString();
            applyName.Text = DataService.dataCurrent.ApplyName;
            visitName.Text = DataService.dataCurrent.VisitName;
            relatoinship.Text = DataService.dataCurrent.Relatoinship;
            phone.Text = DataService.dataCurrent.Phone;
            AddressType = DataService.dataCurrent.AddressType;

            addressLayout = (LinearLayout)FindViewById(Resource.Id.showAddress);
            address1 = FindViewById<Button>(Resource.Id.address1);
            address2 = FindViewById<Button>(Resource.Id.address2);
            address3 = FindViewById<Button>(Resource.Id.address3);



            address1.Click += delegate
            {
                address1.SetBackgroundResource(Resource.Drawable.blue_button);
                address1.SetTextColor(TextColor);
                address2.SetBackgroundResource(Resource.Drawable.blue_button);
                address2.SetTextColor(TextColor);
                address3.SetBackgroundResource(Resource.Drawable.blue_button);
                address3.SetTextColor(TextColor);

                address1.SetBackgroundResource(Resource.Drawable.blue_button_activity);
                address1.SetTextColor(Color.White);
                addressLayout.RemoveAllViews();
                TextView address = new TextView(this);
                address.Text = DataService.dataCurrent.Address1;
                AddressType = "戶籍地址";
                addressLayout.AddView(address);
            };

            address2.Click += delegate
            {
                address1.SetBackgroundResource(Resource.Drawable.blue_button);
                address1.SetTextColor(TextColor);
                address2.SetBackgroundResource(Resource.Drawable.blue_button);
                address2.SetTextColor(TextColor);
                address3.SetBackgroundResource(Resource.Drawable.blue_button);
                address3.SetTextColor(TextColor);

                address2.SetBackgroundResource(Resource.Drawable.blue_button_activity);
                address2.SetTextColor(Color.White);
                addressLayout.RemoveAllViews();
                TextView address = new TextView(this);
                address.Text = DataService.dataCurrent.Address2;
                AddressType = "居住地址";
                addressLayout.AddView(address);
            };

            address3.Click += delegate
            {
                address1.SetBackgroundResource(Resource.Drawable.blue_button);
                address1.SetTextColor(TextColor);
                address2.SetBackgroundResource(Resource.Drawable.blue_button);
                address2.SetTextColor(TextColor);
                address3.SetBackgroundResource(Resource.Drawable.blue_button);
                address3.SetTextColor(TextColor);

                address3.SetBackgroundResource(Resource.Drawable.blue_button_activity);
                address3.SetTextColor(Color.White);

                addressLayout.RemoveAllViews();
                TextInputLayout til = new TextInputLayout(this);
                til.SetHintTextAppearance(Resource.Style.TextInputHint);
                til.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                if (address == null)
                {
                    address = new EditText(this);
                    address.Hint = "訪視地點";
                }
                til.AddView(address);
                addressLayout.AddView(til);

            };

            if (AddressType == "戶籍地址")
            {
                address1.PerformClick();
            }
            else if (AddressType == "居住地址")
            {
                address2.PerformClick();
            }
            else if (AddressType == "其他")
            {
                address = new EditText(this);
                address.Text = DataService.dataCurrent.Address3;
                address3.PerformClick();
            }
            #region visit date
            Calendar myCalendar = Calendar.Instance;
            visitDate = FindViewById<EditText>(Resource.Id.visitDate);
            if (DataService.dataCurrent.VisitDetail.VisitDate == null)
            {
                DataService.dataCurrent.VisitDetail.VisitDate = new DateTime();
            }
            visitDate.Text = DataService.dataCurrent.VisitDetail.VisitDate.GetValueOrDefault().ToString("yyyy/MM/dd HH:mm");
            visitDate.Click += delegate {

                View dialogView = View.Inflate(this, Resource.Layout.date_time_picker, null);
                Android.Support.V7.App.AlertDialog alertDialog = new Android.Support.V7.App.AlertDialog.Builder(this).Create();

                var buttonSubmit = dialogView.FindViewById<Button>(Resource.Id.date_time_set);
                buttonSubmit.Click += delegate
                {
                    DatePicker datePicker = (DatePicker)dialogView.FindViewById(Resource.Id.date_picker);
                    TimePicker timePicker = (TimePicker)dialogView.FindViewById(Resource.Id.time_picker);

                    Calendar calendar = new GregorianCalendar(datePicker.Year,
                                       datePicker.Month,
                                       datePicker.DayOfMonth,
                                       timePicker.Hour,
                                       timePicker.Minute);

                    visitDate.Text = $"{datePicker.Year}/{datePicker.Month}/{datePicker.DayOfMonth} {timePicker.Hour}:{timePicker.Minute}";
                    alertDialog.Dismiss();
                };
                alertDialog.SetView(dialogView);
                alertDialog.Show();
            };
            #endregion

            #region RadioButton
            radoiAtHomeY = FindViewById<RadioButton>(Resource.Id.radoiAtHomeY);
            radoiAtHomeN = FindViewById<RadioButton>(Resource.Id.radoiAtHomeN);
            descLayout = FindViewById<LinearLayout>(Resource.Id.areaDesc);
            radoiAtHomeY.Click += VisitStatusClick;
            radoiAtHomeN.Click += VisitStatusClick;
            if (DataService.dataCurrent.VisitDetail.Status == "N")
            {
                radoiAtHomeN.Checked = true;
                radoiAtHomeN.PerformClick();
            }
            else
            {
                radoiAtHomeY.Checked = true;
                radoiAtHomeY.PerformClick();
            }

            #endregion


            var next = FindViewById<Button>(Resource.Id.btn_page_1_next);
            next.Click += NextButtonEvent;
            
            _description = FindViewById<EditText>(Resource.Id.edittext_desc);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultVal, Intent data)
        {
            if (requestCode != VOICE) return;
            if (resultVal != Result.Ok) return;
            var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
            if (matches.Count != 0)
            {
                var textInput = _description.Text + matches[0];
                if (textInput.Length > 500)
                    textInput = textInput.Substring(0, 500);
                _description.Text = textInput;
            }
            else
            {
                _description.Text = "No speech was recognized";
            }
        }

        /// <summary>
        /// 改變受訪狀態
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VisitStatusClick(object sender, EventArgs e)
        {
            var rb = (RadioButton)sender;
            switch (rb.Id)
            {
                case Resource.Id.radoiAtHomeY:
                    descLayout.Visibility = ViewStates.Invisible;
                    break;
                case Resource.Id.radoiAtHomeN:
                    descLayout.Visibility = ViewStates.Visible;
                    break;
                default:
                    descLayout.Visibility = ViewStates.Invisible;
                    break;
            }
        }

       
        /// <summary>
        /// 下一頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NextButtonEvent(object sender, EventArgs e)
        {
            DataService.dataCurrent.AddressType = AddressType;
            if (AddressType == "其它")
            {
                //DataService.dataCurrent.Address3 = 
            }
            //DataService.dataCurrent.VisitDetail.VisitDate = visitDate.Text;
            if (address != null)
            {
                DataService.dataCurrent.Address3 = address.Text;
            }
            DataService.dataCurrent.VisitDetail.VisitDesc = _description.Text;

            DataService.dataCurrent.VisitDetail.LiveCityStatus = "是";
            DataService.dataCurrent.VisitDetail.LiveStatus = "自有";
            DataService.dataCurrent.VisitDetail.ApplyType = new List<string>() { "低收入戶", "中低收入戶" };
            DataService.dataCurrent.VisitDetail.ApplyReason = "負擔家計者失業";

            if (DataService.dataCurrent.IsLast)
            {
                StartActivity(typeof(Page5Activity));
            }
            else
            {
                StartActivity(typeof(Page2Activity));
            }
        }
    }
}