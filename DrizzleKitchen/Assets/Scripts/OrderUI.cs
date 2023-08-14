using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;
    [SerializeField] private OrderState orderState;

    private void Update(){
        if (textMeshPro != null){
            if(orderState.Order!=null){
                textMeshPro.text = orderState.Order.recipeName;
            }
            else{
                textMeshPro.text = "Hello chef!";
            }
            
        }
    }
    
}
