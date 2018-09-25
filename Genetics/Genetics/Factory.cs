using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace Genetics
{
    public static class Factory
    {
        #region Attributes

        private static List<Player> _listPlayer;
        private static string _pathLoad;
        private static string _pathSave;
        
        //
        //
        private const int nbThread = 1;
        private static int timeout;
        private static float initPosX;
        private static float initPosY;

        #endregion

        #region Getters

        public static List<Player> GetListPlayer()
        {
            return _listPlayer;
        }

        public static void SetListPlayer(List<Player> li)
        {
            _listPlayer = li;
        }

        public static Player GetBestPlayer()
        {
            Player best = new Player();
            foreach (Player p in _listPlayer)
            {
                if (p > best)
                    best = p;
            }
            Console.WriteLine(best.GetScore());

            return best;
        }

        public static Player GetNthPlayer(int nth)
        {

            return _listPlayer[nth];
        }

        #endregion

        #region LoadAndSave

        public static void SetPathLoad(string path)
        {
            _pathLoad = path;
        }

        public static void SetPathSave(string path)
        {
            _pathSave = path;

        }

        public static void SetPathLoadAndSave(string path)
        {
            SetPathLoad(path);         
            SetPathSave(path);
        }

        public static String GetPathLoad()
        {
            return _pathLoad;
        }
        public static String GetPathSave()
        {
            return _pathSave;
        }

        public static void SaveState()
        {
            if(_pathSave == "") throw new ArgumentNullException();
            SaveAndLoad.Save(_pathSave, _listPlayer);
        }

        #endregion

        #region Init

        public static void InitNew(int size = 200)
        {
            _listPlayer = new List<Player>();
            for (int i = 0; i < size; i++)
            {
                _listPlayer.Add(new Player());
            }
        }

        public static void Init()
        {
            if (File.Exists(_pathLoad))
                _listPlayer = SaveAndLoad.Load(_pathLoad);
            else
                InitNew();
            
            Map map = RessourceLoad.GetCurrentMap();
            timeout = map.Timeout;
            initPosX = map.PosInit.X;
            initPosX = map.PosInit.Y;
        }

        #endregion

        #region Display

        public static void PrintScore()
        {
            int n = 0;
            foreach (Player p in _listPlayer)
            {
                Console.WriteLine("Player {0} has a score of {1}", n, p.GetScore());
                n++;
            }
                
        }

        #endregion

        #region Training

        //Train a portion of the player set
        public static void ThreadTraining(int from, int to)
        {   
            for (int i = from; i < to && i < _listPlayer.Count; i++)
            {
                Player p = _listPlayer[i];
                p.ResetScore();
                p.Setposition(initPosX, initPosY);
                
                for (int f = 0; f < timeout; f++)
                    p.PlayAFrame();
            }
        }
        
        public static void TrainWithNew(int generationNumber)
        {
            Train(generationNumber, false);
        }

        public static void Train(int generationNumber, bool replaceWithMutation = true)
        {
            
            int nbPlayerPerThread = _listPlayer.Count/nbThread;

            //Initialisation des thread (task)
            
            Task[] tasks = new Task[nbThread];
            
            Console.WriteLine("New training set for {0} genrerations. Mutation : {1}", generationNumber, replaceWithMutation ? "Yes" : "No");
            //On lance les threads
            for (int b = 0; b < generationNumber; b++)
            {   
                Console.Write("Gen {0} >", b);
                /*for (int i = 0; i < nbThread; i++)
                    tasks[i] = Task.Run(() => ThreadTraining(i * nbPlayerPerThread, (i + 1) * nbPlayerPerThread - 1));
                

                Task.WaitAll(tasks);*/
                //ThreadTraining(0, nbPlayerPerThread);
                for (int i = 0; i < _listPlayer.Count; i++)
                {
                    Player p = _listPlayer[i];
                    p.ResetScore();
                    p.Setposition(initPosX, initPosY);
                
                    for (int f = 0; f < timeout; f++)
                        p.PlayAFrame();
                }
                Regenerate(replaceWithMutation);
            }
            Console.WriteLine();
        }

        private static void Regenerate(bool replace_with_mutation = true)
        {
            SimpleSort();
            int count = _listPlayer.Count;
            for (int i = count / 2; i < count; i++)
            {
                _listPlayer[i].Replace(_listPlayer[i - count/ 2], replace_with_mutation);
            }
        }


        public  static void SimpleSort()
        {
            Quicksort(_listPlayer, 0, _listPlayer.Count - 1);
        }
        
        //From http://snipd.net/quicksort-in-c
        public static void Quicksort(List<Player> elements, int left, int right)
        {
            int i = left, j = right;
            Player pivot = elements[(left + right) / 2];
 
            while (i <= j)
            {
                while (elements[i] > pivot)
                {
                    i++;
                }
 
                while (elements[j] < pivot)
                {
                    j--;
                }
 
                if (i <= j)
                {
                    // Swap
                    Player tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;
 
                    i++;
                    j--;
                }
            }
 
            // Recursive calls
            if (left < j)
            {
                Quicksort(elements, left, j);
            }
 
            if (i < right)
            {
                Quicksort(elements, i, right);
            }
        }

        #endregion
    }
}