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
    public partial class CreatProgWeeksSelection : ContentPage
    {
        bool[] Days;
        public CreatProgWeeksSelection(bool[] days)
        {
            Days = days;
            InitializeComponent();
        }

        private async void Create2(object sender, EventArgs e)
        {
            bool[] weeks = new bool[4];

            weeks[0] = week1.IsChecked;
            weeks[1] = week2.IsChecked;
            weeks[2] = week3.IsChecked;
            weeks[3] = week4.IsChecked;

            if (!(week1.IsChecked || week2.IsChecked || week3.IsChecked || week4.IsChecked))
            {
                await DisplayAlert("Упс...", "Пожалуйста, выберите хотя бы одну неделю для тренировок", "ок");
            }
            else
            {
                await Navigation.PushAsync(new CreatProgDayTimetable(Days, weeks));
            }

        }
    }
}