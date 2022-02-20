using System;
using Stepometer.Page;
using Xamarin.Forms;

namespace Stepometer.ViewModel
{
    public class StartupViewModel : BaseViewModel
    {
        public StartupViewModel()
        {
        }


        public async void CheckLogin()
        {
            try
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            catch (Exception e)
            {
            }
        }
    }
}