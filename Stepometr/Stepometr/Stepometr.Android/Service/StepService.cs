using Android.App;
using Android.Content;
using Android.Hardware;
using Android.Util;
using AndroidX.Core.App;
using System;
using System.Threading.Tasks;
using Application = Android.App.Application;

namespace Stepometer.Droid.Service
{
    [Service(Enabled = true)]
    [IntentFilter(new String[] {"com.companyname.Stepometer"})]
    public class StepService : Android.App.Service, ISensorEventListener
    {
        StepServiceBinder binder;

        Int64 newSteps = 0;
        Int64 lastSteps = 0;

        private bool isRunning;
        private Int64 stepsToday = 0;
        public bool WarningState { get; set; }

        public Int64 StepsToday
        {
            get { return stepsToday; }
            set
            {
                if (stepsToday == value)
                    return;

                StepCounterService.Instance.TempSteps = value;

                stepsToday = value;
                Helpers.Settings.CurrentDaySteps = value;
            }
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
#if DEBUG
            Log.Debug("MyApp", "Start command result called, incoming startup");
#endif
            var warning = false;
            if (intent != null)
                warning = intent.GetBooleanExtra("warning", false);

            // Enlist this instance of the service as a foreground service
            ForegroundNotification(intent);

            Startup();

            return StartCommandResult.Sticky;
        }

        private async void ForegroundNotification(Intent intent)
        {
            var context = Application.Context;
            const int pendingIntentId = 0;
            PendingIntent pendingIntent =
                PendingIntent.GetActivity(context, pendingIntentId, intent, PendingIntentFlags.OneShot);
            var notification = new NotificationCompat.Builder(context)
                .SetContentTitle("Testing")
                .SetChannelId("com.companyname.Stepometer")
                .SetContentText("Physical tracking has begun.")
                .SetSmallIcon(Resource.Mipmap.ic_launcher)
                .SetContentIntent(pendingIntent)
                .SetOngoing(true)
                .Build();
            const int Service_Running_Notification_ID = 935;
            await Task.Run(() => StartForeground(Service_Running_Notification_ID, notification));
        }

        public override void OnTaskRemoved(Intent rootIntent)
        {
            base.OnTaskRemoved(rootIntent);

            UnregisterListeners();
#if DEBUG
            Log.Debug("MyApp", "Task Removed, going down");
#endif
            var intent = new Intent(this, typeof(StepService));
            intent.PutExtra("warning", WarningState);
            // Restart service in 500 ms
            ((AlarmManager) GetSystemService(Context.AlarmService)).Set(AlarmType.Rtc, Java.Lang.JavaSystem
                    .CurrentTimeMillis() + 500,
                PendingIntent.GetService(this, 11, intent, 0));
        }

        private void Startup(bool warning = false)
        {
            //check if kit kat can sensor compatible
            if (!Helpers.Utils.IsKitKatWithStepCounter(PackageManager))
            {
                StopSelf();
                return;
            }


            if (!isRunning)
            {
                RegisterListeners(SensorType.StepCounter);
                WarningState = warning;
            }

            isRunning = true;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            UnregisterListeners();
            isRunning = false;
        }

        void RegisterListeners(SensorType sensorType)
        {
            var sensorManager = (SensorManager) GetSystemService(Context.SensorService);
            var sensor = sensorManager.GetDefaultSensor(sensorType);

            //get faster why not, nearly fast already and when
            //sensor gets messed up it will be better
            sensorManager.RegisterListener(this, sensor, SensorDelay.Normal);
        }

        void UnregisterListeners()
        {
            if (!isRunning)
                return;

            try
            {
                var sensorManager = (SensorManager) GetSystemService(Context.SensorService);
                sensorManager.UnregisterListener(this);
#if DEBUG
                Log.Debug("MyApp", "Sensor listener unregistered.");
#endif
                isRunning = false;
            }
            catch (Exception ex)
            {
#if DEBUG
                Log.Debug("MyApp", "Unable to unregister: " + ex);
#endif
            }
        }

        public override Android.OS.IBinder OnBind(Android.Content.Intent intent)
        {
#if DEBUG
            Log.Info("MyApp", "Method OnBind");
#endif
            binder = new StepServiceBinder(this);
            return binder;
        }

        public void AddSteps(Int64 count)
        {
            //if service rebooted or rebound then this will null out to 0, but count will still be since last boot.
            if (lastSteps == 0)
            {
                lastSteps = count;
            }

            //calculate new steps
            newSteps = count - lastSteps;

            //ensure we are never negative
            //if so, no worries as we are about to re-set the lastSteps to the
            //current count
            if (newSteps < 0)
                newSteps = 1;
            else if (newSteps > 100)
                newSteps = 1;

            lastSteps = count;

            //save total steps!
            Helpers.Settings.TotalSteps += newSteps;

            StepsToday = Helpers.Settings.TotalSteps - Helpers.Settings.StepsBeforeToday;
#if DEBUG
            Log.Debug("MyApp", "New steps: " + newSteps + " total: " + stepsToday);
#endif
        }

        #region ISensorEventListener

        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
            //do nothing here
        }

        public void OnSensorChanged(SensorEvent? e)
        {
            if (lastSteps < 0)
                lastSteps = 0;

            //grab out the current value.
            var count = (Int64) e.Values[0];
#if DEBUG
            Log.Info("MyApp", count.ToString());
#endif
            //in some instances if things are running too long (about 4 days)
            //the value flips and gets crazy and this will be -1
            //so switch to step detector instead, but put up warning sign.
            if (count < 0)
            {
                UnregisterListeners();
                RegisterListeners(SensorType.StepCounter);
                isRunning = true;
#if DEBUG
                Log.Debug("MyApp",
                    "Something has gone wrong with the step counter, simulating steps, 2.");
#endif
                count = lastSteps + 3;

                WarningState = true;
            }
            else
            {
                WarningState = false;
            }

            AddSteps(count);
#if DEBUG
            Log.Info("MyApp", count.ToString());
#endif
        }

        #endregion
    }
}