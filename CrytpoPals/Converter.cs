using System;
using System.Runtime.InteropServices;

namespace CrytpoPals
{
    public static unsafe class Converter
	{
		private static readonly uint[] Lookup32Unsafe = CreateLookup32Unsafe();
		private static readonly uint* Lookup32UnsafeP = (uint*)GCHandle.Alloc(Lookup32Unsafe, GCHandleType.Pinned).AddrOfPinnedObject();
		public static string Convert(byte[] hex)
        {
            return System.Convert.ToBase64String(hex);
        }

        public static string Convert(string hex) => Convert(FromHex(hex));

		// https://stackoverflow.com/a/24343727/2061103
		/// <summary>
		/// Fastest byte array to hex implementation in C#
		/// </summary>
		public static string ToHex(params byte[] bytes)
		{
			if (bytes is null)
			{
				return null;
			}

			if (bytes.Length == 0)
			{
				return "";
			}

			var lookupP = Lookup32UnsafeP;
			var result = new string((char)0, bytes.Length * 2);
			fixed (byte* bytesP = bytes)
			fixed (char* resultP = result)
			{
				uint* resultP2 = (uint*)resultP;
				for (int i = 0; i < bytes.Length; i++)
				{
					resultP2[i] = lookupP[bytesP[i]];
				}
			}
			return result;
		}

		// https://stackoverflow.com/a/5919521/2061103
		// https://stackoverflow.com/a/10048895/2061103
		/// <summary>
		/// Fastest hex to byte array implementation in C#
		/// </summary>
		public static byte[] FromHex(string hex)
		{
			if (hex is null)
			{
				return null;
			}

			if (string.IsNullOrWhiteSpace(hex))
			{
				return Array.Empty<byte>();
			}

			var bytes = new byte[hex.Length / 2];
			var hexValue = new int[]
			{
				0x00, 0x01, 0x02, 0x03, 0x04, 0x05,
				0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F
			};

			for (int x = 0, i = 0; i < hex.Length; i += 2, x += 1)
			{
				bytes[x] = (byte)((hexValue[char.ToUpper(hex[i + 0]) - '0'] << 4) |
					hexValue[char.ToUpper(hex[i + 1]) - '0']);
			}

			return bytes;
		}

		private static uint[] CreateLookup32Unsafe()
		{
			var result = new uint[256];
			for (int i = 0; i < 256; i++)
			{
				string s = i.ToString("x2");
				result[i] = BitConverter.IsLittleEndian ? s[0] + ((uint)s[1] << 16) : s[1] + ((uint)s[0] << 16);
			}
			return result;
		}
	}
}
