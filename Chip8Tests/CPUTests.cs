using Chip8;
using Chip8.Common;
using Chip8Tests.Mocks;

namespace Chip8Tests;

public class CPUTests
{
    Memory memory;
    // todo: this should be a test display
    IDisplay display;
    CPU cpu;

    [SetUp]
    public void Setup()
    {
        memory = new Memory();
        display = new TestDisplay();
        cpu = new CPU(memory, display);
    }

    //[Test]
    //public void FetchInstruction_Success()
    //{
    //    cpu.FetchInstruction();

    //}

    //[Test]
    //public void Op_1NNN_Success()
    //{
    //    Assert.That(cpu.ProgramCounter, Is.EqualTo(0x0), "Program counter isn't set to default of 0 at the start.");
    //    OpCode opCode = new OpCode(0x1AB3);
    //    cpu.ExecuteInstruction(opCode);
    //    Assert.That(cpu.ProgramCounter, Is.EqualTo(opCode.NNN), "Program counter didn't jump to the correct address.");
    //}

    //[Test]
    //public void Op_6XNN_Success()
    //{
    //    int register = 0x3;
    //    Assert.That(cpu.VariableRegisters[register], Is.EqualTo(0x0), "Variable register X isn't set to default of 0 at the start.");
    //    OpCode opCode = new OpCode(0x63A3);
    //    cpu.ExecuteInstruction(opCode);
    //    Assert.That(cpu.VariableRegisters[register], Is.EqualTo(0xA3), "Variable register X isn't set to the correct value.");
    //}

    [Test]
    public void Op_7XNN_Success()
    {
        int[] f = { 0x73, 0x03, 0x73, 0x04 };
        int register = 0x3;
        memory.LoadMemory(f, 0x200);
        Assert.That(cpu.VariableRegisters[register], Is.EqualTo(0x0), "Variable register X isn't set to default of 0 at the start.");
        cpu.Cycle();
        cpu.Cycle();
        Assert.That(cpu.VariableRegisters[register], Is.EqualTo(0x07), "Variable register X isn't set to the correct value.");
    }

    //[Test]
    //public void Op_ANNN_Success()
    //{
    //    Assert.That(cpu.IndexRegister, Is.EqualTo(0x0), "Index register isn't set to default of 0 at the start.");
    //    OpCode opCode = new OpCode(0xA123);
    //    cpu.ExecuteInstruction(opCode);
    //    Assert.That(cpu.IndexRegister, Is.EqualTo(0x123), "Index register isn't set to the correct value.");
    //}
}
