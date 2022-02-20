using System.Threading.Tasks;
using System.Windows.Input;
using Sharpnado.Presentation.Forms;
using Stepometer.Models;
using Stepometer.Service.Interfaces;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Stepometer.ViewModel
{
    public class FriendsViewModel : BaseViewModel
    {
        private IFriendsService _friendsService;

        public ICommand BackButton { get; }
        public ObservableRangeCollection<FriendsModel> Friends { get; set; }
        public TaskLoaderNotifier FriendsLoader { get; set; }

        public FriendsViewModel(IFriendsService friendsService)
        {
            _friendsService = friendsService;

            BackButton = new Command(async () => await GoBack());
            FriendsLoader = new TaskLoaderNotifier();
            Friends = new ObservableRangeCollection<FriendsModel>();
        }

        public async Task Init()
        {
            FriendsLoader.Load(async () => await LoadFriends());
        }

        private async Task LoadFriends()
        {
            var allFrieds = await _friendsService.GetAllFriends();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Friends.AddRange(allFrieds);
            });
        }

        private async Task GoBack()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Friends = new ObservableRangeCollection<FriendsModel>();
            });

            Shell.Current.SendBackButtonPressed();
        }
    }
}