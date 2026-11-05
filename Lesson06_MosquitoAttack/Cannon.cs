using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson06_MosquitoAttack;

public class Cannon
{
    private SimpleAnimation _animation;
    private Vector2 _position;
    private float _speed;
    internal Vector2 Direction {get; set;} = Vector2.Zero;

    private Rectangle _gameBoundingBox;

    private CannonBall _cannonBall;

    private Texture2D _texture;
    private Rectangle BoundingBox
    {
        get 
        { 
            return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width / 4, _texture.Height);
        }
    }

    // private Vector2 _direction = Vector2.Zero;
    // internal Vector2 Direction
    // {
    //     get
    //     {
    //         return _direction;
    //     }
    //     set
    //     {
    //         _direction = value;
    //     }
    // }
    internal void Initialize(Vector2 position, float speed, Rectangle gbb)
    {
        _position = position;  
        _speed = speed;  
        _cannonBall = new CannonBall();
        _gameBoundingBox = gbb;
        _cannonBall.Initialize(40, _gameBoundingBox);
    }
    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Cannon");
        _animation = new SimpleAnimation(_texture, _texture.Width / 4, _texture.Height, 4, 2f);
        _cannonBall.LoadContent(content);
    }
    internal void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _position += Direction * _speed * dt;
        // "?" checks for null before executing the method
        if(Direction != Vector2.Zero)
            _animation?.Update(gameTime);

        _cannonBall.Update(gameTime);
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        SpriteEffects effects = SpriteEffects.None;
        if(Direction.X < 0)
            effects = SpriteEffects.FlipHorizontally;
        _animation?.Draw(spriteBatch, _position, effects);

        _cannonBall.Draw(spriteBatch);
    }

    internal void Shoot()
    {
        float cbPositionX = BoundingBox.Center.X - _cannonBall.BoundingBox.Width / 2;
        float cbPositionY = BoundingBox.Top - _cannonBall.BoundingBox.Height;
        _cannonBall.Shoot(new Vector2(cbPositionX, cbPositionY), new Vector2(0, -1));
    }
}