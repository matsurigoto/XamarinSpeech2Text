using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Speech2TextApp.AndroidIna
{
    [Activity(Label = "VisitListActivity")]
    public class VisitListActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.VisitListActivity);
            this.Title = "需訪視歷表";
        }
    }
}