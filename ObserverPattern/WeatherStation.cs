using System.Collections.Generic;

namespace ObserverPattern
{
    public class WeatherStation : ISubject
    {
        private int temperature;
        private List<IObserver> observers;

        public WeatherStation()
        {
            observers = new List<IObserver>();
        }

        public int Temperature
        { 
            get { return temperature; } 
            set 
            {  
                temperature = value;
                Notify();
            } 
        }

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in observers) 
            {
                observer.Update(temperature);
            }
        }
    }
}
