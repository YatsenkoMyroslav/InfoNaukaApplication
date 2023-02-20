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
    /// Логика взаимодействия для WatchScience.xaml
    /// </summary>
    public partial class WatchScience : Page
    {
        ActivitiesAssociationEntities context;

        public WatchScience()
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
            var input = Input.Text;//отримує текст з вікна пошуку

            using (var context = new ActivitiesAssociationEntities())
                dataGrid.ItemsSource = context.Classifier.Where(org => org.name_direction.Contains(input)).ToArray();//виконує пошук по БД

            dataGrid.Items.Refresh();//оновлює таблицю даних
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)//виконується при завантажені сторінки
        {
            using (var context = new ActivitiesAssociationEntities())
                dataGrid.ItemsSource = context.Classifier.ToArray();//встановлює дані для таблиці
        }
    }
}
