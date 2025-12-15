using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Projekt_OOP
{
    public class PlayerController
    {
        private CharacterBase character;
        private CharacterBase opponent;

        private Keys _up, _down, _left, _right, _attack, _special;

        public PlayerController(CharacterBase p1, CharacterBase p2, Keys up, Keys down, Keys left, Keys right, Keys attack, Keys special)
        {
            character = p1;
            opponent = p2;
            _up = up;
            _down = down;
            _left = left;
            _right = right;
            _attack = attack;
            _special = special;
        }

        public void HandleInput(KeyboardState kState, KeyboardState pState)
        {
            int direction = 0;

            if(kState.IsKeyDown(_left)) direction = 1;
            if(kState.IsKeyDown(_right)) direction = 2;
            if(kState.IsKeyDown(_up)) direction = 3;
            if(kState.IsKeyDown(_down)) direction = 4;

            if(direction != 0)
            {
                character.Move(direction);
            }

            if(kState.IsKeyDown(_attack) && !pState.IsKeyDown(_attack))
            {
                if(character is ICharacterAction actionCharacter)
                {
                    actionCharacter.Attack(opponent);
                }
            }
            if(kState.IsKeyDown(_special) && !pState.IsKeyDown(_special))
            {
                if(character is ICharacterAction actionCharacter)
                {
                    actionCharacter.SpecialAttack(opponent);
                }
            }
        }
    }
}
