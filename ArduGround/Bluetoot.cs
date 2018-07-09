using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;

namespace ArduGround
{
    public class BT_Helper
    {
        BluetoothAdapter bluetoothAdapter;

        Activity mActivity;
        Handler mHandler;
        public void BluetoothServide(Activity activity,Handler handler)
        {
            mActivity = activity;
            mHandler = handler;

            bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            if (!bluetoothAdapter.IsEnabled)
            {
              
                 
            }
        }
       
    }
}