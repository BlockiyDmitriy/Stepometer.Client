using Android.Content;
using Stepometer.Service;
using System;
using Xamarin.Forms;
using StepCounterService = Stepometer.Droid.Service.StepCounterService;

[assembly: Dependency(typeof(StepCounterService))]

namespace Stepometer.Droid.Service
{
    public class StepCounterService : IStepCounterService
    {
        public static IStepCounterService Instance { get; private set; }

        private long _tempSteps;

        public long TempSteps
        {
            get => _tempSteps;
            set
            {
                _tempSteps = value;

                StepsChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<long> StepsChanged;

        public StepCounterService()
        {
            Instance = this;
        }

        public void InitService()
        {
            var activity = MainActivity.Instance;
            StepServiceConnection serviceConnection = new StepServiceConnection();
            var boundServiceIntent = new Intent(activity, typeof(StepService));
            activity.BindService(boundServiceIntent, serviceConnection, Bind.AutoCreate);
            activity.StartForegroundService(boundServiceIntent);
        }
    }
}