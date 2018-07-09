﻿using Android.App;
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
        private readonly IDialogInterfaceOnClickListener listener;
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
            if (!bluetoothAdapter.IsEnabled)
            {
                var Btintent = new Intent(BluetoothAdapter.ActionRequestEnable);
                StartActivity(Btintent);
            }
            
        }
    }
}