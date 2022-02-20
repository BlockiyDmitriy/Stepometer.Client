using System.Threading.Tasks;
using Stepometer.Utils;
using Stepometer.ViewModel;
using Xamarin.Forms;

namespace Stepometer.Page
{
    public partial class FriendsPage : ContentPage
    {
        private FriendsViewModel _viewModel;

        public FriendsPage()
        {
            InitializeComponent();

            _viewModel = ViewModelLocator.FriendsViewModel;
            BindingContext = _viewModel;

            Task.Run(async () => await _viewModel.Init());
        }
    }
}