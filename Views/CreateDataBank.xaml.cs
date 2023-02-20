using InfoNaukaApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для CreateDataBank.xaml
    /// </summary>
    public partial class CreateDataBank : Page
    {
        public CreateDataBank()//ініціалізація сторінки та встановлення кольору фону
        {
            InitializeComponent();
            Background = new SolidColorBrush(Color.FromRgb(222, 247, 254));
        }

        private void OpenPreviousPage(object sender, RoutedEventArgs e)//виконує перехід на минулу сторінку (виклик виконується кнопкою стрілочкою)
        {
            Menu menu = new Menu();
            NavigationService.Navigate(menu);
        }

        private bool IsDataCorrect()//перевіряє валдність даних та виводить помилку за потребі
        {
            if (String.IsNullOrEmpty(NameOrg.Text) || String.IsNullOrEmpty(Responsible.Text) ||
                String.IsNullOrEmpty(Direction.Text) || String.IsNullOrEmpty(BankName.Text))
            {
                ResultMessage.Foreground = new SolidColorBrush(Colors.Red);
                ResultMessage.Text = "Заповніть порожні поля";
                return false;
            }

            return true;
        }

        private void Create(object sender, RoutedEventArgs e)//алгоритм додавання запису до бд
        {
            if (IsDataCorrect())
            {
                using (var context = new ActivitiesAssociationEntities())
                {
                    var organization = context.Organizations.FirstOrDefault(o => o.name_org == NameOrg.Text); //отримує об'єкт організації з бд за назвою
                    var direct = context.Classifier.FirstOrDefault(c => c.name_direction == Direction.Text);//отримує об'єкт напрямку з бд за назвою

                    if (organization == null)//якщо  немає такої то помилка і вихід з методу
                    {
                        ResultMessage.Foreground = new SolidColorBrush(Colors.Red);
                        ResultMessage.Text = "Некоректна назва організації, операцію не виконано";//встановлює повідомлення
                    }
                    else if (direct == null)//якщо  немає такої то помилка і вихід з методу
                    {
                        ResultMessage.Foreground = new SolidColorBrush(Colors.Red);
                        ResultMessage.Text = "Некоректна назва напряму, операцію не виконано";
                    }
                    else
                    {
                        var dataBank = new DataBank //у випадку повної валідносі даних створює новий об'єкт з отриманими даними
                        {
                            id_bank_direct = direct.id,
                            id_bank_owner = organization.id_org,
                            name_bank = BankName.Text,
                            name_respons = Responsible.Text
                        };

                        context.DataBank.Add(dataBank); //додає до таблиці

                        context.SaveChanges(); //виконує зберігання в БД

                        ResultMessage.Foreground = new SolidColorBrush(Colors.BlueViolet);
                        ResultMessage.Text = "Успішно додано";
                    }
                }
            }
        }
    }
}
