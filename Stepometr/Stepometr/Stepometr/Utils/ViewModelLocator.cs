using Stepometer.Service.HttpApi.ConvertService;
using Stepometer.Service.HttpApi.ConvertService.Contracts;
using Stepometer.Service.LoaclDB;
using Stepometer.Service.LoggerService;
using Stepometer.Service.LoginServices;
using Stepometer.Service.MockServices;
using Stepometer.ViewModel;

namespace Stepometer.Utils
{
    public class ViewModelLocator
    {
        public static StartupViewModel StartupViewModel { get; set; } = new StartupViewModel();
        public static LoginViewModel LoginViewModel { get; set; } = new LoginViewModel(DependencyResolver.Get<ILogService>(), DependencyResolver.Get<ILoginService>());
        public static CreateAccountViewModel CreateAccountViewModel { get; set; } = new CreateAccountViewModel(DependencyResolver.Get<ILogService>(), DependencyResolver.Get<ILoginService>());
        public static StepometerViewModel StepometerViewModel { get; set; } = new StepometerViewModel(DependencyResolver.Get<IStepometerService>(), DependencyResolver.Get<ILogService>());
        public static HistoryViewModel HistoryViewModel { get; set; } = new HistoryViewModel(DependencyResolver.Get<HistoryService>(), DependencyResolver.Get<ILogService>());
        public static FriendsViewModel FriendsViewModel { get; set; } = new FriendsViewModel(DependencyResolver.Get<FriendsMockService>());
        public static AchieveViewModel AchieveViewModel { get; set; } = new AchieveViewModel(DependencyResolver.Get<AchieveMockService>(), DependencyResolver.Get<IStepometerService>());
    }
}