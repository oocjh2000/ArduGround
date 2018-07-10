using Android.App;
using Android.OS;
using Android.Widget;
using System;
using Android.Bluetooth;
using Android.Content;
using System.Collections.Generic;
using Android.Util;
using Java.Util;


namespace ArduGround
{
    [Activity(Label = "ConnetActivity")]
    public class ConnetActivity : Activity
    {

        ICollection<BluetoothDevice> mDevice;
        ListView listView;
        Button SearchButton;
        BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
        BluetoothSocket mSocket;
        Toast toast;

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
            UUID Serial = UUID.FromString("00001101 - 0000 - 1000 - 8000 - 00805F9B34FB");
            try
            {
                mSocket = mRemoteDevice.CreateInsecureRfcommSocketToServiceRecord(Serial);
                mSocket.Connect();

                var mOutputStream = mSocket.OutputStream;
                var mInputStream = mSocket.InputStream;
                
            }
            catch
            {
                toast = Toast.MakeText(this, "띠요옹 알수없는 오류인데스웅", ToastLength.Short);
                toast.Show();

            }


        }
        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
           
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
                    List<string>items=new List<string>();
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
    }
}