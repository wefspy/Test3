using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    public partial class App : Application
    {

        private static DB db;
        private static BankExercises bankExercises;
        public static DB Db
        { 
            get 
            {
                if (db == null)
                    db = new DB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "db.sqlite3"));
                return db; 
            } 
        }

        public static BankExercises BankEx
        {
            get
            {
                if (bankExercises == null)
                {
                    string pathBasicEx = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "basicExercises");
                    TransferCreatedDB(pathBasicEx, "basicExs.db");

                    string pathWarmUp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "warmUp");
                    TransferCreatedDB(pathWarmUp, "warmUpExs.db");

                    string pathCoolDown = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "coolDown");
                    TransferCreatedDB(pathCoolDown, "coolDownExs.db");
                    bankExercises = new BankExercises(pathBasicEx, pathWarmUp, pathCoolDown, true);
                }
                return bankExercises;
            }
        }

        private static void TransferCreatedDB(string path, string nameDB)
        {
            if (!File.Exists(path))
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                using (Stream stream = assembly.GetManifestResourceStream($"MobileApp.{nameDB}"))
                {
                    using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        stream.CopyTo(fs);
                        fs.Flush();
                    }
                }
            }
        }


        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ProgressBar());

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
