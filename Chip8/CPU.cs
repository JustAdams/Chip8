using Chip8.Common;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Chip8Tests")]
namespace Chip8;

/// <summary>
/// Central processing unit to process opcodes and perform the operations on memory.
/// </summary>
internal class CPU
{
    private Memory memory;
    IDisplay display;

    public int ProgramCounter { get; private set; }
    public int IndexRegister { get; private set; }

    /// <summary>
    /// 16 8-bit general-purpose variable registers labeled 0 - F.
    /// </summary>
    public int[] VariableRegisters { get; private set; }

    public CPU(Memory memory, IDisplay display)
    {
        this.memory = memory;
        this.display = display;

        ProgramCounter = 0x200;

        VariableRegisters = new int[16];
    }

    public void Cycle()
    {
        OpCode opCode = FetchInstruction();
        ExecuteInstruction(opCode);
    }

    private OpCode FetchInstruction()
    {
        OpCode opCode = memory.GetInstruction(ProgramCounter);

        ProgramCounter += 2;

        return opCode;
    }

    private void ExecuteInstruction(OpCode opCode)
    {
        switch (opCode.F)
        {
            case 0x0:
                Op_00E0();
                break;
            case 0x1:
                Op_1NNN(opCode.NNN);
                break;
            case 0x6:
                Op_6XNN(opCode.X, opCode.NN);
                break;
            case 0x7:
                Op_7XNN(opCode.X, opCode.NN);
                break;
            case 0xA:
                Op_ANNN(opCode.NNN);
                break;
            case 0xD:
                Op_DXYN(opCode.X, opCode.Y, opCode.N);
                break;
            default:
                throw new NotImplementedException("OpCode not supported: " + opCode.NNNN);
        }
    }

    /// <summary>
    /// Clears the display turning all pixels off.
    /// </summary>
    private void Op_00E0()
    {
        display.ClearDisplay();
    }

    /// <summary>
    /// Sets the program counter to NNN.
    /// </summary>
    /// <param name="NNN">Memory address to jump the program counter to.</param>
    private void Op_1NNN(int NNN)
    {
        ProgramCounter = NNN;
    }

    /// <summary>
    /// Sets register VX to the value NN.
    /// </summary>
    /// <param name="X">VX register.</param>
    /// <param name="NN">Value to set VX to.</param>
    private void Op_6XNN(int X, int NN)
    {
        VariableRegisters[X] = NN;
    }

    /// <summary>
    /// Adds the value NN to VX.
    /// </summary>
    /// <param name="X">VX register.</param>
    /// <param name="NN">Value to add to VX.</param>
    private void Op_7XNN(int X, int NN)
    {
        // TODO - doing a modulo here to simulate overflow since we are using integers.
        VariableRegisters[X] += NN % 256;
    }

    /// <summary>
    /// Sets the index register to NNN.
    /// </summary>
    /// <param name="NNN">Value to set the index register to.</param>
    private void Op_ANNN(int NNN)
    {
        IndexRegister = NNN;
    }

    private void Op_DXYN(int X, int Y, int N)
    {
        for (int row = 0; row < N; row++)
        {
            int currIndex = IndexRegister + row;
            int spriteByte = memory.RAM[currIndex];

            // modulo to handle screen wrap
            int yPos = (VariableRegisters[Y] + row) % 32;

            for (int col = 0; col < 8; col++)
            {
                // modulo to handle screen wrap
                int xPos = (VariableRegisters[X] + col) % 64;

                bool bit = (spriteByte >> (7 - col) & 1) == 1;
                display.SetPixel(yPos, xPos, bit);
            }
        }
    }
}
