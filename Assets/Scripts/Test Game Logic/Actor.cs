using UnityEngine;
using NuRpg.Actions;
using NuRpg.Collections;
using System;
using Random = System.Random;
//We should make a general interface for
//actors that have common behavior (move, attack, etc)
public class Actor : MonoBehaviour, IActor
{
    private TimeQueue<IAction> actionQueue = new(DateTime.MinValue);
    public ITimeQueue<IAction> ActionQueue => actionQueue;

    private IAction moveAction;

    [SerializeField]
    private float moveScale = 1;
    void Start()
    {
        moveAction = new MoveAction(this);
    }
    // Update is called once per frame
    void Update()
    {
        ActionQueue.UpdateSeconds(Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log($"Queuing a move action on: {name}");
            ActionQueue.DelaySeconds(moveAction, 1);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (ActionQueue.TryDequeue(out IAction action))
            {
                IBlackboard context = new Blackboard(); 
                Random random = new();
                int range = 5;
                int x = random.Next(-range, range);
                int y = random.Next(range);
                context.SetValue("position", new Vector2Int(x, y));
                action.Do(context);
            }
            else
            {
                Debug.Log($"No action on queue for: {name}");
            }
        }
    }

    public void Move(int x, int y)
    {
        Debug.Log($"Moving to ({x},{y})");
        transform.position = new Vector3(x, y) * moveScale + new Vector3(.5f* moveScale, .5f * moveScale);
    }
}
