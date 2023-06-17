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
    public partial class Training : ContentPage
    {
        public Training()
        {
            InitializeComponent();
            var curProgType = App.Db.GetCurProgramType();
            DefineImageButton(curProgType);
            DefineColorButton(curProgType);
        }

        private async void Program_Now(object sender, EventArgs e)
        {
            if(App.Db.GetCurProgramType() != ProgramType.None)
                await Navigation.PushAsync(new CurProgSchedule());
            else await DisplayAlert("Упс...", "Пожалуйста, выберите или создайте собственную программу тренировок", "ок");
        }

        private async void Create_Program(object sender, EventArgs e)
        {
            if (App.Db.GetCurProgramType() == ProgramType.None)
                await Navigation.PushAsync(new CreatProgDaysSelection());
            else
            {
                var res = await DisplayAlert("Внимание", "Вы уверены, что хотите заменить существующую программу тренировок?", "Да", "Нет");
                if(res) await Navigation.PushAsync(new CreatProgDaysSelection());
            }
        }

        private void DefineImageButton(ProgramType curProgType)
        {
            if (curProgType == ProgramType.None) program_now.Source = "program";
            else if (curProgType == ProgramType.Created) program_now.Source = "curProgCreated";
            else if (curProgType == ProgramType.OnFeet) program_now.Source = "curProgFeet";
            else if (curProgType == ProgramType.OnBody) program_now.Source = "curProgBody";
            else if (curProgType == ProgramType.WholeBody) program_now.Source = "curProgWholeBody";
        }

        private void DefineColorButton(ProgramType curProgType)
        {
            if (curProgType == ProgramType.None) program_now.BackgroundColor = new Color(160, 162, 164);
            else program_now.BackgroundColor = new Color(243,89,108);
        }
    }
}