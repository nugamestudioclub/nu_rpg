namespace NuRpg.Actions {
	public interface IAction {
        //These all need the context
        bool CanUndo();
		void Do(); 
		bool Undo();
	}
}