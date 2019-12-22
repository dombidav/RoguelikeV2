using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RoguelikeV2.Controlers.Entity
{
    internal class Player : Movable
    {
        public byte HP { get; set; } = 100;
        public byte Steps { get; set; } = 100;
        public byte Ruby { get; set; } = 0;
        /// <summary>
        /// Player halott, ha a második param igaz, akkor a halál oka ellenféllel való találkozás
        /// </summary>
        public event EventHandler<bool> Died;
        public event EventHandler<byte> DamageTaken;

        public Player(MapPosition position, Guid? id = null, UIElement uIElement = null) 
            : base(position, 0, id, uIElement, new BitmapImage(new Uri("Img/Rogue.png", UriKind.Relative)), "Player")
        { }

        internal void AddRuby()
        {
            if (Ruby < byte.MaxValue - 1)
                Ruby++;
        }

        internal void AddSteps(byte stepsAmount) 
            => Steps = stepsAmount + Steps > 100 
                        ? (byte)100 
                        : (byte)(stepsAmount + Steps);

        internal void Heal(byte healAmount) 
            => HP = healAmount + HP > byte.MaxValue 
                    ? byte.MaxValue 
                    : (byte)(healAmount + HP);

        public override void OnCollide(EntityBase OtherEntity) { }
        public void TakeDamage(byte amount)
        {
            var newHP = HP - amount;
            DamageTaken?.Invoke(this, amount);
            if (newHP > 0)
                HP = (byte)newHP;
            else
                Died?.Invoke(this, true);
        }

        public override void Tick()
        {
            if(Steps > 1)
                Steps--;
            else
                Died?.Invoke(this, false);
        }

        public override bool MoveCol(sbyte amount)
        {
            var ok = base.MoveCol(amount);
            if (ok)
                this.Tick();
            return ok;
        }

        public override bool MoveRow(sbyte amount)
        {
            var ok = base.MoveRow(amount);
            if (ok)
                this.Tick();
            return ok;
        }
    }
}