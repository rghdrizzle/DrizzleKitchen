using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderState : State
{
    public static OrderState Instance ;
    public WaitState wait;
    [SerializeField]private RecipeListSO recipelistSO;
    [SerializeField]private UnityEngine.AI.NavMeshAgent agent;
    private Vector3 target;
    [SerializeField] private Transform sit; 
    private bool hasGotChair = false;
    public DeliveryRecipeSO Order;
    public override State RunCurrentState(){
        if(Time.timeScale>0){
            agent.ResetPath();
        //agent.SetDestination(target); 
        // Transform des = Chairs.Instance.GetChair();
        // target = des.position;
        // agent.SetDestination(target);
        if (hasGotChair==false) 
        {
            Transform des = Chairs.Instance.GetChair();
            target = des.position;
            hasGotChair = true; 
        }
        agent.SetDestination(target);
        }
        

        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            DeliveryRecipeSO waitingRecipeSO = recipelistSO.recipeSOList[UnityEngine.Random.Range(0,recipelistSO.recipeSOList.Count)];
            Debug.Log("Npc ordered"+ " "+ waitingRecipeSO.recipeName);
            Order = waitingRecipeSO;
            return wait;
        }
        else{
            return this;
        }
    }

    
}
