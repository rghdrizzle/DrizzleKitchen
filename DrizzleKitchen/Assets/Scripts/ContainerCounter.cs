using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField]private KitchenObjectSO KitchenObjectSO;
    public override void Interact(Player player){
        Debug.Log("interact");
            Transform kitchenObjectTransform = Instantiate(KitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetkitchenObjectParent(player);

            OnPlayerGrabbedObject?.Invoke(this,EventArgs.Empty);
       

    }
    
}
