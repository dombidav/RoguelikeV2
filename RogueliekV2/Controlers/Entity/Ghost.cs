using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace RoguelikeV2.Controlers.Entity
{
    /// <summary>
    /// A szellem minden körben mozdul, mindig véletlenszerű irányba
    /// </summary>
    class Ghost : Enemy
    {
        public Ghost(MapPosition position, Guid? id = null, UIElement uIElement = null, Image image = null) : base(position, 20, 0, id, uIElement, image, "Ghost")
        {
        }
        public override void Tick()
        {
            bool cantMove;
            do
            {
                var rand = Map.rnd.Next(0, 4);
                cantMove = rand switch
                {
                    0 => Position.MoveRow(-1),
                    1 => Position.MoveRow(1),
                    2 => Position.MoveCol(-1),
                    _ => Position.MoveCol(1),
                };
            } while (cantMove);
            this.Move();
        }
    }
}
