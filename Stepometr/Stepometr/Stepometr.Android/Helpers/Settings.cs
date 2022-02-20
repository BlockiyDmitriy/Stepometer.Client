using System;
using System.Globalization;
using Stepometer.Settings;

namespace Stepometer.Droid.Helpers
{
    public static class Settings
    {
        private static SettingsHelper appSettings;

        private static SettingsHelper AppSettings
        {
            get { return appSettings ?? (appSettings = new SettingsHelper()); }
        }
        
        /// <summary>
        /// Gets or sets the next goal. for total
        /// </summary>
        /// <value>The next goal.</value>
        public static Int64 NextGoal
        {
            get { return AppSettings.GetValueOrDefault(CounterSettings.NextGoalKey, CounterSettings.NextGoalDefault); }
            set
            {
                //if value has changed then save it!
                if (AppSettings.AddOrUpdateValue(CounterSettings.NextGoalKey, value))
                    AppSettings.Save();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show progress notifications.
        /// </summary>
        /// <value><c>true</c> if progress notifications; otherwise, <c>false</c>.</value>
        public static bool ProgressNotifications
        {
            get { return AppSettings.GetValueOrDefault(CounterSettings.ProgressNotificationsKey, CounterSettings.ProgressNotificationsDefault); }
            set
            {
                //if value has changed then save it!
                if (AppSettings.AddOrUpdateValue(CounterSettings.ProgressNotificationsKey, value))
                    AppSettings.Save();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show accumulative notifications.
        /// </summary>
        /// <value><c>true</c> if accumulative notifications; otherwise, <c>false</c>.</value>
        public static bool AccumulativeNotifications
        {
            get
            {
                return AppSettings.GetValueOrDefault(CounterSettings.AccumulativeNotificationsKey, CounterSettings.AccumulativeNotificationsDefault);
            }
            set
            {
                //if value has changed then save it!
                if (AppSettings.AddOrUpdateValue(CounterSettings.AccumulativeNotificationsKey, value))
                    AppSettings.Save();
            }
        }

        /// <summary>
        /// Gets or sets the high score day.
        /// </summary>
        /// <value>The high score day.</value>
        public static DateTime FirstDayOfUse
        {
            get
            {
                var firstDay = AppSettings.GetValueOrDefault(CounterSettings.FirstDayOfUseKey, (long) -1);
                if (firstDay == -1)
                {
                    FirstDayOfUse = DateTime.Today;
                    CurrentDay = DateTime.Today;
                    return DateTime.Today;
                }
                else
                    return new DateTime(firstDay);
            }
            set
            {
                //if value has changed then save it!
                if (AppSettings.AddOrUpdateValue(CounterSettings.FirstDayOfUseKey, value))
                    AppSettings.Save();
            }
        }

        /// <summary>
        /// Ensure that high score is not today
        /// </summary>
        /// <value><c>true</c> if today is high score; otherwise, <c>false</c>.</value>
        public static bool TodayIsHighScore
        {
            get
            {
                //if first day then always return false;
                if (FirstDayOfUse.DayOfYear == HighScoreDay.DayOfYear && FirstDayOfUse.Year == HighScoreDay.Year)
                    return false;

                //else is same day.
                return DateTime.Today.DayOfYear == HighScoreDay.DayOfYear && DateTime.Today.Year == HighScoreDay.Year;
            }
        }

        /// <summary>
        /// Gets or sets the goal message to display to user
        /// </summary>
        /// <value>The goal today message.</value>
        public static string GoalTodayMessage
        {
            get { return AppSettings.GetValueOrDefault(CounterSettings.GoalTodayMessageKey, CounterSettings.GoalTodayMessageDefault); }
            set
            {
                //if value has changed then save it!
                if (AppSettings.AddOrUpdateValue(CounterSettings.GoalTodayMessageKey, value))
                    AppSettings.Save();
            }
        }

        /// <summary>
        /// Gets or sets the high score.
        /// </summary>
        /// <value>The high score.</value>
        public static Int64 HighScore
        {
            get { return AppSettings.GetValueOrDefault(CounterSettings.HighScoreKey, CounterSettings.HighScoreDefault); }
            set
            {
                //if value has changed then save it!
                if (AppSettings.AddOrUpdateValue(CounterSettings.HighScoreKey, value))
                    AppSettings.Save();
            }
        }

        /// <summary>
        /// Gets or sets the goal today day.
        /// Only display messages if it is currenlty the same day.
        /// </summary>
        /// <value>The goal today day.</value>
        public static DateTime GoalTodayDay
        {
            get { return AppSettings.GetValueOrDefault(CounterSettings.GoalTodayDayKey, CounterSettings.GoalTodayDayDefault); }
            set
            {
                //if value has changed then save it!
                if (AppSettings.AddOrUpdateValue(CounterSettings.GoalTodayDayKey, value))
                    AppSettings.Save();
            }
        }

        /// <summary>
        /// Gets or sets the high score day.
        /// </summary>
        /// <value>The high score day.</value>
        public static DateTime HighScoreDay
        {
            get { return AppSettings.GetValueOrDefault(CounterSettings.HighScoreDayKey, CounterSettings.HighScoreDayDefault); }
            set
            {
                //if value has changed then save it!
                if (AppSettings.AddOrUpdateValue(CounterSettings.HighScoreDayKey, value))
                    AppSettings.Save();
            }
        }

        /// <summary>
        /// Gets or sets the day we are currently tracking
        /// </summary>
        /// <value>The current day.</value>
        public static DateTime CurrentDay
        {
            get { return AppSettings.GetValueOrDefault(CounterSettings.CurrentDayKey, CounterSettings.CurrentDayDefault); }
            set
            {
                //if value has changed then save it!
                if (AppSettings.AddOrUpdateValue(CounterSettings.CurrentDayKey, value))
                    AppSettings.Save();
            }
        }

        /// <summary>
        /// Gets or sets the steps before today.
        /// </summary>
        /// <value>The steps before today.</value>
        public static Int64 StepsBeforeToday
        {
            get { return AppSettings.GetValueOrDefault(CounterSettings.StepsBeforeTodayKey, CounterSettings.StepsBeforeTodayDefault); }
            set
            {
                //if value has changed then save it!
                if (AppSettings.AddOrUpdateValue(CounterSettings.StepsBeforeTodayKey, value))
                    AppSettings.Save();
            }
        }

        /// <summary>
        /// Gets or sets the cadence. (pace of walking)
        /// </summary>
        /// <value>The cadence.</value>
        public static string Cadence
        {
            get { return AppSettings.GetValueOrDefault(CounterSettings.CadenceKey, CounterSettings.CadenceDefault); }
            set
            {
                //if value has changed then save it!
                if (AppSettings.AddOrUpdateValue(CounterSettings.CadenceKey, value))
                    AppSettings.Save();
            }
        }

        /// <summary>
        /// Gets or sets the current day steps.
        /// </summary>
        /// <value>The current day steps.</value>
        public static Int64 CurrentDaySteps
        {
            get { return AppSettings.GetValueOrDefault(CounterSettings.CurrentDayStepsKey, CounterSettings.CurrentDayStepsDefault); }
            set
            {
                //if value has changed then save it!
                if (AppSettings.AddOrUpdateValue(CounterSettings.CurrentDayStepsKey, value))
                    AppSettings.Save();
            }
        }

        /// <summary>
        /// Gets or sets the total steps since the beginning of tracking
        /// </summary>
        /// <value>The total steps.</value>
        public static Int64 TotalSteps
        {
            get { return AppSettings.GetValueOrDefault(CounterSettings.TotalStepsKey, CounterSettings.TotalStepsDefault); }
            set
            {
                //if value has changed then save it!
                if (AppSettings.AddOrUpdateValue(CounterSettings.TotalStepsKey, value))
                    AppSettings.Save();
            }
        }

        /// <summary>
        /// Gets or sets the weight. (used for calulations)
        /// </summary>
        /// <value>The weight.</value>
        public static int Weight
        {
            get { return AppSettings.GetValueOrDefault(CounterSettings.WeightKey, CounterSettings.WeightDefault); }
            set
            {
                //if value has changed then save it!
                if (AppSettings.AddOrUpdateValue(CounterSettings.WeightKey, value))
                    AppSettings.Save();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether we want to use enhanced tracking
        /// </summary>
        /// <value><c>true</c> if enhanced; otherwise, <c>false</c>.</value>
        public static bool Enhanced
        {
            get { return AppSettings.GetValueOrDefault(CounterSettings.EnhancedKey, CounterSettings.EnhancedDefault); }
            set
            {
                //if value has changed then save it!
                if (AppSettings.AddOrUpdateValue(CounterSettings.EnhancedKey, value))
                    AppSettings.Save();
            }
        }

        /// <summary>
        /// Gets a value indicating whether to use kilometeres.
        /// </summary>
        /// <value><c>true</c> if use kilometeres; otherwise, <c>false</c>.</value>
        public static bool UseKilometeres
        {
            get { return CultureInfo.CurrentCulture.Name != "en-US"; }
        }

        
    }
}