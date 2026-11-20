using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson07_MA_Next;

public class Actor
{
    protected SimpleAnimation _animationAlive, _animationDying;
    protected float _dyingTimer = 0;

    protected enum State {Alive, Dying, Dead}
    protected State _state = State.Alive;

    protected Vector2 _position, _dimensions;
    protected float _speed;
    protected Texture2D _texture;

    internal Vector2 Direction {get; set;} = Vector2.Zero;
    internal bool Alive { get => _state == State.Alive; }

    protected Rectangle _gameBoundingBox;

    internal Rectangle BoundingBox { get => 
        new Rectangle((int)_position.X, (int)_position.Y, (int)_dimensions.X, (int)_dimensions.Y);}

    // the children want some of this code, and want to add some extra code of their own
    // so, we make it "virtual"
    internal virtual void Initialize(Vector2 position, float speed, Rectangle gbb)
    {
        _position = position;  
        _speed = speed; 
        _gameBoundingBox = gbb;
    }

}