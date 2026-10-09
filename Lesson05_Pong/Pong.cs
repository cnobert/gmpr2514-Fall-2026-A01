using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson05_Pong;

public class Pong : Game
{
    private const int _WindowWidth = 750, _WindowHeight = 450, _BallWidthAndHeight = 21, _PlayAreaEdgeLineWidth = 12;
    private const int _PaddleWidth = 6 * 2, _PaddleHeight = 54 * 2;
    private const float _PaddleSpeed = 240;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _backgroundTexture;
    
    private Texture2D _ballTexture;
    private Vector2 _ballPosition, _ballVelocity; // velocity = speed * direction

    private Texture2D _paddleTexture;
    private Vector2 _paddlePosition, _paddleVelocity;

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
        _ballVelocity = new Vector2(-160, -160); //up and to the left

        _paddlePosition = new Vector2(690, 198);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _backgroundTexture = Content.Load<Texture2D>("Court");
        _ballTexture = Content.Load<Texture2D>("Ball");
        _paddleTexture = Content.Load<Texture2D>("Paddle");

    }

    protected override void Update(GameTime gameTime)
    {
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

        #region ball
        _ballPosition += _ballVelocity * dt;

        // bounce off sides
        if(_ballPosition.X <= PlayAreaBoundingBox.Left ||
            (_ballPosition.X + _BallWidthAndHeight) >= PlayAreaBoundingBox.Right)
        {
            _ballVelocity.X *= -1;
        }
        // bounce off top and bottom
        if(_ballPosition.Y <= PlayAreaBoundingBox.Top ||
            (_ballPosition.Y + _BallWidthAndHeight) >= PlayAreaBoundingBox.Bottom)
        {
            _ballVelocity.Y *= -1;
        }
        #endregion
        
        #region paddle
        KeyboardState kbState = Keyboard.GetState();
        if(kbState.IsKeyDown(Keys.Up))
            _paddleVelocity = new Vector2(0, -_PaddleSpeed);
        else if(kbState.IsKeyDown(Keys.Down))
            _paddleVelocity = new Vector2(0, _PaddleSpeed);
        else
            _paddleVelocity = Vector2.Zero;

        _paddlePosition += _paddleVelocity * dt;

        // pin the paddle at the to or bottom if it has bled over
        if(_paddlePosition.Y <= PlayAreaBoundingBox.Top)
            _paddlePosition.Y = PlayAreaBoundingBox.Top;
        else if(_paddlePosition.Y + _PaddleHeight >= PlayAreaBoundingBox.Bottom)
            _paddlePosition.Y = PlayAreaBoundingBox.Bottom - _PaddleHeight;

        #endregion

        #region paddle - commented out alternative solution - fewer lines of code
        // KeyboardState kbState = Keyboard.GetState();
       
       // 
        // if(kbState.IsKeyDown(Keys.Up) && _paddlePosition.Y >= PlayAreaBoundingBox.Top)
        //     _paddleVelocity = new Vector2(0, -_PaddleSpeed);
       
        // else if(kbState.IsKeyDown(Keys.Down) && (_paddlePosition.Y + _PaddleHeight) <= PlayAreaBoundingBox.Bottom)
        //     _paddleVelocity = new Vector2(0, _PaddleSpeed);
        // else
        //     _paddleVelocity = Vector2.Zero;
 
        // _paddlePosition += _paddleVelocity * dt;
 
        #endregion
        
        Rectangle ballBoundingBox = new Rectangle(
                (int)_ballPosition.X, (int)_ballPosition.Y, 
                _BallWidthAndHeight, _BallWidthAndHeight);

        Rectangle paddleBoundingBox = new Rectangle(
                (int)_paddlePosition.X, (int)_paddlePosition.Y,
                _PaddleWidth, _PaddleHeight);

        bool colliding = ballBoundingBox.Intersects(paddleBoundingBox);

        if(colliding)
            _ballVelocity.X *= -1;

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

        Rectangle paddleRectangle = new Rectangle(
                (int)_paddlePosition.X, (int)_paddlePosition.Y,
                _PaddleWidth, _PaddleHeight);
        _spriteBatch.Draw(_paddleTexture, paddleRectangle, Color.White);             
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
