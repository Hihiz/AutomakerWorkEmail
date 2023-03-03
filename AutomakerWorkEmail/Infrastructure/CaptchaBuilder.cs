using System;

namespace AutomakerWorkEmail.Infrastructure
{
    internal class CaptchaBuilder
    {
        public static string Refresh()
        {
            string captcha = "";

            Random rnd = new Random();

            for (int i = 0; i < 1; i++)
            {
                captcha += (char)rnd.Next('A', 'Z') + rnd.NextInt64(0, 9).ToString();
            }

            return captcha;
        }
    }
}
