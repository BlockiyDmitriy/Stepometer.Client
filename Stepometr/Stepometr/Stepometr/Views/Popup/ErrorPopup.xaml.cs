using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace Stepometer.Views.Popup
{
    public partial class ErrorPopup : PopupPage
    {
        public string Message { get; set; }

        public ErrorPopup(string message)
        {
            InitializeComponent();

            Message = message;
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}