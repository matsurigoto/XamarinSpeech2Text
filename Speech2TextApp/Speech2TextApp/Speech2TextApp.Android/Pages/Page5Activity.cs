using Android.OS;
using Android.App;

namespace Speech2TextApp.Droid.Pages
{
    [Activity(Label = "Page5Activity")]
    public class Page5Activity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Page5Activity);
        }
    }
}