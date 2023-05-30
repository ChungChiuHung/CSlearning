using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPatter_ex_03
{
    internal class Program
    {
        public interface IObserver
        {
            void GetNotify(string message);
        }

        public interface ISubject
        {
            void SubscribeBy(IObserver observer);
            void UnSubsribeBy(IObserver observer);
            void Notify(string message);
        }

        public class YouTubeChannel : ISubject
        {
            List<IObserver> observers;
            string CurrentMessage;

            public YouTubeChannel()
            {
                observers = new List<IObserver>();
            }

            public void Notify(string message)
            {
                foreach (var observer in observers)
                {
                    observer.GetNotify(message);
                }
            }

            public void SubscribeBy(IObserver observer)
            {
                observers.Add(observer);
            }

            public void UnSubsribeBy(IObserver observer)
            {
                observers.Remove(observer);
            }
        }


        public class User : IObserver
        {
            public string Name { get;}

            public User(string name)
            {
                Name = name;
            }
            public void GetNotify(string message)
            {
                Console.WriteLine(message + " "+ Name );
            }
        }

        static void Main(string[] args)
        {

            YouTubeChannel channel = new YouTubeChannel();
            User Johnny = new User("Johnny");
            User Cannon = new User("Cannon");
            User Ricky = new User("Ricky");

            channel.SubscribeBy(Johnny);
            channel.SubscribeBy(Cannon);
            channel.SubscribeBy(Ricky);

            channel.Notify("Hello");

            Console.ReadLine();

        }
    }
}
