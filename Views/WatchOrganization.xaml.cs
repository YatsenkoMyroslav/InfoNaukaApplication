using InfoNaukaApplication.Models;
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

namespace InfoNaukaApplication.Views
{
    /// <summary>
    /// Логика взаимодействия для WatchOrganization.xaml
    /// </summary>
    public partial class WatchOrganization : Page
    {
        ActivitiesAssociationEntities context;
        public WatchOrganization()
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
            var input = Input.Text;

            using (var context = new ActivitiesAssociationEntities())//виконує пошук по бд
                dataGrid.ItemsSource = context.Organizations.Where(org => org.name_org.Contains(input)).ToArray();

            dataGrid.Items.Refresh();//оновлює таблицю
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) //виконується при завантажені сторінки
        {
            using (var context = new ActivitiesAssociationEntities())//завантажує дані з бд до таблиці
                dataGrid.ItemsSource = context.Organizations.ToArray();
        }
    }
}
