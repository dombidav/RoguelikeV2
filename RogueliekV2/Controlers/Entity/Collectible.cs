using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RoguelikeV2.Controlers.Entity
{
    internal abstract class Collectible : Interactable
    {
        public Collectible(MapPosition position, byte ticks = 0, Guid? id = null, UIElement uIElement = null, BitmapImage image = null, string name = null) : base(position, ticks, id, uIElement, image, name)
        {
        }

        public event EventHandler<Movable> Remove;

        internal void CallRemove(Collectible collectible, Movable collidedEntity) => Remove?.Invoke(collectible, collidedEntity);
    }
}
