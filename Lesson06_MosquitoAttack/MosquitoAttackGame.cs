using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Lesson06_MosquitoAttack;

public class MosquitoAttackGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private const int _WindowWidth = 550;
    private const int _WindowHeight = 400;

    private Texture2D _background;
    private SpriteFont _arial;
    private string _message;

    private Cannon _cannon;
    private Mosquito[] _mosquitoes;

    private enum GameState {Playing, Paused, Over}
    private GameState _gameState = GameState.Playing;
    private KeyboardState _kbCurrent, _kbPrevious;

    private Rectangle GameBoundingBox
    {
        get { return new Rectangle(0, 0, _WindowWidth, _WindowHeight);}
    }

    public MosquitoAttackGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.ApplyChanges();
        
        _cannon = new Cannon();
        _cannon.Initialize(new Vector2(50, 325), 150, GameBoundingBox);

        _mosquitoes = new Mosquito[10];
        for(int c = 0; c < _mosquitoes.Length; c++)
        {
            _mosquitoes[c] = new Mosquito();
        }
        // place the Mosquitoes randomly
        Random random = new Random();
        foreach(Mosquito m in _mosquitoes)
        {
            int direction = random.Next(1, 3);
            if(direction == 2)
                direction = -1;
            int xPosition = random.Next(1, _WindowWidth - 50);
            int yPosition = random.Next(1, 151);
            int speed = random.Next(150, 251);
            m.Initialize(new Vector2(xPosition, yPosition), speed, new Vector2(direction, 0), GameBoundingBox);
        }



        base.Initialize();
    }
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _background = Content.Load<Texture2D>("Background");
        _arial = Content.Load<SpriteFont>("SystemArialFont");
        _cannon.LoadContent(Content);
        foreach(Mosquito m in _mosquitoes)
            m.LoadContent(Content);

    }

    protected override void Update(GameTime gameTime)
    {
        _kbCurrent = Keyboard.GetState();
        switch(_gameState)
        {
            case GameState.Playing:        
                #region keyboard input
                if(_kbCurrent.IsKeyDown(Keys.A))
                    _cannon.Direction = new Vector2(-1, 0);
                else if(_kbCurrent.IsKeyDown(Keys.D))
                    _cannon.Direction = new Vector2(1, 0);
                else
                    _cannon.Direction = Vector2.Zero; // new Vector2(0,0)

                if(Pressed(Keys.Space))
                    _cannon.Shoot();

                if(Pressed(Keys.P))
                {
                    _gameState = GameState.Paused;
                    _message = "Game Paused, press P to start playing.";
                }
                #endregion
                _cannon.Update(gameTime);
                foreach(Mosquito m in _mosquitoes)
                    m.Update(gameTime);
                break;
            case GameState.Paused:
                if(Pressed(Keys.P))
                    _gameState = GameState.Playing;
                break;
            case GameState.Over:
            
                break;
        }
        _kbPrevious = _kbCurrent;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        switch(_gameState)
        {
            case GameState.Playing:        
                _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
                _cannon.Draw(_spriteBatch);
                foreach(Mosquito m in _mosquitoes)
                    m.Draw(_spriteBatch);
                break;
            case GameState.Paused:
                _spriteBatch.Draw(_background, Vector2.Zero, Color.Silver);
                _spriteBatch.DrawString(_arial, _message, new Vector2(10, 135), Color.White);
                _cannon.Draw(_spriteBatch);
                foreach(Mosquito m in _mosquitoes)
                    m.Draw(_spriteBatch);
                break;
            case GameState.Over:
                break;
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    private bool Pressed(Keys key)
    {
        return _kbCurrent.IsKeyDown(key) && _kbPrevious.IsKeyUp(key);
    }
}