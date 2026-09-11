using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson01;

public class SimpleGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _pixel;

    private Vector2 _position, _dimensions;

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
        _position = new Vector2(100, 150);
        _dimensions = new Vector2(300, 200);

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
