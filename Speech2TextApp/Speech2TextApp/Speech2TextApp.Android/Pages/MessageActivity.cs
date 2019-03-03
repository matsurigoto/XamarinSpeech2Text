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
using Speech2TextApp.Droid.Dialog;

namespace Speech2TextApp.Droid.Pages
{
    [Activity(Label = "MessageActivity")]
    public class MessageActivity : BaseActivity, NoticeDialogListener
    {

        private readonly int VOICE = 10;
        LinearLayout messageLayout;
        EditText _description;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MessageActivity);
            this.Title = "語音備忘錄";
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            if (MainActivity.dataCurrent.Message == null) {
                MainActivity.dataCurrent.Message = new List<string>();
            }

            messageLayout = FindViewById<LinearLayout>(Resource.Id.message_layout);
            InitMessages();


            var record = FindViewById<Button>(Resource.Id.btn_record);
            _description = FindViewById<EditText>(Resource.Id.edittext_desc);
            record.Click += RecordEvent;

            //var submit = FindViewById<Button>(Resource.Id.btn_submit);
            //submit.Click += AddMessage;

            //var back = FindViewById<Button>(Resource.Id.btn_back);
            //back.Click += SaveMessageAndBack;
        }

        private void InitMessages() {
            messageLayout.RemoveAllViews();
            foreach (var message in MainActivity.dataCurrent.Message) {
                var layoutParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent, 1.0f);
                layoutParameter.SetMargins(100, 100, 50, 100);
                var layout = new LinearLayout(this)
                {
                    Orientation = Orientation.Vertical,
                    LayoutParameters = layoutParameter
                };

                var layoutTitle = new LinearLayout(this)
                {
                    Orientation = Orientation.Horizontal
                };
                var title = new TextView(this) { Text = $"{MainActivity.dataCurrent.Message.IndexOf(message) +1}" };
                title.SetTextColor(TextColor);
                title.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
                layoutTitle.AddView(title);

                var button = new TextView(this) { Text = "刪除" };
                LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                lp.Gravity = GravityFlags.Right;
                button.LayoutParameters = lp;
                button.SetTextColor(TextColor);
                button.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);

                button.Click += delegate {
                    MainActivity.dataCurrent.Message.Remove(message);
                    InitMessages();
                };

                layoutTitle.AddView(button);
                layout.AddView(layoutTitle);

                var line = new View(this);
                line.SetBackgroundColor(TextColor);
                var lineParameter = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 1);
                line.LayoutParameters = lineParameter;
                layout.AddView(line);

               
                TextView text = new TextView(this);
                text.Text = message;
                text.SetTextColor(TextColor);
                text.SetTextSize(Android.Util.ComplexUnitType.Sp, titleSize);
                layout.AddView(text);
                messageLayout.AddView(layout);
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
            FragmentTransaction transcation = FragmentManager.BeginTransaction();
            RecordDialog recordDialog = new RecordDialog();
            recordDialog.Show(transcation, "Dialog Fragment");
            
            //var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            //voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);

            //voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, Android.App.Application.Context.GetString(Resource.String.messageSpeakNow));
            //voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
            //voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
            //voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
            //voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

            //voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
            //StartActivityForResult(voiceIntent, VOICE);
        }

        //public void AddMessage(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(_description.Text))
        //    {
        //        MainActivity.dataCurrent.Message.Add(_description.Text);
        //        InitMessages();
        //    }
        //    else {
        //        Toast.MakeText(this, "請輸入備註", ToastLength.Long).Show();
        //    }
        //}
        

        //public void SaveMessageAndBack(object sender, EventArgs e)
        //{
        //    var descDocument = GetExternalFilesDir(null).AbsolutePath;
        //    string json = JsonConvert.SerializeObject(MainActivity.dataCurrent);
        //    string destPath = Path.Combine(descDocument, $"{ MainActivity.dataCurrent.Id}.json");
        //    File.WriteAllText(destPath, json);
        //    var intent = new Intent(this, typeof(MainActivity));
        //    StartActivity(intent);
        //}

        public void OnDialogPositiveClick(DialogFragment dialog)
        {
            var recordDialog = dialog as RecordDialog;
            MainActivity.dataCurrent.Message.Add(recordDialog.edittext_desc);
            InitMessages();
            var descDocument = GetExternalFilesDir(null).AbsolutePath;
            string json = JsonConvert.SerializeObject(MainActivity.dataCurrent);
            string destPath = Path.Combine(descDocument, $"{ MainActivity.dataCurrent.Id}.json");
            File.WriteAllText(destPath, json);
            recordDialog.Dismiss();
        }
    }
}