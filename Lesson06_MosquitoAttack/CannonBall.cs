using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson06_MosquitoAttack;

public class CannonBall
{
    Texture2D _texture;
    private Vector2 _position, _direction;
    private float _speed;

    private Rectangle _gameBoundingBox;
    private enum State { Flying, NotFlying }
    private State _state = State.NotFlying;
    
    internal Rectangle BoundingBox
    {
        get => new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
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
        _texture = content.Load<Texture2D>("CannonBall");
    }
    internal void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        switch(_state)
        {
            case State.Flying:
                _position += _direction * _speed * dt;
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
                spriteBatch.Draw(_texture, _position, Color.White);
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