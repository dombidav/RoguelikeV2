using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RoguelikeV2.Controlers.Entity
{
    class Exit : Interactable
    {
        public Exit(MapPosition position,
                    Guid? id = null,
                    UIElement uIElement = null,
                    BitmapImage image = null)
            : base(position, 0, id, uIElement, image, "Exit") => Image = new BitmapImage(new Uri("Img/Exit.png", UriKind.Relative));

        public event EventHandler PlayerExited;

        public override void OnCollide(EntityBase OtherEntity) => PlayerExited?.Invoke(this, null);
        public override void Tick() { }
    }
}
