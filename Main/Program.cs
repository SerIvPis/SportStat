using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Eleron.FootballStatistic.TournamentData;
using Library;
using System.Data.Linq;

namespace Eleron.FootballStatistic.Main
{
    public class PhoneNumber
{
    public string Number { get; set; }
}

public class Person
{
    public IEnumerable<PhoneNumber> PhoneNumbers { get; set; }
    public string Name { get; set; }
}




    class Program
    {
        

        static void Main(string[] args)
        {
            #region Скрытое
           
                 
            #endregion
           
           
            Console.WriteLine("--- Start  SportStat ---\n");

            //Контроль за папкой источник 
           // WatchSourceFileTxt watch = new WatchSourceFileTxt();

            //Создания турнира по исходному текстому файлу
          try
          {

            ScoresAndFixtures scoresAndFixtures = new ScoresAndFixtures();

            var Sequince = scoresAndFixtures.Matches
                                .Where( m => m.dataMatch > new DateTime(2021,4,10) )
                                .Where( m => m.dataMatch < new DateTime(2021,4,20))
                                .Select(m => m);


             foreach (var team in Sequince)
            {
                System.Console.WriteLine(team);
            } 

            /* Tournament tournament = new Tournament(dupl.Name , ListStr);

            var pointsTeams = tournament.Matches
                                .Where(m => m.HomeGoal <= 1) 
                                .Where(m => m.isPlayed)   
                                .TakeWhile(m => m.Round <= 3)   
                                .Reverse()                          
                                .Select(r => r);                           
                                
            foreach (var team in pointsTeams)
            {
                System.Console.WriteLine(team);
            } */

             Console.Read();
          }
          catch (System.Exception)
          {
              System.Console.WriteLine("Что-то пошло не так...");
              
          }
            // tournament.PrintTable();            
            //tournament.PrintCalendar();
            
           

        }
    }
}