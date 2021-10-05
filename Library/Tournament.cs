using System;
using System.Collections.Generic;
using System.Linq;

namespace Eleron.FootballStatistic.TournamentData
{
    /// <summary>
    /// Класс турнир
    /// </summary>
    public class Tournament
    {
        #region Свойства и поля
         // Название команды
        public string Name { get; private set; }
        //
        public List<TeamInTable> TeamsInTable { get; private set; }
        // Матчи турнира
        public HashSet<Match> Matches; 

        #endregion Свойства и поля
       
        #region Конструкторы
        public Tournament()
        {
            
        }
        public Tournament(string _name)
        {
            Name = _name;
            Matches = new HashSet<Match>();
        }

        public Tournament(string _name, List<string> listString)
        {
            Name = _name;
            Matches = new HashSet<Match>();
            
            ImportFromListString(listString);
            CreateTeamsTable();
            AddResults();
        }

        private void AddResults()
        {
            foreach (TeamInTable item in TeamsInTable)
            {
               IEnumerable<Match> seq = Matches
                    .Where(m => ((m.GuestTeam.Equals(item.TeamName)) || (m.HomeTeam.Equals(item.TeamName))) && m.isPlayed)
                    .Select(ma => ma);
               foreach ( Match mch in seq)
               {
                   item.Add(mch);
               }              
            }
        }

        private void CreateTeamsTable()
        {
            if ((Matches != null) && (Matches.Count > 0) )
            {
                HashSet<TeamInTable> TeamsSet = new HashSet<TeamInTable>();
                foreach (Match curMatch in Matches)
                {
                    TeamsSet.Add(new TeamInTable( curMatch.HomeTeam));
                    TeamsSet.Add(new TeamInTable( curMatch.GuestTeam));
                }
                TeamsInTable = new List<TeamInTable>(TeamsSet);               
            }
            else throw new NullReferenceException();
        }

        #endregion

        #region Методы
        /// <summary>
        /// Импорт данных из массива строк определенного формата
        /// и создания соответствующего Hashset<Match>
        /// </summary>
        /// <param name="_listStringMatch"></param>
        public void ImportFromListString(List<string> _listStringMatch)
        {
            byte round = 0;
            foreach (string item in _listStringMatch)
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
                 Matches.Add(new Match( item, round, Name )); 
            }
        }
        /// <summary>
        /// Вывод на консоль содержимого HashSet<Match>
        /// </summary>
        public void PrintCalendar()
        {
            foreach (Match item in Matches)
            {
               
                System.Console.WriteLine(item.ToString());
            }
        }
        
        public void PrintTable()
        {
            TeamsInTable.Sort();
            System.Console.WriteLine($"{"Команды",-23}{"W",3}{"D",3}{"L",3}{"GF",3}{"GA",3}{"GD",3}{"Pts",4}{"Gms",4}");

            foreach (var item in TeamsInTable)
            {
                System.Console.WriteLine(new string('-',50));
                System.Console.WriteLine(item.ToString());
            }
        }
             
        #endregion
     
    }
}