using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Eleron.FootballStatistic.TournamentData;
using Library;

namespace Eleron.FootballStatistic.Main
{
    class Program
    {
        

        static void Main(string[] args)
        {
            /* var queryOperators =from method in typeof(Enumerable).GetMethods()
                                group method by method.Name into queryOperator
                                orderby queryOperator.Key
                                select new {
                                    Operator = queryOperator.Key,
                                    Overloads = queryOperator.Count()
                                };
            
            foreach (var qo in queryOperators)
            {
                System.Console.WriteLine(qo.ToString());
            }
 */

            Console.WriteLine("--- Start  SportStat ---\n");

            //Контроль за папкой источник 
            WatchSourceFileTxt watch = new WatchSourceFileTxt();

            //Создания турнира по исходному текстому файлу
          
            string PathFile =  watch.workDir.GetFiles()[1].FullName;
            List<string> ListStr = new List<string>(File.ReadAllLines( PathFile)); 
            Tournament tournament = new Tournament(watch.workDir.GetFiles()[1].Name , ListStr);

            var pointsTeams = from match in tournament.Matches
                                where match.HomeTeam.Equals(new Team("Монако")) || match.GuestTeam.Equals(new Team("Монако"))  
                                select match;
                                
                                

             
            foreach (var team in pointsTeams)
            {
                System.Console.WriteLine(team);
                /* foreach (var item in team)
                {
                    System.Console.WriteLine(item);
                } */
            }
            

            // tournament.PrintTable();            
            //tournament.PrintCalendar();
            
            Console.Read();

        }
    }
}