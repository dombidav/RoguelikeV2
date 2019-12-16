using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RoguelikeV2.Controlers.Entity
{
    /// <summary>
    /// Legnagyobb távot próbálja kiegyenlíteni, minden második körben pihen
    /// </summary>
    class Rat : Enemy
    {
        public Rat(MapPosition position, Guid? id = null, UIElement uIElement = null, BitmapImage image = null) : base(position, 25, 1, id, uIElement, image, "Rat") => Image = new BitmapImage(new Uri("Img/Rat.png", UriKind.Relative));

        public override void Tick()
        {
            if (Ticks > 0)
            {
                var rowDist = Math.Abs(Map.Player.Position.Row - Position.Row);
                var colDist = Math.Abs(Map.Player.Position.Column - Position.Column);
                _ = rowDist > colDist
                    ? Map.Player.Position.Row > Position.Row ? this.MoveCol(1) : this.MoveCol(-1)
                    : Map.Player.Position.Column > Position.Column ? this.MoveRow(1) : this.MoveCol(-1);
                this.Move();
            }
            else
                Ticks = 1;
        }
    }
}
