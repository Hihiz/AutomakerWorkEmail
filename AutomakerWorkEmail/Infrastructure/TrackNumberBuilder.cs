using System;

namespace AutomakerWorkEmail.Infrastructure
{
    internal class TrackNumberBuilder
    {
        public string GenerateTrackNumber()
        {
            string trackNumber = "";

            Random rnd = new Random();

            for (int i = 0; i < 3; i++)
            {
                trackNumber += (char)rnd.Next('A', 'Z') + rnd.NextInt64(1, 9).ToString();
            }

            return trackNumber;
        }
    }
}
