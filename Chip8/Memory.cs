
using Chip8.Common;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Chip8Tests")]
namespace Chip8;

/// <summary>
/// Represents the system memory including RAM and registers.
/// </summary>
internal class Memory
{
    public int[] RAM { get; private set; }

    private int[] fonts =
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

    public Memory()
    {
        RAM = new int[4096];
        LoadMemory(fonts, 0x050);
    }

    public void LoadMemory(int[] load, int startPos)
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
        int instructionSet = 0;
        instructionSet += RAM[idx];
        instructionSet <<= 8;
        instructionSet += RAM[idx + 1];

        OpCode opCode = new OpCode(instructionSet);
        return opCode;
    }
}
