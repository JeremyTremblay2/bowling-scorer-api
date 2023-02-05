using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGrpcClient
{
    public static class Prompter
    {
        public static Int32 PromptInt()
        {
            Int32 resp;
            while (true)
            {
                try
                {
                    resp = Convert.ToInt32(Console.ReadLine());
                    return resp;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Write a number !");
                    Console.WriteLine("Your choice : ");
                    continue;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Write a number !");
                    Console.WriteLine("Your choice : ");
                    continue;
                }
            }
        }
    }
}
