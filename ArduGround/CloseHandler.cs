
using Android.App;
using Android.Widget;
using System.Net.Http;
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
        public async void OnBackPressedAsync()
        {

            if (JavaSystem.CurrentTimeMillis() > BackPressedTime + 2000)
            {
                BackPressedTime = JavaSystem.CurrentTimeMillis();
                ShowGuide();
                return;
            }
            if (JavaSystem.CurrentTimeMillis() <= BackPressedTime + 2000)
            {
                if (Register.IsServerConnet)
                {
                    var req = new HttpRequestMessage();
                    req.RequestUri = new System.Uri("http://" + Register.serverUrl + "/users/" + Register.player.id.ToString());
                    var cli = new HttpClient();
                    var res = await cli.DeleteAsync(req.RequestUri);
                }
                activity.MoveTaskToBack(true);
                activity.Finish();
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                toast.Cancel();
            }
        }
        public void ShowGuide()
        {
            toast = Toast.MakeText(activity, "한번더누르면 종료", ToastLength.Short);
            toast.Show();
        }
    }
}