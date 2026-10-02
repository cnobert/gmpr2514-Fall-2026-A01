using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson05_Pong;

public class Pong : Game
{
    private const int _WindowWidth = 750, _WindowHeight = 450, _BallWidthAndHeight = 21, _PlayAreaEdgeLineWidth = 12;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _backgroundTexture, _ballTexture;
    private Vector2 _ballPosition, _ballVelocity; // velocity = speed * direction

    private Rectangle PlayAreaBoundingBox
    {
        get 
        {
            return new Rectangle(
                0, 
                _PlayAreaEdgeLineWidth, 
                _WindowWidth, 
                _WindowHeight - (_PlayAreaEdgeLineWidth * 2));
        }
    }

    public Pong()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.ApplyChanges();

        _ballPosition = new Vector2(150, 195);
        _ballVelocity = new Vector2(-60, -60); //up and to the left

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _backgroundTexture = Content.Load<Texture2D>("Court");
        _ballTexture = Content.Load<Texture2D>("Ball");

    }

    protected override void Update(GameTime gameTime)
    {
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

        _ballPosition += _ballVelocity * dt;

        if(_ballPosition.X <= PlayAreaBoundingBox.Left ||
            (_ballPosition.X + _BallWidthAndHeight) >= PlayAreaBoundingBox.Right)
        {
            _ballVelocity.X *= -1;
        }

        if(_ballPosition.Y <= PlayAreaBoundingBox.Top ||
            (_ballPosition.Y + _BallWidthAndHeight) >= PlayAreaBoundingBox.Bottom)
        {
            _ballVelocity.Y *= -1;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _WindowWidth, _WindowHeight), Color.White);
        
        Rectangle ballRectangle = new Rectangle(
                (int)_ballPosition.X, (int)_ballPosition.Y, 
                _BallWidthAndHeight, _BallWidthAndHeight);
        _spriteBatch.Draw(_ballTexture, ballRectangle, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
