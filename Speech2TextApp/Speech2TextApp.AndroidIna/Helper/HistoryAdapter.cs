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
    class HistoryViewHolder : RecyclerView.ViewHolder
    {
        public TextView visitCount { get; set; }
        public TextView visitTime { get; set; }
        public TextView visitPlace { get; set; }
        public TextView visitDesc { get; set; }
        public HistoryViewHolder(View itemView) : base(itemView)
        {
            visitCount = itemView.FindViewById<TextView>(Resource.Id.history_visit_count);
            visitTime = itemView.FindViewById<TextView>(Resource.Id.history_visit_time);
            visitPlace = itemView.FindViewById<TextView>(Resource.Id.history_visit_place);
            visitDesc = itemView.FindViewById<TextView>(Resource.Id.history_visit_desc);
        }
    }

    class HistoryAdapter : RecyclerView.Adapter
    {
        public List<ApplyDetail> datas { get; set; }

        public HistoryAdapter( List<ApplyDetail> datas)
        {
            this.datas = datas;
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
            HistoryViewHolder viewHolder = holder as HistoryViewHolder;
            viewHolder.visitCount.Text = $"第{position+1}次訪視";
            viewHolder.visitTime.Text = datas[position].VisitDate.GetValueOrDefault().ToString("yyyy/MM/dd HH:mm");
            string address = string.Empty;
            string AddressType = datas[position].AddressType;
            if (AddressType == "戶籍地址")
            {
                address = datas[position].Address1;
            }
            else if (AddressType == "居住地址")
            {
                address = datas[position].Address2;
            }
            else if (AddressType == "其他")
            {
                address = datas[position].Address3;
            }
            viewHolder.visitPlace.Text = $"{AddressType} {address}";
            viewHolder.visitDesc.Text = datas[position].VisitDesc;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.history, parent, false);
            return new HistoryViewHolder(itemView);
        }
    }
}