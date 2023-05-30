using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateAndEvent_ex02
{

    public delegate void DelegateEventHandler(object sender, EventArgs e);

    public class ClassToDelegated
    {
        public DelegateEventHandler MyDelegate;

        List<DelegateEventHandler> DelegateList;

        public ClassToDelegated()
        {
            DelegateList = new List<DelegateEventHandler>();
        }

        public void Add(DelegateEventHandler delegateMethod)
        {
            DelegateList.Add(delegateMethod);
        }

        public void Remove(DelegateEventHandler delegateMethod)
        {
            DelegateList.Remove(delegateMethod);
        }

        public void RunDelegate()
        {
            EventArgs eventArgs = new EventArgs();

            MyDelegate?.Invoke(this, eventArgs);

            /*
            if(MyDelegate != null)
            {
                MyDelegate.Invoke(this, eventArgs);
            }
            */

            foreach (var handler in DelegateList)
            {
                handler.Invoke(this, eventArgs);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            ClassToDelegated obj = new ClassToDelegated();

            obj.Add(EventHandler1);
            obj.Add(EventHandler2);
            obj.Add(EventHandler3);

            obj.RunDelegate();

            obj.Remove(EventHandler1);
            obj.Remove(EventHandler2);

            obj.RunDelegate();

            Console.ReadLine();


        }

        static void EventHandler1(object sender, EventArgs e)
        {
            Console.WriteLine("Event handler 1 called");
        }

        static void EventHandler2(object sender, EventArgs e)
        {
            Console.WriteLine("Event handler 2 called");
        }

        static void EventHandler3(object sender, EventArgs e)
        {
            Console.WriteLine("Event handler 3 called");
        }
    }
}
