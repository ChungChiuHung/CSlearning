using System;
using System.Collections.Generic;

namespace ObserverPattern_ex_02
{
    /*
     * Define the interfaces: 
     *          Create the ISubject interface that includes methods for 
     *          attaching, detaching, and notifying observers. Also, 
     *          create the IObserver interface that defines the Update method.
     */

    public interface IObserver<T>
    {
        void Update(T data);
    }

    public interface ISubject<T>
    {
        void Attach(IObserver<T> observer);
        void Detach(IObserver<T> observer);
        void Notify(T data);

    }


    /* Implement the subject: 
    *          Create a concrete subject class that implements the ISubject interface. 
    *          This class should maintain a list of observers and provide methods to manage them.
    */
    public class LiquidLevelMonitoer : ISubject<WaterData>
    {
        List<IObserver<WaterData>> observers;
        WaterData currentData;

        public LiquidLevelMonitoer()
        {
            observers = new List<IObserver<WaterData>>();
        }

        public void Attach(IObserver<WaterData> observer)
        {
            observers.Add(observer);   
        }

        public void Detach(IObserver<WaterData> observer)
        {
            observers.Remove(observer);
        }

        public void Notify(WaterData data)
        {
            foreach (var observer in observers)
            {
                observer.Update(data);
            }
        }

        public void UpdateWaterData(int temperature, int level)
        {
            currentData = new WaterData() { Level = level, Temperature = temperature };
            Notify(currentData);
        }
    }

    public class WaterData
    {
        public int Level { get; set; }
        public int Temperature { get; set; }

    }

    /* Implement the observers: 
    *          Create one or more concrete observer classes that implement the IObserver interface. 
    *          These classes define the Update method to respond to changes in the subject's state.
    */

    // Concrete observer
    public class DataDiplay : IObserver<WaterData>
    {
        public void Update(WaterData data)
        {
            Console.WriteLine("Display: ");
            Console.WriteLine($"Current Temperature: {data.Temperature}");
            Console.WriteLine($"Current Level: {data.Level}");
        }
    }

    // Concrete observer
    public class Fan : IObserver<WaterData>
    {
        public void Update(WaterData data)
        {
            if(data.Temperature > 25)
            {
                Console.WriteLine("Fan: It's hot, turning on the fan.");
            }
            else
            {
                Console.WriteLine("Fan: It's cool, turning off the fan.");
            }
        }
    }

    // Concrete observer
    public class Pump : IObserver<WaterData>
    {
        public void Update(WaterData data)
        {
            if (data.Level < 50)
            {
                Console.WriteLine("Pump: Activate.");
            }
            else
            {
                Console.WriteLine("Pump: Stop.");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            /* Instantiate the subject and observers: 
            *          Create instances of the subject and observer classes.
            */
            LiquidLevelMonitoer liquidLevelMonitoer = new LiquidLevelMonitoer();
            DataDiplay display = new DataDiplay();
            Fan fan = new Fan();
            Pump pump = new Pump();

            /* Attach observers to the subject: 
                *          Use the Attach method of the subject to attach the observer objects. 
                *          This establishes the dependency between the subject and observers.
            */
            liquidLevelMonitoer.Attach(display);
            liquidLevelMonitoer.Attach(fan);
            liquidLevelMonitoer.Attach(pump);


            /* Update the subject's state: 
                *          Modify the state of the subject. 
                *          This could be done through properties or methods provided by the subject.
            */

            WaterData waterData = new WaterData();
            waterData.Temperature = 30;
            waterData.Level = 50;

            liquidLevelMonitoer.Notify(waterData);

            /* Notify observers: 
                *          When the subject's state changes, call the Notify method of the subject. 
                *          This will trigger the Update method of each attached observer, 
                *          allowing them to react to the state change.
            */
            waterData.Temperature = 20;
            waterData.Level = 30;

            liquidLevelMonitoer.Detach(pump);
            liquidLevelMonitoer.Detach(fan);

            liquidLevelMonitoer.Notify(waterData);

            liquidLevelMonitoer.UpdateWaterData(25, 60);

            Console.ReadLine();
        }
    }
}
