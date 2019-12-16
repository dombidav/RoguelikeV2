using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RoguelikeV2.Controlers.Entity
{
    /// <summary>
    /// Először a sor-t próbálja kiegyenlíteni, utána az oszlopot. Minden 3. mozdulatot kihagyja
    /// </summary>
    internal class Zombie : Enemy
    {
        public Zombie(MapPosition position, Guid? id = null, UIElement uIElement = null, BitmapImage image = null) : base(position, 30, 2, id, uIElement, image, "Zombie")
        {
        }

        public override void Tick()
        {
            if (Ticks > 0)
            {
                _ = Map.Player.Position.Row > Position.Row
                    ? this.MoveRow(1)
                    : Map.Player.Position.Row < Position.Row
                        ? this.MoveRow(-1)
                        : Map.Player.Position.Column > Position.Column 
                            ? this.MoveCol(1) 
                            : this.MoveCol(-1);
                Ticks--;
                this.Move();
            }
            else
                Ticks = 2;
        }
    }
}
