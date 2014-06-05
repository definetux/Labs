using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MapBuilder
{
    /// <summary>
    /// Interaction logic for MapInfoWindow.xaml
    /// </summary>
    public partial class MapInfoWindow : Window
    {
        private int parentId;

        private int selectedId;

        private int currentCoordX;
        private int currentCoordY;
        private int currentAngle;
        private int currentScale;
        private System.Drawing.Bitmap currentBitmap;

        public MapInfoWindow( )
        {
            InitializeComponent( );
        }

        [DllImport( "gdi32" )]
        static extern int DeleteObject( IntPtr o );

        private System.Data.Objects.ObjectQuery<Map> GetMapsQuery( DBMapsEntities dBMapsEntities )
        {
            System.Data.Objects.ObjectQuery<MapBuilder.Map> mapsQuery = dBMapsEntities.Maps;
            // To explicitly load data, you may need to add Include methods like below:
            // mapsQuery = mapsQuery.Include("Maps.Environment").
            // For more information, please see http://go.microsoft.com/fwlink/?LinkId=157380
            // Returns an ObjectQuery.
            return mapsQuery;
        }

        private void Window_Loaded_1( object sender, RoutedEventArgs e )
        {

            MapBuilder.DBMapsEntities dBMapsEntities = new MapBuilder.DBMapsEntities( );
            // Load data into Maps. You can modify this code as needed.
            System.Windows.Data.CollectionViewSource mapsViewSource = ( ( System.Windows.Data.CollectionViewSource )( this.FindResource( "mapsViewSource" ) ) );
            System.Data.Objects.ObjectQuery<MapBuilder.Map> mapsQuery = this.GetMapsQuery( dBMapsEntities );
            mapsViewSource.Source = mapsQuery.Execute( System.Data.Objects.MergeOption.AppendOnly );
            // Load data into Positions. You can modify this code as needed.
            System.Windows.Data.CollectionViewSource positionsViewSource = ( ( System.Windows.Data.CollectionViewSource )( this.FindResource( "positionsViewSource" ) ) );
            System.Data.Objects.ObjectQuery<MapBuilder.Position> positionsQuery = this.GetPositionsQuery( dBMapsEntities );
            positionsViewSource.Source = positionsQuery.Execute( System.Data.Objects.MergeOption.AppendOnly );
        }

        public void Show( int id )
        {
            parentId = id;
            var maps = DBProvider.Entities.Maps.Where<Map>( p => p.ID_environment == id );
            mapsDataGrid.ItemsSource = maps;
            this.Show( );
        }

        private void btnDelete_Click( object sender, RoutedEventArgs e )
        {
            int id;
            Int32.TryParse( txtId.Text, out id );

            var item = DBProvider.GetMapById( id ).First( );

            DBProvider.DeleteObject( item );

            mapsDataGrid.ItemsSource = DBProvider.Entities.Maps.Where( p => p.ID_environment == parentId );
        }

        private void btnAdd_Click_1( object sender, RoutedEventArgs e )
        {
            Map map = new Map( );

            int floor;
            Int32.TryParse( txtFloor.Text, out floor );
            map.Floor = floor;
            
            int scale;
            Int32.TryParse( txtScale.Text, out scale );
            map.Scale = scale;

            map.ID_environment = parentId;

            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap( "fill.bmp" );
            MemoryStream ms = new MemoryStream( );
            bmp.Save( ms, ImageFormat.Bmp );
            byte[ ] bitmapData = ms.ToArray( );
            map.Image = bitmapData;

            DBProvider.AddObject( map );
            mapsDataGrid.ItemsSource = DBProvider.Entities.Maps.Where( p => p.ID_environment == parentId );
        }

        public static BitmapImage BitmapFromByteArray( byte[ ] imageData )
        {
            if( imageData == null || imageData.Length == 0 )
                return null;
            var image = new BitmapImage( );
            using( var mem = new MemoryStream( imageData ) )
            {
                mem.Position = 0;
                image.BeginInit( );
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit( );
            }
            image.Freeze( );
            return image;
        }

        public static System.Drawing.Bitmap BmpFromByteArray( byte[ ] imageData )
        {
            TypeConverter tc = TypeDescriptor.GetConverter( typeof( System.Drawing.Bitmap ) );
            System.Drawing.Bitmap bitmap = ( System.Drawing.Bitmap )tc.ConvertFrom( imageData );
            return bitmap;
        }

        private void btnClear_Click( object sender, RoutedEventArgs e )
        {
            txtFloor.Text = "";
            txtId.Text = "";
            txtScale.Text = "";
        }

        private void btnAddPosition_Click( object sender, RoutedEventArgs e )
        {
            RobotPositionsWindows mapWnd = new RobotPositionsWindows( );
            mapWnd.Show( selectedId );

            mapsDataGrid.ItemsSource = DBProvider.Entities.Maps.Where( p => p.ID_environment == parentId );

            var poses = DBProvider.Entities.Positions.Where<Position>( p => p.ID_map == this.selectedId );
            positionsDataGrid.ItemsSource = poses;
        }

        private void mapsDataGrid_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if( mapsDataGrid.SelectedItem is Map )
            {
                this.selectedId = ( ( Map )mapsDataGrid.SelectedItem ).ID_map;
                this.currentScale = ( int )( ( Map )mapsDataGrid.SelectedItem ).Scale;
                var poses = DBProvider.Entities.Positions.Where<Position>( p => p.ID_map == this.selectedId );
                positionsDataGrid.ItemsSource = poses;
                imgResult.Source = BitmapFromByteArray( DBProvider.GetImageByIdMap( this.selectedId ) );
            }
            else
            {
                imgResult.Source = null;
                positionsDataGrid.ItemsSource = null;
            }
        }

        private void btnAddImage_Click( object sender, RoutedEventArgs e )
        {
            currentBitmap = BmpFromByteArray( DBProvider.GetImageByIdMap( selectedId ) );

            MapCreator map = new MapCreator( );
            System.Drawing.Bitmap bmp =  map.GetBitmap( currentBitmap, currentCoordX, currentCoordY, currentScale, currentAngle  );

            IntPtr ip = bmp.GetHbitmap( );
            BitmapSource bs = null;
            try
            {
                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap( ip,
                   IntPtr.Zero, Int32Rect.Empty,
                   System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions( ) );
            }
            finally
            {
                DeleteObject( ip );
            }

            DBMapsEntities entities = DBProvider.Entities;
            var oldMap = entities.Maps.Single( p => p.ID_map == selectedId );

            MemoryStream ms = new MemoryStream( );
            bmp.Save( ms, ImageFormat.Bmp );
            byte[ ] bitmapData = ms.ToArray( );
            oldMap.Image = bitmapData;

            DBProvider.Save( entities );

            imgResult.Source = bs;
        }

        private System.Data.Objects.ObjectQuery<Position> GetPositionsQuery( DBMapsEntities dBMapsEntities )
        {
            System.Data.Objects.ObjectQuery<MapBuilder.Position> positionsQuery = dBMapsEntities.Positions;
            // To explicitly load data, you may need to add Include methods like below:
            // positionsQuery = positionsQuery.Include("Positions.Map").
            // For more information, please see http://go.microsoft.com/fwlink/?LinkId=157380
            // Returns an ObjectQuery.
            return positionsQuery;
        }

        private void positionsDataGrid_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if( positionsDataGrid.SelectedItem is Position )
            {
                currentAngle = (int)( ( Position )positionsDataGrid.SelectedItem ).Angle;
                currentCoordX = (int)( ( Position )positionsDataGrid.SelectedItem ).CoordX;
                currentCoordY = (int)( ( Position )positionsDataGrid.SelectedItem ).CoordY;

            }
        }
    }
}
