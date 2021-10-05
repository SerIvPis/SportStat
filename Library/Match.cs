using System;
using System.Text.RegularExpressions;

namespace Eleron.FootballStatistic.TournamentData
{
    public class Match: IEquatable<Match>, IComparable<Match>
    {
#region Свойства
        
        public bool isPlayed { get; private set; }
        public Team HomeTeam { get; private set; }
        public Team GuestTeam { get; private set; }
        public byte HomeGoal { get; set; }
        public byte GuestGoal { get; set; }
        public  byte Round{get; private set;}
        public  DateTime dataMatch { get; private set; }
        public  string nameTournament {get; private set; }
             
        #endregion
        
#region Конструкторы

        public Match()
        {
            
        }
        //Конструктор по строке
        public Match(string strMatch,
                     byte round,
                     string nameTournament)
        {
            this.nameTournament = nameTournament;
            Round = round;   
            isPlayed = false;  

            try
            {
                //Извлекаем дату матча
                dataMatch = TakeOfDate( strMatch);   

                //Извлекаем голы матча
                TakeOfGoals(strMatch);      

                //Извлекаем имена команд     
                TakeOfNamesTeam(strMatch);

                #region Старая реализация удалить
                /* 
                //Если имеется счет матча
                if (Regex.IsMatch(strMatch, @"[0-9][0-9]?\s:\s[0-9][0-9]?"))
                {
                    //Извлекаем  голы матча
                    string strScore = "";  
                    strScore = Regex.Match(strMatch, @"[0-9][0-9]?\s:\s[0-9][0-9]?").Value.Trim();
                    HomeGoal = byte.Parse(Regex.Match(strScore, @"[0-9][0-9]?\s").Value);
                    GuestGoal = byte.Parse(Regex.Match(strScore, @"\s[0-9][0-9]?").Value);

                    //Извлекаем имена команд
                    string strNameTeam = Regex.Match(strMatch, @"\s\D+").Value.Trim();
                    string[] NameTeam = strNameTeam.Split('-', StringSplitOptions.TrimEntries);
                    HomeTeam = new Team(NameTeam[0]);
                    GuestTeam = new Team(NameTeam[1]);   
                    isPlayed = true;
                }

                //Если матч не сыгран, но есть время
                if (Regex.IsMatch(strMatch, @"[0-9]{2}:[0-9]{2}"))
                {
                     //System.Console.WriteLine($"Матч не сыгран: {strMatch}");
                     //Извлекаем  голы матча
                    HomeGoal = byte.MaxValue;
                    GuestGoal = byte.MaxValue;

                    //Извлекаем имена команд
                    string strNameTeam = Regex.Match(strMatch, @"\s\D+").Value.Trim();
                    string[] NameTeam = strNameTeam.Split('-', StringSplitOptions.TrimEntries);
                    HomeTeam = new Team(NameTeam[0]);
                    GuestTeam = new Team(NameTeam[1]);     
                }

                 if (Regex.IsMatch(strMatch, @"_ : _"))
                {
                   // System.Console.WriteLine($"Время отсутствует: {strMatch}");

                    //Извлекаем  голы матча
                    HomeGoal = byte.MaxValue;
                    GuestGoal = byte.MaxValue;

                    //Извлекаем имена команд
                    // вырезаем подстроку с именами
                    string strNameTeam = strMatch.Substring(10, strMatch.IndexOf("_ : _") - 10);  
                    string[] NameTeam = strNameTeam.Split('-', StringSplitOptions.TrimEntries);
                    HomeTeam = new Team(NameTeam[0]);
                    GuestTeam = new Team(NameTeam[1]);   
                } */

                     
                #endregion
            }
            catch (System.Exception ex)
            {
               System.Console.WriteLine($"Неверный формат строки: {strMatch} \n{ex.Message}");               
            }
               
        }

       
  #endregion
        #region Методы

        /// <summary>
        /// Извлекаем имена команд из строки матча
        /// </summary>
        /// <param name="strMatch"></param>
        private void TakeOfNamesTeam(string strMatch)
        {
            string strNameTeam = strMatch.Substring(10, strMatch.IndexOf(":") - 13);  
            string[] NameTeam = strNameTeam.Split('-', StringSplitOptions.TrimEntries);
            HomeTeam = new Team(NameTeam[0]);
            GuestTeam = new Team(NameTeam[1]);  
        }
      
        /// <summary>
        /// Метод извлекает количество голов из строки,
        /// либо записывает максимальные значения, если матч
        /// не состоялся.
        /// </summary>
        /// <param name="strMatch"></param>
        private void TakeOfGoals(string strMatch)
        {
            HomeGoal = byte.MaxValue;
            GuestGoal = byte.MaxValue;

            if (Regex.IsMatch(strMatch, @"[0-9][0-9]?\s:\s[0-9][0-9]?"))
                {
                    string strScore = "";  
                    strScore = Regex.Match(strMatch, @"[0-9][0-9]?\s:\s[0-9][0-9]?").Value.Trim();
                    HomeGoal = byte.Parse(Regex.Match(strScore, @"[0-9][0-9]?\s").Value);
                    GuestGoal = byte.Parse(Regex.Match(strScore, @"\s[0-9][0-9]?").Value);
                    isPlayed = true;
                }
           
        }
       
        /// <summary>
        /// Метод извлекает дату матча из строки
        /// </summary>
        /// <param name="strMatch"></param>
        /// <returns></returns>
        private DateTime TakeOfDate(string strMatch)
        {
            Regex regex = new Regex(@"[0-9]{2}.[0-9]{2}.[0-9]{4}");
            MatchCollection matches = regex.Matches(strMatch);
            if (matches.Count == 1)
            {
                DateTime TimeToMatch = DateTime.MinValue;
                DateTime.TryParse(matches[0].Value, out TimeToMatch);
                return TimeToMatch;
            }
            else  throw new FormatException();
        }


        

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (object.ReferenceEquals(this, obj))
                return true;

            Team team = obj as Team;
            if (team == null)
                return false;
            else
                return this.Equals(team);
        }

        public override int GetHashCode()
        {
            return HomeTeam.Name.GetHashCode() * GuestTeam.Name.GetHashCode() + dataMatch.GetHashCode();
        }

        public override string ToString()
        {
            return $" {nameTournament} {dataMatch.ToShortDateString()} {Round} Тур {HomeTeam.Name} {HomeGoal} : {GuestGoal} {GuestTeam.Name} Played = {isPlayed} ";
        }

        public int CompareTo(Match other)
        {
            if (this.dataMatch > other.dataMatch)
            {
                return 1;
            }
            else if (this.dataMatch < other.dataMatch)
            {
                return -1;
            }
            else 
                return 0;
        }

        public bool Equals(Match other)
        {
            if (this.HomeTeam.Equals(other.HomeTeam) &&
                this.GuestTeam.Equals(other.GuestTeam) &&
                this.dataMatch == other.dataMatch &&
                this.HomeGoal == other.HomeGoal &&
                this.GuestGoal == other.GuestGoal)
            {
                return true;
            }
            else
                return false;
            
        }

        #endregion

    }
}