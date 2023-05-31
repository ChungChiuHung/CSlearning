using System;

namespace LambdaExpression
{
    internal class Program
    {
        static void Main(string[] args)
        {           
            while (true) 
            {

                Loop();
                Console.ReadLine();
                Console.Clear();
            }

        }

        static void Loop()
        {
            // Func <TResult> : no parameter
            // Func <T1, T2, ..., TResult>
            // Func < input data type, input data type,..., return data type>

            Func<int, int, string> sum = delegate (int a, int b) { return (a + b).ToString(); };
            Console.WriteLine(sum(3, 4));

            // Impliment the Lambda Expression
            Func<int, int, int> sum_2 = (a, b) => a + b;
            Console.WriteLine(sum_2(5, 6));

            Func<int> getRandomNumber = () =>
            {
                Random random = new Random();
                return random.Next(1, 100);
            };

            int randomNumber = getRandomNumber(); // Invoke the Func delegate

            Console.WriteLine("Random number: " + randomNumber);


            Func<int> str = () => myMethod("Hello");
            Console.WriteLine(str());

            Func<int> getRandomNumber_2 = () => GetRandomNumber();

            int randomNumber_2 = getRandomNumber_2(); // Invoke the Func delegate

            Console.WriteLine("Random number: " + randomNumber_2);

            // Action : no parameter
            // Action<T1,T2,...>
            Action greet = () =>
            {
                Console.WriteLine("Hello, world!");
                Console.WriteLine("An exmpale that demonstrate the usage of Action\n " +
                    "delegate with no parameters and return values");
            };

            greet(); // Invoke the Action delegate


            Action del = () => myMethod("Pokymon is got!");

            del(); // Invoke the Action delegate

            // Another example with an anonymous method
            Action printMessage = delegate ()
            {
                Console.WriteLine("This is a message.");
            };

            printMessage(); // Invoke the Action delegate

            // Console.ReadLine();


            
            Action<int, int> sum_3 = (a, b) => Console.WriteLine($"The result: {a + b}");
            sum_3(3, 4);

            // Implement the Lambda Expression
            Action<int, int> sum_4 = (a, b) => Console.WriteLine(a + b);
            sum_4(5, 6);
        }


        static int myMethod(string message)
        {
            Console.WriteLine(message);
            return 0;
        }

        static int GetRandomNumber()
        {
            Random random = new Random();
            return random.Next(1, 200);
        }
    }
}
