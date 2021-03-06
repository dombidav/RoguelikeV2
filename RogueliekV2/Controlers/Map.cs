﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using RoguelikeV2.Controlers.Entity;

namespace RoguelikeV2.Controlers
{
    internal static class Map
    {
        public static List<Interactable> Entites { get; set; }
        public static Random rnd = new Random();
        public static byte Rows { get; } = 10;
        public static byte Cols { get; } = 10;
        public static byte Lvl { get; set; } = 0;
        public static byte GlobalTicks { get; set; }
        public static Player Player { get; set; } = new Player(new MapPosition(0, 0));
        public static Exit Exit { get; set; } = new Exit(RandomPosition);

        public static MapPosition RandomPosition 
        { 
            get 
            {
                MapPosition newPos;
                do
                {
                    newPos = new MapPosition((sbyte)rnd.Next(1, Rows), (sbyte)rnd.Next(1, Cols));
                } while (!PositionNotOccupied(newPos));
                return newPos;
            } 
        }

        public static void Add(Interactable entity)
        {
            if (Entites is null)
                Entites = new List<Interactable>();
            if(entity.Position is null)
                    entity.Position = RandomPosition;
            Entites.Add(entity);
            if (entity is Enemy)
                ((Enemy)entity).Died += RemoveEntity;
            else if (entity is Collectible)
                ((Collectible)entity).Remove += CollectibleRemove;
        }

        private static void CollectibleRemove(object sender, Movable e) => RemoveEntity(sender, null);

        private static void RemoveEntity(object sender, EventArgs e) => Entites.Remove((Interactable)sender);



        /// <summary>
        /// Törli a pályát és újra generálja
        /// </summary>
        /// <param name="next">Következő pálya?</param>
        public static void Reset(bool next = false)
        {
            if (Entites is null)
                Entites = new List<Interactable>();
            if(Entites.Count > 0)
                Entites.Clear();

            if (next)
                Lvl++;

            Exit.Position = RandomPosition;
            Player.Position = new MapPosition(0, 0);
            var diff = Lvl > 0 ? (Lvl / 2) + 1 : 0;
            var cnt = 4;
            /* Minden ellenség különböző nehézségű, szóval "pontokat" rendeltem hozzá, amit egy össz "diff" változóból levonok. A diff minden szinten emelkedik, de sosincs 4-nél több ellenség. Ha Stamina potion-t vagy Hp potion-t generál, akkor lehet magasabb az ellenség szám és a nehézség is
               ________________
               |____Diff______|
               |  Spikes |  1 |
               |   Ghost |  2 |
               |     Rat |  3 |
               |  Zombie |  4 |
               |==============|
        
             */
            for (var i = 0; i < rnd.Next(3); i++)
            {
                Collectible tmp = rnd.Next(0, 3) switch
                {
                    0 => new HPpot(RandomPosition),
                    1 => new StaminaPot(RandomPosition),
                    _ => new Ruby(RandomPosition)
                };
                if((tmp is HPpot) || (tmp is StaminaPot))
                {
                    diff++;
                    cnt++;
                }
                Add(tmp);
            }

            while (diff > 0 && cnt > 0)
            {
                var randomNumber = rnd.Next(1, diff > 5 ? 5 : diff); // Diff a generálás felső határa, de maximum 5 a switch miatt
                Enemy tmp = randomNumber switch
                {
                    1 => new Spikes(RandomPosition),
                    2 => new Ghost(RandomPosition),
                    3 => new Rat(RandomPosition),
                    _ => new Zombie(RandomPosition),
                };
                diff -= randomNumber;
                Add(tmp);
            }
        }


        public static void Tick()
        {
            GlobalTicks++;
            Entites.ForEach(x => x.Tick());
            Entites.Where(x => x.Position == Player.Position).ToList().ForEach(x => x.OnCollide(Player));
            if (Exit.Position == Player.Position)
                Exit.OnCollide(Player);
        }

        /// <summary>
        /// Lehet oda lépni? És ha igen, akkor foglalt-e már az a pozíció?
        /// </summary>
        /// <param name="position"> pozíció</param>
        /// <returns>Lehet és NEM foglalt?</returns>
        private static bool PositionNotOccupied(MapPosition position)
        {
            if (CanMoveTo(position.Row, position.Column))
            {
                if (Entites is null)
                {
                    Entites = new List<Interactable>();
                    return false;
                }
                else
                    return Entites.Where(x => x.Position.Column == position.Column && x.Position.Row == position.Row).Count() <= 0;
            }
            else 
                return false;
        }

        /// <summary>
        /// Lehetséges oda lépni? (nem nézi, hogy van-e ott valaki)
        /// </summary>
        /// <param name="Row">Sor</param>
        /// <param name="column">Oszlop</param>
        /// <returns>Lehet?</returns>
        internal static bool CanMoveTo(int Row, int column) => Row >= 0 && Row < Rows && column >= 0 && column < Cols;

        public enum TileType
        {
            LeftWall,
            LeftTopCorner,
            LeftBotCorner,

            RightWall,
            RightTopCorner,
            RightBotCorner,

            TopWall,

            BotWall,

            Floor
        }
    }
}