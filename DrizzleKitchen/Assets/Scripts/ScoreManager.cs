using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance {get;private set;}
     [SerializeField] private NpcInteractable npc; 
     [SerializeField] private Score score;
     [SerializeField] private OrderState orderstate;
     public float money;
     
     private void Awake(){
        Instance = this;

     }

    private void Update(){
        npc.OnDelivered += Score_OnDelivered;
    }
    public void Score_OnDelivered(object sender , System.EventArgs e){
        if(npc.delivered){
            DeliveryRecipeSO order = orderstate.Order;
            Debug.Log(order.cost);
            score.score += order.cost;
            money = score.score;
            npc.delivered = false;
    
        }   
    }

}
