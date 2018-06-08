using Android.App;
using Android.OS;
using Android.Widget;
using System;
using Android.Bluetooth;
using Android.Content;
using System.Collections.Generic;

namespace ArduGround
{
    [Activity(Label = "ConnetActivity")]
    public class ConnetActivity : Activity
    {
        private object li;
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

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            if (!bluetoothAdapter.IsEnabled)
            {
                if (bluetoothAdapter.Enable()) { }
                else Finish();
            }
            else
            {
              ICollection<BluetoothDevice> bluetoothDevice = bluetoothAdapter.BondedDevices;
                if (bluetoothDevice.Count == 0)
                    Finish();
             
                builder.SetTitle("Set Your Device");

                List<string> vs = new List<string>();
                foreach(BluetoothDevice dv in bluetoothDevice)
                {
                    vs.Add(dv.Name);
                }
                vs.Add("Cancel");
                string items = vs.ToString();
               // builder.SetItems(items.ToCharArray(),new IDialogInterfaceOnClickListener)
                    
               
              
            }
            AlertDialog alert = builder.Create();
        }
    }
}