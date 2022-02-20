using Android.Content;
using Android.OS;
using Android.Util;

namespace Stepometer.Droid.Service
{
	public class StepServiceConnection : Java.Lang.Object, IServiceConnection
	{
		public void OnServiceConnected (ComponentName name, IBinder service)
		{
            var serviceBinder = service as StepServiceBinder;
            Log.Info("MyApp", "Service Connection");
        }

		public void OnServiceDisconnected (ComponentName name)
		{
            Log.Info("MyApp", "Service Disconnection");
		}

	}
}

