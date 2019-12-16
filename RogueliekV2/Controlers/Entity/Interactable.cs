using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RoguelikeV2.Controlers.Entity
{
    abstract class Interactable : EntityBase
    {
        public Interactable(MapPosition position, byte ticks = 0, Guid? id = null, UIElement uIElement = null,  BitmapImage image = null, string name = null) : base(position, ticks, id, uIElement, image, name)
        {
        }

        /// <summary>
        /// Valaki hozzáért
        /// </summary>
        /// <param name="OtherObject">A másik objektum, aki megérintette</param>
        public abstract void OnCollide(EntityBase OtherEntity);
    }
}
