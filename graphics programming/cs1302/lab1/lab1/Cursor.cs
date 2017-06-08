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
    public class Cursor : ISprite
        //FIXME now that the window is adjusted to fit the screen, the mouse may not reach everywhere (or reach too far)
    {
        private Vector2 pos;
        public Texture2D texture;
        private WindowFiller windowFiller;

        public Cursor(WindowFiller wf)
        {
            windowFiller = wf;
        }

        public void Update(GameTime gameTime)
        {
            pos = windowFiller.GetMouse();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y-3, 1, 7), Color.White);
            spriteBatch.Draw(texture, new Rectangle((int)pos.X - 3, (int)pos.Y, 7, 1), Color.White);
        }
    }
}
