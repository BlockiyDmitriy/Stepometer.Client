using Xamarin.Forms;

namespace Stepometer.Service
{
    public class StepCounterService
    {
        private static IStepCounterService _instance;

        public static IStepCounterService Instance() => _instance ??= DependencyService.Get<IStepCounterService>();
    }
}
