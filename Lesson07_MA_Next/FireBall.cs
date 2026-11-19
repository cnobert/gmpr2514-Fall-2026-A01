using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson07_MA_Next;

public class FireBall : Projectile
{
    private SimpleAnimation _animation;
    private const int _FrameCount = 8;
    private Texture2D _texture;

    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Fireball");
        _animation = new SimpleAnimation(_texture, _texture.Width / _FrameCount, _texture.Height, _FrameCount, 10f);
        _dimensions = new Vector2(_texture.Width / _FrameCount, _texture.Height);
    }
    //"override" means "I am explicitly hiding this method
    // that I inherit from my parent
    internal override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        switch(_state)
        {
            case State.Flying:
                _animation.Update(gameTime);
                break;
            case State.NotFlying:
                break;
        }
    }
    internal override void Draw(SpriteBatch spriteBatch)
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
}