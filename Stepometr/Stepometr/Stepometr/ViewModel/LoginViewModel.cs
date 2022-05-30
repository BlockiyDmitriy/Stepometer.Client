using Newtonsoft.Json.Linq;
using Sharpnado.Presentation.Forms;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Stepometer.Page;
using Xamarin.Forms;
using System.Windows.Input;
using Stepometer.Service.LoggerService;
using System.Reflection;
using Rg.Plugins.Popup.Services;
using Stepometer.Views.Popup;
using Stepometer.Service.LoginServices;
using Stepometer.Models.Login;

namespace Stepometer.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ILogService _logService;
        private readonly ILoginService _loginService;

        public TaskLoaderNotifier LoginLoader { get; set; }
        public ICommand LoginCommand { get; }
        public ICommand CreateNewAccountCommand { get; }
        public ICommand DevelopLoginCommand { get; }
        public LoginModel LoginModel { get; set; }

        public LoginViewModel(ILogService logService, ILoginService loginService)
        {
            _logService = logService;
            _loginService = loginService;

            LoginCommand = new Command(() => LoginLoader.Load(OnLoginClicked));
            LoginLoader = new TaskLoaderNotifier();
            LoginLoader.ShowResult = true;
            LoginModel = new();

            CreateNewAccountCommand = new Command(async ()=> await CreateNewAccount());
            DevelopLoginCommand = new Command(async ()=> await DevelopLogin());
        }

        private async Task DevelopLogin()
        {
            await Shell.Current.GoToAsync($"//{nameof(StepometerPage)}");
        }

        private async Task OnLoginClicked()
        {
            try
            {
                var isLogin = await _loginService.Login(LoginModel);

                if (isLogin)
                {
                    await Shell.Current.GoToAsync($"//{nameof(StepometerPage)}");
                }
                else
                {
                    await PopupNavigation.Instance.PushAsync(new ErrorPopup("Wrong login or password"));
                }
            }
            catch(Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                await PopupNavigation.Instance.PushAsync(new ErrorPopup("Error authorization"));
            }
        }

        public async Task CreateNewAccount()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new CreateAccountPopup());
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
            }
        }
    }
}
