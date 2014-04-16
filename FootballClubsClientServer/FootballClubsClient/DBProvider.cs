using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FootballClubsClient
{
    class DBProvider
    {
        private static NetClient client;

        public static NetClient Client
        {
            get
            {
                return client;
            }
        }

        public static void CreateClient( int port, String ipAddress )
        {
            client = new NetClient( ipAddress, port );
        }

        public static int Entities
        {
            get;
            set;
        }

        public static IEnumerable<NewClub> GetClubs( )
        {
            string result = client.SendMessage( "GetClubs|" );
            var query = JsonConvert.DeserializeObject<IEnumerable<NewClub>>( result );
            return query;
        }

        public static IEnumerable<PlayerInfo> GetPlayersByClubName( string p )
        {
            string result = client.SendMessage( "GetPlayersByClubName|" + p );
            var query = JsonConvert.DeserializeObject<IEnumerable<PlayerInfo>>( result );
            return query;
        }

        public static IEnumerable<PlayerInfo> GetOldPlayers( string p )
        {
            string result = client.SendMessage( "GetOldPlayers|" + p );
            var query = JsonConvert.DeserializeObject<IEnumerable<PlayerInfo>>( result );
            return query;
        }

        public static IEnumerable<BestPlayers> GetBestPlayers( )
        {
            string result = client.SendMessage( "GetBestPlayers|" );

            var query = JsonConvert.DeserializeObject<IEnumerable<BestPlayers>>( result );

            return query;
        }

        public static IEnumerable<StaffsInfo> GetCountStaffByClub( )
        {
            string result = client.SendMessage( "GetCountStaffByClub" );

            var query = JsonConvert.DeserializeObject<IEnumerable<StaffsInfo>>( result );

            return query;
        }

        public static IEnumerable<PlayerInfo> GetPlayersInfo( int highWin, int lowWin, int highGoals, int lowGoals, int highAge, int lowAge, int rowsCount, string firstChar, bool? sort )
        {
            string result = client.SendMessage( "GetPlayerInfo|"
                                                + highWin.ToString()
                                                + '|'
                                                + lowWin.ToString()
                                                + '|'
                                                + highGoals.ToString()
                                                + '|'
                                                + lowGoals.ToString()
                                                + '|'
                                                + highAge.ToString()
                                                + '|'
                                                + lowAge.ToString()
                                                + '|'
                                                + rowsCount.ToString()
                                                + '|'
                                                + firstChar
                                                + '|'
                                                + sort.ToString()
                                                + '|');

            var query = JsonConvert.DeserializeObject<IEnumerable<PlayerInfo>>( result );

            return query;
        }

        public static IEnumerable<PlayerInfo> GetPlayersByClubWin( int highWin, int lowWin )
        {
            string result = client.SendMessage( "GetPlayersByClubWin|" + highWin.ToString( ) + '|' + lowWin.ToString( ) );

            var query = JsonConvert.DeserializeObject<IEnumerable<PlayerInfo>>( result );

            return query;
        }

        public static IEnumerable<PlayerInfo> GetPlayersByGoals( int highGoals, int lowGoals )
        {
            string result = client.SendMessage( "GetPlayersByGoals|" + highGoals.ToString( ) + '|' + lowGoals.ToString( ) );

            var query = JsonConvert.DeserializeObject<IEnumerable<PlayerInfo>>( result );

            return query;
        }

        public static IEnumerable<PlayerInfo> GetPlayersByAge( int highAge, int lowAge )
        {
            string result = client.SendMessage( "GetPlayersByAge|" + highAge.ToString( ) + '|' + lowAge.ToString( ) );

            var query = JsonConvert.DeserializeObject<IEnumerable<PlayerInfo>>( result );

            return query;
        }

        public static IEnumerable<PlayerInfo> GetQueryOr( IEnumerable<PlayerInfo> currentQuery, IEnumerable<PlayerInfo> secondQuery )
        {
            string queryFirst = JsonConvert.SerializeObject( currentQuery );
            string querySecond = JsonConvert.SerializeObject( secondQuery );

            string result = client.SendMessage( "GetQueryOr|" + queryFirst + '|' + querySecond );

            var query = JsonConvert.DeserializeObject<IEnumerable<PlayerInfo>>( result );

            return query;
        }

        public static IEnumerable<PlayerInfo> GetQueryAnd( IEnumerable<PlayerInfo> currentQuery, IEnumerable<PlayerInfo> secondQuery )
        {
            string queryFirst = JsonConvert.SerializeObject( currentQuery );
            string querySecond = JsonConvert.SerializeObject( secondQuery );

            string result = client.SendMessage( "GetQueryAnd|" + queryFirst + '|' + querySecond );

            var query = JsonConvert.DeserializeObject<IEnumerable<PlayerInfo>>( result );

            return query;
        }

        public static void AddObject<T>( T newEntity )
        {
            string entity = JsonConvert.SerializeObject( newEntity );

            string type = newEntity.GetType( ).ToString( ).Split( '.' )[ 1 ].Replace( "New", "" );

            string result = client.SendMessage( "Add" + type + '|' + entity );
        }

        public static IEnumerable<NewClub> GetClubsById( int SelectedId )
        {
            string result = client.SendMessage( "GetClubsById|" + SelectedId.ToString( ) );
            var query = JsonConvert.DeserializeObject<IEnumerable<NewClub>>( result );
            return query;
        }

        public static void DeleteObject<T>( IEnumerable<T> items )
        {
            string entities = JsonConvert.SerializeObject( items );

            string type = items.First( ).GetType( ).ToString( ).Split( '.' )[ 1 ].Replace( "New", "" );

            string result = client.SendMessage( "Delete" + type + '|' + entities );
        }

        public static void Save( )
        {
            throw new NotImplementedException( );
        }

        public static IEnumerable<NewPlayer> GetPlayersByClubId( int clubId )
        {
            string result = client.SendMessage( "GetPlayersByClubId|" + clubId.ToString( ) );
            var query = JsonConvert.DeserializeObject<IEnumerable<NewPlayer>>( result );
            return query;
        }

        public static IEnumerable<NewPlayer> GetPlayersById( int SelectedId )
        {
            string result = client.SendMessage( "GetPlayersById|" + SelectedId.ToString( ) );
            var query = JsonConvert.DeserializeObject<IEnumerable<NewPlayer>>( result );
            return query;
        }

        public static IEnumerable<NewStaff> GetStaffsByClubId( int clubId )
        {
            string result = client.SendMessage( "GetStaffsByClubId|" + clubId.ToString( ) );
            var query = JsonConvert.DeserializeObject<IEnumerable<NewStaff>>( result );
            return query;
        }

        public static IEnumerable<NewStaff> GetStaffsById( int SelectedId )
        {
            string result = client.SendMessage( "GetStaffsById|" + SelectedId.ToString( ) );
            var query = JsonConvert.DeserializeObject<IEnumerable<NewStaff>>( result );
            return query;
        }
    }
}
