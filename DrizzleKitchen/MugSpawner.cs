using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MugSpawner : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player){
        if(!player.HasKitchenObject()){
            KitchenObject.SpawnKitchenObject(kitchenObjectSO,player);
    }

}
} 
