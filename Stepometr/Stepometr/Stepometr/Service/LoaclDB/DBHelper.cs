using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Stepometer.Service.LoaclDB
{
    public static class DBHelper
    {
        public static string DBName = "MyDB";

        public static string DBPath = Path.Combine(Environment.GetFolderPath(
            Device.RuntimePlatform == Device.Android ?
                Environment.SpecialFolder.LocalApplicationData :
                Environment.SpecialFolder.Resources), DBName);
        public static string StepometerCollection = "stepometer";
        public static string ActivityDateId = "activityDate";
    }
}
