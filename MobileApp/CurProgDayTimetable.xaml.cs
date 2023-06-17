using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurProgDayTimetable : ContentPage
    {
        double TotalTime = 0;
        public CurProgDayTimetable()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ShowExercises(RazminkaCollection, ExType.Razminka);
            ShowExercises(OsnovaCollection, ExType.Osnova);
            ShowExercises(ZaminkaCollection, ExType.Zaminka);
            ShowTime(trainTimeInMin, trainTimeInSec);
        }
        public void ShowExercises(CollectionView collectionView, ExType exType)
        {
            var exercises = App.Db.GetCurExsByType(App.Db.GetCurDay(), App.Db.GetCurWeek(), exType);
            collectionView.ItemsSource = exercises;
            CalculateTotalTime(exercises);
        }

        public void ShowTime(Label labelInMin, Label labelInSec)
        {
            labelInMin.Text = ((int)TotalTime / 60).ToString();
            labelInSec.Text = ((int)TotalTime % 60).ToString();
        }

        public void CalculateTotalTime(List<Exercise> exercises)
        {
            TotalTime += exercises
                .Select(exercise => int.Parse(exercise.NumberApproaches) * int.Parse(exercise.NumberRepetitions) * exercise.Time)
                .Sum();
        }

        private async void StartTraining(object sender, EventArgs e)
        {
            var exercises = App.Db.GetCurExs(App.Db.GetCurDay(), App.Db.GetCurWeek());
            if(exercises.Count > 0)
                await Navigation.PushAsync(new PreparationTraining(exercises, 0, 0));
            else await Navigation.PushAsync(new CompletionTraining(true));
        }
    }
}