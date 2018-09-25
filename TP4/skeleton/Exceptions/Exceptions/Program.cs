using System;

namespace Exceptions
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            for (float i = -4; i < 5; i += 1f)
            {
                try
                {
                    Console.WriteLine(Base.Pow(i,-1));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            
        }
    }
}