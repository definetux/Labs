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
using FootballClubsClient.Views;

namespace FootballClubsClient
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        /// <summary>
        /// Инициализация элементов управления
        /// </summary>
        public WelcomeWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Закрыть форму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Открыть форму Клубы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();
        }

        private void btnConnect_Click( object sender, RoutedEventArgs e )
        {
            ConnectWindow connectWindow = new ConnectWindow( );
            int port;
            String ip;
            connectWindow.Show( out port, out ip );

            DBProvider.CreateClient( port, ip );
            if( DBProvider.Client.Connect( ) == true )
            {
                btnOpen.Visibility = Visibility.Visible;
            }
            else
            {
                btnOpen.Visibility = Visibility.Hidden;
            }

        }
    }
}
