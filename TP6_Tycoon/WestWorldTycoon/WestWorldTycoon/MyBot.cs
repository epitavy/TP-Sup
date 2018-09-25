using System;

namespace WestWorldTycoon
{
    public class MyBot : Bot
    {
        private int width;
        private int height;

        private int round;
        
        int lvlMinAt;
        int lvlMinHo;
        int lvlMinSh;

        private static int[][] turntab = new int[38][];


        public override void Start(Game game)
        {
            turntab[0] = new[] {0, 2, 0, 0, 0, 0,0,0}; //Turn 34
            turntab[1] = new[] {0, 0, 0, 0, 0, 0,0,0};
            turntab[2] = new[] {0, 0, 0, 0, 0, 0,0,0};
            turntab[3] = new[] {2, 0, 0, 0, 0, 1,0,0}; // Turn 37
            turntab[4] = new[] {0, 10, 0, 0, 0, 0,0,0};
            turntab[5] = new[] {0, 6, 0, 0, 0, 0,0,0};
            turntab[6] = new[] {0, 0, 0, 0, 0, 0,0,0}; //Turn 40
            turntab[7] = new[] {1, 0, 1, 0, 0, 0,0,0};
            turntab[8] = new[] {2, 2, 0, 0, 0, 1,0,0};
            turntab[9] = new[] {0, 28, 0, 0, 0, 0,0,0};
            turntab[10] = new[] {2, 7, 1, 0, 0, 0,0,0};
            turntab[11] = new[] {3, 3, 1, 0, 0, 1,0,0}; //Turn 45
            turntab[12] = new[] {5, 10, 1, 0, 0, 2,0,0};
            turntab[13] = new[] {7, 22, 2, 0, 0, 2,0,0};
            turntab[14] = new[] {13, 3, 3, 0, 0, 3,0,0};
            turntab[15] = new[] {17, 10, 6, 0, 0, 6,0,0};
            turntab[16] = new[] {29, 0, 11, 7, 0, 10, 0, 21};//Turn 50
            turntab[17] = new[] {0, 0, 12, 63, 0, 10, 5, 6};
            turntab[18] = new[] {0, 0, 0, 42, 9, 20,0,0};
            turntab[19] = new[] {0, 0, 0, 44, 5, 22,0,0};
            turntab[20] = new[] {0, 0, 0, 13, 0, 6,0,0};
            turntab[21] = new[] {0, 0, 0, 14, 0, 7,0,0};//Turn 55
            turntab[22] = new[] {0, 0, 0, 12, 3, 7,0,0};
            turntab[23] = new[] {0, 0, 0, 13, 4, 7,0,0};
            turntab[24] = new[] {0, 0, 0, 15, 2, 7,0,0};
            turntab[25] = new[] {0, 0, 0, 11, 60, 5,0,0};
            turntab[26] = new[] {0, 0, 0, 0, 78, 0,0,0};//Turn 60
            turntab[27] = new[] {0, 0, 0, 0, 57, 0,0,0};
            turntab[28] = new[] {0, 0, 0, 0, 25, 0,0,0};
            turntab[29] = new[] {0, 0, 0, 0, 12, 0,0,0};
            turntab[30] = new[] {0, 0, 0, 0, 13, 0,0,0};
            turntab[31] = new[] {0, 0, 0, 0, 12, 0,0,0};//Turn 65
            turntab[32] = new[] {0, 0, 0, 0, 13, 0,0,0};
            turntab[33] = new[] {0, 0, 0, 0, 13, 0,0,0};
            turntab[34] = new[] {0, 0, 0, 0, 13, 0,0,0};
            turntab[35] = new[] {0, 0, 0, 0, 14, 0,0,0};
            turntab[36] = new[] {0, 0, 0, 0, 13, 0,0,0};//Turn 70
            turntab[37] = new[] {0, 0, 0, 0, 5, 0,0,0};
            
            

            width = game.Map.Matrix.GetLength(1);
            height = game.Map.Matrix.GetLength(0);
            round = 0;
            lvlMinAt = 0;
            lvlMinHo = 0;
            lvlMinSh = 0;
            

            game.Build(3, 3, Building.BuildingType.HOUSE);
            game.Build(4, 3, Building.BuildingType.ATTRACTION);
            game.Build(5, 4, Building.BuildingType.SHOP);
            game.Build(6, 4, Building.BuildingType.SHOP);
        }

        public override void Update(Game game)
        {
            round++;
            if (round < 37 || round > 50)
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (round == 4)
                            game.Build(i, j, Building.BuildingType.HOUSE);
                        if (round > 7 && round < 34)
                            game.Build(i, j, Building.BuildingType.SHOP);
                    }
                }
            }

            if (round > 33 && round < 72)
            {
                int[] turn = turntab[round - 34];
                Console.WriteLine("Turn {0} ok:{1}", round ,ApplyTurn(game, turn[0], turn[1], turn[2], turn[3], turn[4], turn[5], turn[6], turn[7]));
            }
        }

        public override void End(Game game)
        {
            // Nothing to do...
        }

        public bool ApplyTurn(Game game, int buHo, int buSh, int buAt, int upHo, int upSh, int upAt, int deHo,
            int deSh)
        {
            //game, build house, build shop, build attraction, upgrade house, upgrade shop, upgrade attraction, destroy...
            
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    //Destroy
                    if (deHo > 0 && game.Map.GetBuildingType(height - i - 1, width - j - 1) == Building.BuildingType.HOUSE &&
                        game.Destroy(height - i - 1, width - j - 1))
                        deHo--;
     
                    if (deSh > 0 && game.Map.GetBuildingType(height - i - 1, width - j - 1) == Building.BuildingType.SHOP &&
                        game.Destroy(height - i - 1, width - j - 1))
                        deSh--;
                        

                    //Build
                    if (buHo > 0 && game.Build(i, j, Building.BuildingType.HOUSE))
                        buHo--;
                    if (buAt > 0 && game.Build(i, j, Building.BuildingType.ATTRACTION))
                        buAt--;
                    if (buSh > 0 && game.Build(i, j, Building.BuildingType.SHOP))
                        buSh--;

                    //Upgrade
                    if (upHo > 0 && game.Map.GetBuildingType(i, j) == Building.BuildingType.HOUSE &&
                        game.Map.GetLvl(i, j) == lvlMinHo && game.Upgrade(i, j))
                        upHo--;
                    if (upAt > 0 && game.Map.GetBuildingType(i, j) == Building.BuildingType.ATTRACTION &&
                        game.Map.GetLvl(i, j) == lvlMinAt && game.Upgrade(i, j))
                        upAt--;
                    if (upSh > 0 && game.Map.GetBuildingType(i, j) == Building.BuildingType.SHOP &&
                        game.Map.GetLvl(i, j) == lvlMinSh && game.Upgrade(i, j))
                        upSh--;

                }
            }

            if (upHo > 0)
                lvlMinHo++;
            if (upAt > 0)
                lvlMinAt++;
            if (upSh > 0)
                lvlMinSh++;
            if ((upHo > 0 || upSh > 0 || upAt > 0) && lvlMinAt < 3 && lvlMinSh < 3 && lvlMinHo < 3)
                ApplyTurn(game, buHo, buSh, buAt, upHo, upSh, upAt, deHo, deSh);
            
            return buHo == 0 && buSh == 0 && buAt == 0 && upHo == 0 && upSh == 0 && upAt == 0 && deHo == 0 && deSh == 0;
        }
    }
}