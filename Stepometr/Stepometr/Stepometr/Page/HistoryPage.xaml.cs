using Stepometer.Utils;
using Stepometer.ViewModel;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Stepometer.Page
{
    public partial class HistoryPage : ContentPage
    {
        public double VisibleContentViewHeight { get; set; }

        private HistoryViewModel _viewModel;

        public HistoryPage()
        {
            InitializeComponent();
            _viewModel = ViewModelLocator.HistoryViewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            var amountDay = GetAmountDayInYear();

            Task.Run( async () => await _viewModel.Init(amountDay));
        }
        private short GetAmountDayInYear()
        {
            if (!DateTime.IsLeapYear(DateTime.Today.Year) &&
                DateTime.Now.Month > 1)
            {
                return 365;
            }

            if (!DateTime.IsLeapYear(DateTime.Today.Year - 1) &&
                DateTime.Now.Month == 1)
            {
                return 365;
            }

            return 366;
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width == -1d || height == -1d || height == 0)
            {
                return;
            }

            var scrollViewGridSpacing = ScrollViewGrid.RowSpacing;
            var gridSpacing = MainGrid.RowSpacing;
            var segmentedControlHeight = SegmentedFrame.Height;

            VisibleContentViewHeight = height - scrollViewGridSpacing - gridSpacing - segmentedControlHeight - Constants.Constants.HistoryViewElements.SafeAreaHeight + 56;
        }
    }
}