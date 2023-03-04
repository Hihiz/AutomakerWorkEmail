using AutomakerWorkEmail.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace AutomakerWorkEmail.Windows
{
    public partial class TrackingClientOrderWindow : Window
    {
        Worker currentWorker;

        public TrackingClientOrderWindow(Worker worker)
        {
            InitializeComponent();

            currentWorker = worker;

            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                gridClientOrder.ItemsSource = db.ClientOrders.Include(s => s.Service).Include(c => c.Client).ToList();
                textBlockCountClientOrder.Text = $"Количество: {db.ClientOrders.Count()}";

                if (currentWorker.RoleId == 1)
                    menuItemAdmin.Visibility = Visibility.Visible;
                else
                    menuItemAdmin.Visibility = Visibility.Collapsed;
            }

            textBlockStatusWorker.Text = $"Вы вошли как: {worker.FirstName} {worker.LastName} {worker.Patronymic} | {worker.Role.Name}";
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonCloseOrder_Click(object sender, RoutedEventArgs e)
        {
            ClientOrder currentClientOrder = gridClientOrder.SelectedItem as ClientOrder;

            new CodeClientWindow(currentClientOrder).ShowDialog();

            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                gridClientOrder.ItemsSource = db.ClientOrders.Include(s => s.Service).Include(c => c.Client).ToList();
                textBlockCountClientOrder.Text = $"Количество: {db.ClientOrders.Count()}";
            }
        }

        private void EditClientOrder_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (currentWorker.RoleId == 1)
            {
                ClientOrder currentClientOrder = gridClientOrder.SelectedItem as ClientOrder;

                new AddEditClientOrderWindow(currentClientOrder).ShowDialog();
            }

            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                gridClientOrder.ItemsSource = db.ClientOrders.Include(s => s.Service).Include(c => c.Client).ToList();
                textBlockCountClientOrder.Text = $"Количество: {db.ClientOrders.Count()}";
            }
        }

        private void ButtonAddOrder_Click(object sender, RoutedEventArgs e)
        {
            new AddEditClientOrderWindow(null).ShowDialog();
        }

        private void MenuItemDataWorker_Click(object sender, RoutedEventArgs e)
        {
            new WorkerWindow(currentWorker).Show();
            Close();
        }

        private void MenuItemServiceWindow_Click(object sender, RoutedEventArgs e)
        {
            new ServiceWindow(currentWorker).Show();
            Close();
        }

        private void ButtonExitRole_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            Close();
        }
    }
}
