using NuRpg.Collections;
using System;

namespace NuRpg.Services {
	public class CreateModelEventArgs : EventArgs {
		private readonly int _id;

		private readonly IReadOnlyBlackboard _data;

		public CreateModelEventArgs(int id, IReadOnlyBlackboard data) {
			_id = id;
			_data = data;
		}

		public int Id => _id;

		public IReadOnlyBlackboard Data => _data;
	}
}