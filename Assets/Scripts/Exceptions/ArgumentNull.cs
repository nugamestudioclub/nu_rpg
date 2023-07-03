using System;

namespace NuRpg.Exceptions {
	public static class ArgumentNull {
		public static void ThrowIfNull(object value, string paramName = null) {
			if( value == null )
				throw new ArgumentNullException(paramName);
		}
	}
}