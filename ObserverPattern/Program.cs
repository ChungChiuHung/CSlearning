using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern
{

    internal class Program
    {
        static void Main(string[] args)
        {
            WeatherStation weatherStation = new WeatherStation();
            TemperatureDisplay temperatureDisplay = new TemperatureDisplay();
            Fan fan = new Fan();

            weatherStation.Attach(temperatureDisplay);
            weatherStation.Attach(fan);

            weatherStation.Temperature = 20;
            weatherStation.Temperature = 30;

            weatherStation.Detach(fan);

            weatherStation.Temperature = 15;

            Console.ReadLine();
        }
    }
}
