using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson05_Pong;

public class Paddle
{
    private const int _PaddleWidth = 6 * 2, _PaddleHeight = 54 * 2;
    private const float _PaddleSpeed = 240;

    private Texture2D _texture;
    private Vector2 _position, _velocity; // velocity = speed * direction
    private Rectangle _playAreaBoundingBox;

    // "value" is what Direction was set equal to
    // _velocity.Length() extracts the speed from the vector
    internal Vector2 Direction
    {
        set{ _velocity = value * _velocity.Length();}
    }

    internal void Initialize(Rectangle playAreaBoundingBox, Vector2 initialPosition)
    {
        _position = initialPosition;
        _playAreaBoundingBox = playAreaBoundingBox;
    }
    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Paddle");
    }
    internal void Update(GameTime gameTime)
    {
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
        _position += _velocity * dt;

        // pin the paddle at the to or bottom if it has bled over
        if(_position.Y <= _playAreaBoundingBox.Top)
            _position.Y = _playAreaBoundingBox.Top;
        else if(_position.Y + _PaddleHeight >= _playAreaBoundingBox.Bottom)
            _position.Y = _playAreaBoundingBox.Bottom - _PaddleHeight;
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        Rectangle paddleRectangle = new Rectangle(
                (int)_position.X, (int)_position.Y,
                _PaddleWidth, _PaddleHeight);
        spriteBatch.Draw(_texture, paddleRectangle, Color.White); 
    }
}