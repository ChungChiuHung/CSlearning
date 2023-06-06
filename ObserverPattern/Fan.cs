using System;

namespace ObserverPattern
{
    public class Fan : IObserver
    {
        public void Update(int temperature)
        {
            if(temperature > 25)
            {
                Console.WriteLine("Fan: It's hot, turning on the fan.");
            }
            else
            {
                Console.WriteLine("Fan: It's cool, turning off the fan.");
            }
        }
    }
}
