using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WestWorldTycoon
{
    public class MyAIBot : Bot
    {
        private const int TOUR = 2;
        
        private int round;
        public override void Start(Game game)
        {
            round = 0;
        }

        public override void Update(Game game)
        {
            Stopwatch swh = new Stopwatch();
            swh.Start();
            round++;
            int[] action_turn = ConvertToActionTab(MyAStar(game));
            long time = swh.ElapsedMilliseconds;
            Console.WriteLine("Round {0} : MyAStar best action found in {1} ms. Apply : {2}",round, time, ApplyTurn(game, action_turn[0], action_turn[1], action_turn[2], action_turn[3], action_turn[4],
                action_turn[5], action_turn[6], action_turn[7]));
        }

        public override void End(Game game)
        {
        }

        #region AStar

        public string MyAStar(Game game)
        {
            List<Game> max_score = new List<Game>();
            
            Queue<Game> games = new Queue<Game>();
            games.Enqueue(new Game(game));
            while (games.Count > 0)
            {
                Game try_actions = games.Dequeue();
                if (try_actions.Round - round >= TOUR)
                    max_score.Add(try_actions);
                else
                {
                    Game build_house = new Game(try_actions);
                    build_house.action_order += "H";
                    if (IsAlphabeticOrder(build_house.action_order) && build_house.TryBuild(Building.BuildingType.HOUSE))
                    {
                        long nbrPossibleAttrac = build_house.Money / Attraction.BUILD_COST;
                        long attractiveness = build_house.Map.GetAttractiveness();
                        long housing = build_house.Map.GetHousing();
                        if ( attractiveness + nbrPossibleAttrac * 500 -  housing > -200)
                            games.Enqueue(build_house);
                    }
                    
                    
                    Game build_shop = new Game(try_actions);
                    build_shop.action_order += "S";
                    if (IsAlphabeticOrder(build_shop.action_order) && build_shop.TryBuild(Building.BuildingType.SHOP))
                            games.Enqueue(build_shop);
                    
                    Game build_attraction = new Game(try_actions);
                    build_attraction.action_order += "A";
                    if (IsAlphabeticOrder(build_attraction.action_order) && build_attraction.TryBuild(Building.BuildingType.ATTRACTION))
                    {
                        long nbrPossibleHouse = build_attraction.Money / House.BUILD_COST;
                        if (build_attraction.Map.GetAttractiveness() - build_attraction.Map.GetHousing() - nbrPossibleHouse * 300 < 200)
                            games.Enqueue(build_attraction);
                    }
                    
                    Game upgrade_house = new Game(try_actions);
                    upgrade_house.action_order += "I";
                    if (IsAlphabeticOrder(upgrade_house.action_order) && upgrade_house.Map.IsMapFull()[0] && upgrade_house.TryUpgrade(Building.BuildingType.HOUSE))
                        games.Enqueue(upgrade_house);
                    
                    Game upgrade_shop = new Game(try_actions);
                    upgrade_shop.action_order += "T";
                    if (IsAlphabeticOrder(upgrade_shop.action_order) && upgrade_shop.Map.IsMapFull()[0] && upgrade_shop.TryUpgrade(Building.BuildingType.SHOP))
                        games.Enqueue(upgrade_shop);
                    
                    Game upgrade_attraction = new Game(try_actions);
                    upgrade_attraction.action_order += "B";
                    if (IsAlphabeticOrder(upgrade_attraction.action_order) && upgrade_attraction.TryUpgrade(Building.BuildingType.ATTRACTION))
                        games.Enqueue(upgrade_attraction);
                    
                    
                    if (try_actions.Round - round < TOUR)
                    {
                        try_actions.UpdateBacktracking();
                        if(try_actions.Score > 0)
                            games.Enqueue(try_actions);
                    }
                }
            }
            //On traite les differentes games opptenues pour trouver le pere a l'origine du meileur fils
            
            //Recherche du meilleur fils
            Game best_son = null;
            long best_score = 0;
            foreach (Game g in max_score)
            {
                if (g.Score > best_score)
                {
                    best_score = g.Score;
                    best_son = g;
                }
            }
            //Recherche du meilleur pere a partir du meilleur fils
            for (int i = 0; i < TOUR; i++)
            {
                best_son = best_son.parent;
            }
            return best_son.action_order;
        }
        #endregion AStar
        
        //On suppose que uniquement le dernier caractere n'est pas das l'ordre alphabetique
        public bool IsAlphabeticOrder(string action_order)
        {
            int length = action_order.Length;
            if (length <= 1)
                return true;
            if (action_order[length - 2] > action_order[length - 1])
                return false;
            return true;
        }
        
        public int[] ConvertToActionTab(string actions)
        {
            int[] action_tab = {0, 0, 0, 0, 0, 0, 0, 0};
            foreach (char c in actions)
            {
                switch (c)
                {
                    case 'H':
                        action_tab[0] += 1;
                        break;
                    case 'S':
                        action_tab[1] += 1;
                        break;
                    case 'A':
                        action_tab[2] += 1;
                        break;
                }
            }
            return action_tab;
        }
        
        
        //From MyBot
        public bool ApplyTurn(Game game, int buHo, int buSh, int buAt, int upHo, int upSh, int upAt, int deHo,
            int deSh)
        {
            //game, build house, build shop, build attraction, upgrade house, upgrade shop, upgrade attraction, destroy...
            int height = game.Map.Matrix.GetLength(0);
            int width = game.Map.Matrix.GetLength(1);

            int lvlMinAt = 0;
            int lvlMinHo = 0;
            int lvlMinSh = 0;

            do
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        //Destroy
                        if (deHo > 0 && game.Map.GetBuildingType(height - i - 1, width - j - 1) ==
                            Building.BuildingType.HOUSE &&
                            game.Destroy(height - i - 1, width - j - 1))
                            deHo--;

                        if (deSh > 0 && game.Map.GetBuildingType(height - i - 1, width - j - 1) ==
                            Building.BuildingType.SHOP &&
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
                
            }while ((upHo > 0 || upSh > 0 || upAt > 0) && lvlMinAt < 3 && lvlMinSh < 3 && lvlMinHo < 3) ;

            
            
            return buHo == 0 && buSh == 0 && buAt == 0 && upHo == 0 && upSh == 0 && upAt == 0 && deHo == 0 && deSh == 0;
        }

    }
}