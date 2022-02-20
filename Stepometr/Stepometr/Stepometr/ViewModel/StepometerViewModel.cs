using System;
using System.Diagnostics;
using System.Linq;
using Rg.Plugins.Popup.Services;
using Stepometer.Service;
using Stepometer.Service.Interfaces;
using Stepometer.Settings;
using Stepometer.Views.Popup;
using System.Threading.Tasks;
using System.Windows.Input;
using Sharpnado.Presentation.Forms;
using Stepometer.Models;
using Stepometer.Service.LoaclDB;
using Xamarin.Forms;

namespace Stepometer.ViewModel
{
    public class StepometerViewModel : BaseViewModel
    {
        private readonly IStepometerService _stepometerService;
        private readonly IDBService _dbService;

        public StepometerModel StepoModel { get; set; }
        public double ProgressBarValue { get; set; }
        public ICommand OpenMenuCommand { get; }
        public TaskLoaderNotifier StepometerLoader { get; set; }

        public StepometerViewModel(IStepometerService stepometerService, IDBService dbService)
        {
            _stepometerService = stepometerService;
            _dbService = dbService;

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

                await Task.Delay(3000);

                StepCounterService.Instance().StepsChanged += Service_StepsChanged;

                await LoadStepometerFromLocalDB();

                IsBusy = false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        private async Task LoadStepometerFromLocalDB()
        {
            var stepometerModel = await _dbService.GetStepometerDataAsync();
            if (stepometerModel == null)
            {
                return;
            }
            StepoModel = stepometerModel;
        }
        public async Task OpenMenu()
        {
            await PopupNavigation.Instance.PushAsync(new MenuPopup());
        }

        private void UpdateProgressBarValue()
        {
            ProgressBarValue = ProgressBarValue < 1
                ? (double) StepoModel.Steps / CounterSettings.DailyStepGoalDefault
                : 1;
        }

        private async void UpdateSteps(long stepValue)
        {
            StepoModel = await _stepometerService.UpdateCurrentStepsData(stepValue);
            await _dbService.UpdateStepometerDataAsync(StepoModel);
            await _dbService.UpdateLastActivityDate(DateTimeOffset.Now);
        }

        private void Service_StepsChanged(object sender, long e)
        {
            UpdateSteps(e);
            UpdateProgressBarValue();
        }
    }
}