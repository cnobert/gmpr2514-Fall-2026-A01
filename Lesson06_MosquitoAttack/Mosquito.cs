using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson06_MosquitoAttack;

public class Mosquito
{
    private SimpleAnimation _animation;
    private Vector2 _position;
    private float _speed;
    Texture2D _texture;
    internal Vector2 Direction {get; set;} = Vector2.Zero;

    private Rectangle _gameBoundingBox;

    // todo: refactor so that it doesn't depend on knowing there are 11 frames to find out the width
    private Rectangle BoundingBox
    {
        get 
        { 
            return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width / 11, _texture.Height);
        }
    }

    internal void Initialize(Vector2 position, float speed, Vector2 direction, Rectangle gameBoundingBox)
    {
        _position = position;
        _speed = speed;
        Direction = direction;
        _gameBoundingBox = gameBoundingBox;
    }
    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Mosquito");
        _animation = new SimpleAnimation(_texture, _texture.Width / 11, _texture.Height, 11, 8f);
    }
    internal void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _position += Direction * _speed * dt;

        // todo: look into making the below better (Direction.X *= -1 didn't work)
        if(BoundingBox.Left < _gameBoundingBox.Left || BoundingBox.Right > _gameBoundingBox.Right)
            Direction = new Vector2(Direction.X * -1, Direction.Y);

        _animation?.Update(gameTime);
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        _animation?.Draw(spriteBatch, _position, SpriteEffects.None);
    }
}