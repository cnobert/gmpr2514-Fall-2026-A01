using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson04_Key_Input;

public class KeyboardInputGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private SpriteFont _arial;

    private string _message = "It is September 25.";

    // 60 times/second, we will inspect what the state of the keyboard is
    // _kbPreviousState will always store the state of the keyboard from the *last* call to update
    private KeyboardState _kbPreviousState;

    public KeyboardInputGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }
    protected override void Initialize()
    {
        base.Initialize();
    }
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _arial = Content.Load<SpriteFont>("SystemArialFont");
    }
    protected override void Update(GameTime gameTime)
    {
        KeyboardState kbCurrentState = Keyboard.GetState();
        _message = "";

        #region arrow keys
        if(kbCurrentState.IsKeyDown(Keys.Up))
        {
            _message += "Up ";
        }
        if(kbCurrentState.IsKeyDown(Keys.Down))
        {
            _message += "Down ";
        }
        if(kbCurrentState.IsKeyDown(Keys.Left))
        {
            _message += "Left ";
        }
        if(kbCurrentState.IsKeyDown(Keys.Right))
        {
            _message += "Right ";
        }
        #endregion

        #region spacebar states
        // this is a "new" press
        if(_kbPreviousState.IsKeyUp(Keys.Space) && kbCurrentState.IsKeyDown(Keys.Space))
        {
            _message += "\n";
            _message += "Space pressed\n";
            _message += "----------------------------------------\n";
            _message += "----------------------------------------\n";
            _message += "----------------------------------------\n";
        }
        else if (kbCurrentState.IsKeyDown(Keys.Space))
        {
             _message += "\n";
            _message += "Space held";           
        }
        else if(_kbPreviousState.IsKeyDown(Keys.Space))
        {
            _message += "\n";
            _message += "Space released\n";
            _message += "----------------------------------------\n";
            _message += "----------------------------------------\n";
            _message += "----------------------------------------\n";
        }

        #endregion
        _kbPreviousState = kbCurrentState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.DrawString(_arial, _message, Vector2.Zero, Color.MintCream);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
