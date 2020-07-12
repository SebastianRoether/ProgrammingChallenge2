using System.Text;

namespace ProgrammingChallenge2.Codecs.FlorianBader
{
    public static class FloEncoding
    {
        public const int BitCount = 6;
        private const int StartCharacterIndex = 'a';

        public static string GetString(byte[] bytes)
        {
            var bitReader = new BitReader(bytes);
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                var b = bitReader.ReadByte() - 1;

                if (b <= 9)
                {
                    stringBuilder.Append(b);
                }
                else
                {
                    var c = (char)(StartCharacterIndex + b - 10);
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString();
        }

        public static byte[] GetBytes(string str)
        {
            var bitWriter = new BitWriter();
            for (int i = 0; i < str.Length; i++)
            {
                var c = (byte)str[i];
                if ((c - 48) <= 9)
                {
                    bitWriter.WriteByte((byte)(c - 48 + 1), offset: 8 - BitCount, bitCount: BitCount);
                }
                else
                {
                    var b = (byte)(c - StartCharacterIndex + 11);
                    bitWriter.WriteByte(b, offset: 8 - BitCount, bitCount: BitCount);
                }
            }

            return bitWriter.ToArray();
        }
    }
}