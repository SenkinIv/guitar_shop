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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        int counter = 1;
        public LoginPage()
        {
            InitializeComponent();
        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentUser = App.Context.users
                                .FirstOrDefault(p => p.login == TBoxLogin.Text && (p.password == PBoxPassword.Password || p.password == tBoxPassword.Text));

                if (currentUser != null)
                {

                    App.CurrentUser = currentUser;
                    NavigationService.Navigate(new Menu(currentUser.id_users));
                }
                else
                {
                    MessageBox.Show("Пользователь с такими данными не найден.", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Отсутствует подключение к базе данных", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
     
        private void BtnHide_Password(object sender, RoutedEventArgs e)
        
        {
            string glaz = System.AppDomain.CurrentDomain.BaseDirectory;
            string password = "";
            counter++;
            if (counter %2 == 0)
            {
                image1.ImageSource = new ImageSourceConverter().ConvertFromString(glaz+"\\Pictures\\glazB.png") as ImageSource;
                password = PBoxPassword.Password;
                PBoxPassword.Visibility = Visibility.Collapsed;
                tBoxPassword.Visibility = Visibility.Visible;
                tBoxPassword.Text = password;
            }
            if (counter %2 != 0)
            {
                 image1.ImageSource = new ImageSourceConverter().ConvertFromString(glaz+"\\Pictures\\glazBZ.png") as ImageSource;
                 password = tBoxPassword.Text;
                 PBoxPassword.Visibility = Visibility.Visible;
                 tBoxPassword.Visibility = Visibility.Collapsed;
                 PBoxPassword.Password = password;
            }
        }
        private void BtnLogin_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }
    }
}
