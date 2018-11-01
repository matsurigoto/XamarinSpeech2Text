using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech;
using Android.Views;
using Android.Widget;

namespace Speech2TextApp.AndroidIna.Dialog
{
    public class RecordDialog : DialogFragment
    {
        public string edittext_desc { get; set; }
        private readonly int VOICE = 10;
        // Use this instance of the interface to deliver action events
        NoticeDialogListener mListener;

        public override void OnAttach(Context context)
        {
            base.OnAttach(context);
            try
            {
                // Instantiate the NoticeDialogListener so we can send events to the host
                mListener = (NoticeDialogListener)this.Activity;
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
            catch (Exception e)
            {
            }
        }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.RecordDialog, container, false);
            Dialog.SetTitle("語音備忘錄");
            
            Button button = view.FindViewById<Button>(Resource.Id.save_record);
            EditText edittext = view.FindViewById<EditText>(Resource.Id.edittext_desc);
            button.Click += delegate
            {
                this.edittext_desc = edittext.Text;
                mListener.OnDialogPositiveClick(this);

            };
            return view;

        }
        
    }
}