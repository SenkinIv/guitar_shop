using kurs.Entities;
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

namespace kurs.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Entities.guitar _currentGuitar = null;
        private byte[] _mainImageData;

        public AddEditPage()
        {

            InitializeComponent();
            
        }

        public AddEditPage(Entities.guitar guitar)
        {
            
            InitializeComponent();
            int typeId = 0;
            var findGuitarType = App.Context.guitar_category.ToList();
            foreach (var findType in findGuitarType)
            {
                if (findType.Name == ComboType.Text)
                    typeId = findType.id_category;
            }
            _currentGuitar = guitar;
            Title = "Редактировать товар";
            typeId = (int)_currentGuitar.id_category;
            TBoxTitle.Text = _currentGuitar.Name;
            TBoxCost.Text = _currentGuitar.Cost.ToString();
            TBoxDiscount.Text = _currentGuitar.Discount.ToString();
            if (_currentGuitar.Image != null)
            {
                ImageGuitar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_currentGuitar.Image);
            }
        }    

        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image |*.png; *.jpg; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                _mainImageData = File.ReadAllBytes(ofd.FileName);
                ImageGuitar.Source = (ImageSource)new ImageSourceConverter()
                    .ConvertFrom(_mainImageData);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {   int typeId = 0;
            var errorMessage = CheckErrors();
            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else

            {
                var findGuitarType = App.Context.guitar_category.ToList();
                foreach (var findType in findGuitarType)
                {
                    if (findType.Name == ComboType.Text)
                        typeId = findType.id_category;
                }
                if (_currentGuitar == null)
                {                         
                    var guitars = new Entities.guitar
                    {
                        Name = TBoxTitle.Text,
                        Cost = int.Parse(TBoxCost.Text),
                        Discount = int.Parse(TBoxDiscount.Text),
                        id_category = typeId,
                        Image = _mainImageData
                    };
                    App.Context.guitar.Add(guitars);
                    App.Context.SaveChanges();
                    MessageBox.Show("Запись успешно добавленна!");
                }
                else
                {
                    _currentGuitar.Name = TBoxTitle.Text;
                    _currentGuitar.Cost = int.Parse(TBoxCost.Text);
                    _currentGuitar.Discount = int.Parse(TBoxDiscount.Text);
                    _currentGuitar.id_category = typeId;
                    if(_mainImageData == null)
                    { 
                        _currentGuitar.Image = _currentGuitar.Image; 
                    }
                    else
                    {
                        _currentGuitar.Image = _mainImageData;
                    }                   
                    App.Context.SaveChanges();
                    MessageBox.Show("Запись успешно редактированна!");
                }
                NavigationService.GoBack();
            }
        }
       
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TBoxTitle.Text))
               
                errorBuilder.AppendLine("Название гитары обязательно для заполнения;");

            if (_currentGuitar == null)
            { 
            if (_mainImageData == null)
                errorBuilder.AppendLine("Выбор картинки обязателен для заполнения");
            }

            if (_currentGuitar == null)
            {
                if (ComboType.Text != "Электрогитара" && ComboType.Text != "Бас-гитара" && ComboType.Text != "Акустические гитары")
                    errorBuilder.AppendLine("Категория гитары обязательно для заполнения;");
            }
            if (_currentGuitar != null)
            {
                if (ComboType.Text != "Электрогитара" && ComboType.Text != "Бас-гитара" && ComboType.Text != "Акустические гитары")
                    errorBuilder.AppendLine("Подтверждение категории гитары обязательно;");
            }

            if (string.IsNullOrWhiteSpace(TBoxCost.Text))
                errorBuilder.AppendLine("Поле цены не должно быть пустым;");

            var serviceFromDB = App.Context.guitar.ToList()
                .FirstOrDefault(p => p.Name.ToLower() == TBoxTitle.Text.ToLower());
            if (serviceFromDB != null && serviceFromDB != _currentGuitar)
                errorBuilder.AppendLine("Такая гитара уже есть в базе данных;");

            decimal cost = 0;
            if (decimal.TryParse(TBoxCost.Text, out cost) == false || cost <= 0)
                errorBuilder.AppendLine("Стоимость услуги должна быть положительным целым  числом;");

           

            if (!string.IsNullOrEmpty(TBoxDiscount.Text))
            {
                int discount = 0;
                if (int.TryParse(TBoxDiscount.Text, out discount) == false
                    || discount < 0 || discount > 100)
                {
                    errorBuilder.AppendLine("Размер скидки  должен быть целым числом в диапазоне от 0 до 100;");
                }
            }

            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки: \n");

            return errorBuilder.ToString();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var guitarTypes = App.Context.guitar_category.ToList();
            foreach(var guitarType in guitarTypes)
            {
                ComboType.Items.Add(guitarType.Name);
            }

            int typeId = 0;
            var findGuitarType = App.Context.guitar_category.ToList();
            foreach (var findType in findGuitarType)
            {
                if (findType.Name == ComboType.Text)
                    typeId = findType.id_category;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
             NavigationService.GoBack();
        }
    }
    
}
