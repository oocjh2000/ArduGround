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
                Selecteddevice = device;
            }
            return Selecteddevice;
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            UUID Serial = UUID.FromString("00001101 - 0000 - 1000 - 8000 - 00805F9B34FB");
        
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (!bluetoothAdapter.IsEnabled)
            {
                var Btintent = new Intent(BluetoothAdapter.ActionRequestEnable);
                StartActivity(Btintent);
            }
            else {
                if (bluetoothAdapter.BondedDevices.Count > 0)
                {
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

                }
            }
            
        }
    }
}