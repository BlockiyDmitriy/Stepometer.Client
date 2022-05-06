using Stepometer.Service.LoggerService;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Stepometer.ViewModel
{
    public class CreateAccountViewModel
    {
        private readonly ILogService _logService;

        private ICommand CreateAccountCommand { get; }

        public CreateAccountViewModel(ILogService logService)
        {
            _logService = logService;

            CreateAccountCommand = new Command(async () => await CreateNewAccount());
        }

        private Task CreateNewAccount()
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
