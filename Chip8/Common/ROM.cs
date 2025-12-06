namespace Chip8.Common
{
    public class ROM
    {
        public byte[] Data { get; init; }

        public ROM(string romPath)
        {
            Data = new byte[3584];
            byte[] romBytes = File.ReadAllBytes(romPath);
            Array.Copy(romBytes, 0, Data, 0, romBytes.Length);
        }
    }
}
