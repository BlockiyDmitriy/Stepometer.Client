using Rg.Plugins.Popup.Services;
using Sharpnado.Presentation.Forms;
using Stepometer.Models;
using Stepometer.Service;
using Stepometer.Service.LoaclDB;
using Stepometer.Settings;
using Stepometer.Views.Popup;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Stepometer.Service.HttpApi.ConvertService.Contracts;
using Xamarin.Forms;
using System.Linq;
using Stepometer.Service.LoggerService;
using System.Reflection;

namespace Stepometer.ViewModel
{
    public class StepometerViewModel : BaseViewModel
    {
        private readonly IStepometerService _stepometerService;
        private readonly ILogService _logService;

        public StepometerModel Stepometer { get; set; }
        public double ProgressBarValue { get; set; }
        public ICommand OpenMenuCommand { get; }
        public TaskLoaderNotifier StepometerLoader { get; set; }

        public StepometerViewModel(IStepometerService stepometerService, IDBService dbService, ILogService logService)
        {
            _stepometerService = stepometerService;
            _logService = logService;

            OpenMenuCommand = new Command(async () => await OpenMenu());
            StepometerLoader = new TaskLoaderNotifier();
        }

        public Task Init()
        {
            StepometerLoader.Load(LoadStepometer);

            return Task.CompletedTask;
        }

        private async Task LoadStepometer()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;
                
                StepCounterService.Instance().StepsChanged += Service_StepsChanged;

                var listData = await _stepometerService.GetData();
                Stepometer = listData.FirstOrDefault();
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        private async void UpdateSteps(long stepValue)
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                var stepometer = new StepometerModel()
                {
                    Id = Stepometer.Id,
                    Steps = Convert.ToInt32(stepValue),
                    Calories = Stepometer.Calories,
                    Distance = Stepometer.Distance,
                    LastActivityDate = DateTimeOffset.Now,
                    Speed = Stepometer.Speed,
                    Date = Stepometer.Date
                };

                Stepometer = await _stepometerService.PutData(stepometer);
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public async Task OpenMenu()
        {
            await PopupNavigation.Instance.PushAsync(new MenuPopup());
        }

        private void UpdateProgressBarValue()
        {
            try
            {
                ProgressBarValue = ProgressBarValue < 1
                    ? (double)Stepometer?.Steps / CounterSettings.DailyStepGoalDefault
                    : 1;
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
            }
        }
        private void Service_StepsChanged(object sender, long e)
        {
            UpdateSteps(e);
            UpdateProgressBarValue();
        }
    }
}