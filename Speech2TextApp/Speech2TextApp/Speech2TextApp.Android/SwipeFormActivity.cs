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

        public void NextClicked(object sender, EventArgs e)
        {
            if (viewPager.CurrentItem == 0)
            {
                return;
            }
            viewPager.CurrentItem = viewPager.CurrentItem + 1;
            //circle1.SetBackgroundResource(Resource.Drawable.circle);
            //circle1.SetTextColor(Color.Blue);
            //circle2.SetBackgroundResource(Resource.Drawable.circle);
            //circle2.SetTextColor(Color.Blue);
            //circle3.SetBackgroundResource(Resource.Drawable.circle);
            //circle3.SetTextColor(Color.Blue);
            //circle4.SetBackgroundResource(Resource.Drawable.circle);
            //circle4.SetTextColor(Color.Blue);
            //circle5.SetBackgroundResource(Resource.Drawable.circle);
            //circle5.SetTextColor(Color.Blue);

            //switch (viewPager.CurrentItem) {
            //    case 0:
            //        circle1.SetBackgroundResource(Resource.Drawable.circle_activity);
            //        circle1.SetTextColor(Color.White);
            //        break;
            //    case 1:
            //        circle2.SetBackgroundResource(Resource.Drawable.circle_activity);
            //        circle2.SetTextColor(Color.White);
            //        break;
            //    case 2:
            //        circle3.SetBackgroundResource(Resource.Drawable.circle_activity);
            //        circle3.SetTextColor(Color.White);
            //        break;
            //    case 3:
            //        circle4.SetBackgroundResource(Resource.Drawable.circle_activity);
            //        circle4.SetTextColor(Color.White);
            //        break;
            //    case 4:
            //        circle5.SetBackgroundResource(Resource.Drawable.circle_activity);
            //        circle5.SetTextColor(Color.White);
            //        break;
            
            //}

        }

    }
}