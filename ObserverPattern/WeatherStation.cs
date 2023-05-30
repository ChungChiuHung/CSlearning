using System.Collections.Generic;

namespace ObserverPattern
{
    public class WeatherStation : ISubject
    {
        private int temperature;
        private List<IObservable> observers;

        public WeatherStation()
        {
            observers = new List<IObservable>();
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

        public void Attach(IObservable observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObservable observer)
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
