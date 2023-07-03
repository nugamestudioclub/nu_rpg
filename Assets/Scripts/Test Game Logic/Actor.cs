using UnityEngine;
using NuRpg.Actions;
using NuRpg.Collections;
using System;
//We should make a general interface for
//actors that have common behavior (move, attack, etc)
public class Actor : MonoBehaviour, IActor
{
    private TimeQueue<IAction> actionQueue = new(DateTime.MinValue);
    public ITimeQueue<IAction> ActionQueue => actionQueue;

    private IAction moveAction;
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
                action.Do();
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
        transform.position = new Vector3(x, y);
    }
}
