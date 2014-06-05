using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballClubsClient
{
    /// <summary>
    /// Структура запроса Информация об игроках
    /// </summary>
    public class PlayerInfo
    {
        public int Id
        {
            get;
            set;
        }

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

        public String Позиция
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

        public Int32? ПобедыВСезоне
        {
            get;
            set;
        }
    }
}
