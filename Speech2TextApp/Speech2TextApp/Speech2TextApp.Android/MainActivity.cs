using Android.App;
using Android.Content.PM;
using Android.Widget;
using Android.OS;
using Android.Content;
using Speech2TextApp.Interface;
using Speech2TextApp.Data;
using System.Collections.Generic;
using Speech2TextApp.Service;
using System;
using System.Linq;
using Speech2TextApp.Droid.Fragments;

namespace Speech2TextApp.Droid
{
    [Activity(Label = "Speech2TextApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //private bool isRecording;
        //private readonly int VOICE = 10;
        //private TextView textBox;
        //private Button recButton;
        private LinearLayout dataLayout;
        private IData dataService;
        public List<ApplyResult> datas { get; set; }
        public List<ApplyResult> datasInStatus { get; set; }
        public static ApplyResult dataCurrent { get; set; }
        Button visitStatusN;
        Button visitStatusY;
        TextView dataCount;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
            string path = this.GetExternalFilesDir(null).AbsolutePath; 
            dataService = new DataService();
            datas = dataService.GetDatas(path);
            
            if (datas.Count == 0) {
                dataService.GenData(path);
                this.datas = dataService.GetDatas(path);
            }

           

            visitStatusN = FindViewById<Button>(Resource.Id.visit_status_n);
            visitStatusY = FindViewById<Button>(Resource.Id.visit_status_Y);
            dataCount = FindViewById<TextView>(Resource.Id.data_count);
            dataLayout = FindViewById<LinearLayout>(Resource.Id.data_layout);


            visitStatusN.Click += LoadVisitStatusClick;
            visitStatusY.Click += LoadVisitStatusClick;
            //isRecording = false;

            //recButton = FindViewById<Button>(Resource.Id.btnRecord);
            //textBox = FindViewById<TextView>(Resource.Id.textYourText);

            //string rec = Android.Content.PM.PackageManager.FeatureMicrophone;
            //if (rec != "android.hardware.microphone")
            //{
            //    var alert = new AlertDialog.Builder(recButton.Context);
            //    alert.SetTitle("You don't seem to have a microphone to record with");
            //    alert.SetPositiveButton("OK", (sender, e) =>
            //    {
            //        textBox.Text = "沒有麥克風";
            //        recButton.Enabled = false;
            //        return;
            //    });

            //    alert.Show();
            //}
            //else
            //    recButton.Click += delegate
            //    {
            //        recButton.Text = "結束錄音";
            //        isRecording = !isRecording;
            //        if (isRecording)
            //        {
            //            // create the intent and start the activity
            //            var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            //            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);

            //            voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, Application.Context.GetString(Resource.String.messageSpeakNow));
            //            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
            //            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
            //            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
            //            voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

            //            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
            //            StartActivityForResult(voiceIntent, VOICE);
            //        }
            //    };
        }


        private void LoadVisitStatusClick(object sender, EventArgs e)
        {
            Button rb = (Button)sender;
            string status = (rb.Id == Resource.Id.visit_status_Y)?"Y":"N";
            string countDesc = (rb.Id == Resource.Id.visit_status_Y) ? "送出資料" : "訪視資料";
            this.datasInStatus = datas.Where(x => x.Status == status).ToList();
            dataCount.Text = string.Format("共 {0} 筆 {1}", datasInStatus.Count().ToString(), countDesc);
            dataLayout.RemoveAllViews();
            foreach (var data in datasInStatus)
            {
                LinearLayout layout = new LinearLayout(this);
                TextView name = new TextView(this);
                name.Text = data.ApplyName;
                layout.AddView(name);
                layout.Click += delegate
                {
                    var intent = new Intent(this, typeof(Page1Fragment));
                    dataCurrent = data;
                    this.StartActivity(intent);
                };
                dataLayout.AddView(layout);
            }


        }
        private void LinearLayout1_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        //protected override void OnActivityResult(int requestCode, Result resultVal, Intent data)
        //{
        //    if (requestCode == VOICE)
        //    {
        //        if (resultVal == Result.Ok)
        //        {
        //            var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
        //            if (matches.Count != 0)
        //            {
        //                string textInput = textBox.Text + matches[0];

        //                // limit the output to 500 characters
        //                if (textInput.Length > 500)
        //                    textInput = textInput.Substring(0, 500);
        //                textBox.Text = textInput;
        //            }
        //            else
        //            {
        //                textBox.Text = "No speech was recognised";
        //            }

        //            recButton.Text = "開始錄音";
        //        }
        //    }

        //    base.OnActivityResult(requestCode, resultVal, data);
        //}
    }
}