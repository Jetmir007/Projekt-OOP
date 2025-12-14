using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_OOP
{
    public class PlayerController
    {
        private CharacterBase character;
        private CharacterBase opponent;

        private Keys _up, _down, _left, _right, _attack, _special;

        public PlayerController(Characterbase p1, Characterbase p2, Keys up, Keys down, Keys left, Keys right, Keys attack, Keys special)
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

        public void HandeInput(KeyBoardState kState, KeyBoardState pState)
        {
            Vector2 direction = Vector2.Zero;

            if(kState.IsKeyDown(_left)) direction = -1;
            if(kState.IsKeyDown(_right)) direction = 1;
            if(kState.IsKeyDown(_up)) direction = -1;
            if(kState.IsKeyDown(_down)) direction = 1;

            if(direction != Vector2.Zero)
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
