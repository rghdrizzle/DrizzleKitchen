using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField]private KitchenObjectSO kitchenObjectSO;

    private IkitchenObjectParent kitchenObjectParent;
    public KitchenObjectSO GetKitchenObjectSO(){
            return kitchenObjectSO;
    }

    public void SetkitchenObjectParent( IkitchenObjectParent kitchenObjectParent){
            if(this.kitchenObjectParent!=null){
                this.kitchenObjectParent.ClearKitchenObject();
            }
            
            this.kitchenObjectParent = kitchenObjectParent;
            if(kitchenObjectParent.HasKitchenObject()){
                Debug.LogError("kitchenObjectParent already has a kitchen object");
            }
            kitchenObjectParent.SetKitchenObject(this);
            transform.parent =kitchenObjectParent.GetKitchenObjectfollowTransform();
            transform.localPosition = Vector3.zero;
    }
    public IkitchenObjectParent GetkitchenObjectParent(){
        return kitchenObjectParent;
    }
    public void DestroySelf(){
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO,IkitchenObjectParent kitchenObjectParent){
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject =kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetkitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject){
        if(this is PlateKitchenObject){
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else{
            plateKitchenObject = null;
            return false;
        }
    }
}
