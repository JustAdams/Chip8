namespace Chip8.Common;

internal readonly struct OpCode
{
    /// <summary>
    /// First nibble
    /// </summary>
    public int F { get; init; }

    /// <summary>
    /// Second nibble
    /// </summary>
    public int X { get; init; }
    /// <summary>
    /// Third nibble
    /// </summary>
    public int Y { get; init; }
    /// <summary>
    /// Fourth nibble
    /// </summary>
    public int N { get; init; }
    /// <summary>
    /// Third and fourth nibbles.
    /// </summary>
    public int NN { get; init; }
    /// <summary>
    /// Second, third, and fourth nibbles.
    /// </summary>
    public int NNN { get; init; }

    /// <summary>
    /// Full opcode
    /// </summary>
    public int NNNN { get; init; }

    public OpCode(int opcode)
    {
        F = (opcode & 0xF000) >> 12;
        X = (opcode & 0x0F00) >> 8;
        Y = (opcode & 0x00F0) >> 4;
        N = opcode & 0x000F;
        NN = opcode & 0x00FF;
        NNN = opcode & 0x0FFF;
        NNNN = opcode;
    }

    public override string ToString()
    {
        return "" + NNNN;
    }
}
