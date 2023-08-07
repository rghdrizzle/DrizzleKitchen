using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderState : State
{
    public IdleState idle;
    [SerializeField]private RecipeListSO recipelistSO;
    [SerializeField]private UnityEngine.AI.NavMeshAgent agent;
    private Vector3 target;
    [SerializeField] private Transform sit; 
    public override State RunCurrentState(){
        agent.ResetPath();
        //agent.SetDestination(target); 
    
        target = sit.position;
        agent.SetDestination(target);
        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            DeliveryRecipeSO waitingRecipeSO = recipelistSO.recipeSOList[UnityEngine.Random.Range(0,recipelistSO.recipeSOList.Count)];
            Debug.Log("Npc ordered"+ " "+ waitingRecipeSO.recipeName);
        //Order = waitingRecipeSO;
            return idle;
        }
        else{
            return this;
        }
    }

    
}
