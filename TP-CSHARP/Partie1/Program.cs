using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Partie1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Fibo
            Console.WriteLine("Fibo 0: {0}", Fibo(0));
            Console.WriteLine("Fibo 1: {0}", Fibo(1));
            Console.WriteLine("Fibo 2: {0}", Fibo(2));
            Console.WriteLine("Fibo 6: {0}", Fibo(6));
            Console.WriteLine("Fibo 42: {0}", Fibo(42));

            // Fact
            Console.WriteLine("\nFact 0: {0}", Fact(0));
            Console.WriteLine("Fact 1: {0}", Fact(1));
            Console.WriteLine("Fact 2: {0}", Fact(2));
            Console.WriteLine("Fact 3: {0}", Fact(3));
            Console.WriteLine("Fact 6: {0}", Fact(6));
            Console.WriteLine("Fact 12: {0}", Fact(12));
            
            // Min
            long[] tab = new long[] {1, 42, -42, 35};
            Console.WriteLine("Min [1, 42, -42, 35]: {0}", MinInTab(tab));
            
            // Sum
            long[,] mat1 = new long[,]
            {
                {61,3,34},
                {31,4,-7},
                {-7,8,2}
            };
            long[,] mat2 = new long[,]
            {
                {0,2,-1},
                {-8,4,26},
                {-2,-8,42}
            };
            Console.Write("Sum: ");
            PrintMat(Sum(mat1, mat2));
            Console.WriteLine();

            // Parite
            List<int> l1 = new List<int>(new int[] { 4, 5, 7, 3, 2, 5});
            Console.WriteLine(Parite(l1));

            int x = 3;
            int y = 42;
            Swap(ref x, ref y);
            Console.WriteLine("x: {0}  y: {1}", x, y);
            
            Console.WriteLine("Nombre de bits a 1 dans 3 : {0}", NbBitsSet(3));
            Console.WriteLine("Nombre de bits a 1 dans 23 : {0}", NbBitsSet(23));
            Console.WriteLine("Nombre de bits a 1 dans 64 : {0}", NbBitsSet(64));
        }

        private static void PrintMat(long[,] mat)
        {
            for(int i = 0; i < mat.GetLength(0); i++)
            {
                Console.Write("\n[");
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    Console.Write(mat[i,j] + ",");
                }
                Console.Write("]");
            }
        }
        private static long Fibo(long n)
        {
            long f1 = 0;
            long f2 = 1;
            long f3 = 0;
            
            while (n > 1)
            {
                f3 = f1 + f2; 
                f1 = f2;      
                f2 = f3;      
                n--;
            }
            return f3;
        }

        

        private static long Fact(long n)
        {
            long fact = 1;

            while (n > 1)
            {
                fact *= n;
                n--;
            }
            return fact;
        }

        private static void Swap(ref int x, ref int y)
        {
            int t = x;
            x = y;
            y = t;
        }
        
        private static long MinInTab(long[] tab)
        {
            long min = tab[0];

            foreach (long e in tab)
            {
                if (e < min)
                    min = e;
            }
            return min;
        }

        private static long[,] Sum(long[,] mat1, long[,] mat2)
        {
            long[,] mat_sum = new long[mat1.GetLength(0),mat1.GetLength(1)];
            for(int i = 0; i < mat1.GetLength(0); i++)
            {
                for (int j = 0; j < mat1.GetLength(1); j++)
                {
                    mat_sum[i, j] = mat1[i, j] + mat2[i, j];
                }
            }
            return mat_sum;
        }

        private static bool Parite(List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] % 2 != i % 2)
                    return false;
            }
            return true;
        }

        private static uint NbBitsSet(long number)
        {
            long nbrOne = 0;
            while (number != 0)
            {
                nbrOne += number & 1;
                number >>= 1;
            }
            return (uint)nbrOne;
        }
    }
}