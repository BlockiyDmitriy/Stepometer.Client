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

namespace Stepometer.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ILogService _logService;

        public Action DisplayInvalidLoginPrompt;

        public Command LoginCommand { get; }
        public TaskLoaderNotifier LoginLoader { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICommand SubmitCommand { protected set; get; }
        public ICommand CreateNewAccountCommand { get; }
        public LoginViewModel(ILogService logService)
        {
            _logService = logService;

            LoginCommand = new Command(() => LoginLoader.Load(OnLoginClicked));
            LoginLoader = new TaskLoaderNotifier();
            LoginLoader.ShowResult = true;

            SubmitCommand = new Command(async () => await OnSubmit());
            CreateNewAccountCommand = new Command(async ()=> await CreateNewAccount());
        }

        private async Task OnLoginClicked()
        {
            try
            {
                await Shell.Current.GoToAsync($"//{nameof(StepometerPage)}");
            }
            catch(Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
            }
        }

  
        public Task OnSubmit()
        {
            if (Email != "macoratti@yahoo.com" || Password != "secret")
            {
                DisplayInvalidLoginPrompt();
            }
            return Task.CompletedTask;
        }

        public Task CreateNewAccount()
        {
            try
            {

                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return Task.CompletedTask;
            }
        }
    }
}
