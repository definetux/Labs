using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubsServer
{
    /// <summary>
    /// Структура запроса Лучшие игроки
    /// </summary>
    class BestPlayers
    {
        public String Фамилия
        {
            get;
            set;
        }

        public String Имя
        {
            get;
            set;
        }

        public Int32? Номер
        {
            get;
            set;
        }

        public Int32? ЗабитыеМячи
        {
            get;
            set;
        }

        public String Клуб
        {
            get;
            set;
        }
    }
}
