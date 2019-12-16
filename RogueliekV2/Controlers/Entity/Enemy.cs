using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RoguelikeV2.Controlers.Entity
{
    abstract class Enemy : Movable
    {
        public Enemy(MapPosition position, byte atk, byte ticks = 0, Guid? id = null, UIElement uIElement = null, BitmapImage image = null, string name = null) : base(position, ticks, id, uIElement, image, name) => ATK = atk;

        /// <summary>
        /// Sebzés
        /// </summary>
        public byte ATK {get; set;}

        public event EventHandler Died;

        public override void OnCollide(EntityBase OtherEntity)
        {
            if (OtherEntity is Player)
            {
                ((Player)OtherEntity).TakeDamage(ATK);
                this.Die();
            }
        }

        public void Die()
        {
            if (Died != null)
                Died?.Invoke(this, null);
        }
    }
}
