using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Speech2TextApp.Droid.Pages
{
    [Activity(Label = "DetailActivity")]
    public class DetailActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DetailActivity);
            this.IsEdit = false;

            this.Title = "已送出訪視資料";
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            var _pageLayout = FindViewById<LinearLayout>(Resource.Id.detail_layout);

            _pageLayout.AddView(GetListPage());

            _pageLayout.AddView(GetFirstPage());
            _pageLayout.AddView(GetSecondPage());
            _pageLayout.AddView(GetThirdPage());
            _pageLayout.AddView(GetForthPage());


            // Create your application here
        }

        private LinearLayout GetListPage() {
            var layout = new LinearLayout(this)
            {
                Orientation = Orientation.Vertical,
                WeightSum = 2,
                LayoutParameters = new LinearLayout.LayoutParams(Android.Views.ViewGroup.LayoutParams.MatchParent, Android.Views.ViewGroup.LayoutParams.WrapContent)
            };

            var layoutParameter = new LinearLayout.LayoutParams(Android.Views.ViewGroup.LayoutParams.MatchParent,
                    Android.Views.ViewGroup.LayoutParams.WrapContent, 1.0f);
            layoutParameter.SetMargins(100, 100,0 , 100);
           
            AddTextView("訪視紀錄", layout);
            foreach (var visitList in MainActivity.dataCurrent.VisitDetails)
            {
                var layoutParameter2 = new LinearLayout.LayoutParams(Android.Views.ViewGroup.LayoutParams.MatchParent,
                  Android.Views.ViewGroup.LayoutParams.WrapContent, 1.0f);
                layoutParameter2.SetMargins(100, 0, 100, 0);
                var _pageLayout = new LinearLayout(this)
                {
                    Orientation = Orientation.Vertical,
                    LayoutParameters = layoutParameter2
                };
                _pageLayout.SetBackgroundResource(Resource.Drawable.main_border);
                AddTextView(string.Format("第{0}次訪視", MainActivity.dataCurrent.VisitDetails.IndexOf(visitList)), _pageLayout);
                AddTextView(visitList.VisitDate.ToString("yyyy/MM/dd HH:MM:ss"), _pageLayout);
                AddTextView("訪視地點", _pageLayout);
                AddTextView(visitList.AddressType, _pageLayout);
                if (visitList.AddressType == "戶籍地址")
                {
                    AddTextView(visitList.Address1, _pageLayout);
                }
                else if (visitList.AddressType == "居住地址")
                {
                    AddTextView(visitList.Address2, _pageLayout);
                }
                else {
                    AddTextView(visitList.Address3, _pageLayout);
                }



                //AddTextView(visitList.Address, _pageLayout);
                AddTextView("訪視概述", _pageLayout);
                AddTextView(visitList.VisitDesc, _pageLayout);
                layout.AddView(_pageLayout);




            }

            return layout;


        }

        private void AddTextView(string value,LinearLayout firstLayout, int textSize = 16)
        {
            var times = new TextView(this)
            {
                Text = value
            };
            times.SetTextColor(TextColor);
            times.SetTextSize(Android.Util.ComplexUnitType.Sp, textSize);
            firstLayout.AddView(times);
            //return firstLayout;
        }

       

}
}