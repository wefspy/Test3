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
    public partial class CreatProgDayTimetable : ContentPage
    {
        bool[] Days;
        bool[] Weeks;
        private int curDay;
        private int curWeek;
        double TotalTime;
        public CreatProgDayTimetable(bool[] days, bool[] weeks)
        {
            InitializeComponent();
            Days = days;
            Weeks = weeks;
            TotalTime = 0;
            GetCurDayAndWeek();
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
            var exercises = App.Db.GetCreateExs(curDay, curWeek, exType);
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

        private void GetCurDayAndWeek()
        {
            for (int i = 0; i < 7; i++)
                if (Days[i])
                {
                    Days[i] = false;
                    curDay = i;
                    break;
                }
        }

        private bool NeedContinue()
        {
            return Days.Any(day => day);
        }

        private async void ContinueOrEnd(object sender, EventArgs e)
        {
            if (NeedContinue()) await Navigation.PushAsync(new CreatProgDayTimetable(Days, Weeks));
            else
            {
                FinishCreateProgram();
                await Navigation.PushAsync(new Training()); // Переделать!! Куда будет выбрасывать после создание тренировки.
            }
        }

        private void FinishCreateProgram()
        {
            App.Db.TransferFromCreateToCurEx(ProgramType.Created);
            App.Db.RestartDate();
        }

        private void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            int imageId = (int)imageButton.CommandParameter;
            App.Db.DeleteExercise(imageId);
            OnAppearing();
        }

        private async void Create_VuborRazminka(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListWarmUpExercises(curDay, curWeek));
        }
        private async void Create_VuborOsnova(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListBasicExercises(curDay, curWeek));
        }
        private async void Create_VuborZaminka(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListCoolDownExercises(curDay, curWeek));
        }
    }
}