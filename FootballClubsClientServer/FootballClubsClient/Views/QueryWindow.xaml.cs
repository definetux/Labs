using System;
using System.Collections;
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
    /// Interaction logic for QueryWindow.xaml
    /// </summary>
    public partial class QueryWindow : Window
    {

        private IEnumerable<PlayerInfo> currentQuery;
        /// <summary>
        /// Инициализация элементов управления
        /// </summary>
        public QueryWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Показать данные в таблице
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click( object sender, RoutedEventArgs e )
        {
            switch( cbTables.SelectedIndex )
            {
                // Информация об игроке
                case 0:
                    {
                        var result = GetInfo( );

                        currentQuery = CheckCondition( result );

                        dgQueryTable.ItemsSource = CheckSort( currentQuery );
                        if( dgQueryTable.Columns.Count != 0 )
                        {
                            dgQueryTable.Columns.RemoveAt( 0 );
                            SetToolTip( "Информация об игроках" );
                        }

                    }
                    break;
                // Игроки по клубу
                case 2:
                    {
                        var result = DBProvider.GetPlayersByClubName( txtClub.Text );

                        currentQuery = CheckCondition( result );

                        dgQueryTable.ItemsSource = CheckSort( currentQuery );

                        if( dgQueryTable.Columns.Count != 0 )
                            dgQueryTable.Columns.RemoveAt( 0 );

                        SetToolTip( "Игроки по клубу" );

                    }
                    break;
                // Игроки по возрасту
                case 3:
                    {
                        var result = DBProvider.GetOldPlayers( txtAge.Text );

                        currentQuery = CheckCondition( result );

                        dgQueryTable.ItemsSource = CheckSort( currentQuery );

                        if( dgQueryTable.Columns.Count != 0 )
                            dgQueryTable.Columns.RemoveAt( 0 );

                        SetToolTip( "Игроки по возрасту" );

                    }
                    break;
                // Лучшие игроки
                case 4:
                    {
                        var query = DBProvider.GetBestPlayers( );
                        dgQueryTable.ItemsSource = query;
                        dgQueryTable.CanUserAddRows = false;
                        
                        dgQueryTable.ToolTip = "Лучшие игроки";
                    }
                    break;
                // Персонал клубов
                case 5:
                    {
                        var query = DBProvider.GetCountStaffByClub( );
                        dgQueryTable.ItemsSource = query;
                        dgQueryTable.CanUserAddRows = false;
                        
                        dgQueryTable.ToolTip = "Количество персонала";
                    }
                    break;
            }
            tbHelp.Visibility = Visibility.Hidden;
       
        }

        /// <summary>
        /// Установить подсказку, которая зависит от выбранного условия
        /// </summary>
        /// <param name="text"> Текст подсказки </param>
        private void SetToolTip( string text )
        {
            if( rbtnAnd.IsChecked == true )
                dgQueryTable.ToolTip += " и " + text;
            else
                if( rbtnOr.IsChecked == true )
                    dgQueryTable.ToolTip += " или " + text;
                else
                    dgQueryTable.ToolTip = text;
        }

        /// <summary>
        /// Отсортировать набор, в зависимости от выбранной сортировки
        /// </summary>
        /// <param name="result"> Отсортированный набор </param>
        /// <returns></returns>
        public IEnumerable<PlayerInfo> CheckSort( IEnumerable<PlayerInfo> result )
        {
            if( currentQuery == null )
                return null;
            if( result == null )
                return null;

            int rowsCount;
            Int32.TryParse( tbRowsCount.Text, out rowsCount );
            if( tbRowsCount.Text == String.Empty )
                rowsCount = Int32.MaxValue;

            if( rbtnGoalsSort.IsChecked == true )
                currentQuery = currentQuery
                                        .Take( rowsCount )
                                        .OrderByDescending( p => p.ЗабитыеМячи );
            else
                currentQuery = currentQuery
                                        .Take( rowsCount )
                                        .OrderBy( p => p.Фамилия );
            return currentQuery;
        }

        /// <summary>
        /// Вернуть информацию об игроках
        /// </summary>
        /// <returns> Список игроков </returns>
        private IEnumerable<PlayerInfo> GetInfo( )
        {
            // Данные о победах
            int lowWin, highWin;
            Int32.TryParse( tbWinMatchesLow.Text, out lowWin );
            Int32.TryParse( tbWinMatchesHigh.Text, out highWin );
            if( tbWinMatchesHigh.Text == String.Empty || tbWinMatchesHigh.Text == "До" )
                highWin = Int32.MaxValue;

            // Данные о голах
            int lowGoals, highGoals;
            Int32.TryParse( tbGoalsLow.Text, out lowGoals );
            Int32.TryParse( tbGoalsHigh.Text, out highGoals );
            if( tbGoalsHigh.Text == String.Empty || tbWinMatchesHigh.Text == "До" )
                highGoals = Int32.MaxValue;

            // Данные о возрасте
            int lowAge, highAge;
            Int32.TryParse( tbPlayerAgeLow.Text, out lowAge );
            Int32.TryParse( tbPlayerAgeHigh.Text, out highAge );
            if( tbPlayerAgeHigh.Text == String.Empty || tbWinMatchesHigh.Text == "До" )
                highAge = Int32.MaxValue;

            // Данные о количестве строк
            int rowsCount;
            Int32.TryParse( tbRowsCount.Text, out rowsCount );
            if( tbRowsCount.Text == String.Empty )
                rowsCount = Int32.MaxValue;

            var query = DBProvider.GetPlayersInfo( highWin,
                                                           lowWin,
                                                           highGoals,
                                                           lowGoals,
                                                           highAge,
                                                           lowAge,
                                                           rowsCount,
                                                           tbClubsFirstChar.Text,
                                                           rbtnLastNameSort.IsChecked );
            return query;
        }

        /// <summary>
        /// Показывает определенные поля для фильтров, согласно выбранному пункту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbTables_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            HideControls( );

            switch( cbTables.SelectedIndex )
            {
                // Информация об игроках
                case 0:
                    {
                        tbWinMatchesHigh.Visibility = Visibility.Visible;
                        tbWinMatchesLow.Visibility = Visibility.Visible;
                        lblWinRate.Visibility = Visibility.Visible;

                        lblRowsCount.Visibility = Visibility.Visible;
                        tbRowsCount.Visibility = Visibility.Visible;

                        lblGoals.Visibility = Visibility.Visible;
                        tbGoalsHigh.Visibility = Visibility.Visible;
                        tbGoalsLow.Visibility = Visibility.Visible;

                        lblClubsFirstChar.Visibility = Visibility.Visible;
                        tbClubsFirstChar.Visibility = Visibility.Visible;

                        lblPlayerAge1.Visibility = Visibility.Visible;
                        tbPlayerAgeHigh.Visibility = Visibility.Visible;
                        tbPlayerAgeLow.Visibility = Visibility.Visible;
                    }
                    break;
                case 1:
                    {
                        tbWinMatchesHigh.Visibility = Visibility.Visible;
                        tbWinMatchesLow.Visibility = Visibility.Visible;
                        lblWinRate.Visibility = Visibility.Visible;

                        lblRowsCount.Visibility = Visibility.Visible;
                        tbRowsCount.Visibility = Visibility.Visible;

                        lblGoals.Visibility = Visibility.Visible;
                        tbGoalsHigh.Visibility = Visibility.Visible;
                        tbGoalsLow.Visibility = Visibility.Visible;

                        lblClubsFirstChar.Visibility = Visibility.Visible;
                        tbClubsFirstChar.Visibility = Visibility.Visible;

                        lblPlayerAge1.Visibility = Visibility.Visible;
                        tbPlayerAgeHigh.Visibility = Visibility.Visible;
                        tbPlayerAgeLow.Visibility = Visibility.Visible;

                        btnAge.Visibility = Visibility;
                        btnClubName.Visibility = Visibility;
                        btnWinrate.Visibility = Visibility;
                        btnGoals.Visibility = Visibility;

                        btnSearch.Visibility = System.Windows.Visibility.Hidden;
                    }
                    break;
                // Игроки по клубу
                case 2:
                    {
                        lblClubName.Visibility = Visibility.Visible;
                        txtClub.Visibility = Visibility.Visible;

                        lblRowsCount.Visibility = Visibility.Visible;
                        tbRowsCount.Visibility = Visibility.Visible;
                    }
                    break;
                // Игроки по возрасту
                case 3:
                    {
                        lblPlayerAge.Visibility = Visibility.Visible;
                        txtAge.Visibility = Visibility.Visible;

                        lblRowsCount.Visibility = Visibility.Visible;
                        tbRowsCount.Visibility = Visibility.Visible;
                    }
                    break;
                case 4:
                    {
                        rbtnAnd.IsEnabled = false;
                        rbtnOr.IsEnabled = false;
                        rbtnOnly.IsEnabled = false;

                        rbtnGoalsSort.IsEnabled = false;
                        rbtnLastNameSort.IsEnabled = false;
                    }
                    break;
                case 5:
                    {
                        rbtnAnd.IsEnabled = false;
                        rbtnOr.IsEnabled = false;
                        rbtnOnly.IsEnabled = false;

                        rbtnGoalsSort.IsEnabled = false;
                        rbtnLastNameSort.IsEnabled = false;
                    }
                    break;
            }
        }

        /// <summary>
        /// Скрыть фильтры
        /// </summary>
        private void HideControls( )
        {
            tbWinMatchesHigh.Visibility = Visibility.Hidden;
            tbWinMatchesLow.Visibility = Visibility.Hidden;
            lblWinRate.Visibility = Visibility.Hidden;

            lblRowsCount.Visibility = Visibility.Hidden;
            tbRowsCount.Visibility = Visibility.Hidden;

            lblGoals.Visibility = Visibility.Hidden;
            tbGoalsHigh.Visibility = Visibility.Hidden;
            tbGoalsLow.Visibility = Visibility.Hidden;

            lblClubsFirstChar.Visibility = Visibility.Hidden;
            tbClubsFirstChar.Visibility = Visibility.Hidden;

            lblPlayerAge1.Visibility = Visibility.Hidden;
            tbPlayerAgeHigh.Visibility = Visibility.Hidden;
            tbPlayerAgeLow.Visibility = Visibility.Hidden;

            lblClubName.Visibility = Visibility.Hidden;
            txtClub.Visibility = Visibility.Hidden;

            lblPlayerAge.Visibility = Visibility.Hidden;
            txtAge.Visibility = Visibility.Hidden;

            btnGoals.Visibility = Visibility.Hidden;
            btnAge.Visibility = Visibility.Hidden;
            btnWinrate.Visibility = Visibility.Hidden;
            btnClubName.Visibility = Visibility.Hidden;

            rbtnAnd.IsEnabled = true;
            rbtnOr.IsEnabled = true;
            rbtnOnly.IsEnabled = true;

            rbtnGoalsSort.IsEnabled = true;
            rbtnLastNameSort.IsEnabled = true;

            btnSearch.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Изменить поле для ввода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbWinMatchesLow_GotFocus( object sender, RoutedEventArgs e )
        {
            ( ( TextBox )sender ).Foreground = Brushes.Black;
            ( ( TextBox )sender ).Text = "";
        }

        /// <summary>
        /// Восстановить значение по умолчанию для поля ввода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbWinMatchesLow_LostFocus( object sender, RoutedEventArgs e )
        {
            if( ( ( TextBox )sender ).Text == "" )
            {
                ( ( TextBox )sender ).Foreground = Brushes.Gray;
                ( ( TextBox )sender ).Text = "От";
            }
        }

        /// <summary>
        /// Восстановить значение по умолчанию для поля ввода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbWinMatchesHigh_LostFocus( object sender, RoutedEventArgs e )
        {
            if( ( ( TextBox )sender ).Text == "" )
            {
                ( ( TextBox )sender ).Foreground = Brushes.Gray;
                ( ( TextBox )sender ).Text = "До";
            }
        }

        /// <summary>
        /// Очистить таблицу, установить значения по умолчанию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearDg_Click( object sender, RoutedEventArgs e )
        {
            currentQuery = null;
            dgQueryTable.ItemsSource = null;
            rbtnOnly.IsChecked = true;
            tbHelp.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// Выборка по количеству выигранных матчей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWinrate_Click( object sender, RoutedEventArgs e )
        {
            tbHelp.Visibility = System.Windows.Visibility.Visible;

            int rowsCount;
            Int32.TryParse( tbRowsCount.Text, out rowsCount );
            if( tbRowsCount.Text == String.Empty )
                rowsCount = Int32.MaxValue;

            int lowWin, highWin;
            Int32.TryParse( tbWinMatchesLow.Text, out lowWin );
            Int32.TryParse( tbWinMatchesHigh.Text, out highWin );
            if( tbWinMatchesHigh.Text == String.Empty || tbWinMatchesHigh.Text == "До" )
                highWin = Int32.MaxValue;

            var result = DBProvider.GetPlayersByClubWin( highWin, lowWin );

            dgQueryTable.ItemsSource = CheckCondition( result ).Take( rowsCount );

            if( dgQueryTable.Columns.Count != 0 )
                dgQueryTable.Columns.RemoveAt( 0 );

            SetToolTip( "Игроки по выигранным матчам" );
            tbHelp.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Выборка по имени клуба
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClubName_Click( object sender, RoutedEventArgs e )
        {
            tbHelp.Visibility = System.Windows.Visibility.Visible;

            int rowsCount;
            Int32.TryParse( tbRowsCount.Text, out rowsCount );
            if( tbRowsCount.Text == String.Empty )
                rowsCount = Int32.MaxValue;

            var result = DBProvider.GetPlayersByClubName( tbClubsFirstChar.Text );


            dgQueryTable.ItemsSource = CheckCondition( result ).Take( rowsCount );

            if( dgQueryTable.Columns.Count != 0 )
                dgQueryTable.Columns.RemoveAt( 0 );

            SetToolTip( "Игроки по клубу" );
            tbHelp.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Выборка по забитым мячам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGoals_Click( object sender, RoutedEventArgs e )
        {
            tbHelp.Visibility = System.Windows.Visibility.Visible;

            int rowsCount;
            Int32.TryParse( tbRowsCount.Text, out rowsCount );
            if( tbRowsCount.Text == String.Empty )
                rowsCount = Int32.MaxValue;

            // Данные о голах
            int lowGoals, highGoals;
            Int32.TryParse( tbGoalsLow.Text, out lowGoals );
            Int32.TryParse( tbGoalsHigh.Text, out highGoals );
            if( tbGoalsHigh.Text == String.Empty || tbWinMatchesHigh.Text == "До" )
                highGoals = Int32.MaxValue;

            var result = DBProvider.GetPlayersByGoals( highGoals, lowGoals );

            dgQueryTable.ItemsSource = CheckCondition( result ).Take( rowsCount );

            if( dgQueryTable.Columns.Count != 0 )
                dgQueryTable.Columns.RemoveAt( 0 );

            SetToolTip( "Игроки по забитым мячам" );
            tbHelp.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Выборка по возрасту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAge_Click( object sender, RoutedEventArgs e )
        {
            tbHelp.Visibility = System.Windows.Visibility.Visible;

            int rowsCount;
            Int32.TryParse( tbRowsCount.Text, out rowsCount );
            if( tbRowsCount.Text == String.Empty )
                rowsCount = Int32.MaxValue;

            // Данные о возрасте
            int lowAge, highAge;
            Int32.TryParse( tbPlayerAgeLow.Text, out lowAge );
            Int32.TryParse( tbPlayerAgeHigh.Text, out highAge );
            if( tbPlayerAgeHigh.Text == String.Empty || tbWinMatchesHigh.Text == "До" )
                highAge = Int32.MaxValue;

            var result = DBProvider.GetPlayersByAge( highAge, lowAge );

            dgQueryTable.ItemsSource = CheckCondition( result ).Take( rowsCount );

            if( dgQueryTable.Columns.Count != 0 )
                dgQueryTable.Columns.RemoveAt( 0 );

            SetToolTip( "Игроки по возрасту" );
            tbHelp.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Проверить условие связи И, ИЛИ, ТОЛЬКО
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public IEnumerable<PlayerInfo> CheckCondition( IEnumerable<PlayerInfo> result )
        {
            if( rbtnOr.IsChecked == true )
            {
                currentQuery = DBProvider.GetQueryOr( currentQuery, result );
            }
            else
                if( rbtnAnd.IsChecked == true )
                {
                    currentQuery = DBProvider.GetQueryAnd( currentQuery, result );
                }
                else
                {
                    currentQuery = result;
                }
            return currentQuery;
        }

        private void rbtnLastNameSort_Checked( object sender, RoutedEventArgs e )
        {
            if( currentQuery != null )
            {
                dgQueryTable.ItemsSource = null;
                dgQueryTable.ItemsSource = currentQuery.OrderBy( p => p.Фамилия );

                if( dgQueryTable.Columns.Count != 0 )
                    dgQueryTable.Columns.RemoveAt( 0 );
            }
        }

        private void rbtnGoalsSort_Checked( object sender, RoutedEventArgs e )
        {
            int rowsCount;
            Int32.TryParse( tbRowsCount.Text, out rowsCount );
            if( tbRowsCount.Text == String.Empty )
                rowsCount = Int32.MaxValue;

            if( currentQuery != null )
            {
                dgQueryTable.ItemsSource = null;
                dgQueryTable.ItemsSource = currentQuery.OrderByDescending( p => p.ЗабитыеМячи ).Take( rowsCount );

                if( dgQueryTable.Columns.Count != 0 )
                    dgQueryTable.Columns.RemoveAt( 0 );
            }
        }
    }
}
