using NuRpg.Collections;

namespace NuRpg.Actions {
	public interface IActor {
		ITimeQueue<IAction> ActionQueue { get; }
		/*
		IAction NextAction { get; }
		void CancelActions(Func<IAction, bool> predicate);
		void CancelActionsAfter(double seconds);
		void CancelActionsLowerThan(int priority);
		void CancelAllActions();
		void DelayAction(IAction action, double seconds);
		void QueueAction(IAction action);
		void QueueAction(IAction action, int priority);
		void ReadyAction(IAction action);
		IAction TakeAction();
		void Update(double seconds);
		*/
	}
}