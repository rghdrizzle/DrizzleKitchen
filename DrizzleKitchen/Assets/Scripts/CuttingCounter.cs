using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField]private CuttingRecipeSO[] cuttingRecipeArray;
    public override void Interact(Player player){
        if(!HasKitchenObject()){
            if(player.HasKitchenObject()){
                if(HasRecipeWithINput(player.GetKitchenObject().GetKitchenObjectSO())){
                    player.GetKitchenObject().SetkitchenObjectParent(this);
                }
                
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
    }
    public override void InteractAlternate(Player player){
        if(HasKitchenObject() && HasRecipeWithINput(GetKitchenObject().GetKitchenObjectSO())){
            //if there is an object then cut it
            KitchenObjectSO outputKitchenObjectSo = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestorySelf();
            
            KitchenObject.SpawnKitchenObject(outputKitchenObjectSo,this);
        }
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputkitchenObjectSO){
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeArray){
            if(cuttingRecipeSO.input == inputkitchenObjectSO){
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }
    private bool HasRecipeWithINput(KitchenObjectSO inputkitchenObjectSo){
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeArray){
            if(cuttingRecipeSO.input == inputkitchenObjectSo){
                return true;
            }

        }
        return false;
    }
}
