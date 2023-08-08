using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveState : State
{
    [SerializeField]private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField]private Transform Exitpoint;
    [SerializeField] private GameObject Npc; 
    private Vector3 target;
    public IdleState idle;
    public override State RunCurrentState(){
        agent.ResetPath();
        target = Exitpoint.position;
        agent.SetDestination(target);
        
        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance){
           Npc.SetActive(false);

	       return idle; 
        }
    else{
        return this;
    }
   }
}
