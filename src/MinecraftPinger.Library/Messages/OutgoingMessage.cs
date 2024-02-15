using MinecraftPinger.Library.Enums;
using System.Text;

namespace MinecraftPinger.Library
{
    public class OutgoingMessage
    {
        public static readonly byte[] Magic = new byte[] { 0x00, 0xFF, 0xFF, 0x00, 0xFE, 0xFE, 0xFE, 0xFE, 0xFD, 0xFD, 0xFD, 0xFD, 0x12, 0x34, 0x56, 0x78 };

        private List<byte> _buffer;

        public int Length => _buffer.Count;

        public OutgoingMessage()
        {
            _buffer = new List<byte>();
        }
        public OutgoingMessage(PacketId packet) : this()
        {
            _buffer.Add((byte)packet);
        }

        public void WriteInt(int value)
        {
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        private void WriteVarInt(int value, List<byte> tempBuffer)
        {
            List<byte> b = tempBuffer ?? _buffer;
            while ((value & 128) != 0)
            {
                b.Add((byte)(value & 127 | 128));
                value = (int)((uint)value) >> 7;
            }
            b.Add((byte)value);
        }
        public void WriteVarInt(int value)
        {
            this.WriteVarInt(value, _buffer);
        }

        public void WriteShort(short value)
        {
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        public void WriteString(string data)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            this.WriteVarInt(buffer.Length);
            _buffer.AddRange(buffer);
        }

        public void WriteLong(long value)
        {
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        public void WriteBytes(byte[] bytes)
        {
            _buffer.AddRange(bytes);
        }

        public void WriteMagic()
        {
            _buffer.AddRange(OutgoingMessage.Magic);
        }

        public byte[] GetData(bool appendSize)
        {
            if (appendSize)
            {
                List<byte> _output = new List<byte>();
                this.WriteVarInt(_buffer.Count, _output);
                _output.AddRange(_buffer);

                return _output.ToArray();
            }
            else
            {
                return _buffer.ToArray();
            }
        }
    }
}
