using System;

namespace Partie2
{
    public class Student
    {
        private string name;
        private int life;
        private int damage;
        public bool isMagician;
        public int physicalArmor;
        public int magicalArmor;

        private static int nbStudent = 0;
        

        public Student(string name, int life, int damage, bool isMagician, int physicalArmor, int magicalArmor)
        {
            this.name = name.ToUpper();
            this.life = life;
            this.damage = damage;
            this.isMagician = isMagician;
            this.physicalArmor = physicalArmor;
            this.magicalArmor = magicalArmor;
            nbStudent++;
            
            Status();
            Console.WriteLine("I hit with a damage of {0} "+ (isMagician ? "\nI'm a magician.\n" : "\nI'm not a magician.\n"),damage);
        }
        
        // Static functions

        public static void DisplayNbStudent()
        {
            Console.WriteLine("There is(are) {0} student(s).", nbStudent);
        }
        
        
        // Methodes

        public void TakeDamage(int damage, bool isMagical)
        {
            if (isMagical)
                Life -= (damage - magicalArmor) < 1 ? 1 : (damage - magicalArmor);
            else
                Life -= (damage - physicalArmor) < 1 ? 1 : (damage - physicalArmor);
        }
        
        public void Attack(Student s)
        {
            if (s.Equals(this))
            {
                throw new Exception("You can't attack yourself! You will just died...");
            }
            s.TakeDamage(Damage, isMagician);
        }

        public virtual void Status()
        {
            Console.WriteLine("My name is {0}, I still have {1} HP",name, life);
        }
        
        // Getters & Setters
        
        
        public int Life
        {
            get { return life; }
            set { life = Math.Max(0, value); }
        }
        
        public string Name
        {
            get { return name; }
            set { name = value.ToUpper(); }
        }

        public int Damage
        {
            get { return damage; }
            set
            {
                if(value < 1)
                    damage = 1;
                else if (value > 10)
                    damage = 10;
                else damage = value;
            }
        }
        
    }
}