﻿using Android.App;
using Android.OS;
using Android.Widget;
using System;
using Android.Bluetooth;
using Android.Content;
using System.Collections.Generic;
using Java.Util;
using System.Threading;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace ArduGround
{
    [Activity(Label = "ArduGround", MainLauncher =true)]
    public class ConnetActivity : Activity
    {
        
        ICollection<BluetoothDevice> mDevice;
        Thread Worker;
        ListView listView;
        Button SearchButton;

        BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
        BluetoothSocket mSocket;
        Toast toast;
        
        List<string> items;

        Stream mOutputStream;
        Stream mInputStream;

        BackPressCloseHandler backPress;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Btconnet);

            listView = FindViewById<ListView>(Resource.Id.BTview);
           
            listView.ItemClick += ListView_ItemClick;
            SearchButton = FindViewById<Button>(Resource.Id.Search);
            SearchButton.Click += SearchButton_Click;
            backPress = new BackPressCloseHandler(this);

            // Create your application here
        }
        public override void OnBackPressed()
        {
            backPress.OnBackPressedAsync();
        }
        BluetoothDevice GetBluetoothDevice(string Name)
        {
            BluetoothDevice Selecteddevice = null;
          foreach(BluetoothDevice device in mDevice)
            {
                if (Name.Equals(device.Name))
                {
                    Selecteddevice = device;
                    break;
                }
            }
            return Selecteddevice;
        }
        void ConnetToSelectedDevice(string DeviceName)
        {
            var mRemoteDevice = GetBluetoothDevice(DeviceName);
            var DeviceCount = mDevice.Count;
            UUID Serial = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");
            try
            {
                
                mSocket = mRemoteDevice.CreateInsecureRfcommSocketToServiceRecord(Serial);
                mSocket.Connect();

                 mOutputStream = mSocket.OutputStream;
                 mInputStream = mSocket.InputStream;
                toast = Toast.MakeText(this, "연결성공", ToastLength.Short);
                toast.Show();

                Handler hendler = new Handler();

           

               Worker = new Thread(new ThreadStart(async delegate () {
                   {
                       int buffer;

                       while (Thread.CurrentThread.IsAlive)
                       {
                           if (!mSocket.IsConnected)
                               mSocket.Connect();
                           if (mInputStream.IsDataAvailable() && Register.IsServerConnet)
                           {
                               buffer = mInputStream.ReadByte();
                               Register.player.hp -= 10;

                               HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
                               httpRequestMessage.RequestUri = new Uri("http://" + Register.serverUrl + "/users/" + Register.player.id.ToString());
                               var content = new StringContent(JsonConvert.SerializeObject(Register.player), Encoding.UTF8, "application/json");
                               httpRequestMessage.Content = content;

                               HttpClient client = new HttpClient();
                               HttpResponseMessage responseMessage = await client.PutAsync(httpRequestMessage.RequestUri, httpRequestMessage.Content);

                               Register.player = JsonConvert.DeserializeObject<Player>(await responseMessage.Content.ReadAsStringAsync());
                               httpRequestMessage.Method = HttpMethod.Put;
                               Thread.Sleep(500);

                           }

                       }

                   }
               }));
               Worker.Start();

                var intent = new Intent(this, typeof(Register));
                intent.SetFlags(ActivityFlags.NoHistory);
                StartActivity(intent);

            }
            catch(Exception e)
            {
                toast = Toast.MakeText(this, e.Message, ToastLength.Short);
                toast.Show();
            }
            

        }
        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            ConnetToSelectedDevice(items[e.Position]);
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (!bluetoothAdapter.IsEnabled)
            {
                toast = Toast.MakeText(this, "블루투스 활성화", ToastLength.Short);
                toast.Show();
                var Btintent = new Intent(BluetoothAdapter.ActionRequestEnable);
                StartActivity(Btintent);
            }
            else {
                if (bluetoothAdapter.BondedDevices.Count > 0)
                {
                    toast = Toast.MakeText(this, "페어링된 장치목록", ToastLength.Short);
                    toast.Show();

                    mDevice = bluetoothAdapter.BondedDevices;
                   items=new List<string>();
                    foreach(BluetoothDevice device in mDevice)
                    {
                        items.Add(device.Name);
                    }
                    listView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items);
                }
                else
                {
                    toast = Toast.MakeText(this, "페어링된 장치가 없음", ToastLength.Short);
                    toast.Show();
                }
            }
            
        }




    }//클래스
}//네임스페이스