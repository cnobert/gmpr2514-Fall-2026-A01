using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson06_MosquitoAttack;

public class Mosquito
{
    private SimpleAnimation _animation;
    private Vector2 _position;
    private float _speed;
    internal Vector2 Direction {get; set;} = Vector2.Zero;

    private Rectangle _gameBoundingBox;

    internal void Initialize(Vector2 position, float speed, Vector2 direction, Rectangle gameBoundingBox)
    {
        _position = position;
        _speed = speed;
        Direction = direction;
        _gameBoundingBox = gameBoundingBox;
    }
    internal void LoadContent(ContentManager content)
    {
        Texture2D texture = content.Load<Texture2D>("Mosquito");
        _animation = new SimpleAnimation(texture, texture.Width / 11, texture.Height, 11, 8f);
    }
    internal void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _position += Direction * _speed * dt;

        _animation?.Update(gameTime);
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        _animation?.Draw(spriteBatch, _position, SpriteEffects.None);
    }
}