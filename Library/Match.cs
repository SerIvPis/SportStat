using System;
using System.Text.RegularExpressions;

namespace Library
{
    public class Match
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
        
#region   Конструкторы

        public Match()
        {
            
        }
        //Конструктор по строке
        public Match(string strMatch, byte round)
        {
            nameTournament = "Пример Англия";
            Round = round;   
            isPlayed = false;    
            try
            {
                //Извлекаем дату матча
                Regex regex = new Regex(@"[0-9]{2}.[0-9]{2}.[0-9]{4}");
                MatchCollection matches = regex.Matches(strMatch);
                if (matches.Count == 1)
                {
                    DateTime TimeToMatch = DateTime.MinValue;
                    DateTime.TryParse(matches[0].Value, out TimeToMatch);
                    dataMatch = TimeToMatch;
                }
                else  throw new FormatException();

                //Извлекаем названия команд матча

                //Если имеется счет матча
                if (Regex.IsMatch(strMatch, @"[0-9][0-9]?\s:\s[0-9][0-9]?"))
                {
                   // System.Console.WriteLine($"Матч сыгран: {strMatch}");
                    
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
                    string strNameTeam = Regex.Match(strMatch, @"\s\D+").Value.Trim();
                    string[] NameTeam = strNameTeam.Split('-', StringSplitOptions.TrimEntries);
                    HomeTeam = new Team(NameTeam[0]);
                    GuestTeam = new Team(NameTeam[1]);   
                }
            }
            catch (System.Exception ex)
            {
               System.Console.WriteLine($"Неверный формат строки: {strMatch} \n{ex.Message}");               
            }
               
        }

        
        #endregion

        #region Методы
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $" {nameTournament} {dataMatch.ToShortDateString()} {Round} Тур {HomeTeam.Name} {HomeGoal} : {GuestGoal} {GuestTeam.Name} Played = {isPlayed} ";
        }

        #endregion

    }
}