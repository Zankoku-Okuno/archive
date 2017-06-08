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
    public class Player : BasePhysical, ISaveableSprite, IIndirectLoadable
    {
        private Environment environment;

        public readonly int Height = 65, Width = 68;
        public override bool IsGravitational { get { return true; } }

        //synthetics
        public float Head { get { return s.Y; } set { s.Y = value; } }
        public override float Feet { get { return S.Y + Height; } set { s.Y = (value - Height); } }
        public float Left { get { return s.X; } set { s.X = value; } }
        public float Right { get { return S.X + Width; } set { s.X = (value - Width); } }

        private WeaponReflect weapon;
        
        public Player(Game game, Environment env)
        {
            environment = env;
            weapon = new WeaponReflect(env, this);
            S = new Vector2(128, environment.windowSize.Y - Height -1);
            V = new Vector2(-40, -20);
            A = new Vector2(0, 0);
        }

        public string TextureKey { get { return "mewtwo"; } }
        public Texture2D Load(ContentManager Content)
        {
            return Content.Load<Texture2D>(TextureKey);
        }

        public void Update(GameTime gameTime)
        {
            //if mewtwo is on the ground
            if (environment.OnGround(Feet))
            {
                //check for lateral motion
                if (Keyboard.GetState().IsKeyDown(Keys.D)
                    && !Keyboard.GetState().IsKeyDown(Keys.A))
                    Ax = 1f;
                else if (Keyboard.GetState().IsKeyDown(Keys.A)
                    && !Keyboard.GetState().IsKeyDown(Keys.D))
                    Ax = -1f;
                else
                    Ax = 0;
                //check for vertical motion
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    Ay = Keyboard.GetState().IsKeyDown(Keys.S) ? -4 : -5;
                //check for stabilize
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    Vx *= 0.9f;
            }
            else
            {
                if (!Keyboard.GetState().IsKeyDown(Keys.W))
                    Ay = 0;
                Ax = 0;
            }
            weapon.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, TextureController textures)
        {
            spriteBatch.Draw(textures[TextureKey], S, null, Color.White,
                0, Vector2.Zero, 1, SpriteEffects.None, 0);
            weapon.Draw(spriteBatch, gameTime, textures);
            /*if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                
                Vector2 offset = new Vector2(Width / 2f, Height / 2f);
                spriteBatch.Draw(textures[TextureKey], S + offset, null, new Color(255, 32, 0, MathHelper.Clamp((S.Y - 256f) / 2048f, 0, 255)),
                    2*(gameTime.TotalGameTime.Seconds + gameTime.TotalGameTime.Milliseconds / 1000f)%MathHelper.TwoPi, offset, new Vector2(2, 2f), SpriteEffects.None, 1);
            }*/
            
        }



    }
}
