using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FootballClubsClient.Views
{
    /// <summary>
    /// Interaction logic for ConnectWindow.xaml
    /// </summary>
    public partial class ConnectWindow : Window, INotifyPropertyChanged
    {
        private int port;

        private String ipAddress;


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged( string info )
        {
            if( PropertyChanged != null )
                PropertyChanged( this, new PropertyChangedEventArgs( info ) );
        }

        public String IpAddress
        {
            get
            {
                return ipAddress;
            }
            set
            {
                ipAddress = value;
                OnPropertyChanged( "IpAddress" );
            }
        }

        public String Port
        {
            get
            {
                return port.ToString( );
            }
            set
            {
                int.TryParse( value, out port );
                OnPropertyChanged( "Port" );
            }
        }

        public ConnectWindow( )
        {
            port = 1532;
            ipAddress = "192.168.0.101";
            InitializeComponent( );
        }

        private void Button_Click_1( object sender, RoutedEventArgs e )
        {
            this.DialogResult = true;
            Close( );
        }

        public void Show( out int port, out String ip )
        {
            if( this.ShowDialog( ) == true )
            {
                port = this.port;
                ip = this.ipAddress;
            }
            else
            {
                port = 0;
                ip = "";
            }
        }
    }
}
