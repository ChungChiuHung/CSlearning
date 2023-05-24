using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LambdaExpression
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<int, int, int> sum = delegate (int a, int b) { return a + b; };
            Console.WriteLine(sum(3,4));

            // Impliment the Lambda Expression
            Func<int, int, int> sum_2 = (a, b) => a + b;
            Console.WriteLine(sum_2(5,6));

            Func<int> str = () => myMethod("Hello");
            Console.WriteLine(str());

            Console.ReadLine();
        }

        static int myMethod(string message)
        {
            Console.WriteLine(message);
            return 0;
        }
    }
}
