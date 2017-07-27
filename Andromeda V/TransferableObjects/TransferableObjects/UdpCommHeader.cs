using System;
using System.IO;

namespace TransferableObjects
{
	public class UdpCommHeader
	{
		public byte requestType;

		public long packetSeq;

		public long playerId;

		public long responseToPacketSeq;

		public ITransferable data;

		public TransferContext context = TransferContext.None;

		public static object lockBytes;

		public static long bytes;

		static UdpCommHeader()
		{
			UdpCommHeader.lockBytes = new object();
			UdpCommHeader.bytes = (long)0;
		}

		public UdpCommHeader()
		{
		}

		public static UdpCommHeader FromBytes(byte[] buffer, int length)
		{
			UdpCommHeader udpCommHeader = new UdpCommHeader();
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(buffer, 0, length));
			udpCommHeader.requestType = binaryReader.ReadByte();
			udpCommHeader.context = (TransferContext)binaryReader.ReadInt16();
			udpCommHeader.packetSeq = binaryReader.ReadInt64();
			udpCommHeader.playerId = binaryReader.ReadInt64();
			udpCommHeader.responseToPacketSeq = binaryReader.ReadInt64();
			udpCommHeader.data = TransferablesFramework.DeserializeITransferable(binaryReader);
			return udpCommHeader;
		}

		internal int ToBytes(byte[] bufferOut, int startIndex)
		{
			MemoryStream memoryStream = new MemoryStream(bufferOut, 4 + startIndex, (int)bufferOut.Length - 4 - startIndex);
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(this.requestType);
			binaryWriter.Write((short)this.context);
			binaryWriter.Write(this.packetSeq);
			binaryWriter.Write(this.playerId);
			binaryWriter.Write(this.responseToPacketSeq);
			TransferablesFramework.SerializeITransferable(binaryWriter, this.data, this.context);
			int position = (int)memoryStream.Position;
			bufferOut[startIndex] = (byte)position;
			bufferOut[startIndex + 1] = (byte)(position >> 8);
			bufferOut[startIndex + 2] = (byte)(position >> 16);
			bufferOut[startIndex + 3] = (byte)(position >> 24);
			return position;
		}
	}
}