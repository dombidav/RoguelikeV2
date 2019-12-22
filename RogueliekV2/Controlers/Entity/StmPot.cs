using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RoguelikeV2.Controlers.Entity
{
    internal class StaminaPot: Collectible
    {
        public StaminaPot(MapPosition position, Guid? id = null, UIElement uIElement = null, string name = null) : base(position, 0, id, uIElement, new BitmapImage(new Uri("Img/StaminaPotion.png", UriKind.Relative)), "StaminaPotion") 
            => StepsAmount = 30;

        public byte StepsAmount { get; private set; }

        public override void OnCollide(EntityBase OtherEntity)
        {
            if(OtherEntity is Player)
            {
                ((Player)OtherEntity).AddSteps(StepsAmount);
                this.CallRemove(this, (Player)OtherEntity);
            }
        }

        public override void Tick() { }
    }
}
