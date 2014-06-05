using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubsServer
{
    /// <summary>
    /// Класс для работы с журналированием
    /// </summary>
    class Logger
    {
        /// <summary>
        /// Вывести лог в консоль
        /// </summary>
        /// <param name="log"> Текст лога </param>
        public static void PringLog( string log )
        {
            StringBuilder sb = new StringBuilder( );
            sb.AppendFormat( DateTime.Now.Hour.ToString( ) + ':' + DateTime.Now.Minute.ToString( ) + ':' + DateTime.Now.Second.ToString( ) );
            Console.WriteLine(sb.ToString() + ' ' + log );
        }

        /// <summary>
        /// Сохранить лог в файл
        /// </summary>
        /// <param name="log"> Текст лога </param>
        public static void SaveLog( string log )
        {
            StringBuilder sb = new StringBuilder( );
            sb.AppendFormat( DateTime.Now.Hour.ToString( ) + ':' + DateTime.Now.Minute.ToString( ) + ':' + DateTime.Now.Second.ToString( ) );
            
            StreamWriter sw = File.AppendText( "log.txt" );
            sw.WriteLine( sb.ToString() + ' ' + log );
            sw.Close( );
        }
    }
}
