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
using System.Management.Automation;

namespace WindowsGame1
{
    public class NotAGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<GameObject> GOList;
        Texture2D white;
        Texture2D mpointer;
        Texture2D background;
        Texture2D death;
        Texture2D ladder;
        Texture2D smith;
        Texture2D ground;

        Texture2D finaltexture;
        RenderTarget2D rendertarget;


        //static MyConsole mc = new MyConsole();
        //public static MyConsole Debug { get { return mc; } }
        delegate void myOnClick(MouseState ms);
        delegate void myOnRelease(MouseState ms);
        public static GraphicsDevice gd;
        public NotAGame()
        {
            IsMouseVisible = true;
          
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
        }


        protected override void Initialize()
        {
            base.Initialize();
            
            LevelEditor.Initialize(Window.ClientBounds, graphics.GraphicsDevice);
            rendertarget = new RenderTarget2D(graphics.GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            

        }



    
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            LevelEditor.AddToTextures( "white" ,        white = Content.Load<Texture2D>("white") );
            LevelEditor.AddToTextures("mpointer" ,      mpointer = Content.Load<Texture2D>("mpointer"));
            LevelEditor.AddToTextures("smith",          smith = Content.Load<Texture2D>("smith"));
            LevelEditor.AddToTextures("ground",         ground = Content.Load<Texture2D>("ground"));
            LevelEditor.AddToTextures("ladder",         ladder = Content.Load<Texture2D>("ladder"));
            LevelEditor.AddToTextures("background" ,    background = Content.Load<Texture2D>("bgnd"));

            LevelEditor.Background = white;

            GOList = LevelEditor.GetEditorObjects();
        }

    
     
        protected override void UnloadContent()  {}

      
        protected override void Update(GameTime gameTime)
        {
           
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

                LevelEditor.MouseEventListener();

       
            GOList = LevelEditor.GetEditorObjects();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            graphics.GraphicsDevice.SetRenderTarget(rendertarget);
            spriteBatch.Begin();

            spriteBatch.Draw(
                LevelEditor.Background, 
                new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight),
                Color.White);

            GOList.ForEach(
                delegate(GameObject g)
                {
                    if (g is TRectangularObject)
                    {
                        var rec = g as TRectangularObject;
                        spriteBatch.Draw(
                            LevelEditor.GetTextureByName(rec.TextureName),
                            new Rectangle(rec.X, rec.Y, rec.Width, rec.Height),
                            ColourHelper.GetColorByString(rec.ColorName));
                    }
                });
      

            LevelEditor.State.Draw(spriteBatch);
            if (LevelEditor.Test != null)
            {
                spriteBatch.Draw(LevelEditor.Test, LevelEditor.Test.Bounds, Color.White);
            }
            spriteBatch.End();
            graphics.GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();

            LevelEditor.Scene = (Texture2D)rendertarget;
            
            spriteBatch.Draw(rendertarget,
                new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight),
                Color.White);

            spriteBatch.End();



            base.Draw(gameTime);
        }


    }
}
