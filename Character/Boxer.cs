using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_OOP
{
    public class Boxer: CharacterBase, ICharacterAction
    {
        private const int PunchRange = 50;
        private const int SpecialRange = 60;
        
        public Boxer(Texture2D texture, Vector2 startPosition) : base(texture, 50, 100)
        {
            Position = startPosition;
            Speed = 2.5f;
            AttackDamage = 15;
            SpecialCooldown = 4f;
        }

        public void Attack(CharacterBase opponent, int range)
        {
            if (!isAttacking)
            {
                SetAttackState(true);
                if (CheckRange(opponent.Bounds.Center.ToVector2(), PunchRange))
                {
                    opponent.TakeDamage(AttackDamage);
                }
                SetAttackState(false);
            }
        }
        public void SpecialAttack(CharacterBase opponent, int range)
        {
            if (!isAttacking && CurrentCooldown <= 0)
            {
                SetAttackState(true);
                if (CheckRange(opponent.Bounds.Center.ToVector2(), SpecialRange))
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

        public override void Update(Gametime gametime)
        {
            base.Update(gametime);
        }


    }
}
