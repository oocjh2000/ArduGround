﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Content;
using System.Threading;
using Java.Net;

namespace ArduGround
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        Toast toast;

        public static int HP = 100;

        BackPressCloseHandler closeHandler;

        Handler handler = new Handler();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            FindViewById<Button>(Resource.Id.ConnetButton).Click += ConnetButton_Click;
            FindViewById<Button>(Resource.Id.DieButton).Click += MainActivity_Click;
            FindViewById<TextView>(Resource.Id.ShowHelth).Text = HP.ToString();
            

            var mth = new Thread(new ThreadStart(RefreshThread));
            mth.Start();

            closeHandler = new BackPressCloseHandler(this);

        }

        void RefreshThread()
        {
            while (Thread.CurrentThread.IsAlive)
            {
                FindViewById<TextView>(Resource.Id.ShowHelth).Text = HP.ToString();
                handler.Post(delegate () { FindViewById<TextView>(Resource.Id.ShowHelth).Text = HP.ToString(); });
                Thread.Sleep(1000);
            }
        }

        void REFRESH(object sender, System.EventArgs e)
        {
            FindViewById<TextView>(Resource.Id.ShowHelth).Text = MainActivity.HP.ToString();
            if (HP < 0)
            {
                toast = Toast.MakeText(this, "사망", ToastLength.Short);
                toast.Show();
            }
        }

        private void MainActivity_Click(object sender, System.EventArgs e)
        {

            closeHandler.OnBackPressed();
        }

        private void ConnetButton_Click(object sender, System.EventArgs e)
        {
           
            StartActivity(typeof(ConnetActivity));
        }
        
    }
}

