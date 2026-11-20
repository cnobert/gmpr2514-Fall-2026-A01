using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson07_MA_Next;

public class Mosquito
{
    private const float _DyingDuration = 1f;
    
    private SimpleAnimation _animationAlive, _animationDying;
    private float _dyingTimer = 0;
    private enum State {Alive, Dying, Dead}
    private State _state = State.Alive;
    
    private Vector2 _position;
    private float _speed;
    private Texture2D _texture;
    private FireBall _fireBall;
    private const float _FireBallSpeed = 180f;
    private const float _ShotsPerSecond = 0.4f;
    private Rectangle _gameBoundingBox;

    internal bool Alive { get => _state == State.Alive;}

    internal Vector2 Direction {get; set;} = Vector2.Zero;
    // todo: refactor so that it doesn't depend on knowing there are 11 frames to find out the width
    internal Rectangle BoundingBox
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
        _gameBoundingBox = gameBoundingBox;
        Direction = direction;
        
        _fireBall = new FireBall();
        _fireBall.Initialize(_FireBallSpeed, gameBoundingBox);
    }
    internal void LoadContent(ContentManager content)
    {
        _fireBall.LoadContent(content);
        _texture = content.Load<Texture2D>("Mosquito");
        _animationAlive = new SimpleAnimation(_texture, _texture.Width / 11, _texture.Height, 11, 8f);
        
        Texture2D dyingTexture = content.Load<Texture2D>("Poof");
        int frameWidth = 16;
        int frameHeight = dyingTexture.Height;
        int frameCount = dyingTexture.Width / frameWidth;
        float framesPerSecond = 8f;
        _animationDying = new SimpleAnimation(dyingTexture, frameWidth, frameHeight, frameCount, framesPerSecond);
    
    }
    internal void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        switch(_state)
        {
            case State.Alive:
                _position += Direction * _speed * dt;
                if(BoundingBox.Left < _gameBoundingBox.Left || BoundingBox.Right > _gameBoundingBox.Right)
                    Direction = new Vector2(Direction.X * -1, Direction.Y);
                _animationAlive?.Update(gameTime);
                
                if (_fireBall.Shootable && Random.Shared.Next(150) == 0)
                {
                    Vector2 firePosition = new Vector2(
                        BoundingBox.Center.X - _fireBall.BoundingBox.Width / 2f,
                        BoundingBox.Bottom);

                    _fireBall.Shoot(firePosition, new Vector2(0, 1));
                }
                break;
            case State.Dying:
                _dyingTimer += dt;
                if(_dyingTimer >= _DyingDuration)
                    _state = State.Dead;
                _animationDying?.Update(gameTime);
                break;
            case State.Dead:
                break;
        }
        _fireBall.Update(gameTime);
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        switch(_state)
        {
            case State.Alive:
                _animationAlive?.Draw(spriteBatch, _position, SpriteEffects.None);
                break;
            case State.Dying:
                _animationDying?.Draw(spriteBatch, _position, SpriteEffects.None);
                break;
            case State.Dead:
                break;
        }
        _fireBall.Draw(spriteBatch);
    }

    internal bool ProcessCollision(Rectangle boundingBox)
    {
        return _fireBall.ProcessCollision(boundingBox);
    }

    internal void Die()
    {
        _state = State.Dying;
        _dyingTimer = 0;
    }
}