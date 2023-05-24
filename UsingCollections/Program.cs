using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingCollections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Array
            int[] myArray = new int[10];
            
            for (int i = 0; i < myArray.Length; i++)
            {
                myArray[i] = i;
            }

            // myArray[10] = 10;

            foreach (int i in myArray)
            { Console.WriteLine(i); }

            Console.WriteLine();

            string[] myArray2 = new string[10];
            myArray2[0] = "a"; // 1
            myArray2[1] = "b"; // 2
            myArray2[2] = "c"; // 3
            myArray2[3] = "d"; // 4
            myArray2[4] = "e"; // 5
            myArray2[5] = "f"; // 6
            myArray2[6] = "g"; // 7
            myArray2[7] = "h"; // 8
            myArray2[8] = "i"; // 9
            myArray2[9] = "j"; // 10

            foreach (string str in myArray2)
            { Console.Write(str+ " "); }

            Console.WriteLine();

            // Reference Array Elements
            string[] myArrayRef = new string[10];
            myArrayRef = myArray2;  // store the address of myArray2

            Console.WriteLine(myArrayRef.GetHashCode());
            Console.WriteLine(myArray2.GetHashCode());

            foreach (string str in myArrayRef)
            {
                Console.Write(str + " ");
            }

            Console.WriteLine();

            myArrayRef[0] = "A-Ref";
            myArrayRef[1] = "B-Ref";

            foreach (string str in myArray2)
            {
                Console.Write(str + " ");
            }

            Console.WriteLine("\n\n");

            // Copy Array Elements
            string[] myArrayCopy = new string[10];
            for (int i = 0; i < myArray2.Length; i++)
            {
                myArrayCopy[i] = myArray2[i];
            }

            Console.WriteLine(myArrayCopy.GetHashCode());
            Console.WriteLine(myArray2.GetHashCode() );

            myArrayCopy[0] = "A-Copy";
            myArrayCopy[1] = "B-Copy";

            foreach(string str in myArray2)
            { Console.Write(str + " ");}

            Console.WriteLine();

            foreach (string str in myArrayCopy)
            { Console.Write(str + " ");}

            Console.WriteLine("\n\n");

            // Remove/Add Elements to Array
            string[] bufferArray = new string[myArray2.Length];
            for (int i = 0; i < bufferArray.Length; i++)
            {
                bufferArray[i] = myArray2[i];
            }

            myArray2 = new string[5];

            for (int i = 0; i < myArray2.Length; i++)
            {
                myArray2[i] = bufferArray[i];
            }

            Console.WriteLine("\n");

            #endregion

            #region List<T>
            List<User> objList = new List<User>();
            objList.Add(new User() { Id = 0, Name = "Alex"});
            objList.Add(new User() { Id = 1, Name = "Hank"});
            objList.Add(new User() { Id = 2, Name = "Eric"});
            objList.Add(new User() { Id = 3, Name = "Simon"});

            foreach (var item in objList)
            {
                Console.WriteLine("Id: " + item.Id + " Name: " + item.Name);
            }

            Console.WriteLine();

            objList.Insert(1, new User { Id = 6, Name = "Canon" });
            objList.Insert(1, new User { Id = 5, Name = "Wooden" });
            objList.Insert(1, new User { Id = 4, Name = "Ricky" });

            foreach (var item in objList)
            {
                Console.WriteLine("Id: " + item.Id + " Name: " + item.Name);
            }

            Console.WriteLine();

            User user_1 = new User() { Id = 11, Name = "Frank" };

            objList.Add(user_1);

            objList.Remove(user_1);
            //objList.RemoveAt(1);
            //objList.RemoveRange(1, 3);
            
            foreach (var item in objList)
            {
                Console.WriteLine("Id: " + item.Id + " Name: " + item.Name);
            }

            Console.WriteLine();
            #endregion

            #region Queue<T>
            // FIFO: First In, First Out
            Queue<User> queue = new Queue<User>();
            queue.Enqueue(new User { Id = 4, Name = "No.4" });
            queue.Enqueue(new User { Id = 5, Name = "No.5" });
            queue.Enqueue(new User { Id = 6, Name = "No.6" });
            queue.Enqueue(new User { Id = 7, Name = "No.7" });

            foreach (var item in queue)
            {
                Console.Write(item.Name + "-");
            }

            Console.WriteLine();

            for (int i = 0; queue.Count > 0; i++)
            {
                queue.Dequeue();
                Console.Write(queue.Count + "-\t");
            }

            Console.WriteLine();

            Queue<string> strQueue = new Queue<string>();
            strQueue.Enqueue("str1");
            strQueue.Enqueue("str2");
            strQueue.Enqueue("str3");
            strQueue.Enqueue("str4");

            Console.WriteLine($"The counts of objects: {strQueue.Count}");

            while (strQueue.Count > 0) 
            {
                string number = strQueue.Dequeue();
                Console.WriteLine($"{number} is Dequeued");
            }

            Console.WriteLine($"The counts of objects: {strQueue.Count}");

            Console.WriteLine();
            #endregion

            #region Stack<T>
            //FILO, Fist In, Last Out
            Stack<User> stack = new Stack<User>();
            
            stack.Push(new User { Id = 5, Name = "Phinex" });
            stack.Push(new User { Id = 6, Name = "Queen" });
            stack.Push(new User { Id = 7, Name = "Kolin" });
            stack.Push(new User { Id = 8, Name = "Miya" });
            foreach (var item in stack)
            {
                Console.Write(item.Name + "-");
            }

            Console.WriteLine();

            int stack_cnt = stack.Count;

            for (int i = 0; i < stack_cnt; i++)
            {
                var user_in_stack = stack.Pop();
                Console.WriteLine($"{user_in_stack.Id} {user_in_stack.Name}: The Object is Poped");
                Console.WriteLine();
            }
            Console.WriteLine();
            #endregion

            #region LinkedList<T>
            LinkedList<string> linkStrList = new LinkedList<string>();
            linkStrList.AddFirst("A");
            linkStrList.AddLast("B");
            linkStrList.AddLast("C");
            linkStrList.AddLast("D");
 

            foreach (var item in linkStrList)
            {
                Console.Write(item + "-");
            }

            Console.WriteLine();
            Console.WriteLine("Add A Element At the Top of those elements");
            linkStrList.AddFirst("00");
            linkStrList.AddLast("EE");
            foreach (var item in linkStrList)
            {
                Console.Write(item + "-");
            }
            Console.WriteLine();
            #endregion

            #region HashSet<T>
            HashSet<User> users = new HashSet<User>();

            users.Add(new User { Id= 100, Name = "Set 01" });
            users.Add(new User { Id= 101, Name = "Set 02" });
            users.Add(new User { Id= 102, Name = "Set 03" });
            users.Add(new User { Id = 103, Name = "Set 04" });


            foreach (var item in users)
            {
                Console.WriteLine(item.Name);
            }

            #endregion

            #region Dictionary<TKey, TValue>

            #endregion






            Console.ReadLine();

        }

        class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
