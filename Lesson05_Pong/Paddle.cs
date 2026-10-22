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
    private bool _boosted = false;
    // in Pong, when the player presses "Shift", set this to true
    // when they're not pressing it, set it to false
    internal bool Boosted
    {
        set{ _boosted = value;}
    }

    // let's say this is the Brick class
    private bool _inPlay = true;
    internal bool InPlay
    {
        set{_inPlay = value;}
    }

    // "value" is what Direction was set equal to
    // _velocity.Length() extracts the speed from the vector
    internal Vector2 Direction
    {
        set{ _velocity = value * _PaddleSpeed;}
    }

    // internal Rectangle BoundingBox
    // {
    //     get { return new Rectangle((int)_position.X, (int)_position.Y, _PaddleWidth, _PaddleHeight);}
    // }

    // BoundingBox for the Brick class
    internal Rectangle BoundingBox
    {
        get 
        {
            if(_inPlay) 
                return new Rectangle((int)_position.X, (int)_position.Y, _PaddleWidth, _PaddleHeight);
            else
                return Rectangle.Empty;
        }
    }

    internal void Initialize(Rectangle playAreaBoundingBox, Vector2 initialPosition)
    {
        _position = initialPosition;
        _playAreaBoundingBox = playAreaBoundingBox;
        _velocity = new Vector2(0, _PaddleSpeed);

    }
    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Paddle");
    }
    internal void Update(GameTime gameTime)
    {
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

        if(_boosted)
        {
            // Update the _position at a fast speed
        }
        else
            _position += _velocity * dt;

        // pin the paddle at the to or bottom if it has bled over
        if(_position.Y <= _playAreaBoundingBox.Top)
            _position.Y = _playAreaBoundingBox.Top;
        else if(_position.Y + _PaddleHeight >= _playAreaBoundingBox.Bottom)
            _position.Y = _playAreaBoundingBox.Bottom - _PaddleHeight;
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        if(_inPlay)
            spriteBatch.Draw(_texture, BoundingBox, Color.White); 
    }
}