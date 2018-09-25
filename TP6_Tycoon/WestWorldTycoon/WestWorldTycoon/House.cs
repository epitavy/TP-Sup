using System;

namespace WestWorldTycoon
{
    public class House : Building
    {
        public const long BUILD_COST = 250;
        public static readonly long[] UPGRADE_COST ={ 750, 3000, 10000 };
        public static readonly long[] HOUSING ={ 300, 500, 650, 750 };
        private int lvl;
        
        
        
        public House()
        {
            type = BuildingType.HOUSE;
            lvl = 0;
        }


        public House(House house)
        {
            type = house.type;
            lvl = house.lvl;
        }

        
        public long Housing()
        {
            return HOUSING[lvl];
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