using Chip8.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chip8.Monogame;

public class Game1 : Core
{
    const int height = 64;
    const int width = 32;
    const int scale = 10;

    Color onColor = Color.Black;
    Color offColor = Color.White;
    
    Chip8 chip8;

    private Color[] colorBuffer;
    private Texture2D _displayTexture;

    public Game1() : base("Chip 8 Emulator", height * scale, width * scale, false)
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {

        base.Initialize();

        colorBuffer = new Color[height * width];
        _displayTexture = new Texture2D(GraphicsDevice, height, width);
    }

    protected override void LoadContent()
    {

        chip8 = new Chip8();
        ROM rom = new ROM("C:\\Users\\adams\\Programming\\C#\\Chip8\\Chip8\\ROMs\\test_opcode.ch8");

        chip8.LoadROM(rom);

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        chip8.Cycle();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // might not want to clear the display each update, since chip8 purposely flips the display bit
       // GraphicsDevice.Clear(offColor);

        UpdateDisplay();

        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        SpriteBatch.Draw(_displayTexture, new Rectangle(0, 0, 64 * 10, 32 * 10), offColor);
        SpriteBatch.End();

        base.Draw(gameTime);
    }

    private void UpdateDisplay()
    {
        for (int i = 0; i < 64 * 32; i++)
        {
            colorBuffer[i] = chip8.Display.Pixels[i] ? onColor : offColor;
        }
        _displayTexture.SetData(colorBuffer);
    }
}
