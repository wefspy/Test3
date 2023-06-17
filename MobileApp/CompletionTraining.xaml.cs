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
    public partial class CompletionTraining : ContentPage
    {
        public CompletionTraining(bool weekend)
        {
            InitializeComponent();
            if (weekend)
            {
                Image.Source = "congratulationsOnWeekend";
                Message.Text = "У вас сегодня выходной.";
            }
            else
            {
                Image.Source = "endTraining";
                Message.Text = "Вы закончили тренировку.";
            }
        }

        private async void EndTraining(object sender, EventArgs e)
        {
            App.Db.UpdateCurDate();
            await Navigation.PushAsync(new CurProgSchedule());
        }
    }
}