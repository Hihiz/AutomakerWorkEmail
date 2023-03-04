using AutomakerWorkEmail.Infrastructure;
using AutomakerWorkEmail.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
        }

        private void MenuItemClientOrder_Click(object sender, RoutedEventArgs e)
        {
            new TrackingClientOrderWindow(null).Show();
            Close();
        }

        private void ButtonSaveClientOrder_Click(object sender, RoutedEventArgs e)
        {
            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                if (currentClientOrder == null)
                {
                    ClientOrder clientOrder = new ClientOrder()
                    {
                        Status = textBoxStatus.Text,
                        ClientId = ((Client)comboBoxClient.SelectedItem).Id,
                        ServiceId = ((Service)comboBoxService.SelectedItem).Id,
                        //TrackNumber = textBoxTrackNumber.Text,     
                        TrackNumber = trackNumberBuilder.GenerateTrackNumber(),
                        //Code = textBoxCode.Text,
                        Code = codeGetOrderClientBuilder.GenerateCode(),
                        DateDispatch = Convert.ToDateTime(textBoxDateDispatch.Text),
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
                        //db.ClientOrders.Update(currentClientOrder);

                        db.Entry(currentClientOrder).State = EntityState.Modified;
                        db.ClientOrders.Attach(currentClientOrder);                       

                        db.SaveChanges();
                        MessageBox.Show("Заказ обновлен", "Успешно");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                TrackingClientOrderWindow trackingClientOrderWindow = Application.Current.Windows.OfType<TrackingClientOrderWindow>().FirstOrDefault();
                (trackingClientOrderWindow.FindName("gridClientOrder") as DataGrid).ItemsSource = db.ClientOrders.Include(c => c.Client).Include(s => s.Service).ToList();
                (trackingClientOrderWindow.FindName("textBlockCountClientOrder") as TextBlock).Text = $"Количество: {db.ClientOrders.Count()}";
            }
        }

        private void ButtonSaveClient_Click(object sender, RoutedEventArgs e)
        {
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
