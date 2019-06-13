
using System.Text;

namespace System.IO
{
	static class StreamExtensions
	{
#if NET35

		class StreamWriterEx : StreamWriter
		{
			private bool leaveOpen;

			public StreamWriterEx(Stream stream, Encoding encoding, int bufferSize, bool leaveOpen) : base(stream, encoding, bufferSize)
			{
				this.leaveOpen = leaveOpen;
			}

			protected override void Dispose(bool disposing)
			{
				if (leaveOpen == false)
					base.Dispose(disposing);
			}
		}

		public static void CopyTo(this Stream fromStream, Stream toStream)

		{

			if (fromStream == null)

				throw new ArgumentNullException("fromStream");

			if (toStream == null)

				throw new ArgumentNullException("toStream");



			var bytes = new byte[8092];

			int dataRead;

			while ((dataRead = fromStream.Read(bytes, 0, bytes.Length)) > 0)

				toStream.Write(bytes, 0, dataRead);

		}
#endif
		public static StreamWriter GetWriter(this Stream stream, Text.Encoding encoding, int bufferSize, bool leaveOpen)
		{
			StreamWriter writer;
#if NET35
			writer = new StreamWriterEx(stream, encoding, bufferSize, leaveOpen);
#else
			writer =new StreamWriter(stream,encoding,bufferSize,leaveOpen);
#endif
			return writer;
		}
	}
}

