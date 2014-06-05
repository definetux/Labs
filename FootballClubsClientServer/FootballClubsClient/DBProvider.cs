using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FootballClubsClient
{
    /// <summary>
    /// Поставщик данных от сервера
    /// </summary>
    class DBProvider
    {
        private static NetClient client;

        /// <summary>
        /// Объект клиентского модуля
        /// </summary>
        public static NetClient Client
        {
            get
            {
                return client;
            }
        }

        /// <summary>
        /// Создание клиента
        /// </summary>
        /// <param name="port"> Номер порта </param>
        /// <param name="ipAddress"> IP адрес сервера </param>
        public static void CreateClient( int port, String ipAddress )
        {
            client = new NetClient( ipAddress, port );
        }

        /// <summary>
        /// Получить список клубов
        /// </summary>
        /// <returns> Список клубов </returns>
        public static IEnumerable<NewClub> GetClubs( )
        {
            string result = client.SendMessage( "GetClubs|" );
            var query = JsonConvert.DeserializeObject<IEnumerable<NewClub>>( result );
            return query;
        }

        /// <summary>
        /// Получить игроков по названию клуба
        /// </summary>
        /// <param name="p"> Название клуба </param>
        /// <returns> Список игроков </returns>
        public static IEnumerable<PlayerInfo> GetPlayersByClubName( string p )
        {
            string result = client.SendMessage( "GetPlayersByClubName|" + p );
            var query = JsonConvert.DeserializeObject<IEnumerable<PlayerInfo>>( result );
            return query;
        }

        /// <summary>
        /// Получить игроков по возрасту
        /// </summary>
        /// <param name="p"> Возраст игрока </param>
        /// <returns> Список игроков </returns>
        public static IEnumerable<PlayerInfo> GetOldPlayers( string p )
        {
            string result = client.SendMessage( "GetOldPlayers|" + p );
            var query = JsonConvert.DeserializeObject<IEnumerable<PlayerInfo>>( result );
            return query;
        }

        /// <summary>
        /// Получить лучших игроков
        /// </summary>
        /// <returns> Список игроков </returns>
        public static IEnumerable<BestPlayers> GetBestPlayers( )
        {
            string result = client.SendMessage( "GetBestPlayers|" );

            var query = JsonConvert.DeserializeObject<IEnumerable<BestPlayers>>( result );

            return query;
        }

        /// <summary>
        /// Получить количество персонала в клубах
        /// </summary>
        /// <returns> Перечень клубов и количество персонала в них </returns>
        public static IEnumerable<StaffsInfo> GetCountStaffByClub( )
        {
            string result = client.SendMessage( "GetCountStaffByClub" );

            var query = JsonConvert.DeserializeObject<IEnumerable<StaffsInfo>>( result );

            return query;
        }

        /// <summary>
        /// Получить информацию об игроках
        /// </summary>
        /// <param name="highWin"> Максимальное число побед клуба </param>
        /// <param name="lowWin"> Минимальное число побед клуба </param>
        /// <param name="highGoals"> Максимальное число забитых голов </param>
        /// <param name="lowGoals"> Минимальное число забитых голов </param>
        /// <param name="highAge"> Максимальный возраст игрока </param>
        /// <param name="lowAge"> Минимальный возраст игрока </param>
        /// <param name="rowsCount"> Количество записей </param>
        /// <param name="firstChar"> Буквы названия клуба </param>
        /// <param name="sort"> Параметр сортировки </param>
        /// <returns> Список игроков </returns>
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

        /// <summary>
        /// Получить список игроков по количеству побед клуба
        /// </summary>
        /// <param name="highWin"> Максимальное число побед клуба </param>
        /// <param name="lowWin"> Минимальное число побед клуба </param>
        /// <returns> Список игроков </returns>
        public static IEnumerable<PlayerInfo> GetPlayersByClubWin( int highWin, int lowWin )
        {
            string result = client.SendMessage( "GetPlayersByClubWin|" + highWin.ToString( ) + '|' + lowWin.ToString( ) );

            var query = JsonConvert.DeserializeObject<IEnumerable<PlayerInfo>>( result );

            return query;
        }

        /// <summary>
        /// Получить список игроков по забитым мячам
        /// </summary>
        /// <param name="highGoals"> Максимальное число забитых мячей </param>
        /// <param name="lowGoals"> Минимальное число забитых мячей </param>
        /// <returns> Список игроков </returns>
        public static IEnumerable<PlayerInfo> GetPlayersByGoals( int highGoals, int lowGoals )
        {
            string result = client.SendMessage( "GetPlayersByGoals|" + highGoals.ToString( ) + '|' + lowGoals.ToString( ) );

            var query = JsonConvert.DeserializeObject<IEnumerable<PlayerInfo>>( result );

            return query;
        }

        /// <summary>
        /// Получить список игроков по возрасту
        /// </summary>
        /// <param name="highAge"> Максимальный возраст игрока </param>
        /// <param name="lowAge"> Минимальный возраст игрока </param>
        /// <returns> Список игроков </returns>
        public static IEnumerable<PlayerInfo> GetPlayersByAge( int highAge, int lowAge )
        {
            string result = client.SendMessage( "GetPlayersByAge|" + highAge.ToString( ) + '|' + lowAge.ToString( ) );

            var query = JsonConvert.DeserializeObject<IEnumerable<PlayerInfo>>( result );

            return query;
        }

        /// <summary>
        /// Объединить два запроса
        /// </summary>
        /// <param name="currentQuery"> Результат первого запроса </param>
        /// <param name="secondQuery"> Результат второго запроса </param>
        /// <returns> Объединение двух запросов </returns>
        public static IEnumerable<PlayerInfo> GetQueryOr( IEnumerable<PlayerInfo> currentQuery, IEnumerable<PlayerInfo> secondQuery )
        {
            string queryFirst = JsonConvert.SerializeObject( currentQuery );
            string querySecond = JsonConvert.SerializeObject( secondQuery );

            string result = client.SendMessage( "GetQueryOr|" + queryFirst + '|' + querySecond );

            var query = JsonConvert.DeserializeObject<IEnumerable<PlayerInfo>>( result );

            return query;
        }

        /// <summary>
        /// Пересечение двух запросов
        /// </summary>
        /// <param name="currentQuery"> Результат первого запроса </param>
        /// <param name="secondQuery"> Результат второго запроса </param>
        /// <returns> Пересечение двух запросов </returns>
        public static IEnumerable<PlayerInfo> GetQueryAnd( IEnumerable<PlayerInfo> currentQuery, IEnumerable<PlayerInfo> secondQuery )
        {
            string queryFirst = JsonConvert.SerializeObject( currentQuery );
            string querySecond = JsonConvert.SerializeObject( secondQuery );

            string result = client.SendMessage( "GetQueryAnd|" + queryFirst + '|' + querySecond );

            var query = JsonConvert.DeserializeObject<IEnumerable<PlayerInfo>>( result );

            return query;
        }

        /// <summary>
        /// Добавить запись в таблицу
        /// </summary>
        /// <typeparam name="T"> Тип таблицы </typeparam>
        /// <param name="newEntity"> Запись </param>
        public static void AddObject<T>( T newEntity )
        {
            string entity = JsonConvert.SerializeObject( newEntity );

            string type = newEntity.GetType( ).ToString( ).Split( '.' )[ 1 ].Replace( "New", "" );

            string result = client.SendMessage( "Add" + type + '|' + entity );
        }

        /// <summary>
        /// Получить клуб по ID
        /// </summary>
        /// <param name="SelectedId"> ID клуба </param>
        /// <returns> Запись клуба </returns>
        public static IEnumerable<NewClub> GetClubsById( int SelectedId )
        {
            string result = client.SendMessage( "GetClubsById|" + SelectedId.ToString( ) );
            var query = JsonConvert.DeserializeObject<IEnumerable<NewClub>>( result );
            return query;
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <typeparam name="T"> Тип таблицы </typeparam>
        /// <param name="items"> Запись </param>
        public static void DeleteObject<T>( IEnumerable<T> items )
        {
            string entities = JsonConvert.SerializeObject( items );

            string type = items.First( ).GetType( ).ToString( ).Split( '.' )[ 1 ].Replace( "New", "" );

            string result = client.SendMessage( "Delete" + type + '|' + entities );
        }

        /// <summary>
        /// Сохранить запись
        /// </summary>
        /// <typeparam name="T"> Тип таблицы </typeparam>
        /// <param name="entity"> Запись </param>
        public static void Save<T>( T entity )
        {
            string entityString = JsonConvert.SerializeObject( entity );

            string type = entity.GetType( ).ToString( ).Split( '.' )[ 1 ].Replace( "New", "" );

            string result = client.SendMessage( "Save" + type + '|' + entityString );
        }

        /// <summary>
        /// Получить список игроков по ID клуба
        /// </summary>
        /// <param name="clubId"> ID клуба</param>
        /// <returns> Список игроков </returns>
        public static IEnumerable<NewPlayer> GetPlayersByClubId( int clubId )
        {
            string result = client.SendMessage( "GetPlayersByClubId|" + clubId.ToString( ) );
            var query = JsonConvert.DeserializeObject<IEnumerable<NewPlayer>>( result );
            return query;
        }

        /// <summary>
        /// Получить игрока по ID
        /// </summary>
        /// <param name="SelectedId"> ID игрока </param>
        /// <returns> Запись игрока </returns>
        public static IEnumerable<NewPlayer> GetPlayersById( int SelectedId )
        {
            string result = client.SendMessage( "GetPlayersById|" + SelectedId.ToString( ) );
            var query = JsonConvert.DeserializeObject<IEnumerable<NewPlayer>>( result );
            return query;
        }

        /// <summary>
        /// Получить список сотрудников по ID клуба
        /// </summary>
        /// <param name="clubId"> ID клуба </param>
        /// <returns> Список сотрудников </returns>
        public static IEnumerable<NewStaff> GetStaffsByClubId( int clubId )
        {
            string result = client.SendMessage( "GetStaffsByClubId|" + clubId.ToString( ) );
            var query = JsonConvert.DeserializeObject<IEnumerable<NewStaff>>( result );
            return query;
        }

        /// <summary>
        /// Получить сотрудника по ID
        /// </summary>
        /// <param name="SelectedId"> ID сотрудника </param>
        /// <returns> Запись сотрудника </returns>
        public static IEnumerable<NewStaff> GetStaffsById( int SelectedId )
        {
            string result = client.SendMessage( "GetStaffsById|" + SelectedId.ToString( ) );
            var query = JsonConvert.DeserializeObject<IEnumerable<NewStaff>>( result );
            return query;
        }
    }
}
