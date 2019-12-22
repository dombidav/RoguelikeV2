using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RoguelikeV2.Controlers.Entity
{
    internal class Ruby : Collectible
    {
        public Ruby(MapPosition position, Guid? id = null, UIElement uIElement = null, string name = null) : base(position, 0, id, uIElement, new BitmapImage(new Uri("Img/Ruby.png", UriKind.Relative)), "Ruby")
        { }

        public override void OnCollide(EntityBase OtherEntity)
        {
            if(OtherEntity is Player)
            {
                ((Player)OtherEntity).AddRuby();
                this.CallRemove(this, (Player)OtherEntity);
            }
        }

        public override void Tick() { }
    }
}
