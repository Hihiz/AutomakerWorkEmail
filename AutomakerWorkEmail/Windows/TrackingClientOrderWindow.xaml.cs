using AutomakerWorkEmail.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace AutomakerWorkEmail.Windows
{
    public partial class TrackingClientOrderWindow : Window
    {
        public TrackingClientOrderWindow()
        {
            InitializeComponent();

            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                gridClientOrder.ItemsSource = db.ClientOrders.Include(s => s.Service).Include(c => c.Client).ToList();
            }
        }

        private void ButtonHello_Click(object sender, RoutedEventArgs e)
        {


        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonCloseOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("На кнопку сделать закрытие заказа(удаление) с условие что работник введет код получения клиента");
        }
    }
}
