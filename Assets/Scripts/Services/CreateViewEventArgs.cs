using NuRpg.Collections;
using System;

namespace NuRpg.Services {
	public class CreateViewEventArgs : EventArgs {
		private readonly int _id;

		private readonly IReadOnlyBlackboard _data;

		private readonly IBlackboard _body;

		public CreateViewEventArgs(int id, IReadOnlyBlackboard data) {
			_id = id;
			_data = data;
			_body = new Blackboard();
		}

		public int Id => _id;

		public IReadOnlyBlackboard Data => _data;

		public IBlackboard Body => _body;

	}
}