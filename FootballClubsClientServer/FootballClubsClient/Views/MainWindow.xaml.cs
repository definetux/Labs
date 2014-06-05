using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FootballClubsClient
{
    enum AudioPlayer { Play, Stop };
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Выбранный ID записи
        /// </summary>
        public int SelectedId { get; set; }

        /// <summary>
        /// Инициализация элементов управления
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            clubsDataGrid.ItemsSource = DBProvider.GetClubs( );
        }

        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }

        /// <summary>
        /// Открыть форму добавления 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddClubWindow addClub = new AddClubWindow();
            addClub.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            addClub.ShowDialog();

            clubsDataGrid.ItemsSource = DBProvider.GetClubs( );
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var items = DBProvider.GetClubsById( SelectedId );

            DBProvider.DeleteObject<NewClub>( items );

            clubsDataGrid.ItemsSource = DBProvider.GetClubs( );
        }

        /// <summary>
        /// Открыть форму игроков
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowPlayers_Click(object sender, RoutedEventArgs e)
        {
            PlayersWindow players = new PlayersWindow();
            players.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            players.ShowDialog(this.SelectedId);
        }

        /// <summary>
        /// Открыть форму сотрудников
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowStaff_Click(object sender, RoutedEventArgs e)
        {
            CustomersWindow customer = new CustomersWindow();
            customer.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            customer.ShowDialog(this.SelectedId);
        }

        /// <summary>
        /// Изменить проигрывание музыки, отображение логотипа, и выбранный ID записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clubsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (clubsDataGrid.SelectedItem is NewClub)
               this.SelectedId = ((NewClub)clubsDataGrid.SelectedItem).ClubID;
        }

        /// <summary>
        /// Открыть форму фильтров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            QueryWindow queryWindows = new QueryWindow();
            queryWindows.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            queryWindows.ShowDialog();
        }

        /// <summary>
        /// Обновить данные в БД 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if( clubsDataGrid.SelectedItem is NewClub )
                this.SelectedId = ( ( NewClub )clubsDataGrid.SelectedItem ).ClubID;

            DBProvider.Save( clubsDataGrid.SelectedItem as NewClub );
        }

        /// <summary>
        /// Сортировать записи по названию клуба
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clubsDataGrid_Sorting( object sender, DataGridSortingEventArgs e )
        {
        }      
    }
}