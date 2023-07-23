using System;

namespace NuRpg.ServiceTesting {
	public interface IEntityView {
		event EventHandler<SelectionEventArgs> Selected;
		int Id { get; set; }
		void PlayAnimation(string name);
	}
}