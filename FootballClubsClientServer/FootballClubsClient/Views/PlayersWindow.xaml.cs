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

namespace FootballClubsClient
{
    /// <summary>
    /// Interaction logic for Players.xaml
    /// </summary>
    public partial class PlayersWindow : Window
    {
        private int clubId;

        /// <summary>
        /// ID выбранной записи
        /// </summary>
        public int SelectedId { get; set; }

        /// <summary>
        /// Инициализация элементов управления
        /// </summary>
        public PlayersWindow()
        {
            InitializeComponent();
            clubId = 0;
        }

        /// <summary>
        /// Открыть форму в виде модального окна, отобрать данные для таблицы по ID
        /// </summary>
        /// <param name="clubId"> ID клуба </param>
        public void ShowDialog(int clubId)
        {
            this.clubId = clubId;
            playersDataGrid.ItemsSource = DBProvider.GetPlayersByClubId(clubId);

            base.ShowDialog();
        }

        /// <summary>
        /// Открыть форму добавления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddPlayerWindow addPlayerWindow = new AddPlayerWindow();
            addPlayerWindow.ShowDialog(clubId);
            playersDataGrid.ItemsSource = DBProvider.GetPlayersByClubId(clubId);
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var items = DBProvider.GetPlayersById( SelectedId );

            DBProvider.DeleteObject( items );

            playersDataGrid.ItemsSource = DBProvider.GetPlayersByClubId(clubId);
        }

        /// <summary>
        /// Изменить выбранный ID 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (playersDataGrid.SelectedItem is NewPlayer)
                this.SelectedId = ((NewPlayer)playersDataGrid.SelectedItem).PlayerID;
        }

        /// <summary>
        /// Обновить данные в БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if( playersDataGrid.SelectedItem is NewPlayer )
                this.SelectedId = ( ( NewPlayer )playersDataGrid.SelectedItem ).PlayerID;

            DBProvider.Save( playersDataGrid.SelectedItem as NewPlayer );
        }
    }
}
