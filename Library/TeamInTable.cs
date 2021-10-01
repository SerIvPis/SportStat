using System;
using System.Collections.Generic;

namespace Library
{
    /// <summary>
    /// Команда в таблице
    /// </summary>
    
    public class TeamInTable : IComparable<TeamInTable>, IEquatable<TeamInTable>
    {
        #region Свойства и поля
        public Team TeamName { get; set; }
        public byte Win { get; private set; }
        public byte Draw { get; private set; }
        public byte Lost { get; private set; }
        public byte Points { get; private set; }
        public byte GoalFor { get; private set; }
        public byte GoalAgainst { get; private set; }
        public int GoalDifference { get; private set; }
        public HashSet<Match> Matches {get; } 
        public int TotalGames { get; private set; }
             
        #endregion
       
        #region Конструкторы
        public TeamInTable(Team _team)
        {
            TeamName = _team;  
            Matches = new HashSet<Match>();    
            Win = 0;
            Draw = 0;
            Lost = 0;
            Points = 0;
            GoalFor = 0;
            GoalAgainst = 0;
            GoalDifference = 0;
        }

        #endregion

        #region Методы

        //Добавляем в коллекцию матч расчитывая показатели таблицы
        public bool Add (Match _match)
        {   
            if (Matches.Add(_match))
            {
               Calculation(_match);
               TotalGames = Matches.Count;
               GoalDifference = GoalFor - GoalAgainst;
               return true;
            }
            return false;            
        }
        /// <summary>
        /// Вычисления результата матча и заполнение полей
        /// Win Draw Lost...etc
        /// </summary>
        /// <param name="_match"></param>
        private void Calculation(Match _match)
        {
            //Если команда хозяин
             if (TeamName.Equals(_match.HomeTeam))
                {
                    GoalFor +=_match.HomeGoal;
                    GoalAgainst += _match.GuestGoal;
                    if (_match.HomeGoal > _match.GuestGoal)
                    {
                        Win++;
                        Points +=3;
                    }
                    else if (_match.HomeGoal == _match.GuestGoal)
                        {
                            Draw++;
                            Points++;
                        }
                        else
                        {
                            Lost++;
                        }
                }
            //Если команда гость
            else
            {
                GoalFor +=_match.GuestGoal;
                GoalAgainst += _match.HomeGoal;
                if (_match.HomeGoal < _match.GuestGoal)
                    {
                        Win++;
                        Points +=3;
                    }
                    else if (_match.HomeGoal == _match.GuestGoal)
                        {
                            Draw++;
                            Points++;
                        }
                        else
                        {
                            Lost++;
                        }
            }

            
        }
   
        public override bool Equals(object obj)
        {
           if (obj == null)
                return false;

            if (object.ReferenceEquals(this, obj))
                return true;

            TeamInTable teamInTable = obj as TeamInTable;
            if (teamInTable == null)
                return false;
            else
                return this.Equals(teamInTable);
        }

        public override int GetHashCode()
        {
            return this.TeamName.Name.GetHashCode();
        }

        public override string ToString()
        {
            return $"{TeamName,-23}{Win,3}{Draw,3}{Lost,3}{GoalFor,3}{GoalAgainst,3}{GoalDifference,3}{Points,3}{TotalGames,4}";
        }

        public bool Equals(TeamInTable other)
        {
            if (other == null)
                return false;
            if (this.TeamName.Equals(other.TeamName))
            {
                return true;
            }
            else
                return false;
            
        }

        public int CompareTo(TeamInTable other)
        {
            if (other != null)
            {
                if (this.Points > other.Points)
                {
                    return -1;
                }
                if (this.Points < other.Points)
                {
                    return 1;
                }
                else 
                    if (this.GoalDifference > other.GoalDifference)
                    {
                         return -1;
                    }
                    if (this.GoalDifference < other.GoalDifference)
                     {
                         return 1;
                     }
                     else
                        return  0;
            }
            else throw new ArgumentException(" Объект не является объектом TeamInTable");
        }

        #endregion
    }
}