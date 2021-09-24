namespace Library
{
    /// <summary>
    /// Команда
    /// </summary>
    public class Team
    {
        /// Название команды
        public string Name { get; private set; }
        public Team() { }
        public Team(string _name) => Name = _name;
    }

    /// <summary>
    /// Команда в таблице
    /// </summary>
    public class TeamTable : Team
    {
        public byte Win { get; private set; }
        public byte Draw { get; private set; }
        public byte Lost { get; private set; }
        public byte Points { get; private set; }
        public byte GoalFor { get; private set; }
        public byte GoalAgainst { get; private set; }
        public byte GoalDifference { get; private set; }
        public byte PlayedMatch { get; private set; }
       
         public TeamTable(string _name):base()
        {
            
        }

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
            return $"{Name} W = {Win}\tD = {Draw}\tL = {Lost}\tGF = {GoalFor}\tGA = {GoalAgainst}\tGD= {GoalDifference}\tPoints = {Points}";
        }
    }
}