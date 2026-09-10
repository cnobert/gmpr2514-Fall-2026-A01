using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson01;

public class SimpleGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _pixel;

    private int _xPosition, _yPosition, _width, _height;

    private Color _rectangleColour;
    private bool _isVisible;

    public SimpleGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _xPosition = 100;
        _yPosition = 150;
        _width = 300;
        _height = 200;

        _rectangleColour = Color.Gainsboro;
        _isVisible = true;

        base.Initialize();
    }

    protected override void LoadContent() 
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // create a 1 x 1 pixel object
        _pixel = new Texture2D(GraphicsDevice, 1, 1);
        _pixel.SetData(new [] { Color.White });
    }

    protected override void Update(GameTime gameTime)
    {

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        Rectangle rect = new Rectangle(_xPosition, _yPosition, _width, _height);
        _spriteBatch.Draw(_pixel, rect, _rectangleColour);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
