using AutomakerWorkEmail.Infrastructure;
using AutomakerWorkEmail.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AutomakerWorkEmail.Windows
{
    public partial class AddEditClientOrderWindow : Window
    {
        TrackNumberBuilder trackNumberBuilder = new TrackNumberBuilder();
        CodeGetOrderClientBuilder codeGetOrderClientBuilder = new CodeGetOrderClientBuilder();
        ClientOrder currentClientOrder;

        public AddEditClientOrderWindow(ClientOrder clientOrder)
        {
            InitializeComponent();

            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                comboBoxService.ItemsSource = db.Services.ToList();
                comboBoxClient.ItemsSource = db.Clients.ToList();

                comboBoxRole.ItemsSource = db.Roles.ToList();
            }

            if (clientOrder != null)
            {
                currentClientOrder = clientOrder;
                Title = $"Редактирование заказа клиента: {clientOrder.Client.FirstName} {clientOrder.Client.LastName} {clientOrder.Client.Patronymic} | {clientOrder.Service.Title}";
                DataContext = currentClientOrder;
            }
            else
            {
                Title = "Добавление заказа";
                panelId.Visibility = Visibility.Collapsed;

                textBoxTrackNumber.Visibility = Visibility.Collapsed;
                textBlockTrackNumber.Visibility = Visibility.Collapsed;

                textBoxCode.Visibility = Visibility.Collapsed;
                textBlockCode.Visibility = Visibility.Collapsed;
            }

            List<string> comboBoxStatusList = new List<string>() { "Создан", "Оформлен", "В ожидании", "Отправлено", "Принят", "Готов", "Выдан" };

            comboBoxStatus.ItemsSource = comboBoxStatusList.ToList();
        }

        private void MenuItemClientOrder_Click(object sender, RoutedEventArgs e)
        {
            new TrackingClientOrderWindow(null).Show();
            Close();
        }

        private void ButtonSaveClientOrder_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(comboBoxStatus.Text))
                errors.AppendLine("Укажите статус");

            if (string.IsNullOrWhiteSpace(comboBoxClient.Text))
                errors.AppendLine("Укажите клиента");

            if (string.IsNullOrWhiteSpace(comboBoxService.Text))
                errors.AppendLine("Укажите усулугу");

            if (string.IsNullOrWhiteSpace(datePickerDateDispatch.Text))
                errors.AppendLine("Укажите дату доставки: dd.mm.yyyy hh:mm:ss");

            if (string.IsNullOrWhiteSpace(textBoxAddress.Text))
                errors.AppendLine("Укажите адрес достави");

            if (string.IsNullOrWhiteSpace(textBoxFinalConst.Text))
                errors.AppendLine("Укажите итоговую цену");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                if (currentClientOrder == null)
                {
                    ClientOrder clientOrder = new ClientOrder()
                    {
                        Status = comboBoxStatus.Text,
                        ClientId = ((Client)comboBoxClient.SelectedItem).Id,
                        ServiceId = ((Service)comboBoxService.SelectedItem).Id,
                        //TrackNumber = textBoxTrackNumber.Text,     
                        TrackNumber = trackNumberBuilder.GenerateTrackNumber(),
                        //Code = textBoxCode.Text,
                        Code = codeGetOrderClientBuilder.GenerateCode(),
                        DateDispatch = Convert.ToDateTime(datePickerDateDispatch.Text),
                        Address = textBoxAddress.Text,
                        FinalCost = Convert.ToDecimal(textBoxFinalConst.Text),
                        Description = textBoxDescription.Text,
                    };

                    if (clientOrder.FinalCost < 0)
                    {
                        MessageBox.Show("Итоговая цена не можежт быть отрицательной", "Внимание");
                        return;
                    }

                    try
                    {
                        db.ClientOrders.Add(clientOrder);
                        db.SaveChanges();
                        MessageBox.Show("Заказ добавлен", "Успешно");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    if (currentClientOrder.FinalCost < 0)
                    {
                        MessageBox.Show("Итоговая цена не можежт быть отрицательной", "Внимание");
                        return;
                    }

                    try
                    {
                        db.ClientOrders.Update(currentClientOrder);
                        db.SaveChanges();

                        MessageBox.Show("Заказ обновлен", "Успешно");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                new UpdateDataGrid().RefreshData();
            }
        }

        private void ButtonSaveClient_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(textBoxLastName.Text))
                errors.AppendLine("Укажите фамилию");

            if (string.IsNullOrWhiteSpace(textBoxFirstName.Text))
                errors.AppendLine("Укажите имя");

            if (string.IsNullOrWhiteSpace(textBoxPatronymic.Text))
                errors.AppendLine("Укажите отчество");

            if (string.IsNullOrWhiteSpace(textBoxPassport.Text))
                errors.AppendLine("Укажите паспортные данные");

            if (string.IsNullOrWhiteSpace(comboBoxRole.Text))
                errors.AppendLine("Укажите роль ");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {

                Client client = new Client()
                {
                    LastName = textBoxLastName.Text,
                    FirstName = textBoxFirstName.Text,
                    Patronymic = textBoxPatronymic.Text,
                    Passport = textBoxPassport.Text,
                    RoleId = ((Role)comboBoxRole.SelectedItem).Id,
                };

                try
                {
                    db.Clients.Add(client);
                    db.SaveChanges();
                    MessageBox.Show("Клиент добавлен", "Успешно");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                comboBoxService.ItemsSource = db.Services.ToList();
                comboBoxClient.ItemsSource = db.Clients.ToList();
            }
        }
    }
}
