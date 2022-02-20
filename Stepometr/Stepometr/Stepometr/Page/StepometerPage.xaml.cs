using System.Threading.Tasks;
using Stepometer.Utils;
using Stepometer.ViewModel;
using Xamarin.Forms;

namespace Stepometer.Page
{
    public partial class StepometerPage : ContentPage
    {
        private StepometerViewModel _viewModel;

        public StepometerPage()
        {
            InitializeComponent();

            _viewModel = ViewModelLocator.StepometerViewModel;
            BindingContext = _viewModel;

            Task.Run(async () => await _viewModel.Init());
        }
    }
}