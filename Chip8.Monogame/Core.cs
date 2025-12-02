using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Chip8.Monogame;

public class Core : Game
{
    internal static Core s_Instance;
    public static Core Instance => s_Instance;

    public static GraphicsDeviceManager Graphics { get; private set; }

    public static new GraphicsDevice GraphicsDevice { get; private set; }

    public static SpriteBatch SpriteBatch { get; private set; }

    public Core(string title, int width, int height, bool fullScreen = false)
    {
        if (s_Instance != null)
        {
            throw new InvalidOperationException($"Only a single Core instance can exist.");
        }

        s_Instance = this;

        Graphics = new GraphicsDeviceManager(this);

        // Set graphics defaults
        Graphics.PreferredBackBufferWidth = width;
        Graphics.PreferredBackBufferHeight = height;
        Graphics.IsFullScreen = fullScreen;

        Graphics.ApplyChanges();

        Window.Title = title;

        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        GraphicsDevice = base.GraphicsDevice;
        SpriteBatch = new SpriteBatch(GraphicsDevice);

    }
}
