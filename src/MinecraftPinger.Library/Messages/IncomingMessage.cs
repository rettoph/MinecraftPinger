using MinecraftPinger.Library.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MinecraftPinger.Library
{
    /// <summary>
    /// Read methods stolen from:
    /// https://wiki.vg/Protocol#VarInt_and_VarLong
    /// </summary>
    public class IncomingMessage
    {
        public readonly PacketId Id;
        public readonly Int32 Length;
        public Int32 Offset { get; set; }
        public Byte[] Buffer { get; private set; }

        public IncomingMessage(Byte[] buffer)
        {
            this.Offset = 0;
            this.Buffer = buffer;
            this.Id = (PacketId)this.ReadByte();
            this.Length = this.Buffer.Length;
        }
        public IncomingMessage(NetworkStream stream)
        {
            var value = 0;
            var size = 0;
            int b;
            while (((b = stream.ReadByte()) & 0x80) == 0x80)
            {
                value |= (b & 0x7F) << (size++ * 7);
                if (size > 5)
                {
                    throw new IOException("This VarInt is an imposter!");
                }
            }
            this.Length = value | ((b & 0x7F) << (size * 7));

            this.Offset = 0;

            // Read the message into the internal buffer
            //this.Buffer = new Byte[this.Length];
            //stream.Read(this.Buffer, 0, this.Length);
            Int32 index = 0;
            List<Byte> _temp = new List<Byte>();

            while (_temp.Count < this.Length && index < this.Length * 2)
            {
                if (stream.DataAvailable)
                    _temp.Add((Byte)stream.ReadByte());
                else
                    Thread.Sleep(1);

                index++;
            }

            if(_temp.Count < this.Length)
            {
                throw new Exception("Not all data recieved!");
            }

            this.Buffer = _temp.ToArray();

            this.Id = (PacketId)this.ReadByte();
        }

        public Byte ReadByte()
        {
            var b = this.Buffer[this.Offset];
            this.Offset += 1;
            return b;
        }

        public Byte[] Read(Int32 length)
        {
            if (this.Buffer.Length >= length)
            {
                var data = new Byte[length];
                Array.Copy(this.Buffer, this.Offset, data, 0, length);
                this.Offset += length;
                return data;
            }

            throw new IOException("Buffer length too short!");
        }

        public Int32 ReadVarInt()
        {
            Int32 value = 0;
            Int32 size = 0;
            Int32 b;
            while (((b = ReadByte()) & 0x80) == 0x80)
            {
                value |= (b & 0x7F) << (size++ * 7);
                if (size > 5)
                {
                    throw new IOException("This VarInt is an imposter!");
                }
            }
            return value | ((b & 0x7F) << (size * 7));
        }

        public String ReadString(Int32 length)
        {
            var data = this.Read(length);
            return Encoding.UTF8.GetString(data);
        }

        public Int64 ReadLong()
        {
            return BitConverter.ToInt64(this.Read(8));
        }
        public Int16 ReadShort()
        {
            return BitConverter.ToInt16(this.Read(2).Reverse().ToArray());
        }

        public Byte[] ReadMagic()
        {
            var b = this.Read(OutgoingMessage.Magic.Length);

            return b;
        }
    }
}
