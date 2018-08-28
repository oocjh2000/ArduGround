using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Threading;
using System.Net.Http;
using Newtonsoft.Json;

namespace ArduGround
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        Toast toast;
        
        TextView textView;

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

            textView = FindViewById<TextView>(Resource.Id.textView1);

            backPress = new BackPressCloseHandler(this);
            var mth = new Thread(new ThreadStart(RefreshThreadAsync));
            var th = new Thread(new ThreadStart(CountThreadAsync));
            mth.Start();
            th.Start();

            closeHandler = new BackPressCloseHandler(this);

        }
        protected override async void OnDestroy()
        {
            if (Register.IsServerConnet)
            {
                var req = new HttpRequestMessage();
                req.RequestUri = new System.Uri("http://" + Register.serverUrl + "/users/" + Register.player.id.ToString());
                var cli = new HttpClient();
                var res = await cli.DeleteAsync(req.RequestUri);
            }
        }
        public override void OnBackPressed()
        {
            backPress.OnBackPressedAsync();
        }
        async void RefreshThreadAsync()
        {
            while (Thread.CurrentThread.IsAlive)
            {
                if (Register.player.hp > 0)
                {
                    handler.Post(delegate () { FindViewById<TextView>(Resource.Id.ShowHelth).Text = Register.player.hp.ToString(); });
                }
                else
                {
                    Register.IsServerConnet = false;
                    handler.Post(delegate ()
                    {
                        FindViewById<TextView>(Resource.Id.ShowHelth).Text = "0";
                        FindViewById<TextView>(Resource.Id.textView1).Text = "사망^^";
                    });
                    var req = new HttpRequestMessage();
                    req.RequestUri = new System.Uri("http://" + Register.serverUrl + "/users/" + Register.player.id.ToString());
                    var cli = new HttpClient();
                    var res = await cli.DeleteAsync(req.RequestUri);
                    break;
                }
                Thread.Sleep(1000);
            }
        }
        async void CountThreadAsync()
        {
            var req = new HttpRequestMessage();
            req.RequestUri = new System.Uri("http://" + Register.serverUrl + "/users/count");
            var cli = new HttpClient();
            while (Thread.CurrentThread.IsAlive)
            {
                if (!Register.IsServerConnet)
                    break;
                HttpResponseMessage res = await cli.GetAsync(req.RequestUri);
                var count = JsonConvert.DeserializeObject<Count>(await res.Content.ReadAsStringAsync());
                handler.Post(delegate () { textView.Text = "생존자: " + count.count; });
                Thread.Sleep(1000);
            }
        }

        private void MainActivity_Click(object sender, System.EventArgs e)
        {

            closeHandler.OnBackPressedAsync();
        }
        
    }
}

