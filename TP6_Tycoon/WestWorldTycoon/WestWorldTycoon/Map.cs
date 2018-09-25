using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;

namespace WestWorldTycoon
{
    public class Map
    {
        private string name;
        private Tile[,] matrix;
        
        public Map(string name)
        {
            this.name = name;
            matrix = TycoonIO.ParseMap(name);
        }
        
        
        public Map(Map map)
        {
            name = map.name;
            matrix = new Tile[map.matrix.GetLength(0),map.matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i,j] = new Tile(map.matrix[i,j]);
                }
            }
            
        }


        public bool Build(int i, int j, ref long money, Building.BuildingType type)
        {
            if (i >= matrix.GetLength(0) || j >= matrix.GetLength(1))
                return false;
                
            return matrix[i, j].Build(ref money, type);
        }


        public bool Destroy(int i, int j)
        {
            return matrix[i, j].Destroy();
        }

        public bool Upgrade(int i, int j, ref long money)
        {
            return matrix[i, j].Upgrade(ref money);
        }
        
        
        public long GetAttractiveness()
        {
            long attractiveness = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                    attractiveness += matrix[i, j].GetAttractiveness();
            }
            return attractiveness;
        }

        
        public long GetHousing()
        {
            long housing = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                    housing += matrix[i, j].GetHousing();
            }
            return housing;
        }


        public long GetPopulation()
        {
            long max_pop = GetHousing();
            long actual_pop = GetAttractiveness();

            return max_pop < actual_pop ? max_pop : actual_pop;
        }
        
        
        public long GetIncome(long population)
        {
            long income = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                    income += matrix[i, j].GetIncome(population);
            }
            return income;
        }

        public bool TryBuild(ref long money, Building.BuildingType type)
        {
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (matrix[i, j].Build(ref money, type))
                        return true;
                }
            }
            return false;
        }
        
        public bool TryUpgrade(ref long money, Building.BuildingType type)
        {
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);
            int lvlMin = 3;
            int mini = 0;
            int minj = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Tile ti = matrix[i, j];
                    if (ti.GetBuilding != null && ti.GetBuilding.Type == type && GetLvl(i, j) < lvlMin)
                    {
                        lvlMin = GetLvl(i, j);
                        mini = i;
                        minj = j;
                        if(lvlMin == 0)
                            break;
                    }
                }
                if(lvlMin == 0)
                    break;
            }
            return Upgrade(mini, minj, ref money);
        }

        public int GetLvl(int i, int j)
        {
            Building b = matrix[i,j].GetBuilding;
            if (b == null)
                return -1;
            switch (b.Type)
            {
                case Building.BuildingType.ATTRACTION:
                    return ((Attraction)b).Lvl;
                case Building.BuildingType.HOUSE:
                    return ((House)b).Lvl;
                case Building.BuildingType.SHOP:
                    return ((Shop)b).Lvl;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool[] IsMapFull()
        {
            bool[] btab = {true, true};
            int i = 0;
            while(i < matrix.GetLength(0) && btab[0])
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == null)
                    {
                        btab[0] = false;
                        btab[1] = false;
                        break;
                    }
                        
                    if (GetLvl(i, j) != 3)
                        btab[1] = false;
                }
                i++;
            }
            return btab;
        }

        public int NbBuildableTile()
        {
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);
            int count = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                    if (matrix[i, j].IsBuildable())
                        count++;
            }

            return count;
        }
        
        public Building.BuildingType GetBuildingType(int i, int j)
        {
            Building b = matrix[i,j].GetBuilding;
            if (b == null)
                return Building.BuildingType.NONE;
            return b.Type;
        }
        
        public Tile[,] Matrix => matrix;
    }
}