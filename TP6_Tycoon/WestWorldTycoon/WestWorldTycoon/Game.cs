using System;
using System.Resources;

namespace WestWorldTycoon
{
    public class Game
    {
        private long score;
        private long money;
        
        private int nbRound;
        private int round;

        public string action_order;
        public Game parent;
        
        private Map map;

        public Game(string name, int nbRound, long initialMoney)
        {
            this.nbRound = nbRound;
            round = 1;
            money = initialMoney;
            map = new Map(name);
            score = 0;
            parent = null;
            action_order = "";
            TycoonIO.GameInit(name, nbRound, initialMoney);
        }

        public Game(Game game)
        {
            map = new Map(game.map);
            money = game.money;
            score = game.score;
            round = game.round;
            nbRound = game.nbRound;
            action_order = game.action_order;
            parent = game.parent;
        }


        public long Launch(Bot bot)
        {
            bot.Start(this);
            while(round <= nbRound)
            {
                bot.Update(this);
                Update();
            }
            bot.End(this);

            return score;
        }
        
        
        public void Update()
        {
            long gain = map.GetIncome(map.GetPopulation());
            money += gain;
            score += gain;
            round++;
            TycoonIO.GameUpdate();
        }

        public void UpdateBacktracking()
        {
            parent = new Game(this);
            long gain = map.GetIncome(map.GetPopulation());
            money += gain;
            score += gain;
            round++;
            action_order = "";
        }


        public bool Build(int i, int j, Building.BuildingType type)
        {
            if (map.Build(i, j, ref money, type))
            {
                TycoonIO.GameBuild(i, j, type);
                return true;
            }
            return false;
        }


        public bool Destroy(int i, int j)
        {
            if (map.Destroy(i, j))
            {
                TycoonIO.GameDestroy(i, j);
                return true;
            }
            return false;
        }
        
        public bool Upgrade(int i, int j)
        {
            if (map.Upgrade(i, j, ref money))
            {
                TycoonIO.GameUpgrade(i, j);
                return true;
            }
            return false;
        }

        public bool TryBuild(Building.BuildingType type)
        {
            return map.TryBuild(ref money, type);
        }

        public bool TryUpgrade(Building.BuildingType type)
        {
            return map.TryUpgrade(ref money, type);
        }

        public long Money => money;
        public long Score => score;
        public int NbRound => nbRound;
        public int Round => round;
        
        public Map Map => map;
    }
}