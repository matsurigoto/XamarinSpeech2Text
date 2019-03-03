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
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Speech2TextApp.AndroidIna.Dialog;
using Speech2TextApp.AndroidIna.Helper;
using Speech2TextApp.Service;

namespace Speech2TextApp.AndroidIna
{
    /// <summary>
    /// 語音備忘錄
    /// </summary>
    [Activity(Label = "MessageActivity")]
    public class MessageActivity : BaseActivity, NoticeDialogListener
    {
        private RecordAdapter adapter;
        private RecyclerView.LayoutManager layoutManager;
        private RecyclerView recycleView;

        RecordDialog recordDialog = new RecordDialog();
        private readonly int VOICE = 10;
        EditText _description;
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MessageActivity);
            if (DataService.dataCurrent.Message == null)
            {
                DataService.dataCurrent.Message = new List<string>();
            }

            this.Title = "語音備忘錄";
            this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            if (DataService.dataCurrent.Message.Count > 0)
            {
                this.Title = $"語音備忘錄({DataService.dataCurrent.Message.Count})";
            }

            recycleView = FindViewById<RecyclerView>(Resource.Id.records);
            InitMessages();


            var record = FindViewById<FloatingActionButton>(Resource.Id.btn_record);

            
            _description = FindViewById<EditText>(Resource.Id.edittext_desc);
            record.Click += RecordEvent;
            
        }

        /// <summary>
        /// 帶入已儲存訊息
        /// </summary>
        private void InitMessages()
        {
            recycleView.RemoveAllViews();
            recycleView.HasFixedSize = true;
            layoutManager = new LinearLayoutManager(this);
            recycleView.SetLayoutManager(layoutManager);
            adapter = new RecordAdapter(this.ApplicationContext, Resource.Layout.record, DataService.dataCurrent.Message);
            recycleView.SetAdapter(adapter);

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
                recordDialog.edittext_desc = textInput;
            }
            else
            {
                recordDialog.edittext_desc = "No speech was recognized";
            }
        }

        /// <summary>
        /// 開啟語音錄製工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RecordEvent(object sender, EventArgs e)
        {
            FragmentTransaction transcation = FragmentManager.BeginTransaction();
            recordDialog.SetStyle(DialogFragmentStyle.NoTitle,0);
            recordDialog.Show(transcation, "Dialog Fragment");
        }

        /// <summary>
        /// 點選確認
        /// 將新的語音資料存入json
        /// </summary>
        /// <param name="dialog"></param>
        public void OnDialogPositiveClick(DialogFragment dialog)
        {
            var recordDialog = dialog as RecordDialog;
            DataService.dataCurrent.Message.Add(recordDialog.edittext_desc);
            InitMessages();
            var descDocument = GetExternalFilesDir(null).AbsolutePath;
            string json = JsonConvert.SerializeObject(DataService.dataCurrent);
            string destPath = Path.Combine(descDocument, $"{ DataService.dataCurrent.Id}.json");
            File.WriteAllText(destPath, json);
            recordDialog.Dismiss();
           
        }
    }
}