namespace Chip8.Tests;

public class CPUTests
{
    Memory memory;
    // todo: this should be a test display
    DisplayBuffer display;
    CPU cpu;

    [SetUp]
    public void Setup()
    {
        memory = new Memory();
        display = new DisplayBuffer();
        cpu = new CPU(memory, display);
    }

    [Test]
    public void Op_1NNN_Success()
    {
        // loading instruction set that should jump the PC to 0xAB3
        ushort expectedAddress = 0xAB3;
        byte[] load = { 0x1A, 0xB3 };
        memory.LoadMemory(load, cpu.ProgramCounter);

        Assert.That(cpu.ProgramCounter, Is.EqualTo(0x200), "Program counter isn't set to default of 0x200 at the start.");
        cpu.Cycle();
        Assert.That(cpu.ProgramCounter, Is.EqualTo(expectedAddress), "Program counter didn't jump to the correct address.");
    }

    [Test]
    public void Op_6XNN_Success()
    {
        // loading instruction set that should set var reg 3 to 0x4A
        byte[] load = { 0x63, 0x4A };
        byte register = 0x3;
        memory.LoadMemory(load, cpu.ProgramCounter);

        Assert.That(cpu.VariableRegisters[register], Is.EqualTo(0x0), "Variable register X isn't set to default of 0 at the start.");
        cpu.Cycle();
        Assert.That(cpu.VariableRegisters[register], Is.EqualTo(0x4A), "Variable register X isn't set to the correct value.");
    }

    [Test]
    public void Op_7XNN_Success()
    {
        // loading two instruction sets that should add 3 and 4 to the var register 3
        byte[] load = { 0x73, 0x03, 0x73, 0xFF };
        byte register = 0x3;
        memory.LoadMemory(load, cpu.ProgramCounter);

        Assert.That(cpu.VariableRegisters[register], Is.EqualTo(0x0), "Variable register X isn't set to default of 0 at the start.");
        cpu.Cycle();
        cpu.Cycle();
        Assert.That(cpu.VariableRegisters[register], Is.EqualTo(0x02), "Variable register X isn't set to the correct value.");
        Assert.That(cpu.VariableRegisters[0xF], Is.EqualTo(0x1), "Variable register F didn't get set to 1 after the overflow.");
    }

    [Test]
    public void Op_8XY6_Success()
    {
        byte inputVal = 0xAB;
        cpu.VariableRegisters[0x4] = inputVal;

        byte expectedVal = 0x55;

        byte[] load = { 0x83, 0x46 };
        memory.LoadMemory(load, cpu.ProgramCounter);

        Assert.That(cpu.VariableRegisters[0x4], Is.EqualTo(inputVal), "VY does not equal the expected start value.");
        cpu.Cycle();
        Assert.That(cpu.VariableRegisters[0xF], Is.EqualTo(0x1));
        Assert.That(cpu.VariableRegisters[0x3], Is.EqualTo(expectedVal), "VX was not set to VY and shifted left 1");
    }

    [Test]
    public void Op_8XYE_Success()
    {
        byte inputVal = 0xAB;
        cpu.VariableRegisters[0x4] = inputVal;

        byte expectedVal = 0x56;

        byte[] load = { 0x83, 0x4E };
        memory.LoadMemory(load, cpu.ProgramCounter);

        Assert.That(cpu.VariableRegisters[0x4], Is.EqualTo(inputVal), "VY does not equal the expected start value.");
        cpu.Cycle();
        Assert.That(cpu.VariableRegisters[0xF], Is.EqualTo(inputVal & 0x1));
        Assert.That(cpu.VariableRegisters[0x3], Is.EqualTo(expectedVal), "VX was not set to VY and shifted left 1");
    }

    [Test]
    public void Op_ANNN_Success()
    {
        byte[] load = { 0xA3, 0xAA };
        memory.LoadMemory(load, cpu.ProgramCounter);

        Assert.That(cpu.IndexRegister, Is.EqualTo(0x0), "Index register isn't set to default of 0 at the start.");
        cpu.Cycle();
        Assert.That(cpu.IndexRegister, Is.EqualTo(0x3AA), "Index register isn't set to the correct value.");
    }
}
