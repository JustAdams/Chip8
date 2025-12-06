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
    private DisplayBuffer display;
    private Random _random;

    public bool KeyPressed { get; set; }
    public byte CurrentKey { get; set; }

    /// <summary>
    /// Current instruction in memory.
    /// </summary>
    public int ProgramCounter { get; private set; }
    /// <summary>
    /// Points to locations in memory.
    /// </summary>
    public ushort IndexRegister { get; private set; }

    /// <summary>
    /// 16 8-bit general-purpose variable registers labeled 0 - F.
    /// </summary>
    public byte[] VariableRegisters { get; private set; }

    public Stack<int> Subroutines { get; private set; }

    public CPU(Memory memory, DisplayBuffer display)
    {
        this.memory = memory;
        this.display = display;

        _random = new Random();

        ProgramCounter = 0x200;

        VariableRegisters = new byte[16];
        Subroutines = new Stack<int>();
    }

    /// <summary>
    /// Fetches the next instruction set from memory and executes it.
    /// </summary>
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
        // todo: maybe a better way to handle all of these opcodes instead of a big switch case
        switch (opCode.F)
        {
            case 0x0:
                switch (opCode.N)
                {
                    case 0x0:
                        Op_00E0();
                        break;
                    case 0xE:
                        Op_00EE();
                        break;
                    default:
                        throw new NotImplementedException("OpCode not supported: " + opCode.ToString());
                }
                break;
            case 0x1:
                Op_1NNN(opCode.NNN);
                break;
            case 0x2:
                Op_2NNN(opCode.NNN);
                break;
            case 0x3:
                Op_3XNN(opCode.X, opCode.NN);
                break;
            case 0x4:
                Op_4XNN(opCode.X, opCode.NN);
                break;
            case 0x5:
                Op_5XY0(opCode.X, opCode.Y);
                break;
            case 0x6:
                Op_6XNN(opCode.X, opCode.NN);
                break;
            case 0x7:
                Op_7XNN(opCode.X, opCode.NN);
                break;
            case 0x8:
                switch (opCode.N)
                {
                    case 0x0:
                        Op_8XY0(opCode.X, opCode.Y);
                        break;
                    case 0x1:
                        Op_8XY1(opCode.X, opCode.Y);
                        break;
                    case 0x2:
                        Op_8XY2(opCode.X, opCode.Y);
                        break;
                    case 0x3:
                        Op_8XY3(opCode.X, opCode.Y);
                        break;
                    case 0x4:
                        Op_8XY4(opCode.X, opCode.Y);
                        break;
                    case 0x5:
                        Op_8XY5(opCode.X, opCode.Y);
                        break;
                    case 0x6:
                        Op_8XY6(opCode.X, opCode.Y);
                        break;
                    case 0x7:
                        Op_8XY7(opCode.X, opCode.Y);
                        break;
                    case 0xE:
                        Op_8XYE(opCode.X, opCode.Y);
                        break;
                    default:
                        throw new NotImplementedException("OpCode not supported: " + opCode.ToString());
                }
                break;
            case 0x9:
                Op_9XY0(opCode.X, opCode.Y);
                break;
            case 0xA:
                Op_ANNN(opCode.NNN);
                break;
            case 0xB:
                Op_BNNN(opCode.NNN);
                break;
            case 0xC:
                Op_CXNN(opCode.X, opCode.NN);
                break;
            case 0xD:
                Op_DXYN(opCode.X, opCode.Y, opCode.N);
                break;
            case 0xE:
                switch (opCode.Y)
                {
                    case 0x9:
                        Op_EX9E(opCode.X);
                        break;
                    case 0xA:
                        Op_EXA1(opCode.X);
                        break;
                        throw new NotImplementedException("OpCode not supported: " + opCode.ToString());
                }
                break;
            case 0xF:
                switch (opCode.Y)
                {
                    case 0x0:
                        Op_FX0A(opCode.X);
                        break;
                    case 0x1:
                        Op_FX1E(opCode.X);
                        break;
                    case 0x3:
                        Op_FX33(opCode.X);
                        break;
                    case 0x5:
                        Op_FX55(opCode.X);
                        break;
                    case 0x6:
                        Op_FX65(opCode.X);
                        break;
                    default:
                        throw new NotImplementedException("OpCode not supported: " + opCode.ToString());
                }
                break;
            default:
                throw new NotImplementedException("OpCode not supported: " + opCode.ToString());
        }
    }

    /// <summary>
    /// Clears the display turning all pixels off.
    /// </summary>
    private void Op_00E0()
    {
        display.Clear();
    }

    private void Op_00EE()
    {
        // todo: validate subroutines stack has a value
        ProgramCounter = Subroutines.Pop();
    }

    /// <summary>
    /// Sets the program counter to NNN.
    /// </summary>
    /// <param name="NNN">Memory address to jump the program counter to.</param>
    private void Op_1NNN(int NNN)
    {
        ProgramCounter = NNN;
    }

    private void Op_2NNN(int NNN)
    {
        Subroutines.Push(ProgramCounter);
        ProgramCounter = NNN;
    }

    /// <summary>
    /// Skips one instruction if the value in VX is equal to NN.
    /// </summary>
    /// <param name="X">Variable register at X.</param>
    /// <param name="NN">Value to compare against.</param>
    private void Op_3XNN(int X, int NN)
    {
        if (VariableRegisters[X] == NN)
        {
            ProgramCounter += 2;
        }
    }

    /// <summary>
    /// Skips one instruction if the value in VX is not equal to NN.
    /// </summary>
    /// <param name="X">Variable register at X.</param>
    /// <param name="NN">Value to compare against.</param>
    private void Op_4XNN(int X, int NN)
    {
        if (VariableRegisters[X] != NN)
        {
            ProgramCounter += 2;
        }
    }

    /// <summary>
    /// Skips one instruction if the value in VX equals the value in VY.
    /// </summary>
    /// <param name="X">Variable register at X.</param>
    /// <param name="Y">Variable register at Y.</param>
    private void Op_5XY0(int X, int Y)
    {
        if (VariableRegisters[X] == VariableRegisters[Y])
        {
            ProgramCounter += 2;
        }
    }

    /// <summary>
    /// Sets register VX to the value NN.
    /// </summary>
    /// <param name="X">VX register.</param>
    /// <param name="NN">Value to set VX to.</param>
    private void Op_6XNN(int X, byte NN)
    {
        VariableRegisters[X] = NN;
    }

    /// <summary>
    /// Adds the value NN to VX.
    /// </summary>
    /// <param name="X">VX register.</param>
    /// <param name="NN">Value to add to VX.</param>
    private void Op_7XNN(int X, byte NN)
    {
        if (VariableRegisters[X] + NN > 255)
        {
            VariableRegisters[0xF] = 0x1;
        }
        VariableRegisters[X] += NN;
    }

    /// <summary>
    /// Sets variable register X to the value of variable register Y
    /// </summary>
    /// <param name="X">Variable register X</param>
    /// <param name="Y">Variable register Y</param>
    private void Op_8XY0(int X, int Y)
    {
        VariableRegisters[X] = VariableRegisters[Y];
    }

    private void Op_8XY1(int X, int Y)
    {
        VariableRegisters[X] |= VariableRegisters[Y];
    }

    private void Op_8XY2(int X, int Y)
    {
        VariableRegisters[X] &= VariableRegisters[Y];
    }

    private void Op_8XY3(int X, int Y)
    {
        VariableRegisters[X] ^= VariableRegisters[Y];
    }

    private void Op_8XY4(int X, int Y)
    {
        VariableRegisters[X] += VariableRegisters[Y];
        if (VariableRegisters[X] > 255)
        {
            VariableRegisters[0xF] = 1;
            VariableRegisters[X] %= 255;
        }
        else
        {
            VariableRegisters[0xF] = 0;
        }
    }

    private void Op_8XY5(int X, int Y)
    {
        if (VariableRegisters[X] > VariableRegisters[Y])
        {
            VariableRegisters[0xF] = 1;
        } else
        {
            VariableRegisters[0xF] = 0;
        }
        VariableRegisters[X] = (byte)((VariableRegisters[X] - VariableRegisters[Y]) & 0xFF);
    }

    /// <summary>
    /// Assigns the value of VY to VX. VF is assigned the first bit of the new VX. VX is then bit-shifted right by 1.
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    private void Op_8XY6(int X, int Y)
    {
        VariableRegisters[X] = VariableRegisters[Y];
        // VF is set to the first bit of VX prior to shifting
        VariableRegisters[0xF] = (byte)(VariableRegisters[X] >> 7);
        VariableRegisters[X] >>= 1;
    }

    private void Op_8XY7(int X, int Y)
    {
        VariableRegisters[X] = (byte)((VariableRegisters[Y] - VariableRegisters[X]) & 0xFF);
    }

    private void Op_8XYE(int X, int Y)
    {
        VariableRegisters[X] = VariableRegisters[Y];
        VariableRegisters[0xF] = (byte)(VariableRegisters[X] >> 7 == 0x1 ? 1 : 0);
        VariableRegisters[X] <<= 1;
    }

    /// <summary>
    /// Skips one instruction if the value in VX does not equal the value in VY.
    /// </summary>
    /// <param name="X">Variable register at X.</param>
    /// <param name="Y">Variable register at Y.</param>
    private void Op_9XY0(int X, int Y)
    {
        if (VariableRegisters[X] != VariableRegisters[Y])
        {
            ProgramCounter += 2;
        }
    }

    /// <summary>
    /// Sets the index register to NNN.
    /// </summary>
    /// <param name="NNN">Value to set the index register to.</param>
    private void Op_ANNN(ushort NNN)
    {
        IndexRegister = NNN;
    }

    private void Op_BNNN(int NNN)
    {
        ProgramCounter = NNN + VariableRegisters[0x0];
    }

    /// <summary>
    /// Generates a random number, ANDs it with NN, and puts value in VX.
    /// </summary>
    /// <param name="X">Variable register to store the value in.</param>
    /// <param name="NN">Value that the random number will be ANDed with.</param>
    /// <exception cref="NotImplementedException"></exception>
    private void Op_CXNN(int X, int NN)
    {
        byte randNum = (byte)(_random.Next(0xF) & NN);
        VariableRegisters[X] = randNum;
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
                display.SetPixel(xPos, yPos, bit);
            }
        }
    }

    private void Op_EX9E(int X)
    {

    }

    private void Op_EXA1(int X)
    {

    }

    /// <summary>
    /// Stops executing instructions and loops until there is a key input.
    /// </summary>
    private void Op_FX0A(int X)
    {
        if (!KeyPressed)
        {
            ProgramCounter -= 2;
        }

     

    }

    /// <summary>
    /// Takes the number in VX and converts to three decimal digits, and stores each individual value in successive memory starting at the index register location.
    /// e.g. 156 => memory[i] = 1, memory[i + 1] = 5, memory[i + 2] 6;
    /// </summary>
    /// <param name="X"></param>
    private void Op_FX33(int X)
    {
        int val = VariableRegisters[X];

        for (int i = 2; i >= 0; i--)
        {
            memory.RAM[IndexRegister + i] = (byte)(val % 10);
            val /= 10;
        }
    }

    /// <summary>
    /// The value of each variable register from V0 to VX will be stored in successive memory addresses starting at the index register.
    /// </summary>
    /// <param name="X"></param>
    private void Op_FX55(int X)
    {
        for (int i = 0; i <= X; i++)
        {
            memory.RAM[IndexRegister + i] = VariableRegisters[i];
        }
    }

    private void Op_FX1E(int X)
    {
        IndexRegister += VariableRegisters[X];
    }

    /// <summary>
    /// The value of each memory location starting from the index register 0 to X will be stored in the variable registers starting at V0.
    /// </summary>
    /// <param name="X"></param>
    private void Op_FX65(int X)
    {
        for (int i = 0; i <= X; i++)
        {
            VariableRegisters[i] = memory.RAM[IndexRegister + i];
        }
    }
}
