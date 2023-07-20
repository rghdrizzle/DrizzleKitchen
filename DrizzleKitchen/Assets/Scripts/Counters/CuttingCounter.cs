using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttingCounter : BaseCounter , IHasProgress
{
    public static event EventHandler OnAnyCut;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCutting;
    [SerializeField]private CuttingRecipeSO[] cuttingRecipeArray;
    private int cuttingProcess;
    public override void Interact(Player player){
        //Debug.Log("Interact");
        if(!HasKitchenObject()){
            if(player.HasKitchenObject()){
                if(HasRecipeWithINput(player.GetKitchenObject().GetKitchenObjectSO())){
                    player.GetKitchenObject().SetkitchenObjectParent(this);
                    cuttingProcess =0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
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
                if(player.GetKitchenObject() .TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    if( plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                        GetKitchenObject().DestroySelf();
               }
                
             }else{
                if(GetKitchenObject().TryGetPlate(out plateKitchenObject)){
                    //counter is holding plate
                    if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())){
                        player.GetKitchenObject().DestroySelf();
                    }
                }
             }

            }else{
                //player not carrying anything
                GetKitchenObject().SetkitchenObjectParent(player);
            }

        }
    }
    public override void InteractAlternate(Player player){
        //Debug.Log("Cutting");
        if(HasKitchenObject() && HasRecipeWithINput(GetKitchenObject().GetKitchenObjectSO())){
            //if there is an object then cut it
            cuttingProcess++;
            OnCutting?.Invoke(this,EventArgs.Empty);
            OnAnyCut?.Invoke(this,EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalized = (float)cuttingProcess / cuttingRecipeSO.cuttingProgressMax
                    });
            if(cuttingProcess>= cuttingRecipeSO.cuttingProgressMax){
                KitchenObjectSO outputKitchenObjectSo = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
            
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
