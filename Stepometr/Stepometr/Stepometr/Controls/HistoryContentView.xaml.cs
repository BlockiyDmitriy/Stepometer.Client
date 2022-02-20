using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Stepometer.Models;
using Stepometer.Controls.Charts;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Stepometer.Controls
{
    public partial class HistoryContentView
    {
        #region HeightView
        
        public double HeightViewGraph { get; set; }

        private void HeightView()
        {
            var heightViewGraph = UnusedSpace - LabelsFrame.Height  ;

            HeightViewGraph = heightViewGraph < 0 ? 0 : heightViewGraph;
        }

        #endregion

        #region VisibleContentViewHeight Property

        public static readonly BindableProperty UnusedSpaceProperty = BindableProperty.Create(
            nameof(UnusedSpace),
            typeof(double),
            typeof(HistoryContentView),
            propertyChanged: OnUnusedSpacePropertyChanged);

        public double UnusedSpace
        {
            get => (double) GetValue(UnusedSpaceProperty);
            set => SetValue(UnusedSpaceProperty, value);
        }

        private static void OnUnusedSpacePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is HistoryContentView view))
            {
                return;
            }

            view.HeightView();
        }

        #endregion

        #region Items Property

        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
            nameof(Items),
            typeof(IEnumerable<StepometerModel>),
            typeof(HistoryContentView),
            propertyChanged: OnItemsPropertyChanged);

        private static void OnItemsPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is HistoryContentView view))
            {
                return;
            }

            if (newvalue == null)
            {
                view.PropertiesDataReset();
                return;
            }

            if (newvalue is IEnumerable<StepometerModel>)
            {
                view.UpdateData();
            }

            if (newvalue is ObservableCollection<StepometerModel> observableCollection)
            {
                observableCollection.CollectionChanged += (sender, args) => view.UpdateData();
            }
        }

        public IEnumerable<StepometerModel> Items
        {
            get => (IEnumerable<StepometerModel>) GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        #endregion

        #region Properties

        public long AverageSteps { get; set; }
        public long AverageDistance { get; set; }
        public long AverageCalories { get; set; }
        public long AverageSpeed { get; set; }
        public DateTime AverageTimeActivity { get; set; }

        #endregion

        public HistoryContentView()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width == -1d || height == -1d)
            {
                return;
            }

            HeightView();
        }

        private void UpdateData()
        {
            if (!Items.Any())
            {
                PropertiesDataReset();
                return;
            }

            AverageSteps = (long) Items.Average(m => m.Steps);
            AverageDistance = (long) Items.Average(m => m.Distance);
            AverageCalories = (long) Items.Average(m => m.Calories);
            AverageSpeed = (long) Items.Average(m => m.Speed);
        }

        private void PropertiesDataReset()
        {
            AverageSteps = 0;
            AverageDistance = 0;
            AverageCalories = 0;
            AverageSpeed = 0;
        }
    }
}