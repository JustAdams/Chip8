using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chip8;

public interface IDisplay
{

    /// <summary>
    /// Draws the pixel array to the screen.
    /// </summary>
    void DrawDisplay();

    void ClearDisplay();

    bool GetPixel(int x, int y);
    void SetPixel(int x, int y, bool bit);
}
