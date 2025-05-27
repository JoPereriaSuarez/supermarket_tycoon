using System;

namespace STycoon.Barcodes.Barcodes
{
	public static class BarcodeGenerator
	{
		private const uint MAX_VALUE = 99_999;
		private const uint MIN_VALUE = 10_000;
		private const ulong TYPE_MULTIPLIER = 100_000_000_000UL;
		private const ulong LEFT_MULTIPLIER = 1_000_000UL;
		private const ulong RIGHT_MULTIPLIER = 10UL;
		
		public static ulong Generate(byte type, uint left, uint right)
		{
			if (type > 9)
				throw new ArgumentException("the type value should be in 0-9 range", nameof(type));

			if (left > MAX_VALUE || right > MAX_VALUE)
				throw new ArgumentException($"left and right value must be less or equal than {MAX_VALUE}");
			if (left < MIN_VALUE || right < MIN_VALUE)
				throw new ArgumentException($"left and right value must be greater or equal than {MIN_VALUE}");

			uint checkDigit = GetCheckDigit(type, left, right);
			return checkDigit +
			       ((ulong)right * RIGHT_MULTIPLIER) +
			       ((ulong)left * LEFT_MULTIPLIER) +
			       ((ulong)type * TYPE_MULTIPLIER);
		}
		private static uint GetCheckDigit(byte type, uint left, uint right)
		{
			uint tempLeft = left;
			uint tempRight = right;
			uint tempOdd = type;
			uint tempEven = 0;

			while (tempLeft > 0)
			{
				uint digit = tempLeft % 10;
				if ((digit & 1) == 1)
					tempOdd += digit;
				else
					tempEven += digit;

				tempLeft /= 10;
			}

			while (tempRight > 0)
			{
				uint digit = tempRight % 10;
				if ((digit & 1) == 1)
					tempOdd += digit;
				else
					tempEven += digit;

				tempRight /= 10;
			}

			tempOdd *= 3;
			tempOdd += tempEven;
			uint mod = tempOdd % 10;
			uint checkDigit = mod == 0 ? mod : 10 - mod;
			return checkDigit;
		}
	}
}