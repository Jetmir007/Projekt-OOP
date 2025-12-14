using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projekt_OOP
{
    public class GameObject
    {
        public Vector2 Position {get; set;}

        public Vector2 Velocity {get; set;}

        public int Height {get; set;}
        public int Width {get; set;}

        public virtual Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

        public Texture2D Texture{get; set;}


    }
}
