using Android.App;
using Android.OS;
using Android.Widget;
using System;
using System.Text;
using System.Threading;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Android.Util;
using Android.Content;

namespace ArduGround
{
    [Activity(Label = "ArduGround")]
    public class Register : Activity
    {
        BackPressCloseHandler backPressCloseHandler;
        Button RegisterButton;
        TextView ServerAdress, UserName;
        Handler handler = new Handler();
        Toast Toast;
        public static string serverUrl;
        public static Player player;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegisterLayout);

            RegisterButton = FindViewById<Button>(Resource.Id.RegisterButton);
            ServerAdress = FindViewById<TextView>(Resource.Id.ServerIpView);
            UserName = FindViewById<TextView>(Resource.Id.UserIdView);
            ServerAdress.Text = "211.225.140.135";
            RegisterButton.Click += RegisterButton_ClickAsync;
            backPressCloseHandler = new BackPressCloseHandler(this);

            // Create your application here
        }
        public override void OnBackPressed()
        {
            backPressCloseHandler.OnBackPressed();
        }


        private async void RegisterButton_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                player = new Player() { name = UserName.Text, hp = 100 };
                string tmp = JsonConvert.SerializeObject(player);
                var content = new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri("http://" + ServerAdress.Text + "/users");
                request.Content = content;
                //request.Content.Headers.Add("Content-Type", "");




                serverUrl = ServerAdress.Text;
                var client = new HttpClient();
                HttpResponseMessage responseMessage = await client.PostAsync(request.RequestUri, request.Content);
              
                player = JsonConvert.DeserializeObject<Player>(await responseMessage.Content.ReadAsStringAsync());

               
                Toast = Toast.MakeText(this, responseMessage.StatusCode.ToString(), ToastLength.Long);
                Toast.Show();
                
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    Intent intent = new Intent(this, typeof(MainActivity));
                    intent.SetFlags(ActivityFlags.NoHistory);
                    StartActivity(intent);
                }
            }catch(Exception ex)
            {
                Log.Debug("Exeption", ex.Message);
                Toast = Toast.MakeText(this, ex.Message, ToastLength.Long);
                Toast.Show();
            }
            
        }

    }
}