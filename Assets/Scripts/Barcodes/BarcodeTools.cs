using System;
using System.Runtime.InteropServices;

namespace STycoon.Barcodes.Tools
{
	public static class BarcodeTools
	{
		private const uint MAX_VALUE = 99_999;
		private const uint MIN_VALUE = 10_000;
		private const ulong TYPE_MULTIPLIER = 100_000_000_000UL;
		private const ulong LEFT_MULTIPLIER = 1_000_000UL;
		private const ulong RIGHT_MULTIPLIER = 10UL;
		
		[DllImport("barcode_gen", CallingConvention = CallingConvention.Cdecl)]
		private static extern int generate_barcode(string path, string code);
		public static bool GenerateSprite(string path, ulong code)
		{
			if (string.IsNullOrEmpty(path))
				return false;
			
			int result = generate_barcode(path, code.ToString("D12"));
			return result == 0;
		}

		public static bool Validate(ulong barcode)
		{
			if (barcode == 0)
				return false;
			
			ulong inputCheckDigit = barcode % 10UL;
			barcode /= 10UL;
			ulong checkDigit = GetCheckDigit(barcode);
			return checkDigit == inputCheckDigit;
		}
		public static ulong Generate(byte type, uint left, uint right)
		{
			if (type > 9)
				throw new ArgumentException("the type value should be in 0-9 range", nameof(type));

			if (left > MAX_VALUE || right > MAX_VALUE)
				throw new ArgumentException($"left and right value must be less or equal than {MAX_VALUE}");
			if (left < MIN_VALUE || right < MIN_VALUE)
				throw new ArgumentException($"left and right value must be greater or equal than {MIN_VALUE}");

			ulong tempValue = (ulong)right * RIGHT_MULTIPLIER +
			                     (ulong)left * LEFT_MULTIPLIER +
			                     (ulong)type * TYPE_MULTIPLIER;
			return GetCheckDigit(tempValue / 10) + tempValue;
		}

		private static ulong GetCheckDigit(ulong value)
		{
			ulong tempOdd = 0;
			ulong tempEven = 0;

			byte counter = 1;
			while (value > 0)
			{
				ulong digit = value % 10;
				if ((counter & 1) == 1)
					tempOdd += digit;
				else
					tempEven += digit;
				value /= 10;
				counter++;
			}

			tempOdd *= 3;
			tempOdd += tempEven;
			ulong mod = tempOdd % 10;
			return mod == 0 ? mod : 10 - mod;
		}
	}
}