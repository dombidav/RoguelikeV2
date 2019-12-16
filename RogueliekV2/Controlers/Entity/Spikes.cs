using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace RoguelikeV2.Controlers.Entity
{
    /// <summary>
    /// Statikus tüskék
    /// </summary>
    class Spikes : Enemy
    {
        public Spikes(MapPosition position, Guid? id = null, UIElement uIElement = null, Image image = null) : base(position, 255, 0, id, uIElement, image, "Spikes")
        {
        }

        public override void Tick()
        {
        }
    }
}
