using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Speech2TextApp.Droid.Fragments;

namespace Speech2TextApp.Droid.Adapter
{
    public class SwipeAdapter : FragmentStatePagerAdapter
    {
        private Fragment[] list = new Fragment[] {
            new Page1Fragment(),
            new Page2Fragment(),
            new Page3Fragment(),
            new Page4Fragment(),
            new Page5Fragment()
        };

        public SwipeAdapter(FragmentManager fm) : base(fm) { }

        public override int Count => 5;

        public override Fragment GetItem(int position)
        {
            Bundle bundle = new Bundle();
            bundle.PutInt("count", position);
            list[position].Arguments = bundle;
            return list[position];
        }
    }
}