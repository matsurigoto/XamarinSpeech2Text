using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Speech2TextApp.Droid.Pages
{
    [Activity(Label = "MessageActivity")]
    public class MessageActivity : BaseActivity
    {

        private readonly int VOICE = 10;
        LinearLayout messageLayout;
        EditText _description;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MessageActivity);
            this.Title = "概述備註";
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            if (MainActivity.dataCurrent.Message == null) {
                MainActivity.dataCurrent.Message = new List<string>();
            }

            messageLayout = FindViewById<LinearLayout>(Resource.Id.message_layout);
            InitMessages();


            var record = FindViewById<Button>(Resource.Id.btn_record);
            _description = FindViewById<EditText>(Resource.Id.edittext_desc);
            record.Click += RecordEvent;

            var submit = FindViewById<Button>(Resource.Id.btn_submit);
            submit.Click += AddMessage;

            var back = FindViewById<Button>(Resource.Id.btn_back);
            back.Click += SaveMessageAndBack;
        }

        private void InitMessages() {
            messageLayout.RemoveAllViews();
            foreach (var message in MainActivity.dataCurrent.Message) {
                var mainLayout = new LinearLayout(this)
                {
                    Orientation = Orientation.Vertical,
                    LayoutParameters = new LinearLayout.LayoutParams(Android.Views.ViewGroup.LayoutParams.MatchParent, Android.Views.ViewGroup.LayoutParams.WrapContent)
                };
                TextView text = new TextView(this);
                text.Text = message;
                mainLayout.AddView(text);
                messageLayout.AddView(mainLayout);
            }
            
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

        public void AddMessage(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_description.Text))
            {
                MainActivity.dataCurrent.Message.Add(_description.Text);
                InitMessages();
            }
            else {
                Toast.MakeText(this, "請輸入備註", ToastLength.Long).Show();
            }
        }
        

        public void SaveMessageAndBack(object sender, EventArgs e)
        {
            var descDocument = GetExternalFilesDir(null).AbsolutePath;
            string json = JsonConvert.SerializeObject(MainActivity.dataCurrent);
            string destPath = Path.Combine(descDocument, $"{ MainActivity.dataCurrent.Id}.json");
            File.WriteAllText(destPath, json);
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
        
    }
}