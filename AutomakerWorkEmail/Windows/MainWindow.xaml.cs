using AutomakerWorkEmail.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace AutomakerWorkEmail.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
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
    }
}
