using System;
using Xunit;
using Eleron.FootballStatistic.TournamentData;

namespace LibraryTests
{
    public class TeamInTables
    {
        [Fact]
        public void Add_Test()
        {
           //  Arrange
           byte round = 1;
           string trn = "turnir";
           Match firstMatch = new Match("14.08.2021 Челси  -  Кристал Пэлас 3 : 0",
                                        round++,
                                        trn);          
           Match secondMatch = new Match("22.08.2021 Арсенал  -  Челси 2 : 2",
                                         round++,
                                         trn);          
           Match thirdMatch = new Match("11.09.2021 Челси  -  Астон Вилла 1 : 2",
                                        round++,
                                        trn);    
           byte points = 4;
           byte win = 1;
           byte lose = 1;
           byte draw = 1;
           byte ga = 6;
           byte gl = 4;
           int gg = 2;

           TeamInTable team = new TeamInTable(new Team("Челси"));     

            //  Act          
               team.Add(firstMatch);
               team.Add(secondMatch);
               team.Add(thirdMatch);

            //  Assert
            Assert.Equal(team.Points, points );
            Assert.Equal(team.Win, win );
            Assert.Equal(team.Lost, lose );
            Assert.Equal(team.Draw, draw );
            Assert.Equal(team.GoalAgainst, gl );
            Assert.Equal(team.GoalFor, ga );
            Assert.Equal(team.GoalDifference, gg );
       
        }
    }
}