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
    /// Interaction logic for AddStaffWindow.xaml
    /// </summary>
    public partial class AddStaffWindow : Window
    {
        private int clubId;

        /// <summary>
        /// Инициализация элементов управления
        /// </summary>
        public AddStaffWindow()
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
            NewStaff newStaff = new NewStaff();
            newStaff.LastName = txtLastName.Text;
            newStaff.FirstName = txtFirstName.Text;
            newStaff.Patronymic = txtPatronymic.Text;
            newStaff.Post = txtPost.Text;
            newStaff.ClubID = this.clubId;

            int experience = 0;
            int.TryParse(txtExperience.Text, out experience);
            newStaff.Experience = experience;

            DBProvider.AddObject(newStaff);

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
