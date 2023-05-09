using System;
using System.Collections.Generic;
using NuRpg.Exceptions;

namespace NuRpg.Navigation {
	public interface IGrid2D<T> {
		T[,] Cells { get; }
		int SizeX { get; }
		int SizeY { get; }
		T this[int x, int y] { get; set; }
		T[,] GetCellValues();
		void SetCellValues(T[,] cells);
		T[] GetXValues(int y);
		void SetXValues(int y, IList<T> values);
		T[] GetYValues(int x);
		void SetYValues(int x, IList<T> values);
	}

	public class Grid2D<T> : IGrid2D<T> {

		private readonly T[,] cells;

		public T[,] Cells => cells;

		public int SizeX => cells.GetLength(0);

		public int SizeY => cells.GetLength(1);

		public T this[int x, int y] {
			get {
				ArgumentOutOfRange.ThrowIfIndexOutOfRange(x, SizeX, "X", nameof(x));
				ArgumentOutOfRange.ThrowIfIndexOutOfRange(y, SizeX, "Y", nameof(y));
				return cells[x, y];
			}
			set {
				ArgumentOutOfRange.ThrowIfIndexOutOfRange(x, SizeX, "X", nameof(x));
				ArgumentOutOfRange.ThrowIfIndexOutOfRange(y, SizeX, "Y", nameof(y));
				cells[x, y] = value;
			}
		}

		public Grid2D(int sizeX, int sizeY) {
			ArgumentOutOfRange.ThrowIfLengthNegative(sizeX, nameof(sizeX));
			ArgumentOutOfRange.ThrowIfLengthNegative(sizeY, nameof(sizeY));
			cells = new T[sizeX, sizeY];
		}

		public Grid2D(T[,] values) {
			ArgumentNull.ThrowIfNull(values, nameof(values));
			int sizeX = values.GetLength(0);
			int sizeY = values.GetLength(1);
			cells = new T[sizeX, sizeY];
			for( int x = 0; x < sizeX; ++x )
				for( int y = 0; y < sizeY; ++y )
					cells[x, y] = values[x, y];
		}

		public T[,] GetCellValues() {
			var values = new T[SizeX, SizeY];
			for( int x = 0; x < SizeX; ++x )
				for( int y = 0; y < SizeY; ++y )
					values[x, y] = cells[x, y];
			return values;
		}

		public void SetCellValues(T[,] values) {
			ArgumentNull.ThrowIfNull(values, nameof(values));
			ArgumentOutOfRange.ThrowIfLengthNotEqual(values.GetLength(0), SizeX, "X", nameof(values));
			ArgumentOutOfRange.ThrowIfLengthNotEqual(values.GetLength(1), SizeY, "Y", nameof(values));
			for( int x = 0; x < SizeX; ++x )
				for( int y = 0; x < SizeY; ++y )
					cells[x, y] = values[x, y];
		}

		public T[] GetXValues(int y) {
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(y, SizeY, "Y", nameof(y));
			T[] values = new T[SizeX];
			for( int x = 0; x < SizeX; ++x )
				values[x] = cells[x, y];
			return values;
		}

		public void SetXValues(int y, IList<T> values) {
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(y, SizeY, "Y", nameof(y));
			ArgumentNull.ThrowIfNull(values, nameof(values));
			ArgumentOutOfRange.ThrowIfLengthNotEqual(values.Count, SizeX, nameof(values));
			for( int x = 0; x < SizeX; ++x )
				cells[x, y] = values[x];
		}

		public T[] GetYValues(int x) {
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(x, SizeX, "X", nameof(x));
			T[] values = new T[SizeY];
			for( int y = 0; y < SizeY; ++y )
				values[y] = cells[x, y];
			return values;
		}

		public void SetYValues(int x, IList<T> values) {
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(x, SizeX, "X", nameof(x));
			ArgumentNull.ThrowIfNull(values, nameof(values));
			ArgumentOutOfRange.ThrowIfLengthNotEqual(values.Count, SizeY, nameof(values));
			for( int y = 0; y < SizeY; ++y )
				cells[x, y] = values[y];
		}
	}
}