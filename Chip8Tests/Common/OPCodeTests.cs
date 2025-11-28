using Chip8.Common;

namespace Chip8Tests.Common;

public class OPCodeTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Constructor_Success()
    {
        int testOPCode = 0x123F;
        OpCode opCode = new OpCode(testOPCode);

        Assert.That(opCode.X, Is.EqualTo(0x2), "X extraction did not work");
        Assert.That(opCode.Y, Is.EqualTo(0x3), "Y extraction did not work");
        Assert.That(opCode.N, Is.EqualTo(0xF), "N extraction did not work");
        Assert.That(opCode.NN, Is.EqualTo(0x3F), "NN extraction did not work");
        Assert.That(opCode.NNN, Is.EqualTo(0x23F), "NNN extraction did not work");
    }
}