using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Java.Util;
using System;
namespace Speech2TextApp.Droid.Fragments
{
    [Activity(Label = "Page1Fragment")]
    public class Page1Fragment :  Activity
    {
        TextView times;
        TextView applyName;
        TextView visitName;
        TextView relatoinship;
        TextView phone;
        LinearLayout addressLayout;
        Button address1;
        Button address2;
        Button address3;
        Button next;
        Button record;
        EditText visitDate;
        EditText editText1;
        RadioButton radioButton1;
        RadioButton radioButton2;
        LinearLayout descLayout;
        private readonly int VOICE = 10;

      

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Page1Fragment);
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

            #region address
            addressLayout = (LinearLayout)FindViewById(Resource.Id.showAddress);
            address1 = FindViewById<Button>(Resource.Id.address1);
            address2 = FindViewById<Button>(Resource.Id.address2);
            address3 = FindViewById<Button>(Resource.Id.address3);
            address1.Click += delegate {
                address1.SetBackgroundResource(Resource.Drawable.blue_button);
                address2.SetBackgroundResource(Resource.Drawable.blue_button);
                address3.SetBackgroundResource(Resource.Drawable.blue_button);

                address1.SetBackgroundResource(Resource.Drawable.blue_button_activity);

                addressLayout.RemoveAllViews();
                TextView address = new TextView(this);
                address.Text = MainActivity.dataCurrent.Address1;
                addressLayout.AddView(address);
            };

            address2.Click += delegate {
                address1.SetBackgroundResource(Resource.Drawable.blue_button);
                address2.SetBackgroundResource(Resource.Drawable.blue_button);
                address3.SetBackgroundResource(Resource.Drawable.blue_button);

                address2.SetBackgroundResource(Resource.Drawable.blue_button_activity);

                addressLayout.RemoveAllViews();
                TextView address = new TextView(this);
                address.Text = MainActivity.dataCurrent.Address2;
                addressLayout.AddView(address);
            };

            address3.Click += delegate {
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
                addressArea = new EditText(this);
                addressArea.Hint = "弄";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
                addressArea = new EditText(this);
                addressArea.Hint = "號";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
                addressArea = new EditText(this);
                addressArea.Hint = "樓";
                addressLayout.AddView(addressArea);
                TextView address = new TextView(this);
                address.Text = "之";
                addressLayout.AddView(address);
                addressArea = new EditText(this);
                addressLayout.AddView(addressArea);
            };

            #endregion

            #region visit date
            Calendar myCalendar = Calendar.Instance;
            visitDate = FindViewById<EditText>(Resource.Id.visitDate);

            #endregion

            #region RadioButton
            radioButton1 = FindViewById<RadioButton>(Resource.Id.radioButton1);
            radioButton2 = FindViewById<RadioButton>(Resource.Id.radioButton2);
            descLayout = FindViewById<LinearLayout>(Resource.Id.areaDesc);
            radioButton1.Click += VisitStatusClick;
            radioButton2.Click += VisitStatusClick;
            #endregion

            next = FindViewById<Button>(Resource.Id.btn_fragment_1_next);
            next.Click += ClickNextButton;

            var isRecording = false;
            record = FindViewById<Button>(Resource.Id.btn_record);
            editText1 = FindViewById<EditText>(Resource.Id.editText1);
            record.Click += delegate
            {
                record.Text = "結束錄音";
                isRecording = !isRecording;
                if (isRecording)
                {
                    // create the intent and start the activity
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
            };

            //return view;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultVal, Intent data)
        {
            if (requestCode == VOICE)
            {
                if (resultVal == Android.App.Result.Ok)
                {
                    var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    if (matches.Count != 0)
                    {
                        string textInput = editText1.Text + matches[0];

                        // limit the output to 500 characters
                        if (textInput.Length > 500)
                            textInput = textInput.Substring(0, 500);
                        editText1.Text = textInput;
                    }
                    else
                    {
                        editText1.Text = "No speech was recognised";
                    }

                    record.Text = "開始錄音";
                }
            }
        }




        private void VisitStatusClick(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Id == Resource.Id.radioButton1)
            {
                descLayout.Visibility = ViewStates.Invisible;
            }
            else if (rb.Id == Resource.Id.radioButton2)
            {
                descLayout.Visibility = ViewStates.Visible;
            }
            else {
                descLayout.Visibility = ViewStates.Invisible;
            }

        }

        /// <summary>
        /// next
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClickNextButton(object sender, EventArgs e)
        {
            
        }

    }
}