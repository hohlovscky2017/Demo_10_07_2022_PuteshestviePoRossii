using System;
using System.Collections.Generic;
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

namespace Demo_10_07_2022_PuteshestviePoRossii.Windows
{
    /// <summary>
    /// Логика взаимодействия для HotelWindow.xaml
    /// </summary>
    public partial class HotelWindow : Window
    {
        public static Demo_10_07_2020Entities _context = new Demo_10_07_2020Entities();
        private Hotel _hotel;
        private int _currentPage = 1;
        private int _maxPage = 0;
        private int _period = 14;
        public HotelWindow()
        {
            InitializeComponent();

            RefreshHotels();
        }

        public void RefreshHotels()
        {
            DataGridHotels.ItemsSource = _context.Hotel.OrderBy(h => h.Name).ToList();
            _maxPage = Convert.ToInt32(Math.Ceiling(_context.Hotel.ToList().Count * 1.0 / _period));

            var listHotels = _context.Hotel.ToList().Skip((_currentPage - 1) * _period).Take(_period).ToList();

            LblTotalPages.Content = "of " + _maxPage.ToString();
            TxtCurrentPageNumber.Text = _currentPage.ToString();
            DataGridHotels.ItemsSource = listHotels;
        }

        private void BtnEditHotelInfo_Click(object sender, RoutedEventArgs e)
        {
            EditHotelInfoWindow editHotelInfoWindow = new EditHotelInfoWindow(_context, sender, this);
            editHotelInfoWindow.ShowDialog();
        }
        /// <summary>
        /// Переход к первой странице
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoFirstPageButton_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            RefreshHotels();
        }
        /// <summary>
        /// Переход к предыдущей странице
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoPrevPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage - 1 < 1)
            {
                return;
            }
            _currentPage--;
            RefreshHotels();
        }
        /// <summary>
        /// Изменение номера текущей страницы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCurrentPageNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_currentPage > 0 && _currentPage <= _maxPage && TxtCurrentPageNumber.Text != "")
            {
                _currentPage = Convert.ToInt32(TxtCurrentPageNumber.Text);
                RefreshHotels();
            }
        }
        /// <summary>
        /// Переход к следующей странице
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoNextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage + 1 > _maxPage)
            {
                return;
            }
            _currentPage++;
            RefreshHotels();
        }
        /// <summary>
        /// Переход к последней странице
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoLastPageButton_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = _maxPage;
            RefreshHotels();
        }

        private void BtnAddHotel_Click(object sender, RoutedEventArgs e)
        {
            AddHotelWindow addHotelWindow = new AddHotelWindow(_context, this);
            addHotelWindow.ShowDialog();
        }
    }
}
