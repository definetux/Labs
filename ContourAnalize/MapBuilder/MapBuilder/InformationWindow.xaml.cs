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
    /// Interaction logic for InformationWindow.xaml
    /// </summary>
    public partial class InformationWindow : Window
    {
        private int selectedId;

        public InformationWindow( )
        {
            InitializeComponent( );
            environmentsDataGrid.ItemsSource = DBProvider.Entities.Environments;
        }

        private System.Data.Objects.ObjectQuery<Environment> GetEnvironmentsQuery( DBMapsEntities dBMapsEntities )
        {
            System.Data.Objects.ObjectQuery<MapBuilder.Environment> environmentsQuery = dBMapsEntities.Environments;
            // Returns an ObjectQuery.
            return environmentsQuery;
        }

        private void Window_Loaded_1( object sender, RoutedEventArgs e )
        {

          //  MapBuilder.DBMapsEntities dBMapsEntities = new MapBuilder.DBMapsEntities( );
            // Load data into Environments. You can modify this code as needed.
          //  System.Windows.Data.CollectionViewSource environmentsViewSource = ( ( System.Windows.Data.CollectionViewSource )( this.FindResource( "environmentsViewSource" ) ) );
          //  System.Data.Objects.ObjectQuery<MapBuilder.Environment> environmentsQuery = this.GetEnvironmentsQuery( dBMapsEntities );
          //  environmentsViewSource.Source = environmentsQuery.Execute( System.Data.Objects.MergeOption.AppendOnly );
        }

        private void btnAdd_Click_1( object sender, RoutedEventArgs e )
        {
            Environment environment = new Environment( );

            int illumination;
            int.TryParse(txtIllumination.Text, out illumination);
            environment.Illumination = illumination;

            int width;
            int.TryParse( txtWidth.Text, out width );
            environment.Robots_width = width;

            DateTime? time = txtTime.SelectedDate;

            environment.Time = time;

            DBProvider.AddObject( environment );
            environmentsDataGrid.ItemsSource = DBProvider.Entities.Environments;
        }

        private void btnDelete_Click( object sender, RoutedEventArgs e )
        {
            int id;
            Int32.TryParse( txtId.Text, out id );

            var item = DBProvider.GetEnvironmentById( id ).First();
            DBProvider.DeleteObject( item );
            environmentsDataGrid.ItemsSource = DBProvider.Entities.Environments;
        }

        private void btnShowCameraInfo_Click( object sender, RoutedEventArgs e )
        {
            CameraInfoWindow cameraWnd = new CameraInfoWindow( );
            cameraWnd.Show( selectedId );
        }

        private void environmentsDataGrid_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if( environmentsDataGrid.SelectedItem is Environment )
                this.selectedId = ( ( Environment )environmentsDataGrid.SelectedItem ).ID_environment;
        }

        private void btnClear_Click( object sender, RoutedEventArgs e )
        {
            txtIllumination.Text = "";
            txtTime.Text = "";
            txtWidth.Text = "";
        }

        private void btnShowMapInfo_Click( object sender, RoutedEventArgs e )
        {
            MapInfoWindow mapWnd = new MapInfoWindow( );
            mapWnd.Show( selectedId );
        }
    }
}
