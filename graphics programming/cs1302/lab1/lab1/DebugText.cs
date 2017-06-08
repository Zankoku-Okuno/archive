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
    class DebugText : ISprite, ILoadable
    {
        SpriteFont spriteFont;
        
        private int frames = 0;
        private float elapsedTime = 0;
        private string fps = "?";

        public void Load(ContentManager Content)
        {
            spriteFont = Content.Load<SpriteFont>("sans");
        }
        public void Unload(ContentManager Content) { }

        public void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            ++frames;
            if (elapsedTime >= 1000)
            {
                fps = frames.ToString();
                elapsedTime = frames = 0;
            }
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(spriteFont, String.Format("Frame rate: {0}fps", fps),
                                   new Vector2(0, 0), Color.White);
        }
    }
}
