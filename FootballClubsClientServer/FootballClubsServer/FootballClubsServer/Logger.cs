using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubsServer
{
    class Logger
    {
        public static void PringLog( string log )
        {
            StringBuilder sb = new StringBuilder( );
            sb.AppendFormat( DateTime.Now.Hour.ToString( ) + ':' + DateTime.Now.Minute.ToString( ) + ':' + DateTime.Now.Second.ToString( ) );
            Console.WriteLine(sb.ToString() + ' ' + log );
        }

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
