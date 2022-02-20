using Stepometer.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Stepometer.Controls
{
    public partial class ExpanderContentView
    {
        #region ItemsSource Property

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable<StepometerModel>),
            typeof(ExpanderContentView),
            propertyChanged: OnItemsSourcePropertyChanged);

        public IEnumerable<StepometerModel> ItemsSource
        {
            get => (IEnumerable<StepometerModel>) GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        private static void OnItemsSourcePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is ExpanderContentView view))
            {
                return;
            }
        }

        #endregion

        public ExpanderContentView()
        {
            InitializeComponent();
        }
    }
}