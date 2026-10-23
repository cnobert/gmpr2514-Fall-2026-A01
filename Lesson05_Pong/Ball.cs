using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson05_Pong;

public class Ball
{
    private const int _WidthAndHeight = 21, _CollisionTimerIntervalMillis = 400;

    private Texture2D _texture;
    private Vector2 _position, _velocity; // velocity = speed * direction
    private Rectangle _playAreaBoundingBox;

    private int _collisionTimerMillis;

    internal Rectangle BoundingBox
    {
        get { return new Rectangle((int)_position.X, (int)_position.Y, _WidthAndHeight, _WidthAndHeight);}
    }

    internal void Initialize(Rectangle playAreaBoundingBox)
    {
        _playAreaBoundingBox = playAreaBoundingBox;
        _position =  new Vector2(150, 195);
        _velocity = new Vector2(-160, -160); //up and to the left
    }
    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Ball");
    }
    internal void Update(GameTime gameTime)
    {
        _collisionTimerMillis += gameTime.ElapsedGameTime.Milliseconds;

        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
        _position += _velocity * dt;

        // bounce off sides
        if(_position.X <= _playAreaBoundingBox.Left ||
            (_position.X + _WidthAndHeight) >= _playAreaBoundingBox.Right)
        {
            _velocity.X *= -1;
        }
        
        // bounce off top and bottom
        if(_position.Y <= _playAreaBoundingBox.Top ||
            (_position.Y + _WidthAndHeight) >= _playAreaBoundingBox.Bottom)
        {
            _velocity.Y *= -1;
        }
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        Rectangle ballRectangle = new Rectangle(
                (int)_position.X, (int)_position.Y, 
                _WidthAndHeight, _WidthAndHeight);
        spriteBatch.Draw(_texture, ballRectangle, Color.White);

    }

    internal bool ProcessCollision(Rectangle otherBoundingBox)
    {
        bool didCollide = false;

        if(_collisionTimerMillis >= _CollisionTimerIntervalMillis &&
            BoundingBox.Intersects(otherBoundingBox))
        {
            _collisionTimerMillis = 0;
            // collision!
            didCollide = true;
            // intersection represents the rectangle created by the overlapping two rectangles
            Rectangle intersection = Rectangle.Intersect(BoundingBox, otherBoundingBox);
            if(intersection.Width > intersection.Height)
                // horizontal rectangle, so it's a top or bottom collision
                _velocity.Y *= -1;
            else
                //vertical rectangle, so it's a side collision
                _velocity.X *= -1;

        }

        return didCollide;
    }
}