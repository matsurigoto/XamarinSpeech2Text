using Android.App;
using Android.Content.PM;
using Android.Widget;
using Android.OS;

namespace Speech2TextApp.Droid
{
    [Activity(Label = "Speech2TextApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private bool isRecording;
        private readonly int VOICE = 10;
        private TextView textBox;
        private Button recButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            isRecording = false;
            SetContentView(Resource.Layout.Main);
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