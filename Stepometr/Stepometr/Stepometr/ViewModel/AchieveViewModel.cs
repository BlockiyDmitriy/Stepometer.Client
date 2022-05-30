using Sharpnado.Presentation.Forms;
using Stepometer.Models;
using Stepometer.Service.HttpApi.ConvertService.Contracts;
using Stepometer.Settings;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Stepometer.ViewModel
{
    public class AchieveViewModel : BaseViewModel
    {
        private readonly IAchieveService _achieveService;
        private readonly IStepometerService _stepometerService;

        public ObservableRangeCollection<AchieveModel> Achieve { get; set; }
        public ICommand BackButton { get; }
        public TaskLoaderNotifier AchieveLoader { get; set; }

        public AchieveViewModel(IAchieveService achieveService, IStepometerService stepometerService)
        {
            _achieveService = achieveService;
            _stepometerService = stepometerService;

            Achieve = new ObservableRangeCollection<AchieveModel>();
            AchieveLoader = new TaskLoaderNotifier();
            AchieveLoader.RefreshCommand = new Command(() => AchieveLoader.Load(UpdateAchieve));
            BackButton = new Command(async ()=> await GoBack());
        }

        public Task Init()
        {
            AchieveLoader.Load(LoadAchieve);

            return Task.CompletedTask;
        }

        private async Task LoadAchieve()
        {
            try
            {
                var achieve = await _achieveService.GetAllAchieve();
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Achieve = new ObservableRangeCollection<AchieveModel>(achieve.OrderByDescending(u => Convert.ToInt32(u.AmountCompleted)));
                });

                var stepometerModelT = await _stepometerService.GetData();
                var stepometerModel = stepometerModelT.FirstOrDefault();

                foreach (var item in achieve)
                {
                    var result = (double) stepometerModel.Steps / Convert.ToInt32(item.Goal);
                    item.ProgressBarCompleted = result > 1 ? 1 : result;
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        private async Task UpdateAchieve()
        {
            try
            {
                var achieve = await _achieveService.GetAllAchieve();
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Achieve = new ObservableRangeCollection<AchieveModel>(achieve);
                });

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        private async Task GoBack()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Achieve = new ObservableRangeCollection<AchieveModel>();
            });

            Shell.Current.SendBackButtonPressed();
        }
    }
}
