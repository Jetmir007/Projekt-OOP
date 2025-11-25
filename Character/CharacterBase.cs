using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_OOP
{
    public abstract class CharacterBase : GameObject
    {
        public int Hp {get; protected set;} = 100;
        public int Attack {get; protected set;} = 10;
    }
}
