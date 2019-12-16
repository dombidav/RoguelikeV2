using System;
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
                    newPos = new MapPosition((sbyte)rnd.Next(0, Rows), (sbyte)rnd.Next(0, Cols));
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
            if(entity is Enemy)
                ((Enemy)entity).Died += EnemyDied;
        }

        private static void EnemyDied(object sender, EventArgs e) => Entites.Remove((Enemy)sender);



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
            /* Minden ellenség különböző nehézségű, szóval "pontokat" rendeltem hozzá, amit egy össz "diff" változóból levonok. A diff minden szinten emelkedik, de sosincs 4-nél több ellenség.
               ______________
               |____Diff____|
               | Spikes | 1 |
               |  Ghost | 2 |
               |    Rat | 3 |
               | Zombie | 4 |
               |============|
        
             */
            while(diff > 0 && cnt > 0)
            {
                #pragma warning disable IDE0007 // Use implicit type
                Enemy tmp = (rnd.Next(diff > 4 ? 4 : diff)) switch
                {
                    0 => new Spikes(RandomPosition),
                    1 => new Ghost(RandomPosition),
                    2 => new Rat(RandomPosition),
                    _ => new Zombie(RandomPosition),
                };
                #pragma warning restore IDE0007 // Use implicit type
                Add(tmp);
            }
        }


        public static void Tick()
        {
            GlobalTicks++;
            Entites.ForEach(x => x.Tick());
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