using System;

namespace Exceptions
{
    public static class Base
    {
        public static int Fibonacci(int n)
        {
            if (n < 0)
                throw new ArgumentException("Argument must be positive");

            if (n == 1 || n == 2)
                return 1;
            
            int fibo = 0;
            int f1 = 0;
            int f2 = 1;

            for (int i = 1; i < n; i++)
            {
                fibo = f1 + f2;
                f1 = f2;
                f2 = fibo;
            }
            return fibo;
        }

        public static float DegToRad(float angle)
        {
            if(angle > 180 || angle < -180)
                throw new ArgumentException("Angle must be between -180 deg and 180 deg");
            return (float) (angle /180 * Math.PI);
        }
        
        public static float RadToDeg(float angle)
        {
            if(angle > Math.PI || angle < -Math.PI)
                throw new ArgumentException("Angle must be between -pi rad and pi rad");
            return (float) (angle / Math.PI * 180);
        }
        
        public static float Pow(float n, int p)
        {
            if(n == 0 && p < 0)
                throw new OverflowException("You reach infinity!");
            if (p == 0)
                return 1;
            if (n == 0)
                return 0;
            if (p < 0)
                return 1 / Pow(n, -p);
            
            float res = 1;
            for (; p > 0; p--)
            {
                res *= n;
                if(res == float.PositiveInfinity || res == float.NegativeInfinity)
                    throw new OverflowException("Your result is too hight");
            }

            return res;
        }
    }
}