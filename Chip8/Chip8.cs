
using Chip8.Common;

namespace Chip8;

public class Chip8
{
    public DisplayBuffer Display { get; init; }

    private readonly Memory _memory;
    private readonly CPU _cpu;

    /// <summary>
    /// Memory start location for a ROM.
    /// </summary>
    int startAddr = 0x200;

    public Chip8()
    {
        _memory = new Memory();
        Display = new DisplayBuffer();

        _cpu = new CPU(_memory, Display);
    }

    /// <summary>
    /// Loads a ROM into memory.
    /// </summary>
    /// <param name="rom">Represents a ROM cartridge.</param>
    public void LoadROM(ROM rom)
    {
        _memory.LoadMemory(rom.Data, startAddr);
    }

    public void Cycle()
    {
        _cpu.Cycle();
    }
}
