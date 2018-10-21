using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech;
using Android.Views;
using Android.Widget;
using Java.Util;
using System;
using System.Collections.Generic;

namespace Speech2TextApp.Droid.Pages
{
    [Activity(Label = "Page1Activity")]
    public class Page1Activity : Activity
    {
        TextView times;
        TextView applyName;
        TextView visitName;
        TextView relatoinship;
        TextView phone;
        LinearLayout addressLayout;
        public string AddressType { get; set; }
        Button address1;
        Button address2;
        Button address3;
        Button next;
        Button record;
        EditText visitDate;
        EditText _description;
        RadioButton radoiAtHomeY;
        RadioButton radoiAtHomeN;
        LinearLayout descLayout;
        private readonly int VOICE = 10;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Page1Activity);
            times = FindViewById<TextView>(Resource.Id.times);
            applyName = FindViewById<TextView>(Resource.Id.apply_name);
            visitName = FindViewById<TextView>(Resource.Id.visit_name);
            relatoinship = FindViewById<TextView>(Resource.Id.relatoinship);
            phone = FindViewById<TextView>(Resource.Id.phone);

            times.Text = MainActivity.dataCurrent.VisitDetails.Count.ToString();
            applyName.Text = MainActivity.dataCurrent.ApplyName;
            visitName.Text = MainActivity.dataCurrent.VisitName;
            relatoinship.Text = MainActivity.dataCurrent.Relatoinship;
            phone.Text = MainActivity.dataCurrent.Phone;
            AddressType = MainActivity.dataCurrent.AddressType;

            addressLayout = (LinearLayout)FindViewById(Resource.Id.showAddress);
            address1 = FindViewById<Button>(Resource.Id.address1);
            address2 = FindViewById<Button>(Resource.Id.address2);
            address3 = FindViewById<Button>(Resource.Id.address3);
            address1.Click += delegate
            {
                address1.SetBackgroundResource(Resource.Drawable.blue_button);
                address2.SetBackgroundResource(Resource.Drawable.blue_button);
                address3.SetBackgroundResource(Resource.Drawable.blue_button);

                address1.SetBackgroundResource(Resource.Drawable.blue_button_activity);

                addressLayout.RemoveAllViews();
                TextView address = new TextView(this);
                address.Text = MainActivity.dataCurrent.Address1;
                AddressType = "戶籍地址";
                addressLayout.AddView(address);
            };

            address2.Click += delegate
            {
                address1.SetBackgroundResource(Resource.Drawable.blue_button);
                address2.SetBackgroundResource(Resource.Drawable.blue_button);
                address3.SetBackgroundResource(Resource.Drawable.blue_button);

                address2.SetBackgroundResource(Resource.Drawable.blue_button_activity);

                addressLayout.RemoveAllViews();
                TextView address = new TextView(this);
                address.Text = MainActivity.dataCurrent.Address2;
                AddressType = "居住地址";
                addressLayout.AddView(address);
            };

            address3.Click += delegate
            {
                address1.SetBackgroundResource(Resource.Drawable.blue_button);
                address2.SetBackgroundResource(Resource.Drawable.blue_button);
                address3.SetBackgroundResource(Resource.Drawable.blue_button);

                address3.SetBackgroundResource(Resource.Drawable.blue_button_activity);


                addressLayout.RemoveAllViews();
                EditText addressArea = new EditText(this);
                addressArea.Hint = "區";
                addressLayout.AddView(addressArea);
                //TextView address = new TextView(this);
                //address.Text = "\\";
                //addressLayout.AddView(address);
                addressArea = new EditText(this);
                addressArea.Hint = "里";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
                addressArea = new EditText(this);
                addressArea.Hint = "鄰";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
                addressArea = new EditText(this);
                addressArea.Hint = "路";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
                addressArea = new EditText(this);
                addressArea.Hint = "段";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
                addressArea = new EditText(this);
                addressArea.Hint = "巷";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
               
            };
            

            #region visit date
            Calendar myCalendar = Calendar.Instance;
            visitDate = FindViewById<EditText>(Resource.Id.visitDate);

            #endregion

            #region RadioButton
            radoiAtHomeY = FindViewById<RadioButton>(Resource.Id.radoiAtHomeY);
            radoiAtHomeN = FindViewById<RadioButton>(Resource.Id.radoiAtHomeN);
            descLayout = FindViewById<LinearLayout>(Resource.Id.areaDesc);
            if (MainActivity.dataCurrent.VisitDetail.Status == "N")
            {
                radoiAtHomeN.Checked = true;
            }
            else
            {
                radoiAtHomeY.Checked = true;
            }
            radoiAtHomeY.Click += VisitStatusClick;
            radoiAtHomeN.Click += VisitStatusClick;
            #endregion


            var next = FindViewById<Button>(Resource.Id.btn_page_1_next);
            next.Click += NextButtonEvent;

            var record = FindViewById<Button>(Resource.Id.btn_record);
            _description = FindViewById<EditText>(Resource.Id.edittext_desc);
            record.Click += RecordEvent;
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

        public void RecordEvent(object sender, EventArgs e)
        {
            var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);

            voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, Android.App.Application.Context.GetString(Resource.String.messageSpeakNow));
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
            voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
            StartActivityForResult(voiceIntent, VOICE);
        }

        public void NextButtonEvent(object sender, EventArgs e)
        {
            MainActivity.dataCurrent.AddressType = AddressType;
            if (AddressType == "其它")
            {
                //MainActivity.dataCurrent.Address3 = 
            }
            //MainActivity.dataCurrent.VisitDetail.VisitDate = visitDate.Text;
            MainActivity.dataCurrent.VisitDetail.VisitDesc = _description.Text;

            MainActivity.dataCurrent.VisitDetail.LiveCityStatus = "是";
            MainActivity.dataCurrent.VisitDetail.LiveStatus = "自有";
            MainActivity.dataCurrent.VisitDetail.ApplyType = new List<string>() { "低收入戶", "中低收入戶" };
            MainActivity.dataCurrent.VisitDetail.ApplyReason = "負擔家計者失業";

            if (MainActivity.dataCurrent.IsLast)
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