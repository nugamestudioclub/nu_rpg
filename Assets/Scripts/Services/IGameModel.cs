using System;
using System.Collections.Generic;

namespace NuRpg.Services {
	public interface IGameModel {
		event EventHandler<CreateModelEventArgs> Creating;
		event EventHandler<ReadModelEventArgs> Reading;
		event EventHandler<UpdateModelEventArgs> Updating;
		event EventHandler<DeleteModelEventArgs> Deleting;

		IEnumerable<(string Action, int Status)> GetActions(int actor, int target);

		void Load();
	}
}