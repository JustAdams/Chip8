using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chip8;

/// <summary>
/// Uses the terminal as the display screen. The terminal should be sized at a minimum height/width or it's going to have weird wrapping and not display properly.
/// </summary>
internal class TerminalDisplay : IDisplay
{
    public const int WIDTH = 64;
    public const int HEIGHT = 32;

    bool[,] pixels;

    public TerminalDisplay()
    {
        pixels = new bool[HEIGHT, WIDTH];
    }

    public void ClearDisplay()
    {
        pixels = new bool[HEIGHT, WIDTH];
    }

    public void DrawDisplay()
    {
        for (int r = 0; r < HEIGHT; r++)
        {
            for (int c = 0; c < WIDTH; c++)
            {
                if (pixels[r, c])
                {
                    Console.Write("*");
                } else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }

    public bool GetPixel(int x, int y)
    {
        return pixels[x, y];
    }

    /// <summary>
    /// Flips the value of the given pixel.
    /// </summary>
    /// <param name="x">Row</param>
    /// <param name="y">Col</param>
    public void SetPixel(int x, int y, bool bit)
    {
        pixels[x, y] = bit;
    }
}
