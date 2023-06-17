using Rg.Plugins.Popup.Services;
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
    public partial class PopUpCardExercise : Rg.Plugins.Popup.Pages.PopupPage
    {
        int CurDay;
        int CurWeek;
        CardExercise cardExercise;
        ExType exType;
        public PopUpCardExercise(CardExercise cardExercise, int day, int week, ExType exType)
        {
            InitializeComponent();
            CurDay = day;
            CurWeek = week;
            this.cardExercise = cardExercise;
            this.exType = exType;
            Name.Text = cardExercise.Name;
            GIF.Source = cardExercise.GIF;
            Description.Text = cardExercise.Description;
        }

        public async void AddExerciseButton(object sender, EventArgs e)
        {
            Exercise exercise = new Exercise
            {
                Name = cardExercise.Name,
                Day = CurDay,
                Week = CurWeek,
                ExType = exType,
                GIF = cardExercise.GIF,
                NumberApproaches = CountApproaches.Text.Trim(),
                NumberRepetitions = CountRepetitions.Text.Trim(),
                Time = int.Parse(Time.Text.Trim())
            };

            App.Db.SaveExercise(exercise);
            CountApproaches.Text = "";
            CountRepetitions.Text = "";
            Time.Text = "";
            await PopupNavigation.Instance.PopAsync();
        }
    }
}