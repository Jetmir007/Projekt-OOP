using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projekt_OOP
{
    public class Swordsman : CharacterBase, ICharacterAction
    {
        private const int swingRange = 90;
        private const int slashRange = 100;
        public Swordsman(Texture2D texture, Vector2 startPosition) : base(texture, 45, 90)
        {
            Position = startPosition;
            Speed = 2.5f;
            AttackDamage = 15;
            SpecialCooldown = 6f;
        }

        public void Attack(CharacterBase opponent)
        {
            if (!isAttacking)
            {
                SetAttackState(true);
                if (CheckRange(opponent.Bounds.Center.ToVector2(), swingRange))
                {
                    opponent.TakeDamage(AttackDamage);
                }
                SetAttackState(false);
            }
        }
        public void SpecialAttack(CharacterBase opponent)
        {
            if (!isAttacking && CurrentCooldown <= 0)
            {
                SetAttackState(true);
                if (CheckRange(opponent.Bounds.Center.ToVector2(), slashRange))
                {
                    opponent.TakeDamage(AttackDamage*2);
                }
                CurrenCooldown = SpecialCooldown;
                SetAttackState(false);
            }
        }

        private bool CheckRange(Vector2 targetDistance, int maxDistance)
        {
            float distance = Vector2.Distance(Bounds.Center.ToVector2(), targetDistance);
            return distance <= maxDistance;
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
        }
    }
}
