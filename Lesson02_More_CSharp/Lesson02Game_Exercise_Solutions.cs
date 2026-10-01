using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson02_MoreCSharp;

public class Lesson02Game_Exercise_Solutions : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _pixel;

    private Vector2 _dimensionsRectangles;
    private Vector2 _velocity;
    private Vector2[] _positions;

    public Lesson02Game_Exercise_Solutions()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _dimensionsRectangles = new Vector2(60, 40);
        _velocity = new Vector2(150, 0);

        _positions = new Vector2[6];
        for(int c = 0; c < _positions.Length; c++)
        {
            int spacing = 10;
            float y = c * (_dimensionsRectangles.Y + spacing) + spacing;
            Vector2 v = new Vector2(10, y);
            _positions[c] = v;
        }
        

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _pixel = new Texture2D(GraphicsDevice, 1, 1);
        _pixel.SetData(new[] { Color.White });
    }

    protected override void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        for(int c = 0; c < _positions.Length; c++)
        {
            _positions[c].X += _velocity.X * dt;
        
            if(_positions[c].X + _dimensionsRectangles.X > GraphicsDevice.Viewport.Bounds.Right
                ||
            _positions[c].X < GraphicsDevice.Viewport.Bounds.Left 
            )
            {
                _velocity.X *= -1;
                //the top rectangle needs to be fixed because it was moved
                //before it triggered the if statement
                _positions[c].X += dt * _velocity.X * 2;
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        foreach(Vector2 position in _positions)
        {
            Rectangle r = new Rectangle(
                (int)position.X,
                (int)position.Y,
                (int)_dimensionsRectangles.X,
                (int)_dimensionsRectangles.Y);

            _spriteBatch.Draw(_pixel, r, Color.LightYellow);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}