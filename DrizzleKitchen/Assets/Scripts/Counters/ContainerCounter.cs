using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField]private KitchenObjectSO KitchenObjectSO;
    public override void Interact(Player player){
        //Debug.Log("Interact");
        if(!player.HasKitchenObject()){
            KitchenObject.SpawnKitchenObject(KitchenObjectSO,player);

            OnPlayerGrabbedObject?.Invoke(this,EventArgs.Empty);
       
        }
            

    }
    
}
