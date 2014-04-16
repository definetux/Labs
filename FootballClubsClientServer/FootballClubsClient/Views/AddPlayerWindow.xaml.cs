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
    /// Interaction logic for AddPlayerWindow.xaml
    /// </summary>
    public partial class AddPlayerWindow : Window
    {
        private int clubId;

        /// <summary>
        /// Инициализация элементов управления
        /// </summary>
        public AddPlayerWindow()
        {
            InitializeComponent();
            clubId = 0;
        }

        /// <summary>
        /// Открыть форму как модальное окно
        /// </summary>
        /// <param name="clubId"></param>
        public void ShowDialog(int clubId)
        {
            this.clubId = clubId;
            base.ShowDialog();
        }

        /// <summary>
        /// Добавить запись
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            NewPlayer newPlayer = new NewPlayer();
            newPlayer.LastName = txtLastName.Text;
            newPlayer.FirstName = txtFirstName.Text;
            newPlayer.Patronymic = txtPatronymic.Text;

            if (dpBirthdate.Text != String.Empty) 
                newPlayer.Birthdate = DateTime.Parse(dpBirthdate.Text);

            int number = 0;
            int.TryParse(txtNumber.Text, out number);
            newPlayer.Number = number;

            newPlayer.Position = txtPosition.Text;
            newPlayer.ClubID = this.clubId;

            int goals = 0;
            int.TryParse(txtGoals.Text, out goals);
            newPlayer.Goals = goals;

            DBProvider.AddObject(newPlayer);

            this.Close();    
        }

        /// <summary>
        /// Закрыть форму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
