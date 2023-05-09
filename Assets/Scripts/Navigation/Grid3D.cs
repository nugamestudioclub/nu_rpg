using NuRpg.Exceptions;
using System;
using System.Collections.Generic;

namespace NuRpg.Navigation {
	public interface IGrid3D<T> {
		T[,,] Cells { get; }
		int SizeX { get; }
		int SizeY { get; }
		int SizeZ { get; }
		T this[int x, int y, int z] { get; set; }
		T[,,] GetCellValues();
		void SetCellValues(T[,,] cells);
		T[] GetXValues(int y, int z);
		void SetXValues(int y, int z, IList<T> values);
		T[] GetYValues(int x, int z);
		void SetYValues(int x, int z, IList<T> values);
		T[] GetZValues(int x, int y);
		void SetZValues(int x, int y, IList<T> values);
		T[,] GetXYValues(int z);
		void SetXYValues(int z, T[,] values);
		T[,] GetXZValues(int y);
		void SetXZValues(int y, T[,] values);
		T[,] GetYZValues(int x);
		void SetYZValues(int x, T[,] values);
	}

	public class Grid3D<T> : IGrid3D<T> {

		private readonly T[,,] cells;

		public T[,,] Cells => cells;

		public int SizeX => cells.GetLength(0);

		public int SizeY => cells.GetLength(1);

		public int SizeZ => cells.GetLength(2);

		public T this[int x, int y, int z] {
			get {
				ArgumentOutOfRange.ThrowIfIndexOutOfRange(x, SizeX, "X", nameof(x));
				ArgumentOutOfRange.ThrowIfIndexOutOfRange(y, SizeX, "Y", nameof(y));
				ArgumentOutOfRange.ThrowIfIndexOutOfRange(z, SizeX, "Z", nameof(z));
				return cells[x, y, z];
			}
			set {
				ArgumentOutOfRange.ThrowIfIndexOutOfRange(x, SizeX, "X", nameof(x));
				ArgumentOutOfRange.ThrowIfIndexOutOfRange(y, SizeX, "Y", nameof(y));
				ArgumentOutOfRange.ThrowIfIndexOutOfRange(z, SizeX, "Z", nameof(z));
				cells[x, y, z] = value;
			}
		}

		public Grid3D(int sizeX, int sizeY, int sizeZ) {
			ArgumentOutOfRange.ThrowIfLengthNegative(sizeX, nameof(sizeX));
			ArgumentOutOfRange.ThrowIfLengthNegative(sizeY, nameof(sizeY));
			ArgumentOutOfRange.ThrowIfLengthNegative(sizeZ, nameof(sizeZ));
			cells = new T[sizeX, sizeY, sizeZ];
		}

		public Grid3D(T[,,] values) {
			ArgumentNull.ThrowIfNull(values, nameof(values));
			int sizeX = values.GetLength(0);
			int sizeY = values.GetLength(1);
			int sizeZ = values.GetLength(2);
			cells = new T[sizeX, sizeY, sizeZ];
			for( int x = 0; x < sizeX; ++x )
				for( int y = 0; y < sizeY; ++y )
					for( int z = 0; z < sizeZ; ++z )
						cells[x, y, z] = values[x, y, z];
		}

		public T[,,] GetCellValues() {
			var values = new T[SizeX, SizeY, SizeZ];
			for( int x = 0; x < SizeX; ++x )
				for( int y = 0; y < SizeY; ++y )
					for( int z = 0; z < SizeZ; ++z )
						values[x, y, z] = cells[x, y, z];
			return values;
		}

		public void SetCellValues(T[,,] values) {
			ArgumentNull.ThrowIfNull(values, nameof(values));
			ArgumentOutOfRange.ThrowIfLengthNotEqual(values.GetLength(0), SizeX, "X", nameof(values));
			ArgumentOutOfRange.ThrowIfLengthNotEqual(values.GetLength(1), SizeY, "Y", nameof(values));
			ArgumentOutOfRange.ThrowIfLengthNotEqual(values.GetLength(2), SizeZ, "Z", nameof(values));
			for( int x = 0; x < SizeX; ++x )
				for( int y = 0; x < SizeY; ++y )
					for( int z = 0; z < SizeZ; ++z )
						cells[x, y, z] = values[x, y, z];
		}

		public T[] GetXValues(int y, int z) {
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(y, SizeY, "Y", nameof(y));
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(z, SizeZ, "Z", nameof(z));
			T[] values = new T[SizeX];
			for( int x = 0; x < SizeX; ++x )
				values[x] = cells[x, y, z];
			return values;
		}

		public void SetXValues(int y, int z, IList<T> values) {
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(y, SizeY, "Y", nameof(y));
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(z, SizeZ, "Z", nameof(z));
			ArgumentNull.ThrowIfNull(values, nameof(values));
			ArgumentOutOfRange.ThrowIfLengthNotEqual(values.Count, SizeX, nameof(values));
			for( int x = 0; x < SizeX; ++x )
				cells[x, y, z] = values[x];
		}

		public T[] GetYValues(int x, int z) {
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(x, SizeX, "X", nameof(x));
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(z, SizeZ, "Z", nameof(z));
			T[] values = new T[SizeY];
			for( int y = 0; y < SizeY; ++y )
				values[y] = cells[x, y, z];
			return values;
		}

		public void SetYValues(int x, int z, IList<T> values) {
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(x, SizeX, "X", nameof(x));
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(z, SizeZ, "Z", nameof(z));
			ArgumentNull.ThrowIfNull(values, nameof(values));
			ArgumentOutOfRange.ThrowIfLengthNotEqual(values.Count, SizeY, nameof(values));
			for( int y = 0; y < SizeY; ++y )
				cells[x, y, z] = values[y];
		}

		public T[] GetZValues(int x, int y) {
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(x, SizeX, "X", nameof(x));
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(y, SizeY, "Y", nameof(y));
			T[] values = new T[SizeZ];
			for( int z = 0; z < SizeZ; ++y )
				values[z] = cells[x, y, z];
			return values;
		}

		public void SetZValues(int x, int y, IList<T> values) {
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(x, SizeX, "X", nameof(x));
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(y, SizeY, "Y", nameof(y));
			ArgumentNull.ThrowIfNull(values, nameof(values));
			ArgumentOutOfRange.ThrowIfLengthNotEqual(values.Count, SizeZ, nameof(values));
			for( int z = 0; z < SizeZ; ++z )
				cells[x, y, z] = values[y];
		}

		public T[,] GetXYValues(int z) {
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(z, SizeZ, "Z", nameof(z));
			var values = new T[SizeX, SizeY];
			for( int x = 0; x < SizeX; ++x  )
				for( int y = 0; y < SizeY; ++y )
					values[x, y] = cells[x, y, z];
			return values;
		}

		public void SetXYValues(int z, T[,] values) {
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(z, SizeZ, "Z", nameof(z));
			ArgumentNull.ThrowIfNull(values, nameof(values));
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(values.GetLength(0), SizeX, "X", nameof(values));
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(values.GetLength(1), SizeX, "Y", nameof(values));
			for( int x = 0; x < SizeX; ++x )
				for( int y = 0; y < SizeY; ++y )
					cells[x, y, z] = values[x, y];
		}

		public T[,] GetXZValues(int y) {
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(y, SizeY, "Y", nameof(y));
			var values = new T[SizeX, SizeZ];
			for( int x = 0; x < SizeX; ++x )
				for( int z = 0; z < SizeZ; ++z )
					values[x, z] = cells[x, y, z];
			return values;
		}

		public void SetXZValues(int y, T[,] values) {
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(y, SizeY, "Y", nameof(y));
			ArgumentNull.ThrowIfNull(values, nameof(values));
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(values.GetLength(0), SizeX, "X", nameof(values));
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(values.GetLength(2), SizeX, "Z", nameof(values));
			for( int x = 0; x < SizeX; ++x )
				for( int z = 0; z < SizeZ; ++z )
					cells[x, y, z] = values[x, z];
		}

		public T[,] GetYZValues(int x) {
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(x, SizeX, "X", nameof(x));
			var values = new T[SizeY, SizeZ];
			for( int y = 0; y < SizeY; ++y )
				for( int z = 0; z < SizeZ; ++z )
					values[y, z] = cells[x, y, z];
			return values;
		}

		public void SetYZValues(int x, T[,] values) {
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(x, SizeX, "X", nameof(x));
			ArgumentNull.ThrowIfNull(values, nameof(values));
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(values.GetLength(1), SizeX, "Y", nameof(values));
			ArgumentOutOfRange.ThrowIfIndexOutOfRange(values.GetLength(2), SizeX, "Z", nameof(values));
				for( int y = 0; y < SizeY; ++y )
					for( int z = 0; z < SizeZ; ++z )
						cells[x, y, z] = values[y, z];
		}
	}
}