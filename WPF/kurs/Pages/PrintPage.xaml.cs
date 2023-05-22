using kurs.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
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
    /// Логика взаимодействия для PrintPage.xaml
    /// </summary>
    public partial class PrintPage : Page
    {
        public PrintPage(string price)
        {
            InitializeComponent();
            TBoxTotalCost.Text = $"{price:N2}" + " Рублей";           
        }
        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            BtnBack.Visibility = Visibility.Hidden;
            BtnPrint.Visibility = Visibility.Hidden;
            Print(this, Convert.ToString(TBoxName.ContentEnd));
        }
        public static void Print(Visual elementToPrint, string description)
        {
            using (var printServer = new LocalPrintServer())
            {
                var dialog = new PrintDialog();
                var qs = printServer.GetPrintQueues();
                dialog.PrintTicket.PageOrientation = PageOrientation.Portrait;
                dialog.PrintVisual(elementToPrint, description);
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {           
            TBoxName.Text = App.CurrentUser.Name;
            TBoxSurname.Text = App.CurrentUser.Surname; 
            var sales = App.Context.users_guitar.Where(c => c.id_users == App.CurrentUser.id_users).ToList();
                LViewPrint.ItemsSource = sales;                    
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }    
}
