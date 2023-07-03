using System;

namespace NuRpg.Exceptions {
	public static class InvalidOperation {
		public static void ThrowIfEmpty(int count) {
			if( count == 0 )
				throw new InvalidOperationException("The specified operation cannot be performed on an empty collection.");
		}
	}
}