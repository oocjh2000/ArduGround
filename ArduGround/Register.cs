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

namespace ArduGround
{
    [Activity(Label = "ArduGround", MainLauncher = true)]
    public class Register : Activity
    {
       
        Button RegisterButton;
        TextView ServerAdress, UserName;
        Handler handler = new Handler();
        Toast Toast;
        public static Player player;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegisterLayout);

            RegisterButton = FindViewById<Button>(Resource.Id.RegisterButton);
            ServerAdress = FindViewById<TextView>(Resource.Id.ServerIpView);
            UserName = FindViewById<TextView>(Resource.Id.UserIdView);
            ServerAdress.Text = "192.168.0.10";
            RegisterButton.Click += RegisterButton_ClickAsync;

            // Create your application here
        }

  

        private async void RegisterButton_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                player = new Player { name = UserName.Text, hp = 100 };

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri("http://" + ServerAdress.Text + "/users");
                request.Method = HttpMethod.Post;
                request.Headers.Add("Created", "application/json");

                var content = new StringContent(JsonConvert.SerializeObject(player),Encoding.UTF8);
                request.Content = content;
                var client = new HttpClient();
                HttpResponseMessage responseMessage = await client.PostAsync(request.RequestUri,request.Content);

                Toast = Toast.MakeText(this, responseMessage.StatusCode.ToString(), ToastLength.Long);
                Toast.Show();
                
            }catch(Exception ex)
            {
                Log.Debug("a", ex.Message);
                Toast = Toast.MakeText(this, ex.Message, ToastLength.Long);
                Toast.Show();
            }
            
        }

    }
}