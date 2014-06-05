using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for RobotPositionsWindows.xaml
    /// </summary>
    public partial class RobotPositionsWindows : Window
    {
        private int parentId;

        public RobotPositionsWindows( )
        {
            InitializeComponent( );
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

        private void Window_Loaded_1( object sender, RoutedEventArgs e )
        {

            MapBuilder.DBMapsEntities dBMapsEntities = new MapBuilder.DBMapsEntities( );
            // Load data into Positions. You can modify this code as needed.
            System.Windows.Data.CollectionViewSource positionsViewSource = ( ( System.Windows.Data.CollectionViewSource )( this.FindResource( "positionsViewSource" ) ) );
            System.Data.Objects.ObjectQuery<MapBuilder.Position> positionsQuery = this.GetPositionsQuery( dBMapsEntities );
            positionsViewSource.Source = positionsQuery.Execute( System.Data.Objects.MergeOption.AppendOnly );
        }

        public void Show( int id )
        {
            parentId = id;
            var maps = DBProvider.Entities.Positions.Where<Position>( p => p.ID_map == id );
            positionsDataGrid.ItemsSource = maps;
            this.ShowDialog( );
        }

        private void btnAdd_Click_1( object sender, RoutedEventArgs e )
        {
            Position pos = new Position( );

            int coordX;
            Int32.TryParse( txtCoordX.Text, out coordX );
            pos.CoordX = coordX;

            int coordY;
            Int32.TryParse( txtCoordY.Text, out coordY );
            pos.CoordY = coordY;

            int angle;
            Int32.TryParse( txtAngle.Text, out angle );
            pos.Angle = angle;

            pos.ID_map = parentId;

            DBProvider.AddObject( pos );
            positionsDataGrid.ItemsSource = DBProvider.Entities.Positions.Where( p => p.ID_map == parentId );
        }

        private void btnClear_Click( object sender, RoutedEventArgs e )
        {
            txtAngle.Text = "";
            txtCoordY.Text = "";
            txtCoordX.Text = "";
            txtId.Text = "";
        }

        private void btnDelete_Click( object sender, RoutedEventArgs e )
        {
            int id;
            Int32.TryParse( txtId.Text, out id );

            var item = DBProvider.GetPositionById( id ).First( );

            DBProvider.DeleteObject( item );

            positionsDataGrid.ItemsSource = DBProvider.Entities.Positions.Where( p => p.ID_map == parentId );
        }
    }
}
