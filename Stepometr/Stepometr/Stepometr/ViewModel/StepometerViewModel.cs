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

namespace Stepometer.ViewModel
{
    public class StepometerViewModel : BaseViewModel
    {
        private readonly IStepometerService _stepometerService;
        private readonly IDBService _dbService;

        public StepometerModel Stepometer { get; set; }
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
                
                StepCounterService.Instance().StepsChanged += Service_StepsChanged;

                await LoadStepometerFromLocalDB();
                Stepometer = await _stepometerService.GetData();

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
            Stepometer = stepometerModel;
        }
        
        private async void UpdateSteps(long stepValue)
        {
            var stepometer = new StepometerModel()
            {
                Id = Stepometer.Id,
                Steps = stepValue,
                Calories = Stepometer.Calories,
                Distance = Stepometer.Distance,
                LastActivityDate = DateTimeOffset.Now,
                Speed = Stepometer.Speed,
                Time = Stepometer.Time
            };

            Stepometer = await _stepometerService.PutData(stepometer);
            await _dbService.UpdateStepometerDataAsync(Stepometer);
            await _dbService.UpdateLastActivityDate(DateTimeOffset.Now);
        }
        public async Task OpenMenu()
        {
            await PopupNavigation.Instance.PushAsync(new MenuPopup());
        }

        private void UpdateProgressBarValue()
        {
            ProgressBarValue = ProgressBarValue < 1
                ? (double)Stepometer.Steps / CounterSettings.DailyStepGoalDefault
                : 1;
        }
        private void Service_StepsChanged(object sender, long e)
        {
            UpdateSteps(e);
            UpdateProgressBarValue();
        }
    }
}