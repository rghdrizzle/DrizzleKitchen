using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{   
    [SerializeField]private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField]private Transform Movepoint;
    private Vector3 target;

    public OrderState orderState;
    public override State RunCurrentState(){
        target = Movepoint.position;
        agent.SetDestination(target);
        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance){  
	       return orderState;
        }
    else{
        return this;
    }
    }
}
