using Chip8.Common;

namespace Chip8;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting Chip8...");

        Chip8 chip8 = new Chip8();
        DisplayBuffer displayBuffer = chip8.Display;
        TerminalDisplay display = new TerminalDisplay(displayBuffer);

        ROM rom = new ROM("C:\\Users\\adams\\Programming\\C#\\Chip8\\Chip8\\ROMs\\test_opcode.ch8");

        chip8.LoadROM(rom);

        bool isPlaying = true;
        while (isPlaying)
        {
            // Retrieve user input


            // Retrieve next instruction set
            chip8.Cycle();

            // Execute instructions


            // Draw to the screen
            // display.DrawDisplay();

            //  Thread.Sleep(200);
        }
        Console.WriteLine("Goodbye!");
    }
}
