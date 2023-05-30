using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateAndEvent_ex04
{
    public delegate void NotifyDelegate(string data);

    public class YouTubeChannel
    {
        //public event Action<string> NotifyEvent;

        public NotifyDelegate NotifyEvent;

        public void Notify(string message)
        {
            if (NotifyEvent != null)
            {
                NotifyEvent.Invoke(message);
            }
        }
    }

    public class User
    {
        public string Name { get; }

        public User(string name)
        {
            Name = name;
        }

        public void GetNotify(string message)
        {
            Console.WriteLine(message + " " + Name);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            YouTubeChannel channel = new YouTubeChannel();
            User Johnny = new User("Johnny");
            User Cannon = new User("Cannon");
            User Ricky = new User("Ricky");

            channel.NotifyEvent += Johnny.GetNotify;
            channel.NotifyEvent += Cannon.GetNotify;
            channel.NotifyEvent += Ricky.GetNotify;

            channel.Notify("Hello");

            channel.NotifyEvent -= Johnny.GetNotify;
            channel.NotifyEvent -= Cannon.GetNotify;

            channel.Notify("Thanks for statying");

            Console.ReadLine();
        }
    }
}
