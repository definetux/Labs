using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubsServer
{
    /// <summary>
    /// Структура таблицы Персонал
    /// </summary>
    class NewStaff
    {
        public Int32 StaffID
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

        public String Post
        {
            get;
            set;
        }

        public Nullable<Int32> Experience
        {
            get;
            set;
        }


    }
}
