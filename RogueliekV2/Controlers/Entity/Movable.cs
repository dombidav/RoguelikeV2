using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RoguelikeV2.Controlers.Entity
{
    abstract class Movable : Interactable
    {
        public Movable(MapPosition position, byte ticks = 0, Guid? id = null, UIElement uIElement = null, BitmapImage image = null, string name = null) : base(position, ticks, id, uIElement, image, name)
        {
        }
        public event EventHandler<MapPosition> Moved;
        public virtual bool MoveCol(sbyte amount) => Position.MoveRow(amount);
        public virtual bool MoveRow(sbyte amount) => Position.MoveCol(amount);

        /// <summary>
        /// Ez CSAK az event hívás miatt kell (mert child osztály nem tud invoke-olni)
        /// </summary>
        public void Move() => Moved?.Invoke(this, Position);
    }
}
