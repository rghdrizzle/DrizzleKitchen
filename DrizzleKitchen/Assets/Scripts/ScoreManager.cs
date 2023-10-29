using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance {get;private set;}
     [SerializeField] private NpcInteractable npc; 
     [SerializeField] private OrderState orderstate;
     private float score;
     public float money;
     
     private void Awake(){
        Instance = this;

     }
     private void Start(){
        score =0;
     }

    private void Update(){
        npc.OnDelivered += Score_OnDelivered;
    }
    public void Score_OnDelivered(object sender , System.EventArgs e){
        if(npc.delivered){
            DeliveryRecipeSO order = orderstate.Order;
            Debug.Log(order.cost);
            score += order.cost;
            Debug.Log(score);
            money = score;
            npc.delivered = false;
    
        }   
    }
    public float GetScore(){
        return money;
    }

}
