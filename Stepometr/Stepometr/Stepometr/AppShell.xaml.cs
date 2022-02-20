using Stepometer.Page;
using System;
using System.Collections.Generic;
using Stepometer.Controls;
using Xamarin.Forms;

namespace Stepometer
{
    public partial class AppShell : Shell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();

        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            BindingContext = this;
        }

        void RegisterRoutes()
        {
            Routes.Add(nameof(LoginPage), typeof(LoginPage));
            Routes.Add(nameof(StepometerPage), typeof(StepometerPage));
            Routes.Add(nameof(HistoryPage), typeof(HistoryPage));
            Routes.Add(nameof(FriendsPage), typeof(FriendsPage));
            Routes.Add(nameof(AchievePage), typeof(AchievePage));

            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }
    }
}