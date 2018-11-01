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
    class MemberViewHolder : RecyclerView.ViewHolder
    {
        public LinearLayout layout { get; set; }
        public TextView name { get; set; }
        public TextView title { get; set; }
        public TextView livetogether { get; set; }
        public TextView healthStatus { get; set; }
        public TextView workStatus { get; set; }
        public TextView isInNursingHome { get; set; }

        public MemberViewHolder(View itemView) : base(itemView)
        {
            layout = itemView.FindViewById<LinearLayout>(Resource.Id.member);
            name = itemView.FindViewById<TextView>(Resource.Id.name);
            title = itemView.FindViewById<TextView>(Resource.Id.title);
            livetogether = itemView.FindViewById<TextView>(Resource.Id.live_together);
            healthStatus = itemView.FindViewById<TextView>(Resource.Id.health_status);
            workStatus = itemView.FindViewById<TextView>(Resource.Id.work_status);
            isInNursingHome = itemView.FindViewById<TextView>(Resource.Id.in_nursing);
        }
    }

    class MemberAdapter : RecyclerView.Adapter
    {

        public List<MemberDetail> datas { get; set; }
        public Context mContent { get; set; }
        public int layoutResource { get; set; }
        public MemberAdapter(Context context, int layoutResource, List<MemberDetail> datas)
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
            MemberViewHolder viewHolder = holder as MemberViewHolder;
            viewHolder.name.Text = datas[position].Name;
            viewHolder.title.Text = string.Format("稱    謂:{0}", (string.IsNullOrEmpty(datas[position].Title) ? "-" : datas[position].Title));
            viewHolder.livetogether.Text = string.Format("是否同住:{0}", (string.IsNullOrEmpty(datas[position].LiveTogether) ? "-" : datas[position].LiveTogether));
            viewHolder.workStatus.Text = string.Format("就業狀況:{0}", (string.IsNullOrEmpty(datas[position].WorkStatus) ? "-" : datas[position].WorkStatus));
            viewHolder.healthStatus.Text = string.Format("健康狀況:{0}", (string.IsNullOrEmpty(datas[position].HealthStatus) ? "-" : datas[position].HealthStatus));
            viewHolder.isInNursingHome.Text = string.Format("是否安置療養院所:{0}", (string.IsNullOrEmpty(datas[position].IsInNursingHome) ? "-" : datas[position].IsInNursingHome));
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(layoutResource, parent, false);
            return new MemberViewHolder(itemView);
        }
    }
}