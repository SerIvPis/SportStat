using System;
using System.Collections.Generic;
namespace Eleron.FootballStatistic.TournamentData
{
    /// <summary>
    /// Команда
    /// </summary>
    public class Team: IComparable<Team>, IEquatable<Team>
    {
        /// Имя команды
        public string Name { get; private set; }
        public Team() { }
        public Team(string _name) => Name = _name;

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
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        public int CompareTo(Team other)
        {
           return this.Name.CompareTo(other.Name);
        }

        public bool Equals(Team other)
        {
            if (other == null)
                return false;

            if (this.Name.Equals(other.Name))
            {
                return true;
            }
            else
                return false;
           
        }
    }
}