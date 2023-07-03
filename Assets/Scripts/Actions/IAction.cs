namespace NuRpg.Actions {
	public interface IAction {
		bool CanUndo();
		void Do();
		bool Undo();
	}
}