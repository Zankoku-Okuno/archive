using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace lab1
{
    public class Environment
    {

        Vector2 jerk;
        Vector2 g = new Vector2(0, 0.3f);
        public Vector2 windowSize { get; set; }

        public Environment(Vector2 windowSize)
        {
            this.windowSize = windowSize;
            jerk = new Vector2(0, 0.5f);
        }

        public bool OnGround(float bottom)
        {
            return bottom >= windowSize.Y;
        }

        public void RunPhysics(BasePhysical obj)
        {
            /*
             * okay, so here's why we order it the way we do:
             * first, player input SETS his physical attributes
             * next, we MODIFY those attributes according to the collision detection
             * third, we perform integration
             * finally, ensure that sprite positions really are in bounds
             * TODO but there's gotta be a better way!
             * yeah, we should have some flags that say: "Hey! We've already got this, thanks!", then those control whether the integration happens or not
             *  that is, out collision detection will not only correct fixes, but report them.
             *  then, we use the report to conditionally integrate, instead of detect, integrate, detect
             * */
            //apply friction
            if (OnGround(obj.Feet))
            {
                if (obj.V.X > 0)
                    obj.Ax -= 0.6f;
                else if (obj.V.X < 0)
                    obj.Ax += 0.6f;
            }
            obj.A += jerk;
            obj.V += obj.A;
            if (obj.IsGravitational) obj.V += g;
            obj.S += obj.V;
        }

        public void CheckBounds(Player player)
        {
            //check position bounds
            {
                if (player.Left < 0 || player.Right > windowSize.X)
                {
                    if (player.Left < 0) player.Left = 0;
                    else if (player.Right > windowSize.X) player.Right = windowSize.X;
                    player.Vx *= -0.2f;
                }
            }
            if (player.Feet > windowSize.Y)
            {
                player.Feet = windowSize.Y;
                player.Vy = 0;
            }
            //check velocity bounds
            if (Math.Abs(player.V.X) < 0.1f)
            {
                player.Vx = 0;
                player.X = (int)Math.Round(player.X); //make sure the image is crisp (no antialiasing)
            }
            if (Math.Abs(player.V.Y) < 0.1f)
            {
                player.Vy = 0;
                //player.Y = (int)Math.Round(player.Y); //make sure the image is crisp (no antialiasing)
                // ^^^ since Vy==0 only while on the ground, this assignment is not necessary
                //     (Y is already an integer from the position bounds checking)
            }
            player.Vx = Math.Min(Math.Max(-15, player.V.X), 15);
            //check acceleration bounds
            if (player.A.Y > 0) player.Ay = 0; //you can't force your character downwards
        }
    }
}
