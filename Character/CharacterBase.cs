using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projekt_OOP
{
    public abstract class CharacterBase : GameObject
    {
        public int Hp {get; protected set;} = 100;
        public int AttackDamage {get; protected set;}
        public float Speed{get; protected set;}
        protected bool isAttacking{get; set;} = false;

        protected float SpecialCooldown = 5f;
        protected float CurrentCooldown = 0f;
        private Microsoft.Xna.Framework.Vector2 position;

        public CharacterType Type{get; protected set;}

        public CharacterBase(Texture2D texture, int width, int height)
        {
            Texture = texture;
            Width = width;
            Height = height;
        }

        public virtual void TakeDamage(int damage)
        {
            Hp -= damage;
        }

        public void Reset(Vector2 startPosition)
        {
            Hp = 100;
            Position = startPosition;
            CurrentCooldown = 0f;
            IsAttacking = false;
        }

        public virtual void Update(GameTime gametime)
        {
            float delta = (float)gametime.ElapsedGameTime.TotalSeconds;

            if(CurrentCooldown > 0)
            {
                CurrentCooldown -= delta;
            }
        }

        public void Move(int direction)
        {
            if(direction == 1)
            {
                position.X -=Speed;
            }
            else if(direction == 2)
            {
                position.X += Speed;
            }
            else if(direction == 3)
            {
                position.Y -= Speed;
            }
            else if(direction == 4)
            {
                position.Y += Speed;
            }
        }

        protected void SetAttackState(bool state)
        {
            isAttacking = state;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
