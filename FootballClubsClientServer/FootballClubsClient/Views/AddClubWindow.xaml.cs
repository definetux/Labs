using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace FootballClubsClient
{
    /// <summary>
    /// Interaction logic for AddClubWindow.xaml
    /// </summary>
    public partial class AddClubWindow : Window
    {
        private byte[] logo;
        private byte[] anthem;

        /// <summary>
        /// Инициализация элементов окна
        /// </summary>
        public AddClubWindow()
        {
            InitializeComponent();
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

        /// <summary>
        /// Добавить запись
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            NewClub newCLub = new NewClub();
            newCLub.Name = txtClubName.Text;
            newCLub.City = txtCity.Text;

            int numberOfMatches;
            int.TryParse(txtPlayed.Text, out numberOfMatches);
            

            int numberOfWinning;
            int.TryParse(txtWin.Text, out numberOfWinning);

            newCLub.NumberOfMatches = numberOfMatches;
            if (numberOfMatches < numberOfWinning)
            {
                MessageBox.Show("Количество игр не может быть меньше количества побед!");
                return;
            }
            else
                newCLub.WinningMatches = numberOfWinning;

            DBProvider.AddObject(newCLub);

            this.Close();    
        }

        /// <summary>
        /// Найти изображение на локальном диске
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Найти аудио на локальном диске
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenAudio_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Загрузить файл
        /// </summary>
        /// <param name="fileName"> Имя файла </param>
        /// <returns> Данные в виде массива байт </returns>
        private byte[] LoadFile(string fileName)
        {
            try
            {
                FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                return data;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message);
                return null;
            }
        }
    }
}