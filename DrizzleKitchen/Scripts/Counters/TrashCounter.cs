using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(Player player){
        if(!HasKitchenObject()){
            if(player.HasKitchenObject()){
                player.GetKitchenObject().DestroySelf();
            }
            else{
                //player not carrying anything
            }
        }
    }
   
}
