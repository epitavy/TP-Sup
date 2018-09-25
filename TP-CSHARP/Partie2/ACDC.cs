using System;

namespace Partie2
{
    public class ACDC : Student
    {
        private static int nbACDC = 0;
        
        public ACDC(string name, int life, int damage, int physicalArmor, int magicalArmor) 
            : base(name, life, damage, true, physicalArmor, magicalArmor)
        {
            nbACDC++;
        }

        public static void DisplayNbACDC()
        {
            Console.WriteLine("There are {0} ACDC.", nbACDC);
        }

        public void Status()
        {
            Console.WriteLine("{0}: You probably can’t beat me, I have {1} HP left.", Name, Life);
        }
    }
}