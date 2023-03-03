using System;

namespace AutomakerWorkEmail.Infrastructure
{
    internal class CodeGetOrderClientBuilder
    {
        public string GenerateCode()
        {
            string code = "";

            Random rnd = new Random();

            for (int i = 0; i < 3; i++)
            {
                code += rnd.NextInt64(1, 9).ToString();
            }

            return code;
        }
    }
}
