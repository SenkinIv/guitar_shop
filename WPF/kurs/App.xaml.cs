using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace kurs
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Entities.GuitarShop Context
        { get; } = new Entities.GuitarShop();

        public static Entities.users CurrentUser = null;
        public static Entities.users_guitar CurrentUserGuitar = null;
        public static Entities.guitar_warehouse CurrentGuitarWarehouse = null;
        public static Entities.guitar CurrentGuitar = null;
    }
}
