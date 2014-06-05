using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubsClient
{
    /// <summary>
    /// Структура запроса Количество персонала
    /// </summary>
    class StaffsInfo
    {
        public String Клуб
        {
            get;
            set;
        }

        public Int32? КоличествоПерсонала
        {
            get;
            set;
        }
    }
}
