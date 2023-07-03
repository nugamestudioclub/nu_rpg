#nullable enable

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NuRpg.Collections {
	public interface IReadOnlyBlackboard : IReadOnlyCollection<KeyValuePair<string, object>> {
		IReadOnlyCollection<string> Names { get; }
		IReadOnlyCollection<object> Values { get; }
		bool Contains<T>(string name);
		bool Contains(string name);
		T GetValue<T>(string name);
		object GetValue(string name);
		T? GetValueOrDefault<T>(string name);
		[return: NotNullIfNotNull(nameof(defaultValue))]
		T? GetValueOrDefault<T>(string name, T? defaultValue);
		[return: NotNullIfNotNull(nameof(defaultValue))]
		object? GetValueOrDefault(string name, object defaultValue);
		object? GetValueOrNull(string name);
		bool TryGetValue<T>(string name, [NotNullWhen(true)] out T? value);
		bool TryGetValue(string name, [NotNullWhen(true)] out object? value);
	}
}