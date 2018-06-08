using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;

namespace ArduGround
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            FindViewById<Button>(Resource.Id.ConnetButton).Click += ConnetButton_Click;
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
        }

        private void ConnetButton_Click(object sender, System.EventArgs e)
        {
           
        }
    }
}

