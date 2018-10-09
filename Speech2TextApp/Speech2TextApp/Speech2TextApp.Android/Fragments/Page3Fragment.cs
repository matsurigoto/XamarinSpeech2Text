using Android.Support.V4.App;
using Android.OS;
using Android.Views;

namespace Speech2TextApp.Droid.Fragments
{
    public class Page3Fragment : Fragment
    {
        public Page3Fragment()
        {

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.Page3Fragment, container, false);
            return view;
        }
    }
}