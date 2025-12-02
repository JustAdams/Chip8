namespace Chip8;

/// <summary>
/// Represents the buffer space for a Chip8 display. Drawing should be handled by the chosen front-end medium.
/// </summary>
public sealed class DisplayBuffer
{
    public const int WIDTH  = 64;
    public const int HEIGHT = 32;

    public bool[] Pixels { get; } = new bool[WIDTH * HEIGHT];

    public void Clear()
    {
        Array.Clear(Pixels);
    }

    /// <summary>
    /// Sets a pixel to a desired value.
    /// </summary>
    /// <param name="x">Row</param>
    /// <param name="y">Column</param>
    /// <param name="pixel">True = on, False = off</param>
    public void SetPixel(int x, int y, bool pixel)
    {
        int index = y * WIDTH + x;
        Pixels[index] = pixel;
    }

    /// <summary>
    /// Inverts a pixel.
    /// </summary>
    /// <param name="x">Row</param>
    /// <param name="y">Column</param>
    public void FlipPixel(int x, int y)
    {
        int index = y * WIDTH + x;
        Pixels[index] = !Pixels[index];
    }
}
