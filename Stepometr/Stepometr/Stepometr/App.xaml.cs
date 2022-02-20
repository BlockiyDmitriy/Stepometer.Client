using Stepometer.Service;
using Stepometer.Service.LoaclDB;
using Stepometer.Service.MockServices;
using Stepometer.Utils;
using Xamarin.Forms;

namespace Stepometer
{
    public partial class App : Application
    {
        public App()
        {
            Device.SetFlags(new string[] { "Brush_Experimental" });

            InitializeComponent();

            RegisterDependencies();

            MainPage = new AppShell();
        }

        private void RegisterDependencies()
        {
            DependencyResolver.Register<StepometerMockService>();
            DependencyResolver.Register<HistoryMockService>();
            DependencyResolver.Register<FriendsMockService>();
            DependencyResolver.Register<AchieveMockService>();
            DependencyResolver.Register<DBService>();
        }

        protected override void OnStart()
        {
            StepCounterService.Instance().InitService();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}