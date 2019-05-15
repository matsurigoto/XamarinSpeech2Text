using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Speech2TextApp.Droid.Dialog
{
    public class RecordDialog : DialogFragment
    {
        public string edittext_desc { get; set; }

        // Use this instance of the interface to deliver action events
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