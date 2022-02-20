using System.Threading.Tasks;
using Stepometer.Utils;
using Stepometer.ViewModel;
using Xamarin.Forms;

namespace Stepometer.Page
{
    public partial class AchievePage : ContentPage
    {
        private AchieveViewModel _viewModel;
        public AchievePage()
        {
            InitializeComponent();

            _viewModel = ViewModelLocator.AchieveViewModel;
            BindingContext = _viewModel;

            Task.Run(async () => await _viewModel.Init());
        }
    }
}