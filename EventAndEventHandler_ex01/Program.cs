using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventAndEventHandler_ex01
{

    public class DeviceMonitor
    {
        Thread thread;
        private void generateRandomNumber()
        {
            while (true)
            {
                Random rndNumb = new Random();
                rndNumb.Next(1, 100);

                var args = new Your_Event_Args();
                args.DeviceInfo.MacAddress = Guid.NewGuid().ToString();
                args.DeviceInfo.Id = rndNumb.Next(1, 100);
                OnSomthingHappend(args);
                Thread.Sleep(1000);
                Console.Clear();
            }
        }

        public DeviceMonitor()
        {
           thread = new Thread(generateRandomNumber);

        }

        public void StartMonitor()
        {
            thread.Start();
        }

        public event EventHandler<Your_Event_Args> Your_Event_Name;

        protected virtual void OnSomthingHappend(Your_Event_Args args)
        {
            var handler = Your_Event_Name;

            if(handler != null) 
            {
                handler.Invoke(this, args);
            }

            // handler?.Invoke(this, args);
        }
        
    }

    public class Your_Event_Args: EventArgs
    {
        public Device DeviceInfo { get; set; } = new Device();

        public string Your_Args_Information { get; set; } = "Somthing Happend!!!";
    }

    public class Device
    {
        public string MacAddress { get; set; }
        public int Id { get; set; }
    }


    public class DeviceDisplay
    {

        public DeviceDisplay(DeviceMonitor monitor)
        {
            monitor.Your_Event_Name += Monitor_Your_Event_Name;   
        }

        private void Monitor_Your_Event_Name(object sender, Your_Event_Args e)
        {
            Console.WriteLine("Device Display Received An Event");
            Console.WriteLine(e.Your_Args_Information);
            Console.WriteLine($"Device Id: {e.DeviceInfo.Id}");
            Console.WriteLine($"Device Mac Address: {e.DeviceInfo.MacAddress}\n");
            
        }
    }

    public class RemoteDisplay
    {
        public RemoteDisplay(DeviceMonitor monitor)
        {
            monitor.Your_Event_Name += Monitor_Your_Event_Name;
        }

        private void Monitor_Your_Event_Name(object sender, Your_Event_Args e)
        {
            Console.WriteLine("Remote Display Received An Event");
            Console.WriteLine(e.Your_Args_Information);
            Console.WriteLine($"Device Id: {e.DeviceInfo.Id}");
            Console.WriteLine($"Device Mac Address: {e.DeviceInfo.MacAddress}\n");
        }
    }
    
    internal class Program
    {
        static void Main(string[] args)
        {
            DeviceMonitor monitor = new DeviceMonitor();
            DeviceDisplay display = new DeviceDisplay(monitor);
            RemoteDisplay remoteDisplay = new RemoteDisplay(monitor);

            monitor.StartMonitor();

            Console.ReadLine();

        }
    }
}
