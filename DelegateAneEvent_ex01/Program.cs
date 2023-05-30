using System;
using System.Collections.Generic;

namespace DelegateAneEvent_ex01
{

    public delegate void NotifyDelegate(string message);


    public class YouTubeChannel
    {
        private List<NotifyDelegate> observers;
        private string currentMessage;

        public YouTubeChannel()
        {
            observers = new List<NotifyDelegate>();
        }

        public void Notify(string message)
        {
            currentMessage = message;
            foreach (var observer in observers)
            {
                observer.Invoke(currentMessage);
            }
        }

        public void Subscribe(NotifyDelegate observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(NotifyDelegate observer)
        {
            observers.Remove(observer);
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


            // Object to Delegate
            NotifyDelegate notify_Johny= Johnny.GetNotify;
            NotifyDelegate notify_Cannon = Cannon.GetNotify;
            NotifyDelegate notify_Ricky = Ricky.GetNotify;

            // Object to Be Delegated
            channel.Subscribe(notify_Johny);
            channel.Subscribe(notify_Cannon);
            channel.Subscribe(notify_Ricky);

            // Invoke The Delegate
            channel.Notify("Hello, We will charge next in next week");

            channel.Unsubscribe(notify_Ricky);
            channel.Unsubscribe(notify_Cannon);

            channel.Notify("Hello, Our Valued Member: ");
            

            Console.ReadLine();

        }
    }
}
