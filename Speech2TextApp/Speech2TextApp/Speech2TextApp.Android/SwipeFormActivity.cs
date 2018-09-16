using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Speech2TextApp.Droid.Adapter;

namespace Speech2TextApp.Droid
{
    [Activity(Label = "SwipeFormActivity")]
    public class SwipeFormActivity : FragmentActivity
    {
        ViewPager viewPager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SwipeFormActivity);
            viewPager = FindViewById<ViewPager>(Resource.Id.swipeFormViewPager);
            SwipeAdapter swipeFormActivity = new SwipeAdapter(this.SupportFragmentManager);
            viewPager.Adapter = swipeFormActivity;
        }
    }
}