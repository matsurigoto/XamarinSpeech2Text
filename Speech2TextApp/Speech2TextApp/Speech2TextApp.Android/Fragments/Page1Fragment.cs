using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Java.Util;
using System;


namespace Speech2TextApp.Droid.Fragments
{
    public class Page1Fragment : Fragment
    {
        TextView circle;
        LinearLayout addressLayout;
        Button address1;
        Button address2;
        Button address3;
        Button next;
        EditText visitDate;
        RadioButton radioButton1;
        RadioButton radioButton2;
        LinearLayout descLayout;

        public Page1Fragment()
        {

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Page1Fragment, container, false);

            #region address
            addressLayout = (LinearLayout)view.FindViewById(Resource.Id.showAddress);
            address1 = view.FindViewById<Button>(Resource.Id.address1);
            address2 = view.FindViewById<Button>(Resource.Id.address2);
            address3 = view.FindViewById<Button>(Resource.Id.address3);
            address1.Click += delegate {
                address1.SetBackgroundResource(Resource.Drawable.blue_button);
                address2.SetBackgroundResource(Resource.Drawable.blue_button);
                address3.SetBackgroundResource(Resource.Drawable.blue_button);

                address1.SetBackgroundResource(Resource.Drawable.blue_button_activity);

                addressLayout.RemoveAllViews();
                TextView address = new TextView(this.Context);
                address.Text = "台北市中山區新生里";
                addressLayout.AddView(address);
            };

            address2.Click += delegate {
                address1.SetBackgroundResource(Resource.Drawable.blue_button);
                address2.SetBackgroundResource(Resource.Drawable.blue_button);
                address3.SetBackgroundResource(Resource.Drawable.blue_button);

                address2.SetBackgroundResource(Resource.Drawable.blue_button_activity);

                addressLayout.RemoveAllViews();
                TextView address = new TextView(this.Context);
                address.Text = "台北市中山區新生里";
                addressLayout.AddView(address);
            };

            address3.Click += delegate {
                address1.SetBackgroundResource(Resource.Drawable.blue_button);
                address2.SetBackgroundResource(Resource.Drawable.blue_button);
                address3.SetBackgroundResource(Resource.Drawable.blue_button);

                address3.SetBackgroundResource(Resource.Drawable.blue_button_activity);


                addressLayout.RemoveAllViews();
                EditText addressArea = new EditText(this.Context);
                addressArea.Hint = "區";
                addressLayout.AddView(addressArea);
                //TextView address = new TextView(this.Context);
                //address.Text = "\\";
                //addressLayout.AddView(address);
                addressArea = new EditText(this.Context);
                addressArea.Hint = "里";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
                addressArea = new EditText(this.Context);
                addressArea.Hint = "鄰";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
                addressArea = new EditText(this.Context);
                addressArea.Hint = "路";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
                addressArea = new EditText(this.Context);
                addressArea.Hint = "段";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
                addressArea = new EditText(this.Context);
                addressArea.Hint = "巷";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
                addressArea = new EditText(this.Context);
                addressArea.Hint = "弄";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
                addressArea = new EditText(this.Context);
                addressArea.Hint = "號";
                addressLayout.AddView(addressArea);
                //addressLayout.AddView(address);
                addressArea = new EditText(this.Context);
                addressArea.Hint = "樓";
                addressLayout.AddView(addressArea);
                TextView address = new TextView(this.Context);
                address.Text = "之";
                addressLayout.AddView(address);
                addressArea = new EditText(this.Context);
                addressLayout.AddView(addressArea);
            };

            #endregion

            #region visit date
            Calendar myCalendar = Calendar.Instance;
            visitDate = view.FindViewById<EditText>(Resource.Id.visitDate);

            #endregion

            #region RadioButton
            radioButton1 = view.FindViewById<RadioButton>(Resource.Id.radioButton1);
            radioButton2 = view.FindViewById<RadioButton>(Resource.Id.radioButton2);
            descLayout = view.FindViewById<LinearLayout>(Resource.Id.areaDesc);
            radioButton1.Click += VisitStatusClick;
            radioButton2.Click += VisitStatusClick;
            #endregion

            next = view.FindViewById<Button>(Resource.Id.btn_fragment_1_next);
            var nextEvent = (SwipeFormActivity)Activity;
            next.Click += nextEvent.ClickNextButton;

            return view;
        }

        private void VisitStatusClick(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Id == Resource.Id.radioButton1)
            {
                descLayout.Visibility = ViewStates.Invisible;
            }
            else if (rb.Id == Resource.Id.radioButton2)
            {
                descLayout.Visibility = ViewStates.Visible;
            }
            else {
                descLayout.Visibility = ViewStates.Invisible;
            }

        }

    }
}