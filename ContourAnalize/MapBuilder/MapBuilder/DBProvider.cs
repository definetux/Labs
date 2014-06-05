using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;

namespace MapBuilder
{
    class DBProvider
    {
        public static DBMapsEntities Entities
        {
            get
            {
                return new DBMapsEntities( );
            }
        }

        public static void AddObject( object entity )
        {
            using( DBMapsEntities entities = new DBMapsEntities( ) )
            {
                string name = entity.ToString().Split('.')[1];
                entities.AddObject(name + "s", entity);
                entities.SaveChanges();
            }
        }

        public static void DeleteObject( object entity )
        {
            using( DBMapsEntities entities = new DBMapsEntities( ) )
            {
                entities.Attach( entity as IEntityWithKey );
                entities.DeleteObject( entity );
                entities.SaveChanges( );
            }
        }

        public static void Save( DBMapsEntities entities )
        {
            entities.SaveChanges( );
        }

        public static byte[ ] GetImageByIdMap( int id )
        {
            DBMapsEntities entities = new DBMapsEntities();
            var result = from table in entities.Maps
                         where table.ID_map == id
                         select table.Image;

            return result.First( );
        }

        public static List<Camera> GetCameraById( int id )
        {
            List<Camera> cameraList;
            using( DBMapsEntities entities = new DBMapsEntities( ) )
            {
                cameraList = entities.Cameras.Where<Camera>( camera => camera.ID_camera == id ).ToList( );
            }
            return cameraList;
        }

        public static List<Environment> GetEnvironmentById( int id )
        {
            List<Environment> environmentList;
            using( DBMapsEntities entities = new DBMapsEntities( ) )
            {
                environmentList = entities.Environments.Where<Environment>( camera => camera.ID_environment == id ).ToList( );
            }
            return environmentList;
        }

        public static List<Map> GetMapById( int id )
        {
            List<Map> mapList;
            using( DBMapsEntities entities = new DBMapsEntities( ) )
            {
                mapList = entities.Maps.Where<Map>( map => map.ID_map == id ).ToList( );
            }
            return mapList;
        }

        public static IEnumerable GetLightInfo( DateTime? date )
        {
            DBMapsEntities entities = new DBMapsEntities( );

            var result = from table in entities.Environments
                         where ( table.Time.Value.Day == date.Value.Day
                                    && table.Time.Value.Year == date.Value.Year
                                    && table.Time.Value.Month == date.Value.Month )
                         select new
                         {
                             ID = table.ID_environment,
                             УровеньОсвещенности = table.Illumination
                         };
            return result;
        }

        public static IEnumerable GetCameras( )
        {
            DBMapsEntities entities = new DBMapsEntities( );

            var result = from table in entities.Cameras
                         select new
                         {
                             ID = table.ID_camera,
                             Наименование = table.Name,
                             РазрещениеМатрицы = table.Resolution_matrix,
                             РазрешениеВидео = table.Resolution_video,
                             ЧастотаКадров = table.Frame_rate
                         };
            return result;
        }

        public static IEnumerable GetCountsOfMap( )
        {
            DBMapsEntities entities = new DBMapsEntities( );

            var query = entities.Maps
                        .GroupBy( p => new
                        {
                            p.Floor
                        } )
                        .Select( g => new
                        {
                            Этаж = g.Key.Floor,
                            КоличествоКарт = g.Count( )
                        } );

            return query;
        }

        public static IEnumerable GetCountsOfPosition( )
        {
            DBMapsEntities entities = new DBMapsEntities( );

            var query = entities.Positions
                        .GroupBy( p => new
                        {
                            p.ID_map,
                            p.Map.Floor
                        } )
                        .Select( g => new
                        {
                            IDКарты = g.Key.ID_map,
                            Этаж = g.Key.Floor,
                            КоличествоПозиций = g.Count( )
                        } );

            return query;
        }

        public static IEnumerable GetCamerasByDate( DateTime? date )
        {
            DBMapsEntities entities = new DBMapsEntities( );

            var result = from table1 in entities.Environments
                         join table2 in entities.Cameras
                         on table1.ID_environment equals table2.ID_environment
                         where( table1.Time.Value.Day == date.Value.Day
                                    && table1.Time.Value.Year == date.Value.Year
                                    && table1.Time.Value.Month == date.Value.Month )
                         select new
                         {
                             ID = table2.ID_camera,
                             Наименование = table2.Name,
                             РазрещениеМатрицы = table2.Resolution_matrix,
                             РазрешениеВидео = table2.Resolution_video,
                             ЧастотаКадров = table2.Frame_rate,
                             УровеньОсвещенности = table1.Illumination
                         };

            return result;
        }

        internal static List<Position>  GetPositionById( int id )
        {
            List<Position> posList;
            using( DBMapsEntities entities = new DBMapsEntities( ) )
            {
                posList = entities.Positions.Where<Position>( pos => pos.ID_position == id ).ToList( );
            }
            return posList;
        }
    }
}
