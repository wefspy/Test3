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
    public partial class CurProgSchedule : ContentPage
    {
        public CurProgSchedule()
        {
            InitializeComponent();
            DefineImageWeek();
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CurProgDayTimetable());
        }

        private void DefineImageWeek()
        {
            var weeks = new List<Image>() { week0, week1, week2, week3 };
            var curWeek = App.Db.GetCurWeek();
            for (int i = 0; i < curWeek; i++)
                weeks[i].Source = "workWeekDay7";
            for(int i = 2; i > curWeek; i--)
                weeks[i].Source = "workWeekDay0";
      
            string variableName;
            if (curWeek == 3) variableName = "lastWeekDay";
            else
            {
                weeks[3].Source = "lastWeekDay0";
                variableName = "workWeekDay";
            }
            weeks[curWeek].Source = variableName + App.Db.GetCurDay().ToString();
        }
    }
}