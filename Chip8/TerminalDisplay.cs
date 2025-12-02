namespace Chip8;

/// <summary>
/// Uses the terminal as the display screen. The terminal should be sized at a minimum height/width or it's going to have weird wrapping and not display properly.
/// </summary>
internal class TerminalDisplay : IDisplay
{

    DisplayBuffer display;

    public TerminalDisplay(DisplayBuffer display)
    {
        this.display = display;
    }

    public void ClearDisplay()
    {
        display.Clear();
    }
    public void DrawDisplay()
    {

        for (int r = 0; r < DisplayBuffer.HEIGHT; r++)
        {
            for (int c = 0; c < DisplayBuffer.WIDTH; c++)
            {
                int index = r * DisplayBuffer.WIDTH + c;
                if (display.Pixels[index])
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }

    public bool GetPixel(int x, int y)
    {
        return display.Pixels[x * DisplayBuffer.WIDTH + y];
    }

    public void SetPixel(int x, int y, bool bit)
    {
        display.SetPixel(x, y, bit);
    }
}
