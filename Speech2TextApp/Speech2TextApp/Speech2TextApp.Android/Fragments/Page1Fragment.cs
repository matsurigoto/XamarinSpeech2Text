using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace Speech2TextApp.Droid.Fragments
{
    public class Page1Fragment : Fragment
    {
        TextView textview;

        public Page1Fragment()
        {

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Page1Fragment, container, false);
            return view;
        }
    }
}