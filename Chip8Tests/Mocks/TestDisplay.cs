using Chip8;

namespace Chip8Tests.Mocks
{
    internal class TestDisplay : IDisplay
    {
        public void ClearDisplay()
        {
            throw new NotImplementedException();
        }

        public void DrawDisplay()
        {
            throw new NotImplementedException();
        }

        public bool GetPixel(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void SetPixel(int x, int y, bool bit)
        {
            throw new NotImplementedException();
        }
    }
}
