using AutomakerWorkEmail.Models;
using System.Linq;
using System.Windows;

namespace AutomakerWorkEmail.Windows
{
    public partial class ServiceWindow : Window
    {
        Worker currentWorker;

        public ServiceWindow(Worker worker)
        {
            InitializeComponent();

            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                gridService.ItemsSource = db.Services.ToList();
                textBlockCountService.Text = $"Количество: {db.Services.Count()}";
            }

            currentWorker = worker;

            if (currentWorker.RoleId != 1)
            {
                menuItemAdmin.Visibility = Visibility.Collapsed;

                buttonEditService.Visibility = Visibility.Collapsed;
                buttonDeleteService.Visibility = Visibility.Collapsed;
            }

            textBlockStatusWorker.Text = $"Вы вошли как: {worker.FirstName} {worker.LastName} {worker.Patronymic} | {worker.Role.Name}";
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItemClientOrder_Click(object sender, RoutedEventArgs e)
        {
            new TrackingClientOrderWindow(currentWorker).Show();
            Close();
        }

        private void MenuItemDataWorker_Click(object sender, RoutedEventArgs e)
        {
            new WorkerWindow(currentWorker).Show();
            Close();
        }

        private void MenuItemExitAdmin_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();

            Close();
        }

        private void ButtonAddService_Click(object sender, RoutedEventArgs e)
        {
            new AddEditServiceWindow(null).ShowDialog();

            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                gridService.ItemsSource = db.Services.ToList();
                textBlockCountService.Text = $"Количество: {db.Services.Count()}";
            }
        }

        private void EditService_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (currentWorker.RoleId == 1)
            {
                Service currentService = gridService.SelectedItem as Service;

                new AddEditServiceWindow(currentService).ShowDialog();
            }

            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                gridService.ItemsSource = db.Services.ToList();
                textBlockCountService.Text = $"Количество: {db.Services.Count()}";
            }
        }

        private void ButtonEditService_Click(object sender, RoutedEventArgs e)
        {
            if (currentWorker.RoleId == 1)
            {
                Service currentService = gridService.SelectedItem as Service;

                new AddEditServiceWindow(currentService).ShowDialog();
            }

            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                gridService.ItemsSource = db.Services.ToList();
                textBlockCountService.Text = $"Количество: {db.Services.Count()}";
            }
        }

        private void ButtonDeleteService_Click(object sender, RoutedEventArgs e)
        {
            if (currentWorker.RoleId == 1)
            {
                using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
                {
                    Service currentService = gridService.SelectedItem as Service;

                    if (MessageBox.Show($"Вы точно хотите удалить {currentService.Title}", "Внимание",
                       MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        db.Services.Remove(currentService);
                        db.SaveChanges();
                        MessageBox.Show($"Услуга: {currentService.Title} удалена", "Успешно");
                    }

                    gridService.ItemsSource = db.Services.ToList();
                    textBlockCountService.Text = $"Количество: {db.Services.Count()}";
                }
            }
        }

        private void ButtonExitRole_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            Close();
        }
    }
}
