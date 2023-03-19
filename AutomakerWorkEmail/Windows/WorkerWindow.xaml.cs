using AutomakerWorkEmail.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AutomakerWorkEmail.Windows
{
    public partial class WorkerWindow : Window
    {
        Worker currentWorker;

        public WorkerWindow(Worker worker)
        {
            InitializeComponent();

            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                gridWorker.ItemsSource = db.Workers.Include(r => r.Role).ToList();
                textBlockCountWorker.Text = $"Количество: {db.Workers.Count()}";
            }

            currentWorker = worker;

            textBlockStatusWorker.Text = $"Вы вошли как: {worker.LastName} {worker.FirstName} {worker.Patronymic} | {worker.Role.Name}";
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItemExitAdmin_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();

            Close();
        }

        private void EditWorker_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Worker currentWorker = gridWorker.SelectedItem as Worker;

            new AddEditWorkerWindow(currentWorker).ShowDialog();
        }

        private void ButtonAddWorker_Click(object sender, RoutedEventArgs e)
        {
            new AddEditWorkerWindow(null).ShowDialog();
        }

        private void ButtonEditWorker_Click(object sender, RoutedEventArgs e)
        {
            Worker currentWorker = gridWorker.SelectedItem as Worker;

            new AddEditWorkerWindow(currentWorker).ShowDialog();
        }

        private void ButtonDeleteWorker_Click(object sender, RoutedEventArgs e)
        {
            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                Worker currentWorker = gridWorker.SelectedItem as Worker;

                if (MessageBox.Show($"Вы точно хотите удалить {currentWorker.FirstName} {currentWorker.LastName}" +
                    $" {currentWorker.Patronymic} | {currentWorker.Role.Name}", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    db.Workers.Remove(currentWorker);
                    db.SaveChanges();
                    MessageBox.Show($"Работник: {currentWorker.FirstName} {currentWorker.LastName} {currentWorker.Patronymic} удален");
                }

                textBlockCountWorker.Text = $"Количество: {db.Workers.Count()}";
                gridWorker.ItemsSource = db.Workers.Include(r => r.Role).ToList();
            }
        }

        private void MenuItemClientOrder_Click(object sender, RoutedEventArgs e)
        {
            new TrackingClientOrderWindow(currentWorker).Show();
            Close();
        }

        private void MenuItemService_Click(object sender, RoutedEventArgs e)
        {
            new ServiceWindow(currentWorker).Show();
            Close();
        }

        private void ButtonExitRole_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            Close();
        }

        private void SearchFio_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                List<Worker> worker = db.Workers.ToList();
                gridWorker.ItemsSource = worker;

                if (textBoxSearchFio.Text.Length > 0)
                    worker = worker.Where(l => l.LastName.Contains(textBoxSearchFio.Text)
                    || l.FirstName.Contains(textBoxSearchFio.Text)
                    || l.Patronymic.Contains(textBoxSearchFio.Text)).ToList();

                gridWorker.ItemsSource = worker;
                textBlockCountWorker.Text = $"Количество: {worker.Count()} из {db.Workers.ToList().Count}";
            }
        }
    }
}
