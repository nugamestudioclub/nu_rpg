using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMapper
{
    //maps IDs to -> (label, action, event)


    //"Permissions" manager
        //something that that can tell an actor what actions they may take at a given time

    //On the Actor
    //Ask Permissions Manager what actions I can perform now
    //Gives back list of actions with their corresponding ready state

    //notes
        //blackboard applications:
            //dealing with varying length action args
            //exposing global gamestate 
                //move: player not immobilized
                //attack: another actor is adjacent
                //wait: always available

}
