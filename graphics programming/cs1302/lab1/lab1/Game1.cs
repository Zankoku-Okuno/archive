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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        int veiwWidth = 1280; //real 1280
        int veiwHeight = 720; //real 1024

        GraphicsDeviceManager graphics;
        RenderTarget2D renderTarget;
        WindowFiller windowFiller;
        TextureController textures;
        SpriteBatch spriteBatch;

        Environment environment;
        Player player;
        Cursor cursor;
        DebugText debugText;

        int screenHeight, screenWidth;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            screenHeight = graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            screenWidth = graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.IsFullScreen = true;

            

            Content.RootDirectory = "Content";
            textures = new TextureController();
        }

        protected override void Initialize()
        {
            renderTarget = new RenderTarget2D(graphics.GraphicsDevice, veiwWidth, veiwHeight);
            windowFiller = new WindowFiller(renderTarget, screenWidth, screenHeight, veiwWidth, veiwHeight);
            environment = new Environment(new Vector2(veiwWidth, veiwHeight));
            player = new Player(this, environment);
            cursor = new Cursor(windowFiller);
            debugText = new DebugText();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textures[player.TextureKey] = player.Load(Content);
            debugText.Load(Content);
            cursor.texture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            cursor.texture.SetData<Color>(new Color[] { Color.White });
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) ||
                GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            environment.RunPhysics(player);
            environment.CheckBounds(player);
            cursor.Update(gameTime);
            debugText.Update(gameTime);
            player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.SetRenderTarget(renderTarget);
            graphics.GraphicsDevice.Clear(Color.Indigo);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
            spriteBatch.Draw(cursor, gameTime);
            spriteBatch.Draw(debugText, gameTime);
            player.Draw(spriteBatch, gameTime, textures);
            spriteBatch.End();

            windowFiller.Draw(spriteBatch, graphics.GraphicsDevice);
            

            base.Draw(gameTime);
        }
    }
}
