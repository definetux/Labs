using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubsServer
{
    class NewClub
    {
        public Int32 ClubID
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public String City
        {
            get;
            set;
        }

        public Nullable<Int32> NumberOfMatches
        {
            get;
            set;
        }

        public Nullable<Int32> WinningMatches
        {
            get;
            set;
        }
    }
}
