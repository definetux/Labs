using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Windows;
using FootballClubsServer.Data;

namespace FootballClubsServer
{
    /// <summary>
    /// Класс позволяет работать с EDM посредством запросов LINQ TO Entities
    /// </summary>
    static class EntitiesController
    {
        /// <summary>
        /// Считывание данных
        /// </summary>
        public static FootballClubsEntities Entities 
        { 
            get 
            { 
                return new FootballClubsEntities(); 
            }
        }

        /// <summary>
        /// Сохранение данных в БД
        /// </summary>
        /// <param name="entities"> Модель данных </param>
        public static void Save(FootballClubsEntities entities)
        {
            entities.SaveChanges();
        }

        /// <summary>
        /// Добавить объект в модель
        /// </summary>
        /// <param name="entity"> Объект данных </param>
        public static void AddObject(object entity)
        {
            using (FootballClubsEntities entities = new FootballClubsEntities())
            {
                string name = entity.ToString().Split('.')[2];
                entities.AddObject(name + "s", entity);
                entities.SaveChanges();
            }
        }

        /// <summary>
        /// Удалить объект из модели
        /// </summary>
        /// <param name="entity"> Объект данных </param>
        public static void DeleteObject(object entity)
        {
            using (FootballClubsEntities entities = new FootballClubsEntities())
            {
                entities.Attach(entity as IEntityWithKey);
                entities.DeleteObject(entity);
                entities.SaveChanges();
            }
        }

        #region GetData Methods
        /// <summary>
        /// Возвращает список игроков по ID клуба
        /// </summary>
        /// <param name="clubId"> ID клуба </param>
        /// <returns> Список игроков </returns>
        public static List<Player> GetPlayersByClubId(int clubId)
        {
            List<Player> playersList;
            using (FootballClubsEntities entities = new FootballClubsEntities())
            {
                playersList = entities.Players.Where<Player>(player => player.ClubID == clubId).OrderBy( p => p.LastName ).ToList();
            }
            return playersList;
        }

        /// <summary>
        /// Возвращает список персонала по ID клуба
        /// </summary>
        /// <param name="clubId"> ID клуба </param>
        /// <returns> Список персонала </returns>
        public static List<Staff> GetStaffsByClubId(int clubId)
        {
            List<Staff> staffsList;
            using (FootballClubsEntities entities = new FootballClubsEntities())
            {
                staffsList = entities.Staffs.Where<Staff>(staff => staff.ClubID == clubId).OrderBy( p => p.LastName ).ToList();
            }
            return staffsList;
        }

        /// <summary>
        /// Возвращает клуб по ID 
        /// </summary>
        /// <param name="clubId"> ID клуба </param>
        /// <returns> Клуб </returns>
        public static List<Club> GetClubsById(int clubId)
        {
            List<Club> clubsList;
            using (FootballClubsEntities entities = new FootballClubsEntities())
            {
                clubsList = entities.Clubs.Where<Club>(club => club.ClubID == clubId).OrderBy( p => p.Name ).ToList();
            }
            return clubsList;
        }

        /// <summary>
        /// Возвращает игроков по забитым голам
        /// </summary>
        /// <param name="goals"> Количество забитых голов </param>
        /// <returns> Список игроков </returns>
        public static IEnumerable<PlayerInfo> GetPlayersByGoals( int goals )
        {
            FootballClubsEntities entities = new FootballClubsEntities();

            var query = from Clubs in entities.Clubs
                        join Players in entities.Players
                        on Clubs.ClubID equals Players.ClubID
                        where Players.Goals >= goals
                        select new PlayerInfo
                        {
                            Id = Players.PlayerID,
                            Фамилия = Players.LastName,
                            Имя = Players.FirstName,
                            Номер = (int)Players.Number,
                            Позиция = Players.Position,
                            ЗабитыеМячи = (int)Players.Goals,
                            Клуб = Clubs.Name,
                            ПобедыВСезоне = (int)Clubs.WinningMatches
                        };

            return query;
        }

        /// <summary>
        /// Возвращает список игроков, играющих за определенный клуб
        /// </summary>
        /// <param name="clubName"> Название клуба </param>
        /// <returns> Список игроков </returns>
        public static IEnumerable<PlayerInfo> GetPlayersByClub( string clubName )
        {
            FootballClubsEntities entities = new FootballClubsEntities();

            var query = from Clubs in entities.Clubs
                         join Players in entities.Players
                         on Clubs.ClubID equals Players.ClubID
                         where Clubs.Name == clubName
                         select new PlayerInfo
                         {
                             Id = Players.PlayerID,
                             Фамилия = Players.LastName,
                             Имя = Players.FirstName,
                             Номер = (int)Players.Number,
                             Позиция = Players.Position,
                             ЗабитыеМячи = (int)Players.Goals,
                             Клуб = Clubs.Name,
                             ПобедыВСезоне = (int)Clubs.WinningMatches
                         };
            return query;     
        }

        /// <summary>
        /// Возвращает игрока по ID 
        /// </summary>
        /// <param name="SelectedId"> ID игрока </param>
        /// <returns> Игрок </returns>
        public static List<Player> GetPlayersById(int SelectedId)
        {
            List<Player> playersList;
            using (FootballClubsEntities entities = new FootballClubsEntities())
            {
                playersList = entities.Players.Where<Player>(player => player.PlayerID == SelectedId).ToList();
            }
            return playersList;
        }

        /// <summary>
        /// Возвращает сотрудника по ID
        /// </summary>
        /// <param name="SelectedId"> ID сотрудника </param>
        /// <returns> Сотрудник </returns>
        public static List<Staff> GetStaffsById(int SelectedId)
        {
            List<Staff> staffsList;
            using (FootballClubsEntities entities = new FootballClubsEntities())
            {
                staffsList = entities.Staffs.Where<Staff>(staff => staff.StaffID == SelectedId).ToList();
            }
            return staffsList;
        }

        /// <summary>
        /// Возвращает игроков старше определенного возраста
        /// </summary>
        /// <param name="age"> Возраст </param>
        /// <returns> Список игроков </returns>
        public static IEnumerable<PlayerInfo> GetOldPlayers( string age )
        {
            FootballClubsEntities entities = new FootballClubsEntities();
            int maxAge;
            int.TryParse(age, out maxAge);

            var query = from Clubs in entities.Clubs
                        join Players in entities.Players
                        on Clubs.ClubID equals Players.ClubID
                        where (DateTime.Now.Year - Players.Birthdate.Value.Year) >= maxAge
                        select new PlayerInfo
                        {
                            Id = Players.PlayerID,
                            Фамилия = Players.LastName,
                            Имя = Players.FirstName,
                            Номер = (int)Players.Number,
                            Позиция = Players.Position,
                            ЗабитыеМячи = (int)Players.Goals,
                            Клуб = Clubs.Name,
                            ПобедыВСезоне = (int)Clubs.WinningMatches
                        };
            return query;
        }

        /// <summary>
        /// Возвращает игроков, которые забили больше всех голов за свой клуб
        /// </summary>
        /// <returns> Список игроков </returns>
        public static IEnumerable<BestPlayers> GetBestPlayers()
        {
            IEnumerable<BestPlayers> result;
            using (FootballClubsEntities entities = new FootballClubsEntities())
            {
                List<Player> bestPlayers = new List<Player>();
                foreach (var club in entities.Clubs)
                {
                    var max = entities.Players.Where(p => p.ClubID == club.ClubID).Max(p => p.Goals);
                    var item = from player in entities.Players
                               where (player.ClubID == club.ClubID && player.Goals == max)
                               select player;
                    foreach (var player in item)
                    {
                        bestPlayers.Add(player);
                    }
                }

                result = from player in bestPlayers
                             select new BestPlayers
                             {
                                 Фамилия = player.LastName,
                                 Имя = player.FirstName,
                                 Номер = (int)player.Number,
                                 ЗабитыеМячи = (int)player.Goals,
                                 Клуб = player.Club.Name
                             };
                return result;
            }           
        }

        /// <summary>
        /// Возвращает количество персонала по каждому клубу
        /// </summary>
        /// <returns> Список клубов </returns>
        public static IEnumerable<StaffsInfo> GetCountStaffByClub()
        {
            FootballClubsEntities entities = new FootballClubsEntities();
            var query = entities.Staffs
                        .GroupBy(p => new
                        {
                            p.Club.ClubID,
                            p.Club.Name
                        })
                        .Select(g => new StaffsInfo
                        {
                            Клуб = g.Key.Name,
                            КоличествоПерсонала = g.Count()
                        } ).AsEnumerable( );
            ;
            return query;          
        }

        /// <summary>
        /// Возвращает информацию об игроках, которая фильтруется с помощью определенных критериев
        /// </summary>
        /// <param name="highWin"> Максимальное количество побед </param>
        /// <param name="lowWin"> Минимальное количество побед </param>
        /// <param name="highGoals"> Максимальное количество забитых голов </param>
        /// <param name="lowGoals"> Минимальное количество забитых голов </param>
        /// <param name="highAge"> Максимальный возраст игрока </param>
        /// <param name="lowAge"> Минимальный возраст игрока </param>
        /// <param name="rowsCount"> Количество выводимых записей </param>
        /// <param name="firstChars"> Первые символы названия клуба </param>
        /// <param name="sort"> Порядок сортировки </param>
        /// <returns> Список игроков </returns>
        public static IEnumerable<PlayerInfo> GetPlayersInfo( int highWin,
                                                 int lowWin,
                                                 int highGoals, 
                                                 int lowGoals, 
                                                 int highAge,
                                                 int lowAge,
                                                 int rowsCount,
                                                 string firstChars,
                                                 bool? sort )
        {
            FootballClubsEntities entities = new FootballClubsEntities( );

            var query = from Clubs in entities.Clubs
                        join Players in entities.Players
                        on Clubs.ClubID equals Players.ClubID
                        where ( Clubs.WinningMatches <= highWin && Clubs.WinningMatches >= lowWin )
                                && ( Players.Goals <= highGoals && Players.Goals >= lowGoals )
                                && ( ( DateTime.Now.Year - Players.Birthdate.Value.Year ) >= lowAge
                                    && ( DateTime.Now.Year - Players.Birthdate.Value.Year ) <= highAge )
                                && ( ( firstChars.Length != 0 ) ? ( Clubs.Name.StartsWith( firstChars ) ) : Clubs.Name != "" )
                        select new PlayerInfo
                        {
                            Id = Players.PlayerID,
                            Фамилия = Players.LastName,
                            Имя = Players.FirstName,
                            Номер = (int)Players.Number,
                            Позиция = Players.Position,
                            ЗабитыеМячи = (int)Players.Goals,
                            Клуб = Clubs.Name,
                            ПобедыВСезоне = (int)Clubs.WinningMatches
                        };


            return query.Take( rowsCount );
        }

        /// <summary>
        /// Выбрать игроков по возрасту
        /// </summary>
        /// <param name="highAge"> До </param>
        /// <param name="lowAge"> От </param>
        /// <returns> Набор данных </returns>
        public static IEnumerable<PlayerInfo> GetPlayersByAge( int highAge, int lowAge )
        {
            FootballClubsEntities entities = new FootballClubsEntities( );

            var query = from Clubs in entities.Clubs
                        join Players in entities.Players
                        on Clubs.ClubID equals Players.ClubID
                        where ( ( DateTime.Now.Year - Players.Birthdate.Value.Year ) >= lowAge
                                    && ( DateTime.Now.Year - Players.Birthdate.Value.Year ) <= highAge ) 
                        select new PlayerInfo
                        {
                            Id = Players.PlayerID,
                            Фамилия = Players.LastName,
                            Имя = Players.FirstName,
                            Номер = (int)Players.Number,
                            Позиция = Players.Position,
                            ЗабитыеМячи = (int)Players.Goals,
                            Клуб = Clubs.Name,
                            ПобедыВСезоне = (int)Clubs.WinningMatches
                        };
            return query;
        }

        /// <summary>
        /// Выбрать игроков по названию клуба
        /// </summary>
        /// <param name="clubName"> Название клуба </param>
        /// <returns> Набор данных </returns>
        public static IEnumerable<PlayerInfo> GetPlayersByClubName( string clubName )
        {
            FootballClubsEntities entities = new FootballClubsEntities( );

            var query = from Clubs in entities.Clubs
                        join Players in entities.Players
                        on Clubs.ClubID equals Players.ClubID
                        where ( (clubName.Length != 0 ) ? ( Clubs.Name.StartsWith( clubName ) ) : Clubs.Name != "" )
                        select new PlayerInfo
                        {
                            Id = Players.PlayerID,
                            Фамилия = Players.LastName,
                            Имя = Players.FirstName,
                            Номер = (int)Players.Number,
                            Позиция = Players.Position,
                            ЗабитыеМячи = (int)Players.Goals,
                            Клуб = Clubs.Name,
                            ПобедыВСезоне = (int)Clubs.WinningMatches
                        };
            return query;
        }

        /// <summary>
        /// Выбрать игроков по количеству выигранных матчей
        /// </summary>
        /// <param name="highWin"> До </param>
        /// <param name="lowWin"> От </param>
        /// <returns> Набор данных </returns>
        public static IEnumerable<PlayerInfo> GetPlayersByClubWin( int highWin, int lowWin )
        {
            FootballClubsEntities entities = new FootballClubsEntities( );

            var query = from Clubs in entities.Clubs
                        join Players in entities.Players
                        on Clubs.ClubID equals Players.ClubID
                        where ( Clubs.WinningMatches <= highWin && Clubs.WinningMatches >= lowWin )
                        select new PlayerInfo
                        {
                            Id = Players.PlayerID,
                            Фамилия = Players.LastName,
                            Имя = Players.FirstName,
                            Номер = (int)Players.Number,
                            Позиция = Players.Position,
                            ЗабитыеМячи = (int)Players.Goals,
                            Клуб = Clubs.Name,
                            ПобедыВСезоне = (int)Clubs.WinningMatches
                        };

            return query;
        }

        /// <summary>
        /// Выбрать игроков по забитым мячам
        /// </summary>
        /// <param name="highGoals"> До </param>
        /// <param name="lowGoals"> От </param>
        /// <returns> Набор данных </returns>
        public static IEnumerable<PlayerInfo> GetPlayersByGoals( int highGoals, int lowGoals )
        {
            FootballClubsEntities entities = new FootballClubsEntities( );

            var query = from Clubs in entities.Clubs
                        join Players in entities.Players
                        on Clubs.ClubID equals Players.ClubID
                        where ( Players.Goals <= highGoals && Players.Goals >= lowGoals )
                        select new PlayerInfo
                        {
                            Id = Players.PlayerID,
                            Фамилия = Players.LastName,
                            Имя = Players.FirstName,
                            Номер = (int)Players.Number,
                            Позиция = Players.Position,
                            ЗабитыеМячи = (int)Players.Goals,
                            Клуб = Clubs.Name,
                            ПобедыВСезоне = (int)Clubs.WinningMatches
                        };

            return query;
        }

        /// <summary>
        /// Условие И
        /// </summary>
        /// <param name="oldQuery"> Прошлый запрос </param>
        /// <param name="newQuery"> Новый запрос </param>
        /// <returns> Набор данных </returns>
        public static IEnumerable<PlayerInfo> GetQueryAnd( IEnumerable<PlayerInfo> oldQuery, IEnumerable<PlayerInfo> newQuery )
        {
            if( oldQuery == null )
                return newQuery;

            var result = from r1 in oldQuery.ToArray( )
                         join r2 in newQuery
                         on r1.Id equals r2.Id
                         select r1;

            return result;
        }

        /// <summary>
        /// Условие Или
        /// </summary>
        /// <param name="oldQuery"> Прошлый запрос </param>
        /// <param name="newQuery"> Новый запрос </param>
        /// <returns> Набор данных </returns>
        public static IEnumerable<PlayerInfo> GetQueryOr( IEnumerable<PlayerInfo> oldQuery, IEnumerable<PlayerInfo> newQuery )
        {
            if( oldQuery == null )
                return newQuery;

            var tmpQuery = newQuery.ToList();

            // Удалить повторяющиеся значения
            foreach( var item in oldQuery )
            {
                for( int i = 0; i < tmpQuery.Count( ); i++ )
                    if( item.Id == tmpQuery[ i ].Id )
                        tmpQuery.Remove( tmpQuery[ i ] );
            }

            oldQuery = oldQuery.Union( tmpQuery );

            return oldQuery;
        }

        #endregion         
    }
}
