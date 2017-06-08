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
    class WeaponReflect : ISaveableSprite
    {
        private Environment environment;
        private Player player;

        private bool active = false;

        private Vector2 offset;
        private float rotation;
        private int windup = 0;

        public WeaponReflect(Environment env, Player owner)
        {
            environment = env;
            player = owner;
            offset = new Vector2(player.Width / 2f, player.Height / 2f);
        }


        public void Update(GameTime gameTime)
        {
            active = windup != 0 || Keyboard.GetState().IsKeyDown(Keys.Space);
            if (active)
            {
                if (windup < 100 && Keyboard.GetState().IsKeyDown(Keys.Space))
                    windup += 2;
                else if (windup > 0)
                    --windup;
                TimeSpan dt = gameTime.ElapsedGameTime;
                rotation += windup/8 * (dt.Seconds + dt.Milliseconds / 1000f);
                if (rotation >= MathHelper.TwoPi)
                    rotation %= MathHelper.TwoPi;
            }
            else
                rotation = 0f;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, TextureController textures)
        {
            if (active)
                spriteBatch.Draw(textures[player.TextureKey], player.S + offset, null, new Color(128, 255, 255, windup),
                    rotation, offset, 2, SpriteEffects.None, 1);
        }
    }
}
