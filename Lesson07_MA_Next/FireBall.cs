using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson07_MA_Next;

public class FireBall
{
    private Texture2D _texture;
    private SimpleAnimation _animation;
    private const int FrameCount = 8;
    private Vector2 _position, _direction;
    private float _speed;

    private Rectangle _gameBoundingBox;
    private enum State { Flying, NotFlying }
    private State _state = State.NotFlying;
    
    internal Rectangle BoundingBox
    {
        get => new Rectangle((int)_position.X, (int)_position.Y, _texture.Width / FrameCount, _texture.Height);
    }
    
    internal bool Shootable { get => _state == State.NotFlying; }

    internal void Initialize(float speed, Rectangle gameBoundingBox)
    {
        _position = Vector2.Zero;
        _direction = Vector2.Zero;
        _speed = speed;
        _gameBoundingBox = gameBoundingBox;
    }
    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Fireball");
        _animation = new SimpleAnimation(_texture, _texture.Width / FrameCount, _texture.Height, FrameCount, 10f);
    }
    internal void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        switch(_state)
        {
            case State.Flying:
                _position += _direction * _speed * dt;
                _animation.Update(gameTime);
                if (!BoundingBox.Intersects(_gameBoundingBox))
                {
                    _state = State.NotFlying;
                }
                break;
            case State.NotFlying:
                break;
        }
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        switch(_state)
        {
            case State.Flying:
                _animation.Draw(spriteBatch, _position, SpriteEffects.None);
                break;
            case State.NotFlying:
                break;
        }
    }

    internal void Shoot(Vector2 position, Vector2 direction)
    {
        if(_state != State.Flying)
        {
            _state = State.Flying;
            _position = position;
            _direction = direction;
        }
    }

    internal bool ProcessCollision(Rectangle boundingBox)
    {
        return _state == State.Flying && BoundingBox.Intersects(boundingBox);
    }
}