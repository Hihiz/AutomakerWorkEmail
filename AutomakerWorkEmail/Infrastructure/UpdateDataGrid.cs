using AutomakerWorkEmail.Models;
using AutomakerWorkEmail.Windows;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AutomakerWorkEmail.Infrastructure
{
    internal class UpdateDataGrid
    {
        public void RefreshData()
        {
            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                TrackingClientOrderWindow trackingClientOrderWindow = Application.Current.Windows.OfType<TrackingClientOrderWindow>().FirstOrDefault();
                (trackingClientOrderWindow.FindName("gridClientOrder") as DataGrid).ItemsSource = db.ClientOrders.Include(s => s.Service).Include(c => c.Client).Where(s => s.Status != "Выдан").ToList();
                (trackingClientOrderWindow.FindName("gridCloseOrder") as DataGrid).ItemsSource = db.ClientOrders.Include(s => s.Service).Include(c => c.Client).Where(s => s.Status == "Выдан").ToList();
                (trackingClientOrderWindow.FindName("textBlockCountClientOrder") as TextBlock).Text = $"Количество Активных закзов: {db.ClientOrders.Where(s => s.Status != "Выдан").Count()}";
                (trackingClientOrderWindow.FindName("textBlockCountCloseClientOrder") as TextBlock).Text = $"Количество Закрытых закзов: {db.ClientOrders.Where(s => s.Status == "Выдан").Count()}";
            }
        }
    }
}
