using Android.App;
using Android.OS;
using Android.Widget;
using System;

namespace ArduGround
{
    [Activity(Label = "ConnetActivity")]
    public class ConnetActivity : Activity
    {
        public static ListView BTlist;
        Button SearchButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Btconnet);

            BTlist = FindViewById<ListView>(Resource.Id.BTview);
            BTlist.Adapter=new ArrayAdapter<string>(this,Android.Resource.Layout.SimpleListItem1,Android.Resource.Id.Text1,BluetoothService.)

            SearchButton = FindViewById<Button>(Resource.Id.search_button);
            SearchButton.Click += SearchButton_Click;
            // Create your application here
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}