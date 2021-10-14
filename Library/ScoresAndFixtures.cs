using System;
using System.Collections.Generic;
using System.IO;

namespace Eleron.FootballStatistic.TournamentData
{
    public class ScoresAndFixtures
    {
    #region Свойства и поля
         
        public HashSet<Match> Matches; 
        //public  DirectoryInfo workDir{get; private set;}
        const string PathSource = @".\Source\Example";

    #endregion Свойства и поля
       
    #region Конструкторы
        public ScoresAndFixtures()
        {
            Matches = new HashSet<Match>();
           

            AddScoreFromFile(PathSource);
        }

       

        #endregion

        #region Методы
         private void AddScoreFromFile(string pathSource)
        {
            DirectoryInfo workDir = new DirectoryInfo(PathSource);
            if (!workDir.Exists)
            {
                workDir.Create();
            }
            foreach (FileInfo item in workDir.GetFiles())
            {
                AddMatchFromListString(item);                
            }
        }

        public void AddMatchFromListString(FileInfo file)          
        {
            
            List<string> fileAsStringList = new List<string>( File.ReadAllLines(file.FullName));

            byte round = 0;
            foreach (string item in fileAsStringList)
            {
                //С учетом последовательного изложения матчей, 
                //выставляем номера раунда по строке содержащий 
                //слово "тур"
                 if (item.IndexOf("тур") != -1)
                 {
                    round = byte.Parse(item.Split('-')[0]);
                    continue;
                 }     
                
                 // Создаем объект Маtch по данным из строки
                 Matches.Add(new Match( item, round, Path.GetFileNameWithoutExtension(file.FullName) )); 
            }
        }


        #endregion
    }
}