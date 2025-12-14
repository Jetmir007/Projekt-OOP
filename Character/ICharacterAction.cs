using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt__OOP
{
    public interface ICharacterAction
    {
        void Attack(CharacterBase opponent, int range);
        void SpecialAttack(CharacterBase Opponent, int range);
    }
}