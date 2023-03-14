using AutomakerWorkEmail.Infrastructure;
using AutomakerWorkEmail.Models;
using AutomakerWorkEmail.Windows;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutomakerWorkEmail.Windows
{
    public partial class LoginWindow : Window
    {
        bool verify = true;
        int verifyCheck = 0;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                if (panelCaptcha.Visibility == Visibility.Visible)
                {
                    if (textBlockCaptcha.Text == textBoxCaptcha.Text)
                        verify = true;
                }

                Worker worker = db.Workers.Where(w => w.Login == textBoxLogin.Text && w.Password == passwordBox.Password).Include(r => r.Role).FirstOrDefault();

                if (worker != null && verify )
                {
                    new TrackingClientOrderWindow(worker).Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Логин или пароль введен не верно!", "Ошибка");

                    verifyCheck += 1;

                    panelCaptcha.Visibility = Visibility.Visible;
                    textBlockCaptcha.Text = CaptchaBuilder.Refresh();
                    verify = false;

                    if (verifyCheck > 1)
                    {
                        DisableButtonAsync();
                        textBlockCaptcha.Text = CaptchaBuilder.Refresh();
                    }
                }
            }
        }

        private async void DisableButtonAsync()
        {
            buttonLogin.IsEnabled = false;
            await Task.Delay(TimeSpan.FromSeconds(3));
            buttonLogin.IsEnabled = true;
        }
    }
}
