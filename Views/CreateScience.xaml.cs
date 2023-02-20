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
    /// Логика взаимодействия для CreateScience.xaml
    /// </summary>
    public partial class CreateScience : Page
    {
        public CreateScience()
        {
            InitializeComponent();
            Background = new SolidColorBrush(Color.FromRgb(222, 247, 254));
        }

        private void OpenPreviousPage(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            NavigationService.Navigate(menu);
        }

        private bool IsDataCorrect() {//валідація даних
            if (String.IsNullOrEmpty(ScienceName.Text)) {
                ResultMessage.Foreground = new SolidColorBrush(Colors.Red);
                ResultMessage.Text = "Введіть назву";
                return false;
            }

            return true;
        }

        private void Create(object sender, RoutedEventArgs e)//додає новий напрям вивчення 
        {
            if(IsDataCorrect())//якщо валідні дані то додає
            {
                Classifier classifier = new Classifier();//створює новий об'єкт

                classifier.name_direction = ScienceName.Text;

                using(var context = new ActivitiesAssociationEntities())
                {
                    context.Classifier.Add(classifier);//додає до таблиці

                    context.SaveChanges();//зберігає дані
                }

                ResultMessage.Foreground = new SolidColorBrush(Colors.BlueViolet);
                ResultMessage.Text = "Успішно додано";
            }
        }
    }
}
