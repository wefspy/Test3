using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SQLite;
using SQLitePCL;

namespace MobileApp
{
    public class DB
    {
        private readonly SQLiteConnection curEx;
        private readonly SQLiteConnection createEx;
        private readonly SQLiteConnection curDate;
        private readonly SQLiteConnection curProgram;
        public DB(string path)
        {
            curEx = new SQLiteConnection(path);
            curEx.CreateTable<Exercise>();

            createEx = new SQLiteConnection(path+"create");
            createEx.CreateTable<Exercise>();

            curDate = new SQLiteConnection(path);
            curDate.CreateTable<CurDate>();
            curDate.Insert(new CurDate());

            curProgram = new SQLiteConnection(path);
            curProgram.CreateTable<curProgram>();
            curProgram.Insert(new curProgram() { Program = ProgramType.None });
        }

        public List<Exercise> GetCurExs(int day, int week)
        {
            return curEx.Table<Exercise>()
                .Where(exercise => exercise.Day == day && exercise.Week == week)
                .ToList();
        }

        public List<Exercise> GetCurExsByType(int day, int week, ExType exType)
        {
            return curEx.Table<Exercise>()
                .Where(exercise => exercise.Day == day && exercise.Week == week && exercise.ExType == exType)
                .ToList();
        }


        public List<Exercise> GetCreateExs(int day, int week, ExType exType)
        {
            return createEx.Table<Exercise>()
                .Where(exercise => exercise.Day == day && exercise.Week == week && exercise.ExType == exType)
                .ToList();
        }

        public int SaveExercise(Exercise exercise)
        {
            return createEx.Insert(exercise);
        }

        public int DeleteExercise(int id)
        {
            Exercise exercise = createEx.Find<Exercise>(id);
            return createEx.Delete(exercise);
        }


        public void UpdateCurDate()
        {
            var curDate = this.curDate.Table<CurDate>().First();
            this.curDate.DeleteAll<CurDate>();
            if (curDate.CurDay == 6)
            {
                curDate.CurDay = 0;
                if (curDate.CurWeek == 3) curDate.CurWeek = 0;
                else curDate.CurWeek++;
            }
            else curDate.CurDay++;
            this.curDate.Insert(curDate);
        }

        public void RestartDate()
        {
            curDate.DeleteAll<CurDate>();
            curDate.Insert(new CurDate());
        }

        public void TransferFromCreateToCurEx(ProgramType programType)
        {
            curEx.DeleteAll<Exercise>();
            ChangeCurProgramType(programType);
            curEx.InsertAll(createEx.Table<Exercise>());
            createEx.DeleteAll<Exercise>();
        }

        public int GetCurDay()
        {
            return curDate.Table<CurDate>().First().CurDay;
        }
        public int GetCurWeek()
        {
            return curDate.Table<CurDate>().First().CurWeek;
        }

        public ProgramType GetCurProgramType()
        {
            return curProgram.Table<curProgram>().First().Program;
        }

        public void ChangeCurProgramType(ProgramType programType)
        {
            curProgram.DeleteAll<curProgram>();
            curProgram.Insert(new curProgram() { Program = programType });
        }
    }
}
