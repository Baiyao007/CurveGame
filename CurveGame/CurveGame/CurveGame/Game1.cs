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
using MyLib.Device;
using CurveGame.Objects;
using CurveGame.Objects.Circles;

namespace CurveGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphicsDeviceManager;
        private GameDevice gameDevice;
        private ResouceManager resouceManager;

        private List<Circle> circles;
        private bool isPause;

        public Game1()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphicsDeviceManager.PreferredBackBufferHeight = 600;
            graphicsDeviceManager.PreferredBackBufferWidth = 800;

            graphicsDeviceManager.IsFullScreen = false;
        }


        protected override void Initialize()
        {
            gameDevice = new GameDevice(Content, graphicsDeviceManager.GraphicsDevice);
            resouceManager = new ResouceManager(Content);

            isPause = false;

            circles = new List<Circle>();
            circles.Add( new Circle(Vector2.Zero));
            circles.Add(new CircleGoAndReturn(Vector2.One * 100, new Vector2(6, 6)));
            circles.Add(new CircleMoveWithRoute(new Vector2(600, 100), new List<Vector2>()
                {   new Vector2(600,100),
                    new Vector2(700,200),
                    new Vector2(500,200),})
            );

            circles.Add(new CircleMoveWithRoute(new Vector2(700, 400), new List<Vector2>()
                {   new Vector2(700,400),
                    new Vector2(700,500),
                    new Vector2(500,500),
                    new Vector2(500,400),})
);

            circles.ForEach(c => c.Initialize());

            Window.Title = "CurveGame";
            IsMouseVisible = true;
            base.Initialize();
        }


        protected override void LoadContent() {
            resouceManager.LoadTextures("Point");
            resouceManager.LoadTextures("Wheel");
            resouceManager.LoadFont("HGPop");
        }

        protected override void UnloadContent() {
            gameDevice.UnLoad();
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) this.Exit();

            gameDevice.Update();

            if (gameDevice.GetInputState.WasDown(Keys.P)) { isPause = !isPause; }

            Vector2 mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            if (Mouse.GetState().LeftButton == ButtonState.Pressed) {
                circles.ForEach(c => c.SetSpot(mousePosition));
            }

            if (!isPause) { circles.ForEach(c => c.Update()); }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            circles.ForEach(c => c.Draw());

            GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
            gameDevice.GetParticleGroup.Draw();

            base.Draw(gameTime);
        }
    }
}
