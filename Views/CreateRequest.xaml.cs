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
    /// Логика взаимодействия для CreateRequest.xaml
    /// </summary>
    public partial class CreateRequest : Page
    {

        private int deadline;
        public CreateRequest()
        {
            InitializeComponent();
            Background = new SolidColorBrush(Color.FromRgb(222, 247, 254));
        }

        private void OpenPreviousPage(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            NavigationService.Navigate(menu);
        }


        private bool IsDataCorrect()//валідація даних
        {
            if (String.IsNullOrEmpty(NameOrg.Text) || String.IsNullOrEmpty(BankName.Text) ||
                String.IsNullOrEmpty(Address.Text) || String.IsNullOrEmpty(Deadline.Text) ||
                String.IsNullOrEmpty(Direction.Text))
            {
                ResultMessage.Foreground = new SolidColorBrush(Colors.Red);
                ResultMessage.Text = "Заповніть порожні поля";
                return false;
            } else if (!Int32.TryParse(Deadline.Text, out deadline)) {
                ResultMessage.Foreground = new SolidColorBrush(Colors.Red);
                ResultMessage.Text = "Встановіть дедлайн коректно";
                return false;
            }

            return true;
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            if (IsDataCorrect())
            {
                using (var context = new ActivitiesAssociationEntities())
                {
                    var bank = context.DataBank.FirstOrDefault(b => b.name_bank == BankName.Text);//отримуємо об'єкт банку з бд його ID буде ключем
                    var direct = context.Classifier.FirstOrDefault(c => c.name_direction == Direction.Text);//отримуємо об'єкт напрямку з бд його ID буде ключем

                    if (bank == null)//перевірка що такие об'єкт існує
                    {
                        ResultMessage.Foreground = new SolidColorBrush(Colors.Red);
                        ResultMessage.Text = "Некоректна назва банку, операцію не виконано";
                    }
                    else if (direct == null)//перевірка що такие об'єкт існує
                    {
                        ResultMessage.Foreground = new SolidColorBrush(Colors.Red);
                        ResultMessage.Text = "Некоректна назва напряму, операцію не виконано";
                    }
                    else
                    {
                        var application = new Applications
                        {
                            deadline_days = deadline,
                            address_customer = Address.Text,
                            name_customer = NameOrg.Text,
                            id_bank_applic = bank.id_bank,
                            id_direct = direct.id
                        };


                        context.Applications.Add(application);//додає об'єкт до таблиці

                        context.SaveChanges();//зберігає зміни

                        ResultMessage.Foreground = new SolidColorBrush(Colors.BlueViolet);
                        ResultMessage.Text = "Успішно додано";
                    }
                }
            }
        }
    }
}
