using System;

namespace MontyHallKata
{
    public class RandomDoorNumberGenerator : DoorNumberGenerator
    {
        private readonly Random rng = new Random();

        public int RoomNumber()
        {
            int number = rng.Next(1, 3);
            return number;
        }
    }
}