using Android.OS;
using Android.Util;

namespace Stepometer.Droid.Service
{
	public class StepServiceBinder : Binder
	{
		public StepService StepService { get; private set; }
		public StepServiceBinder (StepService service)
        {
            Log.Info("MyApp", "Constructor StepServiceBinder");
			this.StepService = service;
		}
	}
}

