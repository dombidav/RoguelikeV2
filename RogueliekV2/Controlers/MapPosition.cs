using System;

namespace RoguelikeV2.Controlers
{
    public class MapPosition
    {

        public sbyte Row { get; set; }
        public sbyte Column { get; set; }

        public MapPosition(sbyte row, sbyte column)
        {
            Row = row;
            Column = column;
        }

        public MapPosition(int row, int column) //Matek miatt kell ilyen
        {
            Row = row > sbyte.MaxValue 
                ? sbyte.MaxValue 
                : row < sbyte.MinValue 
                    ? sbyte.MinValue 
                    : (sbyte)row;
            
            Column = column > sbyte.MaxValue 
                ? sbyte.MaxValue 
                : column < sbyte.MinValue 
                    ? sbyte.MinValue 
                    : (sbyte)column;
        }

        internal bool MoveRow(sbyte amount)
        {
            var newRow = Row + amount;
            if(newRow <= sbyte.MaxValue && newRow >= sbyte.MinValue && Map.CanMoveTo(newRow, Column))
            {
                Row = (sbyte)newRow;
                return true;
            }
            return false;
        }

        internal bool MoveCol(sbyte amount)
        {
            var newCol = Column + amount;
            if (newCol <= sbyte.MaxValue && newCol >= sbyte.MinValue && Map.CanMoveTo(Row, newCol))
            {
                Column = (sbyte)newCol;
                return true;
            }
            return false;
        }
    }
}