using System;
using Xunit;
using Library;

namespace LibraryTests
{
    public class MatchTests
    {
        [Theory]
        [InlineData("14.08.2021 Уотфорд  -  Астон Вилла 3 : 2", true ,"14.08.2021","Уотфорд","Астон Вилла", 3, 2 )]
        [InlineData("30.10.2021 Лестер  -  Арсенал 14:30", false,"30.10.2021", "Лестер", "Арсенал", 255, 255 )]
        [InlineData("27.11.2021 Вулверхэмптон Уондерерс  -  Вест Хэм Юнайтед _ : _", false,"27.11.2021", "Вулверхэмптон Уондерерс","Вест Хэм Юнайтед", 255, 255 )]
        public void ConstructorTest(string strmatch, bool isplay, string dataStr, string home, string guest, int hgoal, int ggoal)
        {
           //  Arrange
          

            //  Act          
                var match = new Match( strmatch, 1, "testTournir");

            //  Assert
            Assert.Equal(home, match.HomeTeam.Name);
            Assert.Equal(guest, match.GuestTeam.Name);
            Assert.Equal(hgoal, match.HomeGoal);
            Assert.Equal(ggoal, match.GuestGoal);
            Assert.Equal(isplay, match.isPlayed);
            Assert.Equal(dataStr, match.dataMatch.ToShortDateString());
        }
    }
}
