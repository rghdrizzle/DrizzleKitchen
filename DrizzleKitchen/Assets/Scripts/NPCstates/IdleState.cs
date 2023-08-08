using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public MoveState move;
    public override State RunCurrentState(){
        return move;
        //return this;
    }
}
