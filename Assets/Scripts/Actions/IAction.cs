using NuRpg.Collections;

namespace NuRpg.Actions {
	public interface IAction {
        bool CanUndo(IBlackboard context);
		void Do(IBlackboard context); 
		bool Undo(IBlackboard context);
	}
}