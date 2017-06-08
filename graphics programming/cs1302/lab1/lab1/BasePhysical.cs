using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace lab1
{
    public abstract class BasePhysical
    {
        //raw physical attributes
        protected Vector2 s, v, a;
        public Vector2 S { get { return s; } set { s = value; } }
        public Vector2 V { get { return v; } set { v = value; } }
        public Vector2 A { get { return a; } set { a = value; } }

        public float X { get { return s.X; } set { s.X = value; } }
        public float Y { get { return s.Y; } set { s.Y = value; } }
        public float Vx { get { return v.X; } set { v.X = value; } }
        public float Vy { get { return v.Y; } set { v.Y = value; } }
        public float Ax { get { return a.X; } set { a.X = value; } }
        public float Ay { get { return a.Y; } set { a.Y = value; } }

        //shape attributes
        public abstract float Feet { get; set; }

        //interactions
        public virtual bool IsGravitational { get { return false; } }
        public virtual float FrictionCoefficient { get { return 0; } }
    }
}
