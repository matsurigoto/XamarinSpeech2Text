using Android.Content;
using Android.OS;
using Android.Speech;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Java.Util;
using System;
namespace Speech2TextApp.Droid.Fragments
{
    public class Page1Fragment : Fragment
    {
        TextView circle;
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

        public Page1Fragment()
        {

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Page1Fragment, container, false);

            #region address
            addressLayout = (LinearLayout)view.FindViewById(Resource.Id.showAddress);
            address1 = view.FindViewById<Button>(Resource.Id.address1);
            address2 = view.FindViewById<Button>(Resource.Id.address2);
            address3 = view.FindViewById<Button>(Resource.Id.address3);
            address1.Click += delegate {
                address1.SetBackgroundResource(Resource.Drawable.blue_button);
                address2.SetBackgroundResource(Resource.Drawable.blue_button);
                address3.SetBackgroundResource(Resource.Drawable.blue_button);

                address1.SetBackgroundResource(Resource.Drawable.blue_button_activity);

                addressLayout.RemoveAllViews();
                TextView address = new TextView(this.Context);
                address.Text = "台北市中山區新生里";
                addressLayout.AddView(address);
            };

            address2.Click += delegate {
                address1.SetBackgroundResource(Resource.Drawable.blue_button);
                address2.SetBackgroundResource(Resource.Drawable.blue_button);
                address3.SetBackgroundResource(Resource.Drawable.blue_button);

                address2.SetBackgroundResource(Resource.Drawable.blue_button_activity);

                addressLayout.RemoveAllViews();
                TextView address = new TextView(this.Context);
                address.Text = "台北市中山區新生里";
                addressLayout.AddView(address);
            };

            address3.Click += delegate {
                address1.SetBackgroundResource(Resource.Drawable.blue_button);
                address2.SetBackgroundResource(Resource.Drawable.blue_button);
                address3.SetBackgroundResource(Resource.Drawable.blue_button);

                address3.SetBackgroundResource(Resource.Drawable.blue_button_activity);


                addressLayout.RemoveAllViews();
                EditText addressArea = new EditText(this.Context);
                addressArea.Hint = "區";
                addressLayout.AddView(addressArea);
                //TextView address = new TextView(this.Context);
                //address.Text = "\\";
                //addressLayout.AddView(address);
                addressArea = new EditText(this.Context);
                addressArea.Hint = "里";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
                addressArea = new EditText(this.Context);
                addressArea.Hint = "鄰";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
                addressArea = new EditText(this.Context);
                addressArea.Hint = "路";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
                addressArea = new EditText(this.Context);
                addressArea.Hint = "段";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
                addressArea = new EditText(this.Context);
                addressArea.Hint = "巷";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
                addressArea = new EditText(this.Context);
                addressArea.Hint = "弄";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
                addressArea = new EditText(this.Context);
                addressArea.Hint = "號";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
                addressArea = new EditText(this.Context);
                addressArea.Hint = "樓";
                addressLayout.AddView(addressArea);
                TextView address = new TextView(this.Context);
                address.Text = "之";
                addressLayout.AddView(address);
                addressArea = new EditText(this.Context);
                addressLayout.AddView(addressArea);
            };

            #endregion

            #region visit date
            Calendar myCalendar = Calendar.Instance;
            visitDate = view.FindViewById<EditText>(Resource.Id.visitDate);

            #endregion

            #region RadioButton
            radioButton1 = view.FindViewById<RadioButton>(Resource.Id.radioButton1);
            radioButton2 = view.FindViewById<RadioButton>(Resource.Id.radioButton2);
            descLayout = view.FindViewById<LinearLayout>(Resource.Id.areaDesc);
            radioButton1.Click += VisitStatusClick;
            radioButton2.Click += VisitStatusClick;
            #endregion

            next = view.FindViewById<Button>(Resource.Id.btn_fragment_1_next);
            var nextEvent = (SwipeFormActivity)Activity;
            next.Click += nextEvent.ClickNextButton;

            var isRecording = false;
            record = view.FindViewById<Button>(Resource.Id.btn_record);
            editText1 = view.FindViewById<EditText>(Resource.Id.editText1);
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

            return view;
        }

        public override void OnActivityResult(int requestCode, int resultVal, Intent data)
        {
            if (requestCode == VOICE)
            {
                if (resultVal == (int)Android.App.Result.Ok)
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

    }
}