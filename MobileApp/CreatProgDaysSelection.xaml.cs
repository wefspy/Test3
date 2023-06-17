using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatProgDaysSelection : ContentPage
    {
        public CreatProgDaysSelection()
        {
            InitializeComponent();
        }

        private async void Create1(object sender, EventArgs e)
        {
            bool[] days = new bool[7];
            days[0] = Monday.IsChecked;
            days[1] = Tuesday.IsChecked;
            days[2] = Wednesday.IsChecked;
            days[3] = Thursday.IsChecked;
            days[4] = Friday.IsChecked;
            days[5] = Saturday.IsChecked;
            days[6] = Sunday.IsChecked;
            if (!(Monday.IsChecked || Tuesday.IsChecked || Wednesday.IsChecked || Thursday.IsChecked || Friday.IsChecked || Saturday.IsChecked || Sunday.IsChecked))
            {
                await DisplayAlert("Упс...", "Пожалуйста, выберите хотя бы один день для тренировок", "ок");
            }
            else
            {
                await Navigation.PushAsync(new CreatProgWeeksSelection(days));
            }

        }

        private void Create0_Monday(object sender, CheckedChangedEventArgs e)
        {

        }
        private void Create0_Tuesday(object sender, CheckedChangedEventArgs e)
        {

        }
        private void Create0_Wednesday(object sender, CheckedChangedEventArgs e)
        {

        }
        private void Create0_Thursday(object sender, CheckedChangedEventArgs e)
        {

        }
        private void Create0_Friday(object sender, CheckedChangedEventArgs e)
        {

        }
        private void Create0_Saturday(object sender, CheckedChangedEventArgs e)
        {

        }
        private void Create0_Sunday(object sender, CheckedChangedEventArgs e)
        {

        }


    }
}