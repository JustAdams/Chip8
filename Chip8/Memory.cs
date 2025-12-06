
using Chip8.Common;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Chip8.Tests")]
namespace Chip8;

/// <summary>
/// Represents the system memory including RAM and registers.
/// </summary>
internal class Memory
{
    public byte[] RAM { get; private set; }

    private byte[] fonts =
    {
        0xF0, 0x90, 0x90, 0x90, 0xF0, // 0
        0x20, 0x60, 0x20, 0x20, 0x70, // 1
        0xF0, 0x10, 0xF0, 0x80, 0xF0, // 2
        0xF0, 0x10, 0xF0, 0x10, 0xF0, // 3
        0x90, 0x90, 0xF0, 0x10, 0x10, // 4
        0xF0, 0x80, 0xF0, 0x10, 0xF0, // 5
        0xF0, 0x80, 0xF0, 0x90, 0xF0, // 6
        0xF0, 0x10, 0x20, 0x40, 0x40, // 7
        0xF0, 0x90, 0xF0, 0x90, 0xF0, // 8
        0xF0, 0x90, 0xF0, 0x10, 0xF0, // 9
        0xF0, 0x90, 0xF0, 0x90, 0x90, // A
        0xE0, 0x90, 0xE0, 0x90, 0xE0, // B
        0xF0, 0x80, 0x80, 0x80, 0xF0, // C
        0xE0, 0x90, 0x90, 0x90, 0xE0, // D
        0xF0, 0x80, 0xF0, 0x80, 0xF0, // E
        0xF0, 0x80, 0xF0, 0x80, 0x80  // F
    };

    /// <summary>
    /// Starting font location in memory for a given hexadecimal character.
    /// </summary>
    public Dictionary<byte, byte> FontLocation { get; init; }

    public Memory()
    {
        RAM = new byte[4096];
        LoadMemory(fonts, 0x050);

        FontLocation = new Dictionary<byte, byte>()
        {
            { 0x0, 0x50 }, // 0
            { 0x1, 0x55 }, // 1
            { 0x2, 0x60 }, // 2
            { 0x3, 0x65 }, // 3
            { 0x4, 0x70 }, // 4
            { 0x5, 0x75 }, // 5
            { 0x6, 0x80 }, // 6
            { 0x7, 0x85 }, // 7
            { 0x8, 0x90 }, // 8
            { 0x9, 0x95 }, // 9
            { 0xA, 0x9A }, // A
            { 0xB, 0x9F }, // B
            { 0xC, 0xA4 }, // C
            { 0xD, 0xA9 }, // D
            { 0xE, 0xAE }, // E
            { 0xF, 0xB3 }, // F
        };
    }

    public void LoadMemory(byte[] load, int startPos)
    {
        Array.Copy(load, 0, RAM, startPos, load.Length);
    }

    /// <summary>
    /// Gets the 16-bit instruction set starting at the provided index and stores it in an OpCode struct. An instruction is derived from two consecutive bytes in RAM.
    /// </summary>
    /// <param name="idx">Starting index of the instruction set.</param>
    /// <returns>OpCode</returns>
    public OpCode GetInstruction(int idx)
    {
        ushort instructionSet = 0;
        instructionSet += RAM[idx];
        instructionSet <<= 8;
        instructionSet += RAM[idx + 1];

        OpCode opCode = new OpCode(instructionSet);
        return opCode;
    }
}
