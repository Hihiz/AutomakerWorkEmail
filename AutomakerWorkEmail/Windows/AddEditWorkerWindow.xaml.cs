using AutomakerWorkEmail.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AutomakerWorkEmail.Windows
{
    public partial class AddEditWorkerWindow : Window
    {
        Worker currentWorker;

        public AddEditWorkerWindow(Worker worker)
        {
            InitializeComponent();

            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                comboBoxRole.ItemsSource = db.Roles.ToList();
            }

            if (worker != null)
            {
                currentWorker = worker;
                Title = $"Редактирование работника: {worker.FirstName} {worker.LastName} {worker.Patronymic} | {worker.Role.Name}";
                DataContext = currentWorker;
            }
            else
            {
                Title = "Добавление работника";
                panelId.Visibility = Visibility.Collapsed;
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(textBoxFirstName.Text))
                errors.AppendLine("Укажите имя");

            if (string.IsNullOrWhiteSpace(textBoxLastName.Text))
                errors.AppendLine("Укажите фамилию");

            if (string.IsNullOrWhiteSpace(textBoxPatronymic.Text))
                errors.AppendLine("Укажите отчество");

            if (string.IsNullOrWhiteSpace(textBoxLogin.Text))
                errors.AppendLine("Укажите логин");

            if (string.IsNullOrWhiteSpace(textBoxPassword.Text))
                errors.AppendLine("Укажите пароль");

            if (string.IsNullOrWhiteSpace(comboBoxRole.Text))
                errors.AppendLine("Укажите роль");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                if (currentWorker == null)
                {
                    Worker worker = new Worker()
                    {
                        FirstName = textBoxFirstName.Text,
                        LastName = textBoxLastName.Text,
                        Patronymic = textBoxPatronymic.Text,
                        Login = textBoxLogin.Text,
                        Password = textBoxPassword.Text,
                        RoleId = ((Role)comboBoxRole.SelectedItem).Id
                    };

                    try
                    {
                        db.Workers.Add(worker);
                        db.SaveChanges();
                        MessageBox.Show("Работник добавлен", "Успешно");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        db.Workers.Update(currentWorker);
                        db.SaveChanges();
                        MessageBox.Show("Работник обновлен", "Успешно");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                WorkerWindow workerWindow = Application.Current.Windows.OfType<WorkerWindow>().FirstOrDefault();
                (workerWindow.FindName("gridWorker") as DataGrid).ItemsSource = db.Workers.Include(r => r.Role).ToList();
                (workerWindow.FindName("textBlockCountWorker") as TextBlock).Text = $"Количество: {db.Workers.Count()}";
            }

        }
    }
}
