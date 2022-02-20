using System;
using System.Collections.Generic;
using System.Text;

namespace Stepometer.Settings
{
    public static class CounterSettings
    {
        #region Setting Constants

        public const string DailyStepGoalKey = "DailyStepGoal";
        public static readonly long DailyStepGoalDefault = 1000;

        public const string WeightKey = "Weight";
        public static readonly int WeightDefault = 0;

        public const string CadenceKey = "Cadence3";
        public static readonly string CadenceDefault = "3";

        public const string EnhancedKey = "Enhanced";
        public static readonly bool EnhancedDefault = false;

        public const string ProgressNotificationsKey = "ProgressNotifications";
        public static readonly bool ProgressNotificationsDefault = true;

        public const string AccumulativeNotificationsKey = "AccumulativeNotifications";
        public static readonly bool AccumulativeNotificationsDefault = true;

        public const string CurrentDayKey = "CurrentDay";
        public static readonly DateTime CurrentDayDefault = DateTime.Today;

        public const string CurrentDayStepsKey = "CurrentDaySteps";
        public static readonly Int64 CurrentDayStepsDefault = 0;


        public const string StepsBeforeTodayKey = "StepsBeforeToday";
        public static readonly Int64 StepsBeforeTodayDefault = 0;

        public const string TotalStepsKey = "TotalSteps";
        public static readonly Int64 TotalStepsDefault = 0;

        public const string GoalTodayMessageKey = "GoalTodayMessage";
        public static readonly string GoalTodayMessageDefault = string.Empty;

        public const string GoalTodayDayKey = "GoalTodayDay";
        public static readonly DateTime GoalTodayDayDefault = DateTime.Today.AddDays(-1);

        public const string NextGoalKey = "NextGoal";
        public const Int64 NextGoalDefault = 100000;

        public const string HighScoreKey = "HighScore";
        public const Int64 HighScoreDefault = 0;

        public const string HighScoreDayKey = "HighScoreDay";
        public static readonly DateTime HighScoreDayDefault = DateTime.Today;


        public const string FirstDayOfUseKey = "FirstDayOfUse";
        public static readonly DateTime FirstDayOfUseDefault = DateTime.Today;

        #endregion
    }
}
