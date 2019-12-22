using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RoguelikeV2.Controlers.Entity
{
    internal class HPpot : Collectible
    {
        public HPpot(MapPosition position, Guid? id = null, UIElement uIElement = null, string name = null) : base(position, 0, id, uIElement, new BitmapImage(new Uri("Img/HpPotion.png", UriKind.Relative)), "HpPotion") 
            => HealAmount = 30;

        public byte HealAmount { get; private set; }

        public override void OnCollide(EntityBase OtherEntity)
        {
            if(OtherEntity is Player)
            {
                ((Player)OtherEntity).Heal(HealAmount);
                this.CallRemove(this, (Player)OtherEntity);
            }
        }

        public override void Tick() { }
    }
}
