
using Microsoft.Win32;
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
using System.IO;
using CheckTestLibrary;

namespace kurs.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        
        private byte[] _mainImageData;
        public ProfilePage()
        {
            InitializeComponent();
        }
        int counter = 0;

        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image |*.png; *.jpg; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                _mainImageData = File.ReadAllBytes(ofd.FileName);
                ImageAvatar.Source = (ImageSource)new ImageSourceConverter()
                    .ConvertFrom(_mainImageData);
            }
        }
        
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var errorMessage = CheckErrors();
                if (errorMessage.Length > 0)
                {
                    MessageBox.Show(errorMessage, "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    App.CurrentUser.login = TBoxProfileLogin.Text;
                    App.CurrentUser.Name = TBoxProfileName.Text;
                    App.CurrentUser.Surname = TBoxProfileSurname.Text;
                    App.CurrentUser.password = TBoxProfilePassword.Text;
                    App.CurrentUser.photo = _mainImageData;
                    App.Context.SaveChanges();
                    MessageBox.Show("Профиль успешно редактирован!");
                    TBoxProfileLogin.IsReadOnly = true;
                    TBoxProfileName.IsReadOnly = true;
                    TBoxProfileSurname.IsReadOnly = true;
                    TBoxProfilePassword.IsReadOnly = true;
                    BtnSelectImage.Visibility = Visibility.Hidden;
                    BtnProfileDelete.Visibility = Visibility.Hidden;
                    BtnSave.Visibility = Visibility.Hidden;
                    UpdateProfile();
                }
            }
           
             catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void BtnProfileEdit_Click(object sender, RoutedEventArgs e)
        {
            counter++;
            if(counter %2 == 1)
            {
                BtnSave.Visibility = Visibility.Visible;
                TBlockPasswordCheck.Visibility = Visibility.Visible;
                TBoxPasswordCheck.Visibility = Visibility.Visible;
                TBoxProfileLogin.IsReadOnly = false;
                TBoxProfileName.IsReadOnly = false;
                TBoxProfileSurname.IsReadOnly = false;
                TBoxProfilePassword.IsReadOnly = false;
                BtnSelectImage.Visibility = Visibility.Visible;
                BtnProfileDelete.Visibility = Visibility.Visible;
            }
            if(counter %2 == 0)
            {
                BtnSave.Visibility = Visibility.Hidden;
                TBlockPasswordCheck.Visibility = Visibility.Visible;
                TBoxPasswordCheck.Visibility = Visibility.Visible;
                TBoxProfileLogin.IsReadOnly = true;
                TBoxProfileName.IsReadOnly = true;
                TBoxProfileSurname.IsReadOnly = true;
                TBoxProfilePassword.IsReadOnly = true;
                BtnSelectImage.Visibility = Visibility.Hidden;
                BtnProfileDelete.Visibility = Visibility.Hidden;
            }
            
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TBoxProfileName.Text))
                errorBuilder.AppendLine("Введите новое имя;");

            if (string.IsNullOrWhiteSpace(TBoxProfileSurname.Text))
                errorBuilder.AppendLine("Введите новую фамилию;");
        
            if (string.IsNullOrWhiteSpace(TBoxProfileLogin.Text))
                errorBuilder.AppendLine("Поле Логин обязателен к заполнению;");

            if (TBoxProfileLogin.Text.Length < 5)
                errorBuilder.AppendLine("Новый логин должен иметь длину от 5 символов");

            if (TBoxProfileLogin.Text.Length > 13)
                errorBuilder.AppendLine("Новый логин должен иметь длину до 13 символов");

            if (TBoxProfilePassword.Text.Length < 8)
                errorBuilder.AppendLine("Новый пароль должен иметь длину от 8 символов");
            if (TBoxProfilePassword.Text.Length > 16)
                errorBuilder.AppendLine("Пароль должен иметь длину до 16 символов");

            if (TBoxPasswordCheck.Text == "Ненадежный" || TBoxPasswordCheck.Text == "Простой")

                errorBuilder.AppendLine("Пароль должен быть сложнее");

            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки: \n \n");

            return errorBuilder.ToString();    
        }   
        private void UpdateProfile()
        {
            TBoxProfileName.Text = App.CurrentUser.Name;
            TBoxProfileSurname.Text = App.CurrentUser.Surname;
            TBoxProfilePassword.Text = App.CurrentUser.password;
            TBoxProfileLogin.Text = App.CurrentUser.login;
            TBoxProfileName.Text = App.CurrentUser.Name;
            _mainImageData = App.CurrentUser.photo;  
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateProfile();
            if(_mainImageData != null)
            {
              LoadPhoto();
            }           
        }
        private void LoadPhoto()
        {
            Image image = new Image();
            BitmapImage bitmapimage = new BitmapImage();
            MemoryStream stream = new MemoryStream(_mainImageData);
            bitmapimage.BeginInit();
            bitmapimage.StreamSource = stream;
            bitmapimage.EndInit();
            image.Source = bitmapimage;
            ImageAvatar.Source = bitmapimage;    
        }
        private void BtnProfileDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы уверены, что хотите удалить аккаунт", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {    
                    App.Context.users.Remove(App.CurrentUser); 
                    App.Context.SaveChanges();
                    NavigationService.Navigate(new LoginPage());
                    UpdateProfile();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Чтобы удалить профиль, сначала нужно очистить корзину", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void TBoxProfilePassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            var passwordCheck = Password.PasswordCheck(TBoxProfilePassword.Text);
            TBoxPasswordCheck.Text = passwordCheck;
        }
        private void TBoxProfileName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (int.TryParse(e.Text, out int value))
            {
                e.Handled = true;
            }
        }
        private void TBoxProfileSurname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (int.TryParse(e.Text, out int value))
            {
                e.Handled = true;
            }
        }
    }
}
