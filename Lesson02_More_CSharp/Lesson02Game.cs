using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson02_MoreCSharp;

public class Lesson02Game : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _pixel;

    private Vector2 _position, _dimensions;

    private int _count;
    private float _spacing;

    public Lesson02Game()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _position = new Vector2(50, 200);
        _dimensions = new Vector2(60, 40);

        _count = 6;
        _spacing = 10;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _pixel = new Texture2D(GraphicsDevice, 1, 1);
        _pixel.SetData(new[] { Color.White });

    }

    protected override void Update(GameTime gameTime)
    {

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        for(int i = 0; i <_count; i++)
        {
            float x = _position.X + i * (_dimensions.X + _spacing);
            Rectangle r = 
                new Rectangle((int)x, (int)_position.Y, (int)_dimensions.X, (int)_dimensions.Y);
            _spriteBatch.Draw(_pixel, r, Color.LightYellow);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
