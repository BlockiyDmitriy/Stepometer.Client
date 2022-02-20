using System;

namespace Stepometer.Service
{
    public interface IStepCounterService
    {
        long TempSteps { get; set; }

        event EventHandler<long> StepsChanged;

        void InitService();
    }

}