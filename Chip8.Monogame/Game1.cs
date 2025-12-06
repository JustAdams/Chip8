using Chip8.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Chip8.Monogame;

public class Game1 : Core
{
    const int height = 64;
    const int width = 32;
    const int scale = 10;

    Color onColor = Color.White;
    Color offColor = Color.Black;

    Chip8 chip8;

    private Color[] colorBuffer;
    private Texture2D _displayTexture;

    public Dictionary<Keys, byte> InputMap { get; init; }

    public Game1() : base("Chip 8 Emulator", height * scale, width * scale, false)
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        InputMap = new Dictionary<Keys, byte>()
        {
            { Keys.D1, 0x1 },
            { Keys.D2, 0x2 },
            { Keys.D3, 0x3 },
            { Keys.NumPad4, 0xC },
            { Keys.Q, 0x4 },
            { Keys.W, 0x5 },
            { Keys.E, 0x6 },
            { Keys.R, 0xD },
            { Keys.A, 0x7 },
            { Keys.S, 0x8 },
            { Keys.D, 0x9 },
            { Keys.F, 0xE },
            { Keys.Z, 0xA },
            { Keys.X, 0x0 },
            { Keys.C, 0xB },
            { Keys.V, 0xF },
        };
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

        GetUserInput();
        chip8.Cycle();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // might not want to clear the display each update, since chip8 purposely flips the display bit
        // GraphicsDevice.Clear(offColor);

        UpdateDisplay();

        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        SpriteBatch.Draw(_displayTexture, new Rectangle(0, 0, 64 * 10, 32 * 10), Color.LightGreen);
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

    private void GetUserInput()
    {
        byte currKey = 0x0;

        KeyboardState keyboardState = Keyboard.GetState();
        if (keyboardState.GetPressedKeyCount() == 0)
        {
            chip8.KeyUp();
            return;
        }

        chip8.KeyDown(InputMap[keyboardState.GetPressedKeys()[0]]);

        currKey = InputMap[keyboardState.GetPressedKeys()[0]];
    }
}
