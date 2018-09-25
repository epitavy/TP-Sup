using System;

namespace Partie2
{
    public class Sup : Student
    {
        private static int nbSup = 0;

        public Sup(string name, int life, int damage, int physicalArmor, int magicalArmor) 
            : base(name, life, damage, false, physicalArmor, magicalArmor)
        {
            nbSup++;
        }

        public static void DisplayNbSup()
        {
            Console.WriteLine("There are {0} Sup(s).",nbSup);
        }

        public void Status()
        {
            Console.WriteLine("{0} : HELP ME, I'm the youngest and taller guys are attacking me! I have {1} HP left", Name, Life);
        }
    }
}