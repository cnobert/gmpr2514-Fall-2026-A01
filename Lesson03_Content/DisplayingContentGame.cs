using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson03_Content;

public class DisplayingContentGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _ship, _spaceStation;
    private SpriteFont _arial;
    private string _output = "The string to output.";

    private SimpleAnimation _walkingAnimation;

    public DisplayingContentGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = 640;
        _graphics.PreferredBackBufferHeight = 320;
        _graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _spaceStation = Content.Load<Texture2D>("Station");
        _ship = Content.Load<Texture2D>("Beetle");
        _arial = Content.Load<SpriteFont>("SystemArialFont");

        Texture2D walking = Content.Load<Texture2D>("Walking");
        //                           (Texture2D, width, height, number of frames, frame per second)
        _walkingAnimation = new SimpleAnimation(walking, 81, 144, 8, 8);
    }
    protected override void Update(GameTime gameTime)
    {
        _walkingAnimation.Update(gameTime);
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_spaceStation, Vector2.Zero, Color.White);
        _spriteBatch.Draw(_ship, new Vector2(300, 140), Color.White);
        _spriteBatch.DrawString(_arial, _output, new Vector2(20, 20), Color.White);
        _walkingAnimation.Draw(_spriteBatch, new Vector2(100, 200), SpriteEffects.None);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
