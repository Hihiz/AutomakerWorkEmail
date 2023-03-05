using AutomakerWorkEmail.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AutomakerWorkEmail.Windows
{

    public partial class AddEditServiceWindow : Window
    {
        Service currentService;

        public AddEditServiceWindow(Service service)
        {
            InitializeComponent();

            if (service != null)
            {
                currentService = service;
                Title = $"Редактирование услуги: {service.Id} {service.Title}";
                DataContext = currentService;
            }
            else
            {
                Title = "Добавление услуги";
                panelId.Visibility = Visibility.Collapsed;
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(textBoxTitle.Text))
                errors.AppendLine("Укажите название");

            if (string.IsNullOrWhiteSpace(textBoxCost.Text))
                errors.AppendLine("Укажите цену");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                if (currentService == null)
                {
                    Service service = new Service()
                    {
                        Title = textBoxTitle.Text,
                        Cost = Convert.ToDecimal(textBoxCost.Text),
                        Description = textBoxDescription.Text
                    };

                    try
                    {
                        db.Services.Add(service);
                        db.SaveChanges();
                        MessageBox.Show("Услуга добавлена", "Успешно");
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
                        db.Services.Update(currentService);
                        db.SaveChanges();
                        MessageBox.Show("Услуга обновлена", "Успешно");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                ServiceWindow serviceWindow = Application.Current.Windows.OfType<ServiceWindow>().FirstOrDefault();
                (serviceWindow.FindName("gridService") as DataGrid).ItemsSource = db.Services.ToList();
                (serviceWindow.FindName("textBlockCountService") as TextBlock).Text = $"Количество: {db.Services.Count()}";
            }
        }
    }
}
