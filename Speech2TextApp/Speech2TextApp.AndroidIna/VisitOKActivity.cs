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
using Speech2TextApp.AndroidIna.Helper;
using Speech2TextApp.Service;

namespace Speech2TextApp.AndroidIna
{
    /// <summary>
    /// 已訪視列表
    /// </summary>
    [Activity(Label = "VisitOKActivity")]
    public class VisitOKActivity : BaseActivity
    {
        private HistoryAdapter adapter;
        private RecyclerView.LayoutManager layoutManager;
        private RecyclerView recycleView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.VisitOKActivity);
            this.Title = "已送出訪視資料";

            recycleView = FindViewById<RecyclerView>(Resource.Id.history);


            recycleView.HasFixedSize = true;
            layoutManager = new LinearLayoutManager(this);
            recycleView.SetLayoutManager(layoutManager);
            adapter = new HistoryAdapter(DataService.dataCurrent.VisitDetails);
            recycleView.SetAdapter(adapter);
        }
    }
}