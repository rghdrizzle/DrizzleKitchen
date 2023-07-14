using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs{
        public float progressNormalized;
    }
    public event EventHandler OnCutting;
    [SerializeField]private CuttingRecipeSO[] cuttingRecipeArray;
    private int cuttingProcess;
    public override void Interact(Player player){
        if(!HasKitchenObject()){
            if(player.HasKitchenObject()){
                if(HasRecipeWithINput(player.GetKitchenObject().GetKitchenObjectSO())){
                    player.GetKitchenObject().SetkitchenObjectParent(this);
                    cuttingProcess =0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs{
                        progressNormalized = (float)cuttingProcess / cuttingRecipeSO.cuttingProgressMax
                    });
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
            cuttingProcess++;
            OnCutting?.Invoke(this,EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs{
                        progressNormalized = (float)cuttingProcess / cuttingRecipeSO.cuttingProgressMax
                    });
            if(cuttingProcess>= cuttingRecipeSO.cuttingProgressMax){
                KitchenObjectSO outputKitchenObjectSo = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestorySelf();
            
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSo,this);
            }
            
        }
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputkitchenObjectSO){
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSoWithInput(inputkitchenObjectSO);
        if(cuttingRecipeSO != null){
            return cuttingRecipeSO.output;
        }
        else{
            return null;
        }
    }
    private bool HasRecipeWithINput(KitchenObjectSO inputkitchenObjectSo){
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSoWithInput(inputkitchenObjectSo);
        return cuttingRecipeSO != null;
    }
    private CuttingRecipeSO GetCuttingRecipeSoWithInput(KitchenObjectSO inputkitchenObjectSO){
         foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeArray){
            if(cuttingRecipeSO.input == inputkitchenObjectSO){
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
