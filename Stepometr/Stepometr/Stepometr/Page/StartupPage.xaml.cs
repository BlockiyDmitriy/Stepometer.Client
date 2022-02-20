using Stepometer.Utils;
using Stepometer.ViewModel;
using Xamarin.Forms;

namespace Stepometer.Page
{
    public partial class StartupPage : ContentPage
    {
        StartupViewModel _viewModel;
        public StartupPage()
        {
            InitializeComponent();

            _viewModel = ViewModelLocator.StartupViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.CheckLogin();
        }
    }
}