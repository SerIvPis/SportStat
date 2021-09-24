using System;
using System.Collections.Generic;

namespace Library
{
    /// <summary>
    /// Класс турнир
    /// </summary>
    public class Tournament
    {
        // Название команды
        public string Name { get; private set; }
        // Матчи турнира
        public List<Match> Matches; 
        public Tournament()
        {
            
        }
        public Tournament(string _name)
        {
            Name = _name;
            Matches = new List<Match>();
        }
        public void ImportMatches(List<string> _listStringMatch)
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
               
                    Matches.Add(new Match( item, round ));                         

            }
        }
        public void PrintCalendar()
        {
            foreach (Match item in Matches)
            {
                System.Console.WriteLine(item.ToString());
            }
        }
    }
}