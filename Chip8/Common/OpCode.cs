namespace Chip8.Common;

internal readonly struct OpCode
{
    /// <summary>
    /// First nibble
    /// </summary>
    public byte F { get; init; }

    /// <summary>
    /// Second nibble
    /// </summary>
    public byte X { get; init; }
    /// <summary>
    /// Third nibble
    /// </summary>
    public byte Y { get; init; }
    /// <summary>
    /// Fourth nibble
    /// </summary>
    public byte N { get; init; }
    /// <summary>
    /// Third and fourth nibbles.
    /// </summary>
    public byte NN { get; init; }
    /// <summary>
    /// Second, third, and fourth nibbles.
    /// </summary>
    public ushort NNN { get; init; }

    /// <summary>
    /// Full opcode
    /// </summary>
    public ushort NNNN { get; init; }

    public OpCode(ushort opcode)
    {
        F = (byte)((opcode & 0xF000) >> 12);
        X = (byte)((opcode & 0x0F00) >> 8);
        Y = (byte)((opcode & 0x00F0) >> 4);
        N = (byte)(opcode & 0x000F);
        NN = (byte)(opcode & 0x00FF);
        NNN = (ushort)(opcode & 0x0FFF);
        NNNN = opcode;
    }

    public override string ToString()
    {
        return $"0x{NNNN:X}";
    }
}
