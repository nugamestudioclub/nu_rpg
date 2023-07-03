using System;

namespace NuRpg.Exceptions {
	public static class InvalidOperation {
		public static void ThrowIfEmpty(int count) {
			if( count == 0 )
				throw new InvalidOperationException("The specified operation cannot be performed on an empty collection.");
		}

		public static void ThrowIfNotOfType<T>(object value) {
			if( value is not T )
				throw new InvalidOperationException($"Object is not of specified type. (Type '{typeof(T)}')");
		}
	}
}