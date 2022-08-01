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
    /// Логика взаимодействия для AddHotelWindow.xaml
    /// </summary>
    public partial class AddHotelWindow : Window
    {
        private Demo_10_07_2020Entities _context;
        private HotelWindow _window;
        public AddHotelWindow(Demo_10_07_2020Entities demo_10_07_2020Entities, HotelWindow hotelWindow)
        {
            InitializeComponent();
            this._context = demo_10_07_2020Entities;
            this._window = hotelWindow;

            CmbNameCountry.ItemsSource = _context.Country.ToList();
        }

        private void BtnAddHotel_Click(object sender, RoutedEventArgs e)
        {
            _context.Hotel.Add(new Hotel() {
                Name = TxtNameHotel.Text,
                CountOfStars = Convert.ToInt32(TxtCountStars.Text),
                Description = TxtDescriptionHotel.Text,
                Country = CmbNameCountry.SelectedItem as Country
            });
            _context.SaveChanges();
            _window.RefreshHotels();

            this.Close();
        }
    }
}
