using Android.OS;
using Android.Support.V4.App;

namespace Speech2TextApp.Droid.Adapter
{
    public class SwipeAdapter : FragmentStatePagerAdapter
    {
        public SwipeAdapter(FragmentManager fm) : base(fm) { }

        public override int Count => 5;

        public override Fragment GetItem(int position)
        {
            Fragment fragment = new Fragment();
            Bundle bundle = new Bundle();
            bundle.PutInt("count", position);
            fragment.Arguments = bundle;
            return fragment;
        }
    }
}