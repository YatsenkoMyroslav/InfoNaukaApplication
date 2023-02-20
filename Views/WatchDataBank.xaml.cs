using InfoNaukaApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace InfoNaukaApplication.Views
{
    /// <summary>
    /// Логика взаимодействия для WatchDataBank.xaml
    /// </summary>
    public partial class WatchDataBank : Page
    {
        
        public WatchDataBank()
        {
            InitializeComponent();
            Background = new SolidColorBrush(Color.FromRgb(222, 247, 254));
        }
        private void OpenPreviousPage(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            NavigationService.Navigate(menu);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            var input = Input.Text;//отримує введений текст

            using (var context = new ActivitiesAssociationEntities()) 
                dataGrid.ItemsSource = context.DataBankView.Where(r => r.NameBank.Contains(input)).ToArray();//виконує пошук бо БД за введеним текстом по полю назви банку у вікні пошуку
            

            dataGrid.Items.Refresh(); //оновлює datagrid (дані у вікні)
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)//виконується після завантаження вікна
        {
            using (var context = new ActivitiesAssociationEntities()) 
                dataGrid.ItemsSource = context.DataBankView.ToArray(); //заповнює datagrid даними з БД
            
        }
    }
}
