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
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        public ResultsWindow( )
        {
            InitializeComponent( );
        }

        private void btnSearch_Click( object sender, RoutedEventArgs e )
        {
            string query = cbQuery.Text;

            switch( query )
            {
                case "Уровень освещенности":
                    gridResult.ItemsSource = DBProvider.GetLightInfo( dpDate.SelectedDate );
                    break;
                case "Использованные видеокамеры":
                    gridResult.ItemsSource = DBProvider.GetCameras( );
                    break;
                case "Использованные камеры времени суток":
                    if( dpDate.Text != "" )
                        gridResult.ItemsSource = DBProvider.GetCamerasByDate( dpDate.SelectedDate );
                    else
                        gridResult.ItemsSource = DBProvider.GetCameras( );
                    break;
                case "Количество карт по этажам":
                    gridResult.ItemsSource = DBProvider.GetCountsOfMap( );
                    break;
                case "Количество позиций робота на карте":
                    gridResult.ItemsSource = DBProvider.GetCountsOfPosition( );
                    break;
            }
            dpDate.Text = "";
        }

        private void cbQuery_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            string query = ( ( ComboBoxItem )cbQuery.SelectedItem ).Content.ToString( );

            switch( query )
            {
                case "Уровень освещенности":
                    dpDate.Visibility = Visibility.Visible;
                    tbDate.Visibility = Visibility.Visible;
                    break;
                case "Использованные видеокамеры":
                    dpDate.Visibility = Visibility.Hidden;
                    tbDate.Visibility = Visibility.Hidden;
                    break;
                case "Использованные камеры времени суток":
                    dpDate.Visibility = Visibility.Visible;
                    tbDate.Visibility = Visibility.Visible;
                    break;
                case "Количество карт по этажам":
                    dpDate.Visibility = Visibility.Hidden;
                    tbDate.Visibility = Visibility.Hidden;
                    break;
                case "Количество позиций робота на карте":
                    dpDate.Visibility = Visibility.Hidden;
                    tbDate.Visibility = Visibility.Hidden;
                    break;
            }
        }
    }
}
