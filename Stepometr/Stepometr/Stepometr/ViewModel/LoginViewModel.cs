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

namespace Stepometer.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public TaskLoaderNotifier LoginLoader { get; set; }


        public Action DisplayInvalidLoginPrompt;
        public string Email { get; set; }
        public string Password { get; set; }
        public ICommand SubmitCommand { protected set; get; }
        public LoginViewModel()
        {
            LoginCommand = new Command(() => LoginLoader.Load(OnLoginClicked));
            LoginLoader = new TaskLoaderNotifier();
            LoginLoader.ShowResult = true;

            SubmitCommand = new Command(OnSubmit);
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

  
        public void OnSubmit()
        {
            if (Email != "macoratti@yahoo.com" || Password != "secret")
            {
                DisplayInvalidLoginPrompt();
            }
        }
    }
}
