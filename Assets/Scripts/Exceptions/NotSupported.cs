using System;
using System.Diagnostics.CodeAnalysis;

namespace NuRpg.Exceptions {
	public static class NotSupported {
		public static void ThrowIfReadOnly([DoesNotReturnIf(true)] bool isReadOnly) {
			if( isReadOnly )
				throw new NotSupportedException("The specified operation is not supported by this read-only collection.");
		}
	}
}