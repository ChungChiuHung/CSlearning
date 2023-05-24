using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace UsingCollections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Array
            int[] numbers = new int[10]; // initial the array size
            for (int i = 0; i < numbers.Length; i++) // define the value of each element
            {
                numbers[i] = i + 1;
            }

            foreach (var item in numbers)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine("\n");

            // Ref Element
            int[] ref_numbers = new int[10];
            ref_numbers = numbers;

            ref_numbers[0] = 999;

            foreach (var item in numbers)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine("\n");

            // Copy Element
            int[] copy_numbers = new int[10];

            for (int i = 0; i < numbers.Length; i++)
            {
                copy_numbers[i] = numbers[i];
            }

            copy_numbers[0] = 666;

            foreach (var item in numbers)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine("\n");

            Console.WriteLine("numbers HashCode: \t" + numbers.GetHashCode());
            Console.WriteLine("ref_numbers HashCode: \t" + ref_numbers.GetHashCode());
            Console.WriteLine("copy_numbers HashCode: \t"+copy_numbers.GetHashCode());


            Console.WriteLine("\n");

            // Remove or Resize the Array
            int[] bufferArray = new int[numbers.Length];  // Create a array with the same size

            for (int i = 0; i < numbers.Length; i++)  // Copy each element in an array
            {
                bufferArray[i] = numbers[i];
            }

            numbers = new int[6];

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = bufferArray[i];
            }

            
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
            var findUserById = objList.Find(x => x.Id == 4);
            var findNameById = objList.Find(x => x.Id == 5).Name;

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
            linkStrList.AddFirst("FRONT");
            linkStrList.AddLast("END");

            

            foreach (var item in linkStrList)
            {
                Console.Write(item + "-");
            }
            Console.WriteLine();
            #endregion

            #region HashSet<T>
            // this class is optimized for performing set operations,
            // such as determining set membership and generating the 
            // union and intersection of sets

            // SortedSet<T>

            HashSet<int> set_1 = new HashSet<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9};
            HashSet<int> set_2 = new HashSet<int>() { 2, 4, 6, 8 };
            HashSet<int> set_3 = new HashSet<int>() { 1, 3, 5, 7 };
            HashSet<int> set_4 = new HashSet<int>() { 13, 23, 43, 73 };

            
            
            Console.WriteLine(set_2.IsSubsetOf(set_1));
            Console.WriteLine(set_1.IsSupersetOf(set_2));
            Console.WriteLine(set_2.IsProperSubsetOf(set_1));
            Console.WriteLine(set_1.IsProperSupersetOf(set_3));

            //set_1.UnionWith(set_4); // 聯集
            //set_1.ExceptWith(set_3); // 移除交集元素
            //set_1.IntersectWith(set_2); // 交集

            foreach (var item in set_1)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine("\n");

            #endregion

            #region Dictionary<TKey, TValue>
            // Associative array / maps or dictionaries
            // an abstract data type that can hold data in (key, value) pairs
            Dictionary<string, int> memberRank = new Dictionary<string, int>();

            memberRank.Add("Alex", 3);
            memberRank.Add("Hank", 10);
            memberRank.Add("Frank", 5);
            memberRank.Add("Ricky", 7);
            memberRank.Add("Stacy", 3);
            memberRank.Add("Miya", 8);
            memberRank.Add("Mickey", 9);

            // Collection cannot contain duplicate keys.
            // memberRank.Add("Hank", 13);

            var rank_of_alex = memberRank["Alex"];

            foreach (var item in memberRank)
            {
                Console.WriteLine(item.Key + " "+ item.Value);
            }

            memberRank.Remove("Alex");

            

            Console.ReadLine();

            #endregion

            #region SortedList<TKey, TValue>
            // the keys array is always sorted

            SortedList<int, string> ScoreRanks = new SortedList<int, string>();
            ScoreRanks.Add(10, "Phinex");
            ScoreRanks.Add(21, "Drogon");
            ScoreRanks.Add(34, "Dinosaur");
            ScoreRanks.Add(78, "Heliena");
            ScoreRanks.Add(67, "RobotCat");
            ScoreRanks.Add(13, "Pokymon");

            foreach (var item in ScoreRanks)
            {
                Console.Write(item.Key + ": ");
                Console.WriteLine(item.Value);
            }

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
