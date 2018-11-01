using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Speech2TextApp.Data;

namespace Speech2TextApp.AndroidIna.Helper
{
    class RecordViewHolder : RecyclerView.ViewHolder
    {
        public TextView count { get; set; }
        public TextView message { get; set; }

        public RecordViewHolder(View itemView) : base(itemView)
        {
            count = itemView.FindViewById<TextView>(Resource.Id.count);
            message = itemView.FindViewById<TextView>(Resource.Id.message);
        }
    }

    class RecordAdapter : RecyclerView.Adapter
    {

        public List<string> datas { get; set; }
        public Context mContent { get; set; }
        public int layoutResource { get; set; }
        public RecordAdapter(Context context, int layoutResource, List<string> datas)
        {
            this.datas = datas;
            this.mContent = context;
            this.layoutResource = layoutResource;
        }
        public override int ItemCount
        {
            get
            {
                return datas.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecordViewHolder viewHolder = holder as RecordViewHolder;
            viewHolder.count.Text = (position+1).ToString();
            viewHolder.message.Text = datas[position];
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(layoutResource, parent, false);
            return new RecordViewHolder(itemView);
        }
    }
}