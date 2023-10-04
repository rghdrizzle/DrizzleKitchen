using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrashCounter : BaseCounter
{
public static event EventHandler OnAnyTrash;
 new public static void ResetStaticData(){
        OnAnyTrash= null;
    }
    public override void Interact(Player player){
        if(!HasKitchenObject()){
            if(player.HasKitchenObject()){
                player.GetKitchenObject().DestroySelf();
                OnAnyTrash?.Invoke(this,EventArgs.Empty);
            }
            else{
                //player not carrying anything
            }
        }
    }
   
}
