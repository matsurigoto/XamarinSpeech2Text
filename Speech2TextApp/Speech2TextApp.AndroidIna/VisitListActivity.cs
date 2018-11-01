using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Speech2TextApp.AndroidIna.Helper;
using Speech2TextApp.Data;
using Speech2TextApp.Interface;
using Speech2TextApp.Service;

namespace Speech2TextApp.AndroidIna
{
    [Activity(Label = "VisitListActivity", Theme = "@style/AppTheme", MainLauncher = true)]
    public class VisitListActivity : AppCompatActivity
    {
        
        private DataAdapter adapter;
        private RecyclerView.LayoutManager layoutManager;
        private RecyclerView recycleView;

        private Button visitN;
        private Button visitY;

        private IData dataService;
        public List<ApplyResult> datas { get; set; }
        public List<ApplyResult> datasY { get; set; }
        public List<ApplyResult> datasN { get; set; }
        public static ApplyResult dataCurrent { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.VisitListActivity);
            this.Title = "台北市社會輔助訪視調查表";
            recycleView = FindViewById<RecyclerView>(Resource.Id.data_list);
            visitN = FindViewById<Button>(Resource.Id.visit_status_n);
            visitY = FindViewById<Button>(Resource.Id.visit_status_Y);

            Init();

            this.datasY = datas.Where(x => x.Status == "Y").ToList();
            this.datasN = datas.Where(x => x.Status == "N").ToList();

            if (this.datasY.Count > 0) {
                visitY.Text = $"已送出訪視表({this.datasY.Count})";
            }
            if (this.datasN.Count > 0)
            {
                visitN.Text = $"需訪視列表({this.datasN.Count})";
            }

            visitN.Click += delegate {
                visitN.SetBackgroundResource(Resource.Drawable.blue_button_activity);
                visitN.SetTextColor(Color.White);
                visitY.SetBackgroundResource(Resource.Drawable.blue_button);
                visitY.SetTextColor(Color.Black);
                InitVisitList(this.datasN, Resource.Layout.data);
            };
            visitY.Click += delegate {
                visitY.SetBackgroundResource(Resource.Drawable.blue_button_activity);
                visitY.SetTextColor(Color.White);
                visitN.SetBackgroundResource(Resource.Drawable.blue_button);
                visitN.SetTextColor(Color.Black);
                InitVisitList(this.datasY, Resource.Layout.data_ok);
            };
            visitN.PerformClick();

        }

        private void Init() {
            var path = GetExternalFilesDir(null).AbsolutePath;
            dataService = new DataService();
            datas = dataService.GetDatas(path);

            if (datas.Count == 0)
            {
                dataService.GenData(path);
                datas = dataService.GetDatas(path);
            }
        }

        private void InitVisitList(List<ApplyResult> datas, int layoutResource)
        {
            
            recycleView.HasFixedSize = true;
            layoutManager = new LinearLayoutManager(this);
            recycleView.SetLayoutManager(layoutManager);
            adapter = new DataAdapter(this.ApplicationContext, layoutResource, datas);
            recycleView.SetAdapter(adapter);
        }
    }
}