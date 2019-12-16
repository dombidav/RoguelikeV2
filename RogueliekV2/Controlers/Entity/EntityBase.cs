using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace RoguelikeV2.Controlers.Entity
{
    /// <summary>
    /// Minden ami a térképen szerepel és nem statikus, az egy entitás
    /// </summary>
    abstract class EntityBase
    {
        public EntityBase(MapPosition position, byte ticks = 0, Guid ? id = null, UIElement uIElement = null, Image image = null, string name = null)
        {
            Id = id ?? Guid.NewGuid();
            Position = position ?? throw new ArgumentNullException(nameof(position));
            UIElement = uIElement;
            Image = image;
            Name = name;
            Ticks = ticks;
        }

        public EntityBase(sbyte Row, sbyte Column, byte ticks = 0, Guid? id = null, UIElement uIElement = null, Image image = null, string name = null)
        {
            Id = id ?? Guid.NewGuid();
            Position = new MapPosition(Row, Column);
            UIElement = uIElement;
            Ticks = ticks;
            Image = image;
            Name = name;
        }

        public Guid Id { get; }
        public bool Visible { get; private set; }
        public MapPosition Position { get; set;}
        /// <summary>
        /// Ezt lehet megjeleníteni egy Grid-en
        /// </summary>
        public UIElement UIElement { get; set; }
        /// <summary>
        /// Ez többnyire be ban égetve a kódba, de lehet hogy változik futás alatt
        /// </summary>
        public Image Image { get; set;}
        public string Name { get; set; }
        /// <summary>
        /// Ennek a mozgásnál van szerepe
        /// </summary>
        public byte Ticks { get; set; }

        /// <summary>
        /// Ez minden player mozdulat után meg lesz hívva
        /// </summary>
        public abstract void Tick();

        public virtual void Show() => Visible = true;
        public virtual void Hide() => Visible = false;
    }
}
