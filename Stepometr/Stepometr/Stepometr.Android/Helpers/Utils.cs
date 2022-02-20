using Android.OS;
using System;
using System.Globalization;
using Android.Content.PM;

namespace Stepometer.Droid.Helpers
{
    public static class Utils
    {
        public static bool IsKitKatWithStepCounter(PackageManager pm)
        {
            // Require at least Android KitKat
            int currentApiVersion = (int) Build.VERSION.SdkInt;
            // Check that the device supports the step counter and detector sensors
            return currentApiVersion >= 19
                   && pm.HasSystemFeature(Android.Content.PM.PackageManager.FeatureSensorStepCounter);
        }

        public static string DateString
        {
            get
            {
                var day = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
                var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
                var dayNum = DateTime.Now.Day;
                if (Settings.UseKilometeres)
                    return day + " " + dayNum + " " + month;

                return day + " " + month + " " + dayNum;
            }
        }

        public static string GetDateStaring(DateTime date)
        {
            string day = date.ToString("ddd");
            string month = date.ToString("MMM");
            int dayNum = date.Day;
            if (Settings.UseKilometeres)
                return day + " " + dayNum + " " + month;

            return day + " " + month + " " + dayNum;
        }

        public static bool IsSameDay
        {
            get
            {
                return DateTime.Today.DayOfYear == Settings.CurrentDay.DayOfYear &&
                       DateTime.Today.Year == Settings.CurrentDay.Year;
            }
        }

        public static string FormatSteps(Int64 steps)
        {
            return steps.ToString("N0");
        }
    }
}