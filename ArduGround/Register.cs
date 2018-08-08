using Android.App;
using Android.OS;
using Android.Widget;
using System;
using Java.Net;
using Org.Json;
using Java.IO;
using System.Text;

namespace ArduGround
{
    [Activity(Label = "Register", MainLauncher = true)]
    public class Register : Activity
    {
        Button RegisterButton;
        TextView ServerAdress, UserName;
        Toast Toast;
        public static int id;
        public static string name;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegisterLayout);

            RegisterButton = FindViewById<Button>(Resource.Id.RegisterButton);
            ServerAdress = FindViewById<TextView>(Resource.Id.ServerIpView);
            UserName = FindViewById<TextView>(Resource.Id.UserIdView);

            RegisterButton.Click += RegisterButton_Click;
               
            // Create your application here
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            register();
        }

        private void register()
        {
            try
            {
                URL uRL = new URL(ServerAdress.Text);
                HttpURLConnection connection = null;

                connection = (HttpURLConnection)uRL.OpenConnection();
                connection.ConnectTimeout = 3000;
                connection.ReadTimeout = 3000;
                connection.RequestMethod = "POST";
                connection.SetRequestProperty("Cache-Contril", "no-cache");
                connection.SetRequestProperty("Content-Type", "application/json");
                connection.SetRequestProperty("Accept", "application/json");
                connection.AddRequestProperty("name", UserName.Text);
                connection.DoInput = true;
                connection.DoOutput = true;

                JSONObject jSONObject = new JSONObject();
                jSONObject.Put("name", UserName.Text);

                connection.OutputStream.Write(Encoding.UTF8.GetBytes(jSONObject.ToString()));
                connection.OutputStream.Flush();

                string Res;

#pragma warning disable CS0618 // 형식 또는 멤버는 사용되지 않습니다.
                if (connection.ResponseCode == HttpURLConnection.HttpCreated)
#pragma warning restore CS0618 // 형식 또는 멤버는 사용되지 않습니다.
                {
                    var input = connection.InputStream;
                    var baos = new ByteArrayOutputStream();
                    byte[] bytebuf = new byte[1024];
                    byte[] bytedata = null;
                    int nLength = 0;
                    while ((nLength = input.Read(bytebuf, 0, bytebuf.Length)) != -1)
                        baos.Write(bytebuf, 0, nLength);

                    bytedata = baos.ToByteArray();

                    Res = new string(bytedata.ToString());

                    JSONObject responseObj = new JSONObject(Res);
                    id = (int)responseObj.Get("id");
                    name = (string)responseObj.Get("name");
                }
            }
            catch(Exception e) {
                Toast = Toast.MakeText(this, e.Message, ToastLength.Short);
                Toast.Show();

            }

        }
    }
}