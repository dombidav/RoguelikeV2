using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using RoguelikeV2.Controlers.Entity;

namespace RoguelikeV2.Controlers
{
    internal static class Map
    {
        public static List<EntityBase> Entites { get; } = new List<EntityBase>();
        public static Random rnd = new Random();
        public static byte Rows { get; } = 10;
        public static byte Cols { get; } = 10;
        public static byte Lvl { get; set; } = 0;
        public static byte GlobalTicks { get; set; }
        public static Player Player { get; set; }

        public static MapPosition RandomPosition 
        { 
            get 
            {
                MapPosition newPos;
                do
                {
                    newPos = new MapPosition((sbyte)rnd.Next(0, Rows), (sbyte)rnd.Next(0, Cols));
                } while (PositionOccupied(newPos));
                return newPos;
            } 
        }

        public static void Add(EntityBase entity)
        {
            if(entity.Position is null)
                    entity.Position = RandomPosition;
            Entites.Add(entity);
        }

        public static void Reset()
        {
            Entites.Clear();
            Populate();
        }

        private static void Populate() => throw new NotImplementedException();

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
        private static bool PositionOccupied(MapPosition position) 
            => CanMoveTo(position.Row, position.Column)
                ? Entites.Where(x => x.Position.Column == position.Column && x.Position.Row == position.Row).Count() <= 0
                : false;

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