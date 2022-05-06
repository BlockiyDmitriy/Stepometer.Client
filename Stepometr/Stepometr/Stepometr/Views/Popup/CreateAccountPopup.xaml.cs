using Rg.Plugins.Popup.Pages;
using Stepometer.Utils;
using Stepometer.ViewModel;

namespace Stepometer.Views.Popup
{
    public partial class CreateAccountPopup : PopupPage
    {
        private CreateAccountViewModel viewModel;

        public CreateAccountPopup()
        {
            InitializeComponent();

            viewModel = ViewModelLocator.CreateAccountViewModel;
            BindingContext = viewModel;
        }
    }
}