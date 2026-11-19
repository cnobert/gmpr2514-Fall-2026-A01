using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson07_MA_Next;

public class Cannon
{
    private SimpleAnimation _animationAlive, _animationDying;
    private const float _DyingDuration = 1f;
    private float _dyingTimer = 0;
    private enum State {Alive, Dying, Dead}
    private State _state = State.Alive;

    internal bool Alive { get => _state == State.Alive; }
    private Vector2 _position;
    private float _speed;
    internal Vector2 Direction {get; set;} = Vector2.Zero;

    private Rectangle _gameBoundingBox;

    private CannonBall[] _cBalls;

    private Texture2D _texture;
    internal Rectangle BoundingBox
    {
        get 
        { 
            return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width / 4, _texture.Height);
        }
    }

    internal void Initialize(Vector2 position, float speed, Rectangle gbb)
    {
        _position = position;  
        _speed = speed;  
        _cBalls = new CannonBall[10];
        for(int c = 0; c < _cBalls.Length; c++)
        {
            _cBalls[c] = new CannonBall();
            _cBalls[c].Initialize(40, gbb);
        }

        _gameBoundingBox = gbb;
    }
    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Cannon");
        _animationAlive = new SimpleAnimation(_texture, _texture.Width / 4, _texture.Height, 4, 2f);
        Texture2D dyingTexture = content.Load<Texture2D>("Poof");
        int frameWidth = 16;
        int frameHeight = dyingTexture.Height;
        int frameCount = dyingTexture.Width / frameWidth;
        float framesPerSecond = 8f;
        _animationDying = new SimpleAnimation(dyingTexture, frameWidth, frameHeight, frameCount, framesPerSecond);

        foreach(CannonBall c in _cBalls)
            c.LoadContent(content);
    }
    internal void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        switch(_state)
        {
            case State.Alive:
                _position += Direction * _speed * dt;
                //MathHelper.Clamp keeps a number between a minimum and a maximum:
                _position.X = MathHelper.Clamp(
                    _position.X,
                    _gameBoundingBox.Left,
                    _gameBoundingBox.Right - BoundingBox.Width);
                if(Direction != Vector2.Zero)
                    _animationAlive?.Update(gameTime);
                break;
            case State.Dying:
                _dyingTimer += dt;
                if(_dyingTimer >= _DyingDuration)
                    _state = State.Dead;
                _animationDying?.Update(gameTime);
                break;
            case State.Dead:
                break;
        }

        foreach(CannonBall c in _cBalls)
            c.Update(gameTime);
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        switch(_state)
        {
            case State.Alive:
                SpriteEffects effects = SpriteEffects.None;
                if(Direction.X < 0)
                    effects = SpriteEffects.FlipHorizontally;
                _animationAlive?.Draw(spriteBatch, _position, effects);
                break;
            case State.Dying:
                _animationDying?.Draw(spriteBatch, _position, SpriteEffects.None);
                break;
            case State.Dead:
                break;
        }

        foreach(CannonBall c in _cBalls)
            c.Draw(spriteBatch);
    }

    internal void Die()
    {
        if (_state == State.Alive)
        {
            _state = State.Dying;
            _dyingTimer = 0;
        }
    }

    internal void Shoot()
    {
        if (_state != State.Alive)
            return;

        int index = 0;
        while(index < _cBalls.Length)
        {
            if(_cBalls[index].Shootable)
            {
                //shoot it
                float cbPositionX = BoundingBox.Center.X - _cBalls[index].BoundingBox.Width / 2;
                float cbPositionY = BoundingBox.Top - _cBalls[index].BoundingBox.Height;
                _cBalls[index].Shoot(new Vector2(cbPositionX, cbPositionY), new Vector2(0, -1));
                index = _cBalls.Length; //we've shot a Cannon Ball, now exit loop
            }
            index++;
        }
    }

    internal bool ProcessCollision(Rectangle boundingBox)
    {
        foreach(CannonBall c in _cBalls)
        {
            if(c.ProcessCollision(boundingBox))
                return true;
        }
        return false;
    }

}