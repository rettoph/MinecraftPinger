using MinecraftPinger.Library.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MinecraftPinger.Library
{
    public class OutgoingMessage
    {
        public static readonly Byte[] Magic = new Byte[] { 0x00, 0xFF, 0xFF, 0x00, 0xFE, 0xFE, 0xFE, 0xFE, 0xFD, 0xFD, 0xFD, 0xFD, 0x12, 0x34, 0x56, 0x78 };

        private List<Byte> _buffer;

        public Int32 Length => _buffer.Count;

        public OutgoingMessage()
        {
            _buffer = new List<Byte>();
        }
        public OutgoingMessage(PacketId packet) : this()
        {
            _buffer.Add((Byte)packet);
        }

        public void WriteInt(Int32 value)
        {
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        private void WriteVarInt(Int32 value, List<Byte> tempBuffer = default)
        {
            List<Byte> b = tempBuffer ?? _buffer;
            while ((value & 128) != 0)
            {
                b.Add((Byte)(value & 127 | 128));
                value = (Int32)((UInt32)value) >> 7;
            }
            b.Add((Byte)value);
        }
        public void WriteVarInt(Int32 value)
        {
            this.WriteVarInt(value, _buffer);
        }

        public void WriteShort(Int16 value)
        {
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        public void WriteString(String data)
        {
            Byte[] buffer = Encoding.UTF8.GetBytes(data);
            this.WriteVarInt(buffer.Length);
            _buffer.AddRange(buffer);
        }

        public void WriteLong(Int64 value)
        {
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        public void WriteBytes(Byte[] bytes)
        {
            _buffer.AddRange(bytes);
        }

        public void WriteMagic()
        {
            _buffer.AddRange(OutgoingMessage.Magic);
        }

        public Byte[] GetData(Boolean appendSize)
        {
            if(appendSize)
            {
                List<Byte> _output = new List<Byte>();
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
