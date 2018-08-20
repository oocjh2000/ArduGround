using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Content;
using System.Threading;
using Java.Net;

namespace ArduGround
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        Toast toast;

        

        BackPressCloseHandler closeHandler;
        BackPressCloseHandler backPress;

        Handler handler = new Handler();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
         
            FindViewById<Button>(Resource.Id.DieButton).Click += MainActivity_Click;
            FindViewById<TextView>(Resource.Id.ShowHelth).Text = Register.player.hp.ToString();
            FindViewById<TextView>(Resource.Id.ShowUsername).Text = Register.player.name;

            backPress = new BackPressCloseHandler(this);
            var mth = new Thread(new ThreadStart(RefreshThread));
            mth.Start();

            closeHandler = new BackPressCloseHandler(this);

        }
        public override void OnBackPressed()
        {
            backPress.OnBackPressed();
        }
        void RefreshThread()
        {
            while (Thread.CurrentThread.IsAlive)
            {
                handler.Post(delegate () { FindViewById<TextView>(Resource.Id.ShowHelth).Text = Register.player.hp.ToString(); });
                Thread.Sleep(1000);
            }
        }

        private void MainActivity_Click(object sender, System.EventArgs e)
        {

            closeHandler.OnBackPressed();
        }

        private void ConnetButton_Click(object sender, System.EventArgs e)
        {
           
            StartActivity(typeof(ConnetActivity));
        }
        
    }
}

