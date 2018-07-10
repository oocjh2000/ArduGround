using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Content;

namespace ArduGround
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Toast toast;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            FindViewById<Button>(Resource.Id.ConnetButton).Click += ConnetButton_Click;
            FindViewById<Button>(Resource.Id.DieButton).Click += MainActivity_Click;

        }

        private void MainActivity_Click(object sender, System.EventArgs e)
        {
            toast = Toast.MakeText(this, "미구현기능", ToastLength.Short);
            toast.Show();
        }

        private void ConnetButton_Click(object sender, System.EventArgs e)
        {
           
            StartActivity(typeof(ConnetActivity));
        }
    }
}

