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
    /// Логика взаимодействия для CreateOrganization.xaml
    /// </summary>
    public partial class CreateOrganization : Page
    {
        public CreateOrganization()
        {
            InitializeComponent();
            Background = new SolidColorBrush(Color.FromRgb(222, 247, 254));
        }
        
        private bool IsDataCorrect()//перевіряє валдність даних та виводить помилку за потребі
        {
            if (String.IsNullOrEmpty(NameOrg.Text) || String.IsNullOrEmpty(LeaderName.Text) || 
                String.IsNullOrEmpty(Address.Text) || String.IsNullOrEmpty(Accountr.Text) || 
                String.IsNullOrEmpty(AccountantName.Text))
            {
                ResultMessage.Foreground = new SolidColorBrush(Colors.Red);
                ResultMessage.Text = "Заповніть порожні поля";
                return false;
            }

            return true;
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            if (IsDataCorrect()) //у випадку валідності даних допускається створення та додавання нового об'єкту
            {
                var organization = new Organizations //новий об'єкт який потім додається до БД
                {
                    name_org = NameOrg.Text,
                    address = Address.Text,
                    accountr = Accountr.Text,
                    fullname_leader = LeaderName.Text,
                    fullname_accountant = AccountantName.Text
                }; 

                using (var context = new ActivitiesAssociationEntities())// у конструкції using створюється об'єкт контексту БД до нього додається зміна та зберігаються зміни
                {
                    context.Organizations.Add(organization);

                    context.SaveChanges();
                }

                ResultMessage.Foreground = new SolidColorBrush(Colors.BlueViolet);
                ResultMessage.Text = "Успішно додано";
            }
        }

        private void OpenPreviousPage(object sender, RoutedEventArgs e)//перехід на головну сторінку (вона завжди попередня тут)
        {
            Menu menu = new Menu();
            NavigationService.Navigate(menu);
        }
    }
}
