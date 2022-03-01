using Sharpnado.Presentation.Forms;
using SkiaSharp;
using Stepometer.Controls.Charts;
using Stepometer.Enum;
using Stepometer.Models;
using Stepometer.Page;
using Stepometer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Stepometer.ViewModel
{
    public class HistoryViewModel : BaseViewModel
    {
        private IList<StepometerModel> _allDataStat;
        private IHistoryService _historyService;

        public ObservableRangeCollection<StepometerModel> Data { get; set; }
        public ObservableRangeCollection<ChartEntry> Entries { get; set; }
        public ObservableRangeCollection<ExpanderHistoryModel> ExpanderHistory { get; set; }
        public ICommand SegmentChangedCommand { get; }
        public ICommand OpenAchievePageCommand { get; }
        public TaskLoaderNotifier SegmentLoader { get; set; }
        public byte SelectedSegment { get; set; }

        public HistoryViewModel(IHistoryService historyService)
        {
            _historyService = historyService;

            Data = new ObservableRangeCollection<StepometerModel>();
            Entries = new ObservableRangeCollection<ChartEntry>();
            ExpanderHistory = new ObservableRangeCollection<ExpanderHistoryModel>();
            SegmentChangedCommand =
                new AsyncCommand(async () => await OnSegmentChanged(), continueOnCapturedContext: false);
            OpenAchievePageCommand = new AsyncCommand(async () => await OpenAchievePage(), continueOnCapturedContext: false);
            SegmentLoader = new TaskLoaderNotifier();

        }

        public Task Init(int amountDay)
        {
            SegmentLoader.Load(async ()=> await InitFillAllData(amountDay));

            return Task.CompletedTask;
        }


        public async Task OnSegmentChanged() => await PrintGraphExpander();


        private static (EnumTypePeriod period, byte ValuePoint, byte ValueCountRootExpander)
            ConvertSegmentToNecessaryData(byte segment) =>
            segment switch
            {
                0 => (EnumTypePeriod.Week, 7, 1),
                1 => (EnumTypePeriod.Month, 4, 1),
                2 => (EnumTypePeriod.HalfYear, 6, 6),
                3 => (EnumTypePeriod.Year, 12, 12),
                _ => throw new ArgumentException()
            };

        /// <summary>
        /// Draws a graph and the necessary expander depending on the selected period
        /// </summary>
        /// <param name="segment">Selected segment/period</param>
        private async Task PrintGraphExpander()
        {
            try
            {
                var (period, valuePoint, valueCountRootExpander) = ConvertSegmentToNecessaryData(SelectedSegment);

                var tempPeriod = period == EnumTypePeriod.Week ? "Week" : "Month";

                Data?.Clear();
                Entries?.Clear();
                ExpanderHistory?.Clear();

                var data = await PrepareGraphStat(valuePoint);
                var entries = await CreateGraphData(data);
                var expanderData = await PrepareExpanderData(_allDataStat, valueCountRootExpander, tempPeriod);

                Data.AddRange(data);
                Entries.AddRange(entries);
                ExpanderHistory.AddRange(expanderData);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        private async Task OpenAchievePage()
        {
            await Shell.Current.GoToAsync($"{nameof(AchievePage)}");
        }

        public async Task InitFillAllData(int amountDayInYear)
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                var historyData = await _historyService.GetHistoryData(amountDayInYear);

                _allDataStat = historyData;
                await PrintGraphExpander();

                IsBusy = false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Fill and prepare expander data
        /// </summary>
        /// <param name="data">List with all data</param>
        /// <param name="valueCountRootExpander">Amount root expander</param>
        /// <param name="period">Selected period</param>
        /// <returns>Return data for expander</returns>
        private async Task<IList<ExpanderHistoryModel>> PrepareExpanderData(
            IList<StepometerModel> data,
            byte valueCountRootExpander,
            string period)
        {
            try
            {
                IList<ExpanderHistoryModel> expanderData = new List<ExpanderHistoryModel>();
                IList<StepometerModel> expanderContentMonthData = new List<StepometerModel>();
                IList<IList<StepometerModel>> expanderContentSplitMonthData = new List<IList<StepometerModel>>();


                for (int i = 0;
                    i < valueCountRootExpander;
                    i++)
                {
                    var firstTimeInList = data[i];
                    var daysInPeriod = period == "Week"
                        ? 7
                        : DateTime.DaysInMonth(firstTimeInList.Time.Year, firstTimeInList.Time.Month);
                    for (int j = 1; j < daysInPeriod + 1; j++)
                    {
                        expanderContentMonthData.Add(data[j - 1]);
                    }

                    expanderContentSplitMonthData.Add(expanderContentMonthData);

                    expanderData.Add(new ExpanderHistoryModel
                    {
                        Title = (i + 1) + " " + period,
                        ExpanderContent = new List<StepometerModel>(expanderContentSplitMonthData[i])
                    });
                    expanderContentMonthData.Clear();
                }

                return expanderData;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Fill and prepare data for graph
        /// </summary>
        /// <param name="valuePoint">Amount point to graph</param>
        /// <returns>Return prepare data for graph</returns>
        private async Task<IList<StepometerModel>> PrepareGraphStat(byte valuePoint)
        {
            try
            {
                var historyData = await _historyService.GetHistoryData(valuePoint);

                return historyData;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Fill result data for graph
        /// </summary>
        /// <param name="data">List with all data</param>
        /// <returns>Return final result for graph</returns>
        private async Task<IList<ChartEntry>> CreateGraphData(IList<StepometerModel> data)
        {
            try
            {
                IList<ChartEntry> entries = new List<ChartEntry>();
                foreach (var model in data)
                {
                    entries.Add(new ChartEntry(model.Steps)
                    {
                        Color = SKColor.Parse("#db7900"),
                        Label = model.Time.ToShortDateString(),
                        TextColor = SKColors.Yellow,
                        ValueLabelColor = SKColors.White,
                    });
                }

                return entries;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }
    }
}