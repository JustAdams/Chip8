using Chip8.Common;

namespace Chip8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Chip8...");

            IDisplay display = new TerminalDisplay();
            Chip8 chip8 = new Chip8(display);

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
                display.DrawDisplay();
              //  Thread.Sleep(200);
            }
            Console.WriteLine("Goodbye!");
        }
    }
}
