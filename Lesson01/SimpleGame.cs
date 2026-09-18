using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson01;

public class SimpleGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _pixel;

    private int _screenWidth, _screenHeight;

    private Vector2 _position, _dimensions;

    private float _speedX, _speedY;

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
        _screenWidth = _graphics.GraphicsDevice.Viewport.Width;
        _screenHeight = _graphics.GraphicsDevice.Viewport.Height;

        _position = new Vector2(100, 150);
        _dimensions = new Vector2(300, 200);

        _speedX = 150;    // the "f" tells C# to consider this literal a float rather than a double
        _speedY = 100;

        _rectangleColour = Color.DarkGoldenrod;
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
        // TotalSeconds is the amount of time that has passed since the last
        // call to Update (roughly 0.16 seconds if there has been no lag)
        float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

        // same as typing "_position.X = _position.X + _speed"
        
        _position.X += _speedX * deltaTime;
        _position.Y += _speedY * deltaTime;

        // in C#, the "and" operator is "&&"
        // the "or" operator is "||"
        if(_position.X < 0 || _position.X + _dimensions.X > _screenWidth)
        {
            _speedX *= -1;
        }

        // IF there is only one line of code after the "if" statement, the braces
        // are not necessary
        if(_position.Y < 0 || _position.Y + _dimensions.Y > _screenHeight)
            _speedY *= -1;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Exercise:
        //  add code so that the rectangle does not draw if _isVisible == false
        _spriteBatch.Begin();

        if(_isVisible) // == true)
        {
            // the X and Y values in a Vector2 are float data types
            // all values stored in a Rectangle are int data types
            Rectangle rect = new Rectangle(
                (int) _position.X, (int) _position.Y, 
                (int) _dimensions.X, (int) _dimensions.Y
            );

            //  draw this pixel, stretched over this rectangle, in this colour
            _spriteBatch.Draw(_pixel, rect, _rectangleColour);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
