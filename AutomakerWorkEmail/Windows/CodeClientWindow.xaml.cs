using AutomakerWorkEmail.Infrastructure;
using AutomakerWorkEmail.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AutomakerWorkEmail.Windows
{
    public partial class CodeClientWindow : Window
    {
        ClientOrder currentClientOrder;

        public CodeClientWindow(ClientOrder clientOrder)
        {
            InitializeComponent();

            if (clientOrder != null)
            {
                currentClientOrder = clientOrder;
                DataContext = currentClientOrder;
            }
            else
                MessageBox.Show("Клиент не выбран");
        }

        private void ButtonGetOrderClient_Click(object sender, RoutedEventArgs e)
        {
            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                if (currentClientOrder != null)
                {
                    if (textBoxCodeClient.Text == currentClientOrder.Code)
                    {
                        if (MessageBox.Show($"Вы точно хотите выдать {currentClientOrder.Client.FirstName} {currentClientOrder.Client.LastName}" +
                            $" {currentClientOrder.Client.Patronymic} | {currentClientOrder.Service.Title}", "Внимание",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            currentClientOrder.Status = "Выдан";
                            db.ClientOrders.Update(currentClientOrder);
                            db.SaveChanges();

                            MessageBox.Show($"Заказ: {currentClientOrder.Service.Title} выдан: {currentClientOrder.Client.LastName} {currentClientOrder.Client.FirstName}", "Успешно");
                        }

                        new UpdateDataGrid().RefreshData();
                    }
                    else
                    {
                        MessageBox.Show("Код не совпадает");
                    }
                }
            }
        }
    }
}
