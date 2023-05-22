using kurs.Entities;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace kurs.Pages
{
    /// <summary>
    /// Логика взаимодействия для GuitarPage.xaml
    /// </summary>
    public partial class GuitarPage1 : Page
    {
        int id_user = 0;
        public GuitarPage1()
        {
            InitializeComponent();
            LViewGuitar.ItemsSource = App.Context.guitar.ToList();
            if (App.CurrentUser.id_role == 1)
            {
                BtnAddGuitar.Visibility = Visibility.Visible;
            }
            else
            {
                BtnAddGuitar.Visibility = Visibility.Collapsed;
            }            
            ComboDiscount.SelectedIndex = 0;
            ComboSortyBy.SelectedIndex = 0;
            UpdateGuitar();
        }
        public GuitarPage1(int user_id)
        {
            InitializeComponent();
            LViewGuitar.ItemsSource = App.Context.guitar.ToList();          
            id_user = user_id;
            if (App.CurrentUser.id_role == 1)
            {
                BtnAddGuitar.Visibility = Visibility.Visible;
            }
            else
            {
                BtnAddGuitar.Visibility = Visibility.Collapsed;
            }
            ComboDiscount.SelectedIndex = 0;
            ComboSortyBy.SelectedIndex = 0;
            UpdateGuitar();
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService.GoBack();
        }
        private void BtnAddGuitar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditPage());
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var currentPastrys = (sender as Button).DataContext as Entities.guitar;
            NavigationService.Navigate(new AddEditPage(currentPastrys));
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentGuitar = (sender as Button).DataContext as Entities.guitar;

            if (MessageBox.Show($"Вы уверены, что хотите удалить товар: " + $"{currentGuitar.Name}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.guitar.Remove(currentGuitar);
                App.Context.SaveChanges();
                UpdateGuitar();
            }
        }
        private void UpdateGuitar()
        {            
            var guitar = App.Context.guitar.ToList();
            if (ComboSortyBy.SelectedIndex == 0)
                guitar = guitar.OrderBy(p => p.TotalCost).ToList();
            if (ComboSortyBy.SelectedIndex == 1)
                guitar = guitar.OrderByDescending(p => p.TotalCost).ToList();
            if (ComboDiscount.SelectedIndex == 1)
                guitar = guitar.Where(p => p.Discount >= 0 && p.Discount < 25).ToList();
            if (ComboDiscount.SelectedIndex == 2)
                guitar = guitar.Where(p => p.Discount >= 25 && p.Discount < 50).ToList();
            if (ComboDiscount.SelectedIndex == 3)
                guitar = guitar.Where(p => p.Discount >= 50 && p.Discount < 75).ToList();
            if (ComboDiscount.SelectedIndex == 4)
                guitar = guitar.Where(p => p.Discount >= 75 && p.Discount < 100).ToList();
            if (ComboType.SelectedIndex == 1)
                guitar = guitar.Where(p => p.guitar_category.id_category == 1).ToList();
            if (ComboType.SelectedIndex == 2)
                guitar = guitar.Where(p => p.guitar_category.id_category == 2).ToList();
            if (ComboType.SelectedIndex == 3)
                guitar = guitar.Where(p => p.guitar_category.id_category == 3).ToList();
            guitar = guitar.OrderBy(p => p.guitar_category.id_category).ToList();
            LViewGuitar.ItemsSource = guitar;
        }       
        private void ComboSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGuitar();
        }
        private void ComboDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGuitar();
        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var search = App.Context.guitar.ToList();
            search = search.Where(p => p.Name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            LViewGuitar.ItemsSource = search;
        }
        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGuitar();
        }
        private void BtnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            var currentGuitar = (sender as Button).DataContext as Entities.guitar;
            var currentGuitarWarehouse = App.Context.guitar_warehouse.FirstOrDefault(p => p.id_guitar == currentGuitar.id_guitar);
            App.CurrentGuitarWarehouse = currentGuitarWarehouse;
            var newUser = new Entities.users_guitar
            {
                id_guitar = currentGuitar.id_guitar,
                id_users = App.CurrentUser.id_users,
                quantity = 1,
                Image = currentGuitar.Image
            };
            var guitarWarehouse = App.Context.guitar_warehouse.ToList();
            foreach (var item in guitarWarehouse)
            {   
                if (item.id_guitar == currentGuitar.id_guitar)
                {
                    if (App.CurrentGuitarWarehouse.amount == 0)
                    {
                        MessageBox.Show("Этот товар на складке закончился");                       
                        return;
                    }
                    else   App.CurrentGuitarWarehouse.amount -= 1;                    
                }                     
            }           
            App.Context.users_guitar.Add(newUser);
            App.Context.SaveChanges();
            MessageBox.Show("Товар успешно добавлен в корзину");
            UpdateGuitar();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateGuitar();
        }
    }
}
