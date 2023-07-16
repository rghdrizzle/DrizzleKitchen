using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoveCounter : BaseCounter , IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs{
        public State state;
    }
    public enum State{
        Idle,
        Frying,
        Fried,
        Burned,
    }
    [SerializeField]private FryingRecipeSO[] fryingrecipeSOArray;
    [SerializeField]private BurningRecipeSO[] burningRecipeSOArray;

    private float fryingTimer;
    private State state;
    private FryingRecipeSO fryingrecipeSO;
    private float BurningTimer;
    private BurningRecipeSO burningRecipeSO;

    private void Start(){
        state = State.Idle;

    }
    private void Update(){
        
        if(HasKitchenObject()){
            switch(state){
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{
                            progressNormalized = fryingTimer / fryingrecipeSO.FryingTimerMax
                        });
                    if(fryingTimer > fryingrecipeSO.FryingTimerMax){
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(fryingrecipeSO.output,this);
                        state = State.Fried;
                        BurningTimer =0f;
                        burningRecipeSO = GetBurningRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                            state= state
                        }); 
                        }
                        break;
                case State.Fried:
                    BurningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{
                            progressNormalized = BurningTimer / burningRecipeSO.burningTimerMax});
                    if(BurningTimer > burningRecipeSO.burningTimerMax){
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output,this);
                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                            state= state
                        });

                        OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{
                            progressNormalized = 0f
                        });
                        
                        }
                    break;
                case State.Burned:
                    break;
            }
        }
    }
    
    public override void Interact(Player player){
        if(!HasKitchenObject()){
            if(player.HasKitchenObject()){
                if(HasRecipeWithINput(player.GetKitchenObject().GetKitchenObjectSO())){
                    player.GetKitchenObject().SetkitchenObjectParent(this);
                    fryingrecipeSO = GetFryingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;
                    fryingTimer =0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                            state= state
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
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                            state= state
                        });
                OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{
                            progressNormalized = 0f
                        });
            }

        }
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputkitchenObjectSO){
        FryingRecipeSO fryingrecipeSO = GetFryingRecipeSoWithInput(inputkitchenObjectSO);
        if(fryingrecipeSO != null){
            return fryingrecipeSO.output;
        }
        else{
            return null;
        }
    }
    private bool HasRecipeWithINput(KitchenObjectSO inputkitchenObjectSo){
        FryingRecipeSO fryingrecipeSO = GetFryingRecipeSoWithInput(inputkitchenObjectSo);
        return fryingrecipeSO != null;
    }
    
    private FryingRecipeSO GetFryingRecipeSoWithInput(KitchenObjectSO inputkitchenObjectSO){
         foreach(FryingRecipeSO fryingrecipeSO in fryingrecipeSOArray){
            if(fryingrecipeSO.input == inputkitchenObjectSO){
                return fryingrecipeSO;
            }
        }
        return null;
    }
    private BurningRecipeSO GetBurningRecipeSoWithInput(KitchenObjectSO inputkitchenObjectSO){
         foreach(BurningRecipeSO burningRecipeSO in burningRecipeSOArray){
            if(burningRecipeSO.input == inputkitchenObjectSO){
                return burningRecipeSO;
            }
        }
        return null;
    }
    

}