using System;
using System.Diagnostics.Eventing.Reader;

namespace Partie2
{
    public class Fight
    {
        private uint round;
        private Student student1;
        private Student student2;
        
        public Fight(Student student1, Student student2)
        {
            this.student1 = student1;
            this.student2 = student2;
            
            Console.WriteLine("ACDC {1} VERSUS Sup {0}\n---------------\n\n", student1.Name, student2.Name);
        }

        public uint Round => round;

        public bool isFinished()
        {
            return student1.Life == 0 || student2.Life == 0;
        }

        public void Update(bool verbose)
        {
            student1.Attack(student2);
            student2.Attack(student1);

            round ++;

            if(verbose)
            {
                Console.WriteLine("- Round {0}\n", round);
                student1.Status();
                student2.Status();
            }
        }

        public Student GetWinner()
        {
            if (student1.Life == 0 && student2.Life == 0)
                return null;
            if (student1.Life == 0)
                return student2;
            return student1;
        }

        public void Finish()
        {
            if (GetWinner() != null)
                Console.WriteLine("The fight is done.\n{0} won this fight!", GetWinner().Name);
            else
                Console.WriteLine("No one won this fight.");
        }
    }
}