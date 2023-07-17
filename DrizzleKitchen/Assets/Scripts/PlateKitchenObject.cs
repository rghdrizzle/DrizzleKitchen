using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateKitchenObject : KitchenObject
{
    
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs: EventArgs{
        public KitchenObjectSO kitchenObjectSO;
    }
    [SerializeField]private List<KitchenObjectSO> validKitchenObjectSOList;
    private List<KitchenObjectSO> kitchenObjectSOList;
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO){
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO)){
            return false;

        }
        if(kitchenObjectSOList.Contains(kitchenObjectSO)){
            return false;
        }
        else{
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs{
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
        
        }

    private void Awake(){
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public List<KitchenObjectSO> GetKitchenObjectList(){
        return kitchenObjectSOList;
    }


}
