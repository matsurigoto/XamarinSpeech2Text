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
using Speech2TextApp.Service;

namespace Speech2TextApp.AndroidIna.Helper
{
    class DataViewHolder : RecyclerView.ViewHolder
    {
        public LinearLayout layout { get; set; }
        public TextView applyName { get; set; }
        public TextView applyDate { get; set; }
        public TextView applyCount { get; set; }
        public ImageButton record { get; set; }
        
        public DataViewHolder(View itemView) : base(itemView ) {
            applyName = itemView.FindViewById<TextView>(Resource.Id.apply_name);
            applyDate = itemView.FindViewById<TextView>(Resource.Id.apply_date);
            applyCount = itemView.FindViewById<TextView>(Resource.Id.apply_times);
            record = itemView.FindViewById<ImageButton>(Resource.Id.record);
            layout = itemView.FindViewById<LinearLayout>(Resource.Id.data);
        }
    }

    class DataAdapter : RecyclerView.Adapter
    {
       
        public List<ApplyResult> datas { get; set; }
        public Context mContent { get; set; }
        public int layoutResource { get; set; }
        public DataAdapter(Context context, int layoutResource,List<ApplyResult> datas)
        {
            this.datas = datas;
            this.mContent = context;
            this.layoutResource = layoutResource;
        }
        public override int ItemCount {
            get {
                return datas.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            DataViewHolder viewHolder = holder as DataViewHolder;
            viewHolder.applyName.Text = datas[position].ApplyName;
            if (datas[position].VisitDetail.VisitDate != null)
            {
                viewHolder.applyDate.Text = datas[position].VisitDetail.VisitDate.GetValueOrDefault().ToString("yyyy/MM/dd HH:mm");
            }
            else
            {
                viewHolder.applyDate.Text = "-";
            }
            viewHolder.applyCount.Text = datas[position].VisitDetails.Count.ToString() ;

            viewHolder.layout.Click += delegate {
                if (datas[position].Message == null) {
                    datas[position].Message = new List<string>();
                }
                DataService.dataCurrent = datas[position];
                var intent = new Intent(mContent, typeof(Page1Activity));
                mContent.StartActivity(intent);
            };

            if (viewHolder.record != null) {
                viewHolder.record.Click += delegate {
                    if (datas[position].Message == null)
                    {
                        datas[position].Message = new List<string>();
                    }
                    DataService.dataCurrent = datas[position];
                    var intent = new Intent(mContent, typeof(MessageActivity));
                    mContent.StartActivity(intent);
                };
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(layoutResource, parent,false);
            return new DataViewHolder(itemView);
        }
    }

}