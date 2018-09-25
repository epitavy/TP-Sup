using System;

namespace WestWorldTycoon
{
    public class Attraction :Building
    {
        public const long BUILD_COST = 10000;
        public static readonly long[] UPGRADE_COST = { 5000, 10000, 45000 };
        public static readonly long[] ATTRACTIVENESS = { 500, 1000, 1300, 1500 };
        private int lvl;



        public Attraction()
        {
            type = BuildingType.ATTRACTION;
            lvl = 0;
        }



        public Attraction(Attraction attraction)
        {
            type = attraction.type;
            lvl = attraction.lvl;
        }

        public long Attractiveness()
        {
            return ATTRACTIVENESS[lvl];
        }


        public bool Upgrade(ref long money)
        {
            if (lvl == 3)
                return false;
            
            long cost = UPGRADE_COST[lvl];
            if (money - cost < 0)
                return false;
            money -= cost;
            lvl++;
            return true;
        }


        public int Lvl => lvl;
    }
}
