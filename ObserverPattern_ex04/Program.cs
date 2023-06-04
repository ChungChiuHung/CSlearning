using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Threading;

namespace ObserverPattern_ex04
{

    public class WeatherSation: IObservable<int>
    {
        private int temperature;
        private List<IObserver<int>> observers;

        public WeatherSation()
        {
            observers = new List<IObserver<int>>();
        }

        public int Temperature
        {
            get { return temperature; }
            set 
            { 
                temperature = value;
                NotifyObservers();
            }
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            if(!observers.Contains(observer))
            {
                observers.Add(observer);
            }

            return new Unsubscribe(observers, observer);
        }

        private void NotifyObservers()
        {
            foreach(var observer in observers)
            {
                observer.OnNext(temperature);
            }
        }

        private class Unsubscribe : IDisposable
        {
            private List<IObserver<int>> observers;
            private IObserver<int> observer;

            public Unsubscribe(List<IObserver<int>> observers, IObserver<int> observer)
            {
                this.observers = observers;
                this.observer = observer;
            }

            public void Dispose()
            {
                if(observers.Contains(observer))
                {
                    observers.Remove(observer);
                }
            }
        }

       
    }


    public class TemperatureDisplay : IObserver<int>
    {

        public void Subscribe(IObservable<int> provider) 
        {
            provider.Subscribe(this);
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(int value)
        {
            Console.Write("Temperature Display : ");
            Console.Write($"Current Temperature is {value}\n");
        }
    }

    public class Fan : IObserver<int>
    {
        public void Subscribe(IObservable<int> provider)
        {
            provider.Subscribe(this);
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(int value)
        {
            if(value>25)
            {
                Console.WriteLine("Fan: It is hot, turning on the fan.");
            }
            else
            {
                Console.WriteLine("Fan: It's cool, turning off the fan.");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            WeatherSation weatherStation = new WeatherSation();
            TemperatureDisplay display = new TemperatureDisplay();
            Fan fan = new Fan();

            display.Subscribe(weatherStation);
            fan.Subscribe(weatherStation);


            Random randNumb = new Random();
            int temp = 20;



            while(true) 
            {
                temp = randNumb.Next(20,32);
                weatherStation.Temperature = temp;
                Thread.Sleep(1000);
                Console.Clear();
            }

        }
    }
}
