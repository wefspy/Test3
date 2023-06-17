using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListWarmUpExercises : ContentPage
    {
        int CurDay;
        int CurWeek;
        public ListWarmUpExercises(int day, int week)
        {
            InitializeComponent();
            CurDay = day;
            CurWeek = week;
        }

        protected override void OnAppearing()
        {
            warmUpCollection.ItemsSource = App.BankEx.GetWarmUp();
        }
        private async void CreatePopUpCard(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int parameter = int.Parse(button.CommandParameter.ToString());
            await PopupNavigation.Instance.PushAsync(new PopUpCardExercise(App.BankEx.GetWarnUpExercise(parameter), CurDay, CurWeek, ExType.Razminka));
        }
    }
}