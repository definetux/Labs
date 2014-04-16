using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections;

namespace FootballClubsServer
{
    class Server
    {
        const int BUFFER_SIZE = 512;

        private int port;

        private TcpListener tcpL;

        private NetworkStream stream;

        public Server( int port )
        {
            this.port = port;
            tcpL = new TcpListener( IPAddress.Any, port );
        }

        public bool StartServer( )
        {
            try
            {
                tcpL.Start( );
                WaitSocket( );
                return true;
            }
            catch( Exception ex )
            {
                Console.WriteLine( ex.Message );
                return false;
            }
        }

        public void WaitSocket( )
        {
            while( true )
            {
                TcpClient socket = tcpL.AcceptTcpClient( );

                Console.WriteLine( "Клиент подключился" );

                new Thread( new ParameterizedThreadStart( HandlingSocket ) ).Start( socket );
            }
        }

        public void HandlingSocket( object client )
        {
            TcpClient socket = client as TcpClient;

            if( socket != null && socket.Connected )
            {
                while( socket.Connected )
                {
                    byte[ ] buffer = new byte[ BUFFER_SIZE ];
                    int bytesRec = 0;
                    string message = "";

                    stream = socket.GetStream( );

                    try
                    {
                        do
                        {
                            bytesRec = stream.Read( buffer, 0, buffer.Length );
                            message += Encoding.Unicode.GetString( buffer, 0, bytesRec );
                        } while( stream.DataAvailable );

                        if( message != String.Empty )
                            HandlignMessage( message );
                    }
                    catch( IOException )
                    {
                        Console.WriteLine( "Клиент отключился" );
                        return;
                    }
                }
            }
        }

        private void HandlignMessage( string message )
        {
            string[ ] args = message.Split( '|' );
            string result = "";

            switch( args[0] )
            {
                case "AddClub":
                    {
                        var newClub = JsonConvert.DeserializeObject<NewClub>( args[ 1 ] );
                        Data.Club club = new Data.Club( );
                        club.Name = newClub.Name;
                        club.City = newClub.City;
                        club.NumberOfMatches = newClub.NumberOfMatches;
                        club.WinningMatches = newClub.WinningMatches;

                        EntitiesController.AddObject( club );

                        result = "Добавление успешно";
                    }
                    break;
                case "AddPlayer":
                    {
                        var newPlayer = JsonConvert.DeserializeObject<NewPlayer>( args[ 1 ] );
                        Data.Player player = new Data.Player( );
                        player.FirstName = newPlayer.FirstName;
                        player.LastName = newPlayer.LastName;
                        player.Patronymic = newPlayer.Patronymic;
                        player.Number = newPlayer.Number;
                        player.Position = newPlayer.Position;
                        player.ClubID = newPlayer.ClubID;
                        player.Birthdate = newPlayer.Birthdate;
                        player.Goals = newPlayer.Goals;
                        
                        EntitiesController.AddObject( player );

                        result = "Добавление успешно";
                    }
                    break;
                case "AddStaff":
                    {
                        var newStaff = JsonConvert.DeserializeObject<NewStaff>( args[ 1 ] );
                        Data.Staff staff = new Data.Staff( );
                        staff.FirstName = newStaff.FirstName;
                        staff.LastName = newStaff.LastName;
                        staff.Patronymic = newStaff.Patronymic;
                        staff.Post = newStaff.Post;
                        staff.Experience = newStaff.Experience;
                        staff.ClubID = newStaff.ClubID;
                        
                        EntitiesController.AddObject( staff );

                        result = "Добавление успешно";
                    }
                    break;
                case "DeleteClub":
                    {
                        var clubId = JsonConvert.DeserializeObject<IEnumerable<NewClub>>( args[ 1 ] ).ToList()[0].ClubID;
                        var oldClubs = EntitiesController.GetClubsById( clubId );
                             
                        if( oldClubs != null )
                        {
                            foreach( var item in oldClubs )
                            {
                                EntitiesController.DeleteObject( item );
                            }

                            result = "Удаление успешно";
                        }
                        else
                            result = "Не удалось удалисть запись";
                    }
                    break;
                case "DeletePlayer":
                    {
                        var playerId = JsonConvert.DeserializeObject<IEnumerable<NewPlayer>>( args[ 1 ] ).ToList( )[ 0 ].PlayerID;
                        var oldPlayers = EntitiesController.GetPlayersById( playerId );

                        if( oldPlayers != null )
                        {
                            foreach( var item in oldPlayers )
                            {
                                EntitiesController.DeleteObject( item );
                            }

                            result = "Удаление успешно";
                        }
                        else
                            result = "Не удалось удалисть запись";
                    }
                    break;
                case "DeleteStaff":
                    {
                        var staffId = JsonConvert.DeserializeObject<IEnumerable<NewStaff>>( args[ 1 ] ).ToList( )[ 0 ].StaffID;
                        var oldStaffs = EntitiesController.GetStaffsById( staffId );

                        if( oldStaffs != null )
                        {
                            foreach( var item in oldStaffs )
                            {
                                EntitiesController.DeleteObject( item );
                            }

                            result = "Удаление успешно";
                        }
                        else
                            result = "Не удалось удалисть запись";
                    }
                    break;
                case "GetClubs":
                    {
                        IEnumerable<NewClub> query = (from table in EntitiesController.Entities.Clubs
                                    select new NewClub
                                    {
                                        ClubID = table.ClubID,
                                        Name = table.Name,
                                        City = table.City,
                                        NumberOfMatches = (int)table.NumberOfMatches,
                                        WinningMatches = (int)table.WinningMatches
                                    });

                        result = JsonConvert.SerializeObject( query );

                    }
                    break;
                case "GetPlayersByClubId":
                    {
                        IEnumerable<NewPlayer> query = ( from table in EntitiesController.GetPlayersByClubId( int.Parse(args[1]) )
                                                       select new NewPlayer
                                                       {
                                                           PlayerID = table.PlayerID,
                                                           LastName = table.LastName,
                                                           FirstName = table.FirstName,
                                                           Patronymic = table.Patronymic,
                                                           Number = (int)table.Number,
                                                           Position = table.Position,
                                                           Goals = (int)table.Goals,
                                                           Birthdate = table.Birthdate,
                                                           ClubID = table.ClubID
                                                       } );

                        result = JsonConvert.SerializeObject( query );
                    }
                    break;
                case "GetClubsById":
                    {
                        IEnumerable<NewClub> query = ( from table in EntitiesController.GetClubsById( int.Parse( args[1] ) )
                                                       select new NewClub
                                                       {
                                                           ClubID = table.ClubID,
                                                           Name = table.Name,
                                                           City = table.City,
                                                           NumberOfMatches = ( int )table.NumberOfMatches,
                                                           WinningMatches = ( int )table.WinningMatches
                                                       } );

                        result = JsonConvert.SerializeObject( query );
                    }
                    break;
                case "GetPlayersById":
                    {
                        IEnumerable<NewPlayer> query = ( from table in EntitiesController.GetPlayersById( int.Parse( args[ 1 ] ) )
                                                       select new NewPlayer
                                                       {
                                                           PlayerID = table.PlayerID,
                                                           FirstName = table.FirstName,
                                                           LastName = table.LastName,
                                                           Patronymic = table.Patronymic,
                                                           Birthdate = table.Birthdate,
                                                           Position = table.Position,
                                                           Goals = ( int )table.Goals,
                                                           Number = ( int )table.Number,
                                                           ClubID = table.ClubID
                                                       } );

                        result = JsonConvert.SerializeObject( query );
                    }
                    break;
                case "GetStaffsById":
                    {
                        IEnumerable<NewStaff> query = ( from table in EntitiesController.GetStaffsById( int.Parse( args[ 1 ] ) )
                                                        select new NewStaff
                                                         {
                                                             StaffID = table.StaffID,
                                                             FirstName = table.FirstName,
                                                             LastName = table.LastName,
                                                             Patronymic = table.Patronymic,
                                                             Post = table.Post,
                                                             Experience = ( int )table.Experience,
                                                             ClubID = table.ClubID
                                                         } );

                        result = JsonConvert.SerializeObject( query );
                    }
                    break;
                case "GetStaffsByClubId":
                    {
                        IEnumerable<NewStaff> query = from table in EntitiesController.GetStaffsByClubId( int.Parse( args[ 1 ] ) )
                                                       select new NewStaff
                                                       {
                                                           StaffID = table.StaffID,
                                                           FirstName = table.FirstName,
                                                           LastName = table.LastName,
                                                           Patronymic = table.Patronymic,
                                                           Post = table.Post,
                                                           Experience = (int )table.Experience,
                                                           ClubID = table.ClubID
                                                       };

                        result = JsonConvert.SerializeObject( query );
                    }
                    break;
                case "GetBestPlayers":
                    {
                        IEnumerable<BestPlayers> query = EntitiesController.GetBestPlayers( );

                        result = JsonConvert.SerializeObject( query );
                    }
                    break;

                case "GetCountStaffByClub":
                    {
                        IEnumerable<StaffsInfo> query = EntitiesController.GetCountStaffByClub( );

                        result = JsonConvert.SerializeObject( query );
                    }
                    break;
                case "GetPlayersByClubName":
                    {
                        IEnumerable<PlayerInfo> query = EntitiesController.GetPlayersByClubName( args[1] );

                        result = JsonConvert.SerializeObject( query );
                    }
                    break;
                case "GetOldPlayers":
                    {
                        IEnumerable<PlayerInfo> query = EntitiesController.GetOldPlayers( args[ 1 ] );

                        result = JsonConvert.SerializeObject( query );
                    }
                    break;
                case "GetPlayersByClubWin":
                    {
                        IEnumerable<PlayerInfo> query = EntitiesController.GetPlayersByClubWin( int.Parse( args[ 1 ] ),
                                                                                                int.Parse( args[2] ) );

                        result = JsonConvert.SerializeObject( query );
                    }
                    break;
                case "GetPlayersByGoals":
                    {
                        IEnumerable<PlayerInfo> query = EntitiesController.GetPlayersByGoals( int.Parse( args[ 1 ] ),
                                                                                              int.Parse( args[ 2 ] ) );

                        result = JsonConvert.SerializeObject( query );
                    }
                    break;
                case "GetPlayersByAge":
                    {
                        IEnumerable<PlayerInfo> query = EntitiesController.GetPlayersByAge( int.Parse( args[ 1 ] ),
                                                                                            int.Parse( args[ 2 ] ) );

                        result = JsonConvert.SerializeObject( query );
                    }
                    break;
                case "GetPlayerInfo":
                    {
                        IEnumerable<PlayerInfo> query = EntitiesController.GetPlayersInfo( int.Parse( args[ 1 ] ),
                                                                                           int.Parse( args[ 2 ] ),
                                                                                           int.Parse( args[ 3 ] ),
                                                                                           int.Parse( args[ 4 ] ),
                                                                                           int.Parse( args[ 5 ] ),
                                                                                           int.Parse( args[ 6 ] ),
                                                                                           int.Parse( args[ 7 ] ),
                                                                                           args[ 8 ],
                                                                                           bool.Parse( args[ 9 ] ) );

                        result = JsonConvert.SerializeObject( query );
                    }
                    break;
                case "GetQueryOr":
                    {
                        var queryFirst = JsonConvert.DeserializeObject<IEnumerable<PlayerInfo>>( args[ 1 ] );
                        var querySecond = JsonConvert.DeserializeObject<IEnumerable<PlayerInfo>>( args[ 2 ] );

                        IEnumerable<PlayerInfo> queryResult = EntitiesController.GetQueryOr( queryFirst, querySecond );

                        result = JsonConvert.SerializeObject( queryResult );
                    }
                    break;
                case "GetQueryAnd":
                    {
                        var queryFirst = JsonConvert.DeserializeObject<IEnumerable<PlayerInfo>>( args[ 1 ] );
                        var querySecond = JsonConvert.DeserializeObject<IEnumerable<PlayerInfo>>( args[ 2 ] );

                        IEnumerable<PlayerInfo> queryResult = EntitiesController.GetQueryAnd( queryFirst, querySecond );

                        result = JsonConvert.SerializeObject( queryResult );
                    }
                    break;
            }
            SendMessage( result );
        }

        public void SendMessage( string message )
        {
            byte[ ] data = Encoding.Unicode.GetBytes( message );

            stream.Write( data, 0, data.Length );
        }
    }
}
