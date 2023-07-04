using NuRpg.Collections;
using System;
using UnityEngine;

namespace NuRpg.Actions
{
    public class MoveAction : IAction
    {
        private readonly Actor actor;
        public MoveAction(Actor actor)
        {
            this.actor = actor;
        }
        public bool CanUndo(IBlackboard context) => false; //change later

        public void Do(IBlackboard context)
        {
            string key = "position";
            if (context.TryGetValue(key, out Vector2Int vector)) {
                actor.Move(vector.x, vector.y);
            }
            else
            {
                Debug.Log($"No context key: {key}");
            }
        }

        public bool Undo(IBlackboard context)
        {
            throw new System.NotImplementedException();
        }
    }
}