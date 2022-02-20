using Android.App;
using Android.Content;
using System;

namespace Stepometer.Droid.Service
{
    [BroadcastReceiver(Enabled = true, Exported = false)]
    [IntentFilter(new String[] { "com.companyname.Stepometer" })]
    public class BootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var stepServiceIntent = new Intent(context, typeof(StepService));
            context.StartService(stepServiceIntent);
        }
    }
}