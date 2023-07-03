using System;

namespace NuRpg.Actions
{
    public class MoveAction : IAction
    {
        private readonly Actor actor;
        public MoveAction(Actor actor)
        {
            this.actor = actor;
        }
        public bool CanUndo() => false; //change later

        public void Do()
        {
            Random random = new();
            int range = 10;
            int x = random.Next(range);
            int y = random.Next(range);
            actor.Move(x, y);
        }

        public bool Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}