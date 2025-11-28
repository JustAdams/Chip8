
using Chip8.Common;

namespace Chip8
{
    public class Chip8
    {
        IDisplay display;
        Memory memory;
        CPU cpu;

        /// <summary>
        /// Memory start location for a ROM.
        /// </summary>
        int startAddr = 0x200;

        public Chip8(IDisplay display)
        {
            this.display = display;
            memory = new Memory();
            cpu = new CPU(memory, display);
        }

        /// <summary>
        /// Loads a ROM into memory.
        /// </summary>
        /// <param name="rom">Represents a ROM cartridge.</param>
        public void LoadROM(ROM rom)
        {
            memory.LoadMemory(rom.Data, startAddr);
        }

        public void Cycle()
        {
            cpu.Cycle();
        }
    }
}
