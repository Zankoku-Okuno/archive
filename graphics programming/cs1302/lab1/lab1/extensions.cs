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
    public static class Extensions
    {
        public static void Draw(this SpriteBatch spriteBatch, ISprite sprite, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, gameTime);
        }
        public static void Draw(
            this SpriteBatch spriteBatch,
            ISaveableSprite sprite,
            GameTime gameTime,
            TextureController textures)
        {
            sprite.Draw(spriteBatch, gameTime, textures);
        }
    }
}
