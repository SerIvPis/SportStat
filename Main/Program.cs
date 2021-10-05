using System;
using System.Collections.Generic;
using System.IO;
using Eleron.FootballStatistic.TournamentData;

namespace Eleron.FootballStatistic.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Start  SportStat ---\n");

            //Контроль за папкой источник 
            WatchSourceFileTxt watch = new WatchSourceFileTxt();

            //Создания турнира по исходному текстому файлу
          
            string PathFile =  watch.workDir.GetFiles()[1].FullName;
            List<string> ListStr = new List<string>(File.ReadAllLines( PathFile)); 
            Tournament tournament = new Tournament(watch.workDir.GetFiles()[1].Name , ListStr);
            tournament.PrintTable();            
            //tournament.PrintCalendar();
            
            Console.Read();
        }
    }
}
