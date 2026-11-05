using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson06_MosquitoAttack;

public class CannonBall
{
    Texture2D _texture;
    private Vector2 _position;
    private float _speed;
    internal Vector2 Direction {get; set;} = Vector2.Zero;

    private Rectangle _gameBoundingBox;

    private enum State { Flying, NotFlying }
    private State _state = State.Flying;
    
    internal Rectangle BoundingBox
    {
        get => new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
    }
    internal void Initialize(float speed, Rectangle gameBoundingBox)
    {
        _position = new Vector2(40, 400);
        Direction = new Vector2(0, -1);
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
                _position += Direction * _speed * dt;
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
}