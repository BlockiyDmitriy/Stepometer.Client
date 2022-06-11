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
            nameof(Item),
            typeof(AvgPeriodDataModel),
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
                return;
            }

            if (newvalue is AvgPeriodDataModel)
            {
                view.UpdateData();
            }
        }

        public AvgPeriodDataModel Item
        {
            get => (AvgPeriodDataModel) GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        #endregion

        #region Properties

        public double AverageSteps { get; set; }
        public double AverageDistance { get; set; }
        public double AverageCalories { get; set; }
        public double AverageSpeed { get; set; }
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
            if (Item == null)
            {
                return;
            }

            AverageSteps = Item.AvgSteps;
            AverageDistance = Item.AvgDistance;
            AverageCalories = Item.AvgCalories;
            AverageSpeed = Item.AvgSpeed;
        }
    }
}