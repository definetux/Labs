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
    /// <summary>
    /// Модуль клиента для работы с сервером
    /// </summary>
    class NetClient
    {
        /// <summary>
        /// Размер буфера
        /// </summary>
        const int BUFFER_SIZE = 512;

        /// <summary>
        /// IP адрес сервера
        /// </summary>
        private string ipAddress;

        /// <summary>
        /// Номер порта сервера
        /// </summary>
        private int port;

        private TcpClient tcpC;

        /// <summary>
        /// Проверка соединения
        /// </summary>
        public static bool IsConnected = false;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="ip"> IP адрес </param>
        /// <param name="port"> Номер порта </param>
        public NetClient( string ip, int port )
        {
            this.ipAddress = ip;
            this.port = port;
            this.tcpC = new TcpClient( );
        }

        /// <summary>
        /// Выполнить соединение
        /// </summary>
        /// <returns> Результат соединения </returns>
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

        /// <summary>
        /// Отправить сообщение серверу
        /// </summary>
        /// <param name="message"> Текст сообщения </param>
        /// <returns> Ответ от сервера </returns>
        public string SendMessage( string message )
        {
            Byte[] buffer = Encoding.Unicode.GetBytes( message );
            string Request = "";
            byte[ ] Buffer = new byte[ BUFFER_SIZE ];
            // Переменная для хранения количества байт, принятых от клиента
            int Count;

            try
            {
                // Записать данные в поток
                NetworkStream stream = tcpC.GetStream( );
                stream.Write( buffer, 0, buffer.Length );

                // Читать данные из потока, по не пуст
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
