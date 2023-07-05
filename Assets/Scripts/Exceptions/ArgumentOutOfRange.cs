#nullable enable

using System;

namespace NuRpg.Exceptions {
	public static class ArgumentOutOfRange {
		public static void ThrowIfCountOutOfRange(int value, int length, string paramName) {
			if( value < 0 || value >= length )
				throw new ArgumentOutOfRangeException(paramName, $"Count must be in the range [0,{length}).");
		}

		public static void ThrowIfIndexOutOfRange(int value, int length, string paramName) {
			if( value < 0 || value >= length )
				throw new ArgumentOutOfRangeException(paramName, $"Index must be in the range [0,{length}).");
		}

		public static void ThrowIfIndexOutOfRange(int value, int length, string rangeName, string paramName) {
			if( value < 0 || value >= length )
				throw new ArgumentOutOfRangeException(paramName, value, $"{rangeName} index must be in the range [0,{length}).");
		}

		public static void ThrowIfInvalidRange(int index, int count, int size) {
			if( size - index < count )
				throw new ArgumentOutOfRangeException(null, "Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
		}

		public static void ThrowIfLengthNegative(int value, string paramName) {
			if( value < 0 )
				throw new ArgumentOutOfRangeException(paramName, "Length cannot be negative.");
		}

		public static void ThrowIfLengthNotEqual(int value, int expectedValue, string paramName) {
			if( value != expectedValue )
				throw new ArgumentOutOfRangeException(paramName, value, $"Length must be {expectedValue}.");
		}

		public static void ThrowIfLengthNotEqual(int value, int expectedValue, string rangeName, string paramName) {
			if( value != expectedValue )
				throw new ArgumentOutOfRangeException(paramName, value, $"{rangeName} length must be {expectedValue}.");
		}

		public static void ThrowIfNegative(int value, string? paramName = null) {
			if( value < 0 )
				throw new ArgumentOutOfRangeException(paramName, "Non-negative number is required.");
		}
	}
}