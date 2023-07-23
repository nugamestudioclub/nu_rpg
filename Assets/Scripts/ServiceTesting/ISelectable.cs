using System;

namespace NuRpg.ServiceTesting {
	public interface ISelectable {
		event EventHandler<SelectionEventArgs> Selected;
	}
}