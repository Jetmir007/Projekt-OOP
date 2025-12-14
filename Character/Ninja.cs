using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projekt_OOP
{
    public class Ninja : CharacterBase, ICharacterAction
    {
        private const int shurikenRange = 400;

        public Ninja(Texture2D texture, Vector2 startPosition) : base(texture, 40, 90)
        {
            Position = startPosition;
            Speed = 3.5f;
            AttackDamage = 10;
            SpecialCooldown = 7f;
        }

        public void Attack(CharacterBase opponent)
        {
            const int hitRange = 45;
            if (!isAttacking)
            {
                SetAttackState(true);
                if (CheckRange(opponent.Bounds.Center.ToVector2(), hitRange))
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
                if (CheckRange(opponent.Bounds.Center.ToVector2(), shurikenRange))
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
