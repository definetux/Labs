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
    /// Interaction logic for CameraInfoWindow.xaml
    /// </summary>
    public partial class CameraInfoWindow : Window
    {
        private int parentId;

        public CameraInfoWindow( )
        {
            InitializeComponent( );
        }

        private System.Data.Objects.ObjectQuery<Camera> GetCamerasQuery( DBMapsEntities dBMapsEntities )
        {
            System.Data.Objects.ObjectQuery<MapBuilder.Camera> camerasQuery = dBMapsEntities.Cameras;
            // To explicitly load data, you may need to add Include methods like below:
            // camerasQuery = camerasQuery.Include("Cameras.Environment").
            // For more information, please see http://go.microsoft.com/fwlink/?LinkId=157380
            // Returns an ObjectQuery.
            return camerasQuery;
        }

        private void Window_Loaded_1( object sender, RoutedEventArgs e )
        {

            MapBuilder.DBMapsEntities dBMapsEntities = new MapBuilder.DBMapsEntities( );
            // Load data into Cameras. You can modify this code as needed.
            System.Windows.Data.CollectionViewSource camerasViewSource = ( ( System.Windows.Data.CollectionViewSource )( this.FindResource( "camerasViewSource" ) ) );
            System.Data.Objects.ObjectQuery<MapBuilder.Camera> camerasQuery = this.GetCamerasQuery( dBMapsEntities );
            camerasViewSource.Source = camerasQuery.Execute( System.Data.Objects.MergeOption.AppendOnly );
        }

        public void Show( int id )
        {
            parentId = id;
            var cameras = DBProvider.Entities.Cameras.Where( p => p.ID_environment == id );
            camerasDataGrid.ItemsSource = cameras;
            this.Show( );
        }

        private void btnAdd_Click_1( object sender, RoutedEventArgs e )
        {
            Camera camera = new Camera( );
            camera.Name = txtName.Text;

            int video;
            Int32.TryParse( txtVideo.Text, out video );
            camera.Resolution_video = video;

            int matrix;
            Int32.TryParse( txtMatrix.Text, out matrix );
            camera.Resolution_matrix = matrix;

            int frame;
            Int32.TryParse( txtFrame.Text, out frame );
            camera.Frame_rate = frame;

            camera.ID_environment = parentId;

            DBProvider.AddObject( camera );
            camerasDataGrid.ItemsSource = DBProvider.Entities.Cameras.Where( p => p.ID_environment == parentId );
        }

        private void btnClear_Click( object sender, RoutedEventArgs e )
        {
            txtFrame.Text = "";
            txtMatrix.Text = "";
            txtName.Text = "";
            txtVideo.Text = "";
        }

        private void btnDelete_Click( object sender, RoutedEventArgs e )
        {
            int id;
            Int32.TryParse( txtId.Text, out id );

            var item = DBProvider.GetCameraById( id ).First();

            DBProvider.DeleteObject( item );
            
            camerasDataGrid.ItemsSource = DBProvider.Entities.Cameras.Where( p => p.ID_environment == parentId );
        }
    }
}
