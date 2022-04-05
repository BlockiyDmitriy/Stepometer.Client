using Stepometer.Service.HttpApi.ConvertService;
using Stepometer.Service.HttpApi.ConvertService.Contracts;
using Stepometer.Service.LoaclDB;
using Stepometer.Service.LoggerService;
using Stepometer.Service.MockServices;
using Stepometer.ViewModel;

namespace Stepometer.Utils
{
    public class ViewModelLocator
    {
        public static StartupViewModel StartupViewModel { get; set; } = new StartupViewModel();
        public static LoginViewModel LoginViewModel { get; set; } = new LoginViewModel();
        public static StepometerViewModel StepometerViewModel { get; set; } = new StepometerViewModel(DependencyResolver.Get<IStepometerService>(), DependencyResolver.Get<IDBService>(), DependencyResolver.Get<ILogService>());
        public static HistoryViewModel HistoryViewModel { get; set; } = new HistoryViewModel(DependencyResolver.Get<HistoryMockService>());
        public static FriendsViewModel FriendsViewModel { get; set; } = new FriendsViewModel(DependencyResolver.Get<FriendsMockService>());
        public static AchieveViewModel AchieveViewModel { get; set; } = new AchieveViewModel(DependencyResolver.Get<AchieveMockService>(), DependencyResolver.Get<StepometerMockService>());
    }
}