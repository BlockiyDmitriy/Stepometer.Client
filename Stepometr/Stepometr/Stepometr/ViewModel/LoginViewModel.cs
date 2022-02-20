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

namespace Stepometer.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public TaskLoaderNotifier LoginLoader { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(() => LoginLoader.Load(OnLoginClicked));
            LoginLoader = new TaskLoaderNotifier();
            LoginLoader.ShowResult = true;
        }

        private async Task OnLoginClicked()
        {
            try
            {
                await Shell.Current.GoToAsync($"//{nameof(StepometerPage)}");
            }
            catch(Exception e)
            {
                
            }
        }
       
    }
}
