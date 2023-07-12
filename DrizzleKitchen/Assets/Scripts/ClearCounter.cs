using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter

{

    [SerializeField]private KitchenObjectSO KitchenObjectSO;

    public override void Interact(Player player){
        Debug.Log("interact");

        //Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);

    }
}
