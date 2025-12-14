using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_OOP
{
    public interface ICharacterAction
    {
        void Attack(CharacterBase opponent);
        void SpecialAttack(CharacterBase Opponent);
    }
}