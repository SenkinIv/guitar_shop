
using kurs.Entities;
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
    /// Логика взаимодействия для GuitarCart.xaml
    /// </summary>
    public partial class GuitarCart : Page
    {
        double totalCost = 0;
        public GuitarCart()
        {
            InitializeComponent();
            UpdateGuitar();

        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentGuitar = (sender as Button).DataContext as Entities.users_guitar;

            if (MessageBox.Show($"Вы уверены, что хотите удалить товар из корзины: " + $"{currentGuitar.guitar.Name}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.users_guitar.Remove(currentGuitar);
                App.Context.SaveChanges();
                UpdateGuitar();
                NavigationService.Navigate(new GuitarCart());   
            }            
        }      
        private void UpdateGuitar()
        {
            var guitarItem = App.Context.users_guitar.Where(c=>c.id_users == App.CurrentUser.id_users).ToList();           
            LViewCart.ItemsSource = guitarItem;
            var total = App.Context.users_guitar.Where(c=>c.id_users ==App.CurrentUser.id_users).ToList();
            foreach(var item in total)
            {
                totalCost += item.CostWithDiscount;
            }
            TotalCartCost.Text = "Итого: " + totalCost.ToString() + " Рублей";
        }       
        private void BtnCalculationOrder_Click(object sender, RoutedEventArgs e)
        {

        }       
        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {               
            NavigationService.Navigate(new Pages.PrintPage(totalCost.ToString()));
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
