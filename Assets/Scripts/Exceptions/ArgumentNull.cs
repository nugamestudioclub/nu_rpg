#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;

namespace NuRpg.Exceptions {
	public static class ArgumentNull {
		public static void ThrowIfNull([NotNull] object? argument, string? paramName = null) {
			if( argument == null )
				throw new ArgumentNullException(paramName);
		}
	}
}