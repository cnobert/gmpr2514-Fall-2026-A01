using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson05_Pong;

public class Pong : Game
{
    private const int _WindowWidth = 750, _WindowHeight = 450, _PlayAreaEdgeLineWidth = 12;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _backgroundTexture;

    private Paddle _paddleRight;
    private Ball _ball;

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

        _ball = new Ball();
        _ball.Initialize(PlayAreaBoundingBox);

        _paddleRight = new Paddle();
        _paddleRight.Initialize(PlayAreaBoundingBox, new Vector2(690, 198));

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _backgroundTexture = Content.Load<Texture2D>("Court");
        
        _ball.LoadContent(Content);
        _paddleRight.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

        _ball.Update(gameTime);
        
        

        KeyboardState kbState = Keyboard.GetState();
        if(kbState.IsKeyDown(Keys.Up))
            _paddleRight.Direction = new Vector2(0, -1);
        else if(kbState.IsKeyDown(Keys.Down))
            _paddleRight.Direction = new Vector2(0, 1);
        else
            _paddleRight.Direction = Vector2.Zero;
        _paddleRight.Update(gameTime);

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
        
        // Rectangle ballBoundingBox = new Rectangle(
        //         (int)_ballPosition.X, (int)_ballPosition.Y, 
        //         _BallWidthAndHeight, _BallWidthAndHeight);

        // Rectangle paddleBoundingBox = new Rectangle(
        //         (int)_paddlePosition.X, (int)_paddlePosition.Y,
        //         _PaddleWidth, _PaddleHeight);

        // bool colliding = ballBoundingBox.Intersects(paddleBoundingBox);

        // if(colliding)
        //     _ballVelocity.X *= -1;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _WindowWidth, _WindowHeight), Color.White);
        
        _ball.Draw(_spriteBatch);

        _paddleRight.Draw(_spriteBatch);
                    
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
