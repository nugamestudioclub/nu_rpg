using NuRpg.Collections;
using System;

namespace NuRpg.Services {
	public interface IGameView {
		event EventHandler<CreateViewEventArgs> Creating;
		event EventHandler<ReadViewEventArgs> Reading;
		event EventHandler<UpdateViewEventArgs> Updating;
		event EventHandler<DeleteViewEventArgs> Deleting;
		void Create(int id, IReadOnlyBlackboard data);
	}
}