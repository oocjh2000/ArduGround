﻿
using Android.App;
using Android.Widget;
using Java.Lang;

namespace ArduGround
{
    public class BackPressCloseHandler
    {
        private long BackPressedTime = 0;
        private Toast toast;
        private Activity activity;
        public BackPressCloseHandler(Activity context)
        {
            this.activity = context;
        }
        public void OnBackPressed()
        {

            if (JavaSystem.CurrentTimeMillis() > BackPressedTime + 2000)
            {
                BackPressedTime = JavaSystem.CurrentTimeMillis();
                ShowGuide();
                return;
            }
            if (JavaSystem.CurrentTimeMillis() <= BackPressedTime + 2000)
            {
                activity.MoveTaskToBack(true);
                activity.Finish();
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                toast.Cancel();
            }
        }
        public void ShowGuide()
        {
            toast = Toast.MakeText(activity, "한번더누르면 자살", ToastLength.Short);
            toast.Show();
        }
    }
}