using System;
using System.Collections.Generic;
using System.IO;
using Library;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Start  SportStat ---\n");

            //Контроль за папкой источник 
            WatchSourceFileTxt watch = new WatchSourceFileTxt();

            //Создания турнира по исходному текстому файлу
            Tournament tournament = new Tournament("Пример");
            List<string> FileText = new List<string>(File.ReadAllLines( watch.workDir.GetFiles()[0].FullName)); 
            tournament.ImportMatches(FileText);
            tournament.PrintCalendar();
         
            Console.Read();
        }
    }
}
