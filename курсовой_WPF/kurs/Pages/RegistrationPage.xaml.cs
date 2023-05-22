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
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data.Entity;
using kurs.Entities;
using CheckTestLibrary;

namespace kurs.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        int counter = 1;
        private Entities.users _currentUser = null;

        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                if (CheckErrors().Length != 0)
                {

                    MessageBox.Show(CheckErrors());
                    return;
                }
                else
                {
                    var newUser = new Entities.users
                    {
                        login = TBoxLogin.Text.ToString(),
                        password = PBoxPassword.Password.ToString(),
                        id_role = 3,
                    };
                    App.Context.users.Add(newUser);
                    App.Context.SaveChanges();
                    MessageBox.Show("Вы успешно зарегистрировались!");
                    NavigationService.GoBack();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        
        private void PBoxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordCheck = Password.PasswordCheck(PBoxPassword.Password);
            TBoxPasswordCheck.Text = passwordCheck;
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TBoxLogin.Text)) 
                errorBuilder.AppendLine("Поле Логин обязателен к заполнению;");
            if (TBoxLogin.Text.Length < 5)
                errorBuilder.AppendLine("Логин должен иметь длину от 5 символов");
            if (TBoxLogin.Text.Length > 13)
                errorBuilder.AppendLine("Логин должен иметь длину до 13 символов");
            if (PBoxPassword.Password.Length < 8)
                errorBuilder.AppendLine("Пароль должен иметь длину от 8 символов");
            if (PBoxPassword.Password.Length > 16)
                errorBuilder.AppendLine("Пароль должен иметь длину до 16 символов");

            if (PBoxPassword.Password != PBoxPassword1.Password)
                errorBuilder.AppendLine("Пароли должны совпадать");

            if (string.IsNullOrWhiteSpace(PBoxPassword.Password)) 
                errorBuilder.AppendLine("Поле Пароль обязателен к заполнению;");

            if (string.IsNullOrWhiteSpace(PBoxPassword1.Password)) 
                errorBuilder.AppendLine("Поле Повторите Пароль обязателен к заполнению;");

            var userFromDB = App.Context.users.ToList().FirstOrDefault(p => p.login.ToLower() == TBoxLogin.Text.ToLower());
            if (_currentUser != userFromDB && userFromDB != null)
            {
              errorBuilder.AppendLine("Такой пользователь уже есть в базе данных");
            }
            if (_currentUser == null && (TBoxPasswordCheck.Text == "Ненадежный" || TBoxPasswordCheck.Text == "Простой"))
            
               errorBuilder.AppendLine("Пароль должен быть сложнее");

            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n \n");
           
            return errorBuilder.ToString();
        }       
        private void BtnHideReg_Password(object sender, RoutedEventArgs e)
        {
            string glaz = System.AppDomain.CurrentDomain.BaseDirectory;
            string password = "";
            counter++;
            if (counter % 2 == 0)
            {
                image1.ImageSource = new ImageSourceConverter().ConvertFromString(glaz+"/Pictures/glazB.png") as ImageSource;
                password = PBoxPassword.Password;
                PBoxPassword.Visibility = Visibility.Collapsed;
                tBoxPasswordReg.Visibility = Visibility.Visible;
                tBoxPasswordReg.Text = password;
            }
            if (counter % 2 != 0)
            {
                image1.ImageSource = new ImageSourceConverter().ConvertFromString(glaz+"/Pictures/glazBZ.png") as ImageSource;
                password = tBoxPasswordReg.Text;
                PBoxPassword.Visibility = Visibility.Visible;
                tBoxPasswordReg.Visibility = Visibility.Collapsed;
                PBoxPassword.Password = password;
            }
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
