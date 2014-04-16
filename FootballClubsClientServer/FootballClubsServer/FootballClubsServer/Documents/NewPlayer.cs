using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubsServer
{
    class NewPlayer
    {
        public Int32 PlayerID
        {
            get;
            set;
        }

        public Nullable<global::System.Int32> ClubID
        {
            get;
            set;
        }

        public String LastName
        {
            get;
            set;
        }

        public String FirstName
        {
            get;
            set;
        }

        public String Patronymic
        {
            get;
            set;
        }

        public Nullable<Int32> Number
        {
            get;
            set;
        }

        public String Position
        {
            get;
            set;
        }

        public Nullable<Int32> Goals
        {
            get;
            set;
        }

        public Nullable<global::System.DateTime> Birthdate
        {
            get;
            set;
        }
    }
}
