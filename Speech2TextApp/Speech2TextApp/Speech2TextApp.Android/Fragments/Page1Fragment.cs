using Android.App;
using Android.OS;
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
            textview = view.FindViewById<TextView>(Resource.Id.textPage1Fragement);
            textview.Text = Arguments.GetInt("count").ToString();
            return inflater.Inflate(Resource.Layout.Page1Fragment, container, false);
        }
    }
}