using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ObserverPattern_ex05
{

    public class Device
    {
        public enum Status
        {
            Start,
            Stop,
            Idle,
            Alert
        };

        public int Id { get; set; }
        public string Name { get; set; }
        public Status CurrentStatus { get; set; }
    }

    public class DeviceMonitor : IObservable<Device>
    {
        private List<IObserver<Device>> observers;
        private List<Device> devices;


        public DeviceMonitor()
        {
            observers = new List<IObserver<Device>>();
            devices = new List<Device>();
        }

        private void Dev_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyObservers(sender as Device);
        }

        public void AddDevice(Device device)
        {
            devices.Add(device);
            NotifyObservers(device);
        }

        private void NotifyObservers(Device device)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(device);
            }
        }

        public IDisposable Subscribe(IObserver<Device> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
                foreach (Device dev in devices)
                {
                    observer.OnNext(dev);
                }
            }

            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<Device>> Observers;
            private IObserver<Device> Observer;

            public Unsubscriber(List<IObserver<Device>> observers, IObserver<Device> observer)
            {
                this.Observers = observers;
                this.Observer = observer;
            }

            public void Dispose()
            {
                if (Observers.Contains(Observer))
                {
                    Observers.Remove(Observer);
                }
            }
        }
    }

    public class DeviceDisplay : IObserver<Device>
    {
        public void Subscribe(IObservable<Device> provider)
        {
            provider.Subscribe(this);
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(Device device)
        {
            Console.WriteLine("Device Display: ");
            Console.WriteLine($"Device Id: {device.Id}\n" +
                $"Device Name: {device.Name}\n" +
                $"Device Status: {device.CurrentStatus}\n");
        }
    }

    public class DeviceLogger : IObserver<Device>
    {

        public void Subscribe(IObservable<Device> provider)
        {
            provider.Subscribe(this);
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(Device value)
        {
            Console.WriteLine($"Device Logger: Loggin " +
                $"{value.Name} {value.Id} {value.CurrentStatus}\n");
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            DeviceMonitor monitor = new DeviceMonitor();
            DeviceDisplay display = new DeviceDisplay();
            DeviceLogger logger = new DeviceLogger();

            display.Subscribe(monitor);
            logger.Subscribe(monitor);

            Device dev1 = new Device { Id = 1, Name = "UBL01", CurrentStatus = Device.Status.Start };
            Device dev2 = new Device { Id = 2, Name = "UBL02", CurrentStatus = Device.Status.Stop };
            Device dev3 = new Device { Id = 3, Name = "UBL03", CurrentStatus = Device.Status.Idle };
            Device dev4 = new Device { Id = 4, Name = "UBL04", CurrentStatus = Device.Status.Alert };

            monitor.AddDevice(dev1);
            monitor.AddDevice(dev2);

            //IDisposable subscription = monitor.Subscriber(logger);
            //subscription.Dispose();

            monitor.AddDevice(dev3);
            monitor.AddDevice(dev4);

            Console.ReadLine();
        }
    }
}
