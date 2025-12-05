using Chip8;
using Chip8.Common;

namespace Chip8Tests;

public class MemoryTests
{
    Memory memory;

    [SetUp]
    public void Setup()
    {
        memory = new Memory();
    }

    [Test]
    public void LoadMemory_Success()
    {
        byte[] load = { 0xFF, 0x12, 0xAB };
        int startPos = 0x9;

        memory.LoadMemory(load, startPos);
        Assert.That(memory.RAM[startPos], Is.EqualTo(load[0]));
        Assert.That(memory.RAM[startPos + 1], Is.EqualTo(load[1]));
        Assert.That(memory.RAM[startPos + 2], Is.EqualTo(load[2]));
        Assert.That(memory.RAM[startPos + 3], Is.EqualTo(0));
    }

    [Test]
    public void GetInstruction_Success()
    {
        OpCode expectedOpCode = new OpCode(0xABA2);
        byte[] load = { 0xAB, 0xA2 };
        int instructionPos = 0xAF;
        memory.LoadMemory(load, instructionPos);
        
        OpCode opCode = memory.GetInstruction(instructionPos);


        Assert.That(opCode, Is.EqualTo(expectedOpCode));
    }
}
