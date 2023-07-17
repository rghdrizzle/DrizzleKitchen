using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter

{

    [SerializeField]private KitchenObjectSO KitchenObjectSO;

    public override void Interact(Player player){
        //Debug.Log("Interact");
        if(!HasKitchenObject()){
            if(player.HasKitchenObject()){
                player.GetKitchenObject().SetkitchenObjectParent(this);
            }
            else{
                //player not carrying anything
            }
        }
        else{
            if(player.HasKitchenObject()){
             if(player.GetKitchenObject() .TryGetPlate(out PlateKitchenObject plateKitchenObject)){

               if( plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                    GetKitchenObject().DestroySelf();
               }
                
             }else{
                //player something other than plate
                if(GetKitchenObject().TryGetPlate(out plateKitchenObject)){
                    if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        player.GetKitchenObject().DestroySelf();
                    }
                }
             }

            }else{
                //player not carrying anything
                GetKitchenObject().SetkitchenObjectParent(player);
            }

        }

        //Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);

    }
}
