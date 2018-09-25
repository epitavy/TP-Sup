using System;

namespace WestWorldTycoon
{
    public class Shop : Building
    {
        
        public const long BUILD_COST = 300;
        public static readonly long[] UPGRADE_COST = { 2500, 10000, 50000 };
        public static readonly long[] INCOME = { 7, 8, 9, 10 };
        private int lvl;

        
        public Shop()
        {
            type = BuildingType.SHOP;
            lvl = 0;
        }


        public Shop(Shop shop)
        {
            type = shop.type;
            lvl = shop.lvl;
        }
        
        
        public long Income(long population)
        {
            return INCOME[lvl] * population /100;
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
        
        
        public int Lvl
        {
            get { return lvl; }
        }
    }
}