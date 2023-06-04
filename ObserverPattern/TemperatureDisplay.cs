using System;

namespace ObserverPattern
{
    public class TemperatureDisplay : IObserver
    {
        public void Update(int temperature)
        {
            Console.WriteLine("Temperature Display: Current temperature is "
                + temperature
                + "degress.");
        }
    }
}
