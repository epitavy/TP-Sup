using System;
using System.Xml.Schema;

namespace WestWorldTycoon
{
    public class Tile
    {
        
        public enum Biome
        {
            SEA, MOUNTAIN, PLAIN
        }

        private Biome biome;
        private Building building;
        
        public Tile(Biome b)
        {
            biome = b;
            building = null;
        }

        
        public Tile(Tile tile)
        {
            biome = tile.biome;
            if (tile.building == null)
                building = null;
            else
            {
                switch (tile.building.Type)
                {
                    case Building.BuildingType.ATTRACTION:
                        building = new Attraction(tile.building as Attraction);
                        break;
                    case Building.BuildingType.HOUSE:
                        building = new House(tile.building as House);
                        break;
                    case Building.BuildingType.SHOP:
                        building = new Shop(tile.building as Shop);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        
        public bool Build(ref long money, Building.BuildingType type)
        {

            if (biome != Biome.PLAIN || building != null)
                return false;
                
            
            long cost;
            
            switch (type)
            {
                case Building.BuildingType.ATTRACTION:
                    cost = Attraction.BUILD_COST;
                    break;
                case Building.BuildingType.HOUSE:
                    cost = House.BUILD_COST;
                    break;
                case Building.BuildingType.SHOP:
                    cost = Shop.BUILD_COST;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (money - cost < 0)
                return false;
                

            money -= cost;
            switch (type)
            {
                case Building.BuildingType.ATTRACTION:
                    building = new Attraction();
                    break;
                case Building.BuildingType.HOUSE:
                    building = new House();
                    break;
                case Building.BuildingType.SHOP:
                    building = new Shop();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            return true;
        }


        public bool Upgrade(ref long money)
        {
            if (building != null)
            {
                Building.BuildingType type = building.Type;
                if (type == Building.BuildingType.ATTRACTION)
                    return ((Attraction) building).Upgrade(ref money);
                if (type == Building.BuildingType.HOUSE)
                    return ((House) building).Upgrade(ref money);
                if (type == Building.BuildingType.SHOP)
                    return ((Shop) building).Upgrade(ref money);
            }
            return false;
        }
        
        
        public long GetHousing()
        {
            if (building != null && building.Type == Building.BuildingType.HOUSE)
                return ((House) building).Housing();
            return 0;
        }
        
        
        public long GetAttractiveness()
        {
            if (building != null && building.Type == Building.BuildingType.ATTRACTION)
                return ((Attraction) building).Attractiveness();
            return 0;
        }
        
        
        public long GetIncome(long population)
        {
            if (building != null && building.Type == Building.BuildingType.SHOP)
                return ((Shop) building).Income(population);
            return 0;
        }


        public bool Destroy()
        {
            if (building != null)
            {
                building = null;
                return true;
            }

            return false;

        }

        public bool IsBuildable()
        {
            return building == null && biome == Biome.PLAIN;
        }

        public Biome GetBiome => biome;
        public Building GetBuilding => building;
    }
}