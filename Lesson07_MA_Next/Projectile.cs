// Proud parent to CannonBall and FireBall
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson07_MA_Next;

public abstract class Projectile
{
    protected Vector2 _position, _direction, _dimensions;
    protected float _speed;

    protected Rectangle _gameBoundingBox;
    protected enum State { Flying, NotFlying }
    protected State _state = State.NotFlying;

    internal Rectangle BoundingBox
    {
        get =>  new Rectangle((int)_position.X, (int)_position.Y, 
                (int)_dimensions.X, (int)_dimensions.Y);
    }
    internal bool Shootable { get => _state == State.NotFlying; }

    internal void Initialize(float speed, Rectangle gameBoundingBox)
    {
        _position = Vector2.Zero;
        _direction = Vector2.Zero;
        _speed = speed;
        _gameBoundingBox = gameBoundingBox;
    }

    //"virtual" means "my children MAY override this method, but they don't have to"
    internal virtual void Update(GameTime gameTime)
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

    // I want to force the children of this class to have a draw method
    // "abstract" forces the child class to define a method with this signature
    // but does not define said method itself
    internal abstract void Draw(SpriteBatch spriteBatch);

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