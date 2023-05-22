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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kurs.Pages
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        int id_user = 0;
        public Menu()
        {
            InitializeComponent();
            
        }
        public Menu(int user_id)
        {
            InitializeComponent();
            id_user = user_id;
            if (App.CurrentUser.id_role == 3)
            {
                BtnToGuitarCart.Visibility = Visibility.Visible;
            }
            else
            {
                BtnToGuitarCart.Visibility = Visibility.Collapsed;
            }
        }
        private void BtnToCatalog_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GuitarPage1(id_user));  
        }
        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
              NavigationService.Navigate(new ProfilePage());
        }
        private void BtnToGuitarCart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GuitarCart());
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }
    }
}
