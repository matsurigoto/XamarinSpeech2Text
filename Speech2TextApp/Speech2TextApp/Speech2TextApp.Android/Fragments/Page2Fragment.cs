using Android.Support.V4.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace Speech2TextApp.Droid.Fragments
{
    public class Page2Fragment : Fragment
    {
        TextView circle;
        public Page2Fragment()
        {

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.Page2Fragment, container, false);
            return view;
        }
    }
}