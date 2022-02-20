using Stepometer.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stepometer.Page
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModelLocator.LoginViewModel;
        }
      
    }
}