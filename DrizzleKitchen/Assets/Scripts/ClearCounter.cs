using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter

{

    [SerializeField]private KitchenObjectSO KitchenObjectSO;

    public override void Interact(Player player){
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
                

            }else{
                //player not carrying anything
                GetKitchenObject().SetkitchenObjectParent(player);
            }

        }

        //Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);

    }
}
