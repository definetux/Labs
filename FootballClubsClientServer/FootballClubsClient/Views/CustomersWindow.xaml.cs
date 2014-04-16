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
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class CustomersWindow : Window
    {        
        private int clubId;
        
        /// <summary>
        /// ID выбранной записи
        /// </summary>
        public int SelectedId { get; set; }

        /// <summary>
        /// Инициализация элементом управления
        /// </summary>
        public CustomersWindow()
        {
            InitializeComponent();
            clubId = 0;
        }

        /// <summary>
        /// Показать форму в виде модального окна, присоединить к таблице отобранные записи
        /// </summary>
        /// <param name="clubId"> ID клуба </param>
        public void ShowDialog(int clubId)
        {       
            this.clubId = clubId;
            staffsDataGrid.ItemsSource = DBProvider.GetStaffsByClubId(clubId);

            base.ShowDialog();
        }

        /// <summary>
        /// Открыть форму добавление сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddStaffWindow addStaffWindow = new AddStaffWindow();
            addStaffWindow.ShowDialog(clubId);
            staffsDataGrid.ItemsSource = DBProvider.GetStaffsByClubId(clubId);
        }

        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var items = DBProvider.GetStaffsById( SelectedId );

            DBProvider.DeleteObject( items );

            staffsDataGrid.ItemsSource = DBProvider.GetStaffsByClubId(clubId);
        }

        /// <summary>
        /// Изменить выбранный ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void staffsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (staffsDataGrid.SelectedItem is NewStaff)
                this.SelectedId = ((NewStaff)staffsDataGrid.SelectedItem).StaffID;
        }

        /// <summary>
        /// Обновить строку в БД 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //if (staffsDataGrid.SelectedItem is Staff)
            //    this.SelectedId = ((Staff)staffsDataGrid.SelectedItem).StaffID;

            //var entities = EntitiesController.Entities;

            //var oldStaff = entities.Staffs.Single(p => p.StaffID == SelectedId);

            //oldStaff.LastName = ((Staff)staffsDataGrid.SelectedItem).LastName;
            //oldStaff.FirstName = ((Staff)staffsDataGrid.SelectedItem).FirstName;
            //oldStaff.Patronymic = ((Staff)staffsDataGrid.SelectedItem).Patronymic;
            //oldStaff.Post = ((Staff)staffsDataGrid.SelectedItem).Post;
            //oldStaff.Experience = ((Staff)staffsDataGrid.SelectedItem).Experience;

            //EntitiesController.Save(entities);
        }
    }
}
