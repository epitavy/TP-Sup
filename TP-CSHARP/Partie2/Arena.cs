using System;
using System.Collections;
using System.Collections.Generic;

namespace Partie2
{
    public class Arena
    {
        public Stack<Fight> matchUp = new Stack<Fight>();


        public Arena(uint nbFight)
        {
            Random random = new Random();
            string[] nameAcdc = {"Florian", "Julien", "Erwan", "Alexandre", "Maxence", "Sam", "Juliette", "Danae", "Thomas", "Robin", "Maxime", "Hugo", "Cyril"};
            string[] nameSup = {"Eliaz", "Theo", "Ines", "Matthieu", "Jules", "Selim", "Sara", "Baptise", "Corentin","Jean-Gabriel", "Anthony","Bora","Gabriel","Yohan" };

            for (int i = 0; i < nbFight; i++)
            {
                Sup sup = new Sup(nameSup[random.Next(nameSup.Length)], random.Next(50,100), random.Next(11),
                    random.Next(11), random.Next(11));
                ACDC acdc = new ACDC(nameAcdc[random.Next(nameAcdc.Length)], random.Next(55,100), random.Next(11),
                    random.Next(1,11), random.Next(1,11));
                Fight fight = new Fight(sup, acdc);

                matchUp.Push(fight);
            }
        }

        public void ResolveFights()
        {
            int winACDC = 0;
            int winSup = 0;
            int draw = 0;
            while (matchUp.Count > 0)
            {
                Fight fight = matchUp.Pop();
                while(!fight.isFinished())
                    fight.Update(false);
                if (fight.GetWinner() != null)
                {

                    if (fight.GetWinner().GetType() == typeof(ACDC))
                        winACDC++;
                    else 
                        winSup++;
                }
                else draw++;
            }
            
            Console.WriteLine("ACDC: {0} wins / Sups: {1} wins / Draws: {2}",winACDC, winSup, draw);
        }
    }
}