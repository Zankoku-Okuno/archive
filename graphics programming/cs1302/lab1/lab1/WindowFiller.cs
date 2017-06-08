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
    public class WindowFiller
    {
        private Texture2D render;
        private float sX, sY, rX, rY;
        public int screenWidth { get { return (int)sX; } set { sX = value; Recalculate(); } }
        public int screenHeight { get { return (int)sY; } set { sY = value; Recalculate(); } }
        public int renderWidth { get { return (int)rX; } set { rX = value; Recalculate(); } }
        public int renderHeight { get { return (int)rY; } set { rY = value; Recalculate(); } }

        private float scale;
        private Vector2 offset;

        public WindowFiller(Texture2D render, int screenWidth, int screenHeight, int renderWidth, int renderHeight)
        {
            this.render = render;
            this.sX = screenWidth;
            this.sY = screenHeight;
            this.rX = renderWidth;
            this.rY = renderHeight;
            Recalculate();
        }
        private void Recalculate()
        {
            float sRatio = sX / sY, rRatio = rX / rY;
            if (sRatio < rRatio) //Letter box
            {
                scale = sX / rX;
                offset = new Vector2(0,
                                    (screenHeight - renderHeight * scale) / 2);
            }
            else if (sRatio > rRatio) //Pillar box
            {
                scale = sY / rY;
                offset = new Vector2((screenWidth - renderWidth * scale) / 2,
                                     0);
            }
            else //No box
            {
                scale = sY / rY;
                offset = Vector2.Zero;
            }
        }

        public Vector2 GetMouse()
        {
            //This doesn't maintain aspect ratio on the mouse
            /*float x = Mouse.GetState().X * rX / sX;
            float y = Mouse.GetState().Y * rY / sY;
            Vector2 acc = new Vector2(x, y);*/
            //Too large on some resolutions
            Mouse.SetPosition((int)MathHelper.Clamp(Mouse.GetState().X, offset.X, screenWidth - offset.X),
                              (int)MathHelper.Clamp(Mouse.GetState().Y, offset.Y, screenHeight - offset.Y));
            float x = Mouse.GetState().X;
            float y = Mouse.GetState().Y;
            Vector2 acc = (new Vector2(x, y) - offset) / scale;
            //Too small a window on some resolutions
            /*Vector2 acc = new Vector2(Mouse.GetState().X, Mouse.GetState().Y) / scale - offset;
            acc.X = MathHelper.Clamp(acc.X, 0, renderWidth-1);
            acc.Y = MathHelper.Clamp(acc.Y, 0, renderHeight-1);*/
            
            return acc;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice gDevice)
        {
            gDevice.SetRenderTarget(null);
            gDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(render, offset, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}
