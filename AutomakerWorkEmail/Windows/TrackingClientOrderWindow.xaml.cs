using AutomakerWorkEmail.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
                textBlockCountClientOrder.Text = $"Количество Активных закзов: {db.ClientOrders.Where(s => s.Status != "Выдан").Count()}";
                textBlockCountCloseClientOrder.Text = $"Количество Закрытых закзов: {db.ClientOrders.Where(s => s.Status == "Выдан").Count()}";

                if (currentWorker.RoleId == 1)
                    menuItemAdmin.Visibility = Visibility.Visible;
                else
                    menuItemAdmin.Visibility = Visibility.Collapsed;

                gridCloseOrder.ItemsSource = db.ClientOrders.Include(s => s.Service).Include(c => c.Client).Where(s => s.Status == "Выдан").ToList();
                gridClientOrder.ItemsSource = db.ClientOrders.Include(s => s.Service).Include(c => c.Client).Where(s => s.Status != "Выдан").ToList();
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
        }

        private void EditClientOrder_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (currentWorker.RoleId == 1)
            {
                ClientOrder currentClientOrder = gridClientOrder.SelectedItem as ClientOrder;

                new AddEditClientOrderWindow(currentClientOrder).ShowDialog();
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

        private void SearchTrackNumber_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            SearchClientOrder();
        }

        private void SearchLastName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            SearchClientOrder();
        }

        private void SearchClientOrder()
        {
            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                List<ClientOrder> clientOrder = db.ClientOrders.Include(c => c.Client).Include(s => s.Service).ToList();
                gridClientOrder.ItemsSource = clientOrder;

                if (textBoxSearchTrackNumber.Text.Length > 0)
                    clientOrder = clientOrder.Where(t => t.TrackNumber.Contains(textBoxSearchTrackNumber.Text)).ToList();

                if (textBoxSearchLastName.Text.Length > 0)
                    clientOrder = clientOrder.Where(l => l.Client.LastName.Contains(textBoxSearchLastName.Text)).ToList();

                gridClientOrder.ItemsSource = clientOrder;
                textBlockCountClientOrder.Text = $"Количество: {clientOrder.Count()} из {db.ClientOrders.ToList().Count}";
            }
        }
    }
}
