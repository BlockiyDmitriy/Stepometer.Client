using Stepometer.Service.HttpApi.ConvertService;
using Stepometer.Service.HttpApi.UoW;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: Dependency(typeof(UnitOfWork))]
[assembly: Dependency(typeof(StepometerService))]
