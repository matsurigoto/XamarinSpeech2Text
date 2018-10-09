using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Widget;
using Speech2TextApp.Droid.Adapter;
using System;

namespace Speech2TextApp.Droid
{
    [Activity(Label = "SwipeFormActivity")]
    public class SwipeFormActivity : FragmentActivity
    {
        ViewPager viewPager;
        TextView circle1;
        TextView circle2;
        TextView circle3;
        TextView circle4;
        TextView circle5;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SwipeFormActivity);
            viewPager = FindViewById<ViewPager>(Resource.Id.swipeFormViewPager);
            SwipeAdapter swipeFormActivity = new SwipeAdapter(this.SupportFragmentManager);
            viewPager.Adapter = swipeFormActivity;
            circle1 = FindViewById<TextView>(Resource.Id.circle1);
            circle2 = FindViewById<TextView>(Resource.Id.circle2);
            circle3 = FindViewById<TextView>(Resource.Id.circle3);
            circle4 = FindViewById<TextView>(Resource.Id.circle4);
            circle5 = FindViewById<TextView>(Resource.Id.circle5);
        }


        //private void scroll()
        //{
        //    mNoHorizontalScrollView.scrollTo(0, 0);
        //}

        //public override void OnWindowFocusChanged(bool hasFocus)
        //{
        //    base.OnWindowFocusChanged(hasFocus);
        //    if (hasFocus && first)
        //    {
        //        first = false;
        //        scroll();
        //    }
        //}

        public void ClickNextButton(object sender, EventArgs e)
        {
            if (viewPager.CurrentItem < 4)
            {
                viewPager.CurrentItem = viewPager.CurrentItem + 1;
            }         
        }

        public void ClickPrevButton(object sender, EventArgs e)
        {
            if (viewPager.CurrentItem > 0)
            {
                viewPager.CurrentItem = viewPager.CurrentItem -1 ;
            }
        }

    }
}