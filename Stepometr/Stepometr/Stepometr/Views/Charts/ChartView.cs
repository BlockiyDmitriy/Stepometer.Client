using System;
using Stepometer.Controls.Charts.Charts;
using Stepometer.Controls.Charts.WeakEventHandlers;

namespace Stepometer.Controls.Charts
{
    using Xamarin.Forms;
    using SkiaSharp.Views.Forms;
    using SkiaSharp;

    public class ChartView : SKCanvasView
    {
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (chart !=null)
            {
                chart.BindingContext = BindingContext;
                chart.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(LineChart.BindableData))
                    {
                        InvalidateSurface();
                    }
                };
            }
        }

        #region Constructors

        public ChartView()
        {
            this.BackgroundColor = Color.Transparent;
            this.PaintSurface += OnPaintCanvas;
        }

        #endregion

        #region Static fields

        public static readonly BindableProperty ChartProperty = BindableProperty.Create(nameof(Chart), typeof(Chart), typeof(ChartView), null, propertyChanged: OnChartChanged);

        #endregion

        #region Fields

        private InvalidatedWeakEventHandler<ChartView> handler;

        private Chart chart;

        #endregion

        #region Properties

        public Chart Chart
        {
            get { return (Chart)GetValue(ChartProperty); }
            set { SetValue(ChartProperty, value); }
        }

        #endregion

        #region Methods

        private static void OnChartChanged(BindableObject d, object oldValue, object value)
        {
            var view = d as ChartView;

            if (view.chart != null)
            {
                view.handler.Dispose();
                view.handler = null;
            }

            view.chart = value as Chart;

            //view.InvalidateSurface();

            if (view.chart != null)
            {
                view.chart.BindingContext = view.BindingContext;
                view.chart.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(LineChart.BindableData))
                    {
                        view.InvalidateSurface();
                    }
                };

                view.handler = view.chart.ObserveInvalidate(view, (v) => v.InvalidateSurface());
            }
        }

        private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            if (this.chart != null)
            {
                this.chart.Draw(e.Surface.Canvas, e.Info.Width, e.Info.Height);
            }
            else
            {
                e.Surface.Canvas.Clear(SKColors.Transparent);
            }
        }

        #endregion
    }
}
