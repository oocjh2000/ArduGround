using Android.App;
using Android.OS;
using Android.Widget;
using System;
using Android.Bluetooth;
using Android.Content;
using System.Collections.Generic;
using Android.Util;
using Java.Util;
using System.Threading;
using Java.Lang;
using System.IO;

namespace ArduGround
{
    [Activity(Label = "ConnetActivity")]
    public class ConnetActivity : Activity
    {

        ICollection<BluetoothDevice> mDevice;
        System.Threading.Thread Worker;
        ListView listView;
        Button SearchButton;

        BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
        BluetoothSocket mSocket;
        Toast toast;

        List<string> items;

        Stream mOutputStream;
        Stream mInputStream;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Btconnet);

            listView = FindViewById<ListView>(Resource.Id.BTview);
           
            listView.ItemClick += ListView_ItemClick;
            SearchButton = FindViewById<Button>(Resource.Id.Search);
            SearchButton.Click += SearchButton_Click;

            // Create your application here
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

           

               Worker = new System.Threading.Thread(new ThreadStart(run));
               Worker.Start();
                

            }
            catch(System.Exception e)
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
        void run()
        {
            int buffer;
            
            while (System.Threading.Thread.CurrentThread.IsAlive)
            {
                if (mInputStream.IsDataAvailable())
                {
                    MainActivity.HP--;
                    buffer=mInputStream.ReadByte();
                   

                }
              
            }

        }
       
    }//클래스
}//네임스페이스