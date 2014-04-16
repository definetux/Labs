using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubsServer
{
    class Program
    {
        static void Main( string[ ] args )
        {
            Server server = new Server( 1532 );
            server.StartServer( );
            Console.ReadKey( );
        }
    }
}
