
using System;

namespace NuRpg.ServiceTesting {
	public class SelectionEventArgs : EventArgs {
		private readonly int _id;

		public SelectionEventArgs(int id) {
			_id = id;
		}

		public int Id => _id;
	}
}