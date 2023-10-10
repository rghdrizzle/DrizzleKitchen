using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
     [SerializeField] private NpcInteractable npc; 
     [SerializeField] private OrderState orderstate;
     float score =0;
     private void Start(){
        score =0;
     }

    private void Update(){
        npc.OnDelivered += Score_OnDelivered;
    }
    private void Score_OnDelivered(object sender , System.EventArgs e){
        if(npc.delivered){
            DeliveryRecipeSO order = orderstate.Order;
            Debug.Log(order.cost);
            score += order.cost;
            Debug.Log(score);
        }   
    }

}
