using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FootballClubsClient
{
    class NetClient
    {
        const int BUFFER_SIZE = 512;

        private string ipAddress;

        private int port;

        private TcpClient tcpC;

        public static bool IsConnected = false;

        public NetClient( string ip, int port )
        {
            this.ipAddress = ip;
            this.port = port;
            this.tcpC = new TcpClient( );
        }

        public bool Connect( )
        {
            try
            {
                tcpC.Connect( ipAddress, port );

                IsConnected = true;

                return true;
            }
            catch( Exception )
            {
                return false;
            }
        }

        public string SendMessage( string message )
        {
            Byte[] buffer = Encoding.Unicode.GetBytes( message );
            string Request = "";
            byte[ ] Buffer = new byte[ BUFFER_SIZE ];
            // Переменная для хранения количества байт, принятых от клиента
            int Count;

            try
            {
                NetworkStream stream = tcpC.GetStream( );
                stream.Write( buffer, 0, buffer.Length );

                do
                {
                    Count = stream.Read( Buffer, 0, Buffer.Length );
                    // Преобразуем эти данные в строку и добавим ее к переменной Request
                    Request += Encoding.Unicode.GetString( Buffer, 0, Count );
                    Thread.Sleep( 1 );
                }
                while( stream.DataAvailable );
            }
            catch( IOException )
            {
                MessageBox.Show( "Потеряна связь с сервером" );
                IsConnected = false;
                
            }
            catch( InvalidOperationException)
            {
                MessageBox.Show( "Нет соединения" );
            }
            return Request;
        }
    }
}
