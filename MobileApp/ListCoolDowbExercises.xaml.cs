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
    public partial class ListCoolDownExercises : ContentPage
    {
        int CurDay;
        int CurWeek;
        public ListCoolDownExercises(int day, int week)
        {
            InitializeComponent();
            CurDay = day;
            CurWeek = week;
        }
        protected override void OnAppearing()
        {
            CoolDownCollection.ItemsSource = App.BankEx.GetCoolDown();
        }
        private async void CreatePopUpCard(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int parameter = int.Parse(button.CommandParameter.ToString());
            await PopupNavigation.Instance.PushAsync(new PopUpCardExercise(App.BankEx.GetCoolDownExercise(parameter), CurDay, CurWeek, ExType.Zaminka));
        }
    }
}