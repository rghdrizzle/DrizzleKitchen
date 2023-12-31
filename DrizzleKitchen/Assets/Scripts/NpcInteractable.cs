using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NpcInteractable : BaseCounter
{
    public static NpcInteractable Instance{get;private set;}
    public event EventHandler OnDelivered;
    [SerializeField] private OrderState orderState;
    public bool delivered;
    public void Interact(Player player){
        if(player.HasKitchenObject()){
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                
                //DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                delivered= DeliverToCustomer(plateKitchenObject);
                player.GetKitchenObject().SetkitchenObjectParent(this);
                OnDelivered?.Invoke(this,EventArgs.Empty);
               
            
            }
            else if(player.GetKitchenObject().name == "mug(Clone)" && orderState.Order.recipeName =="Juice"){
                player.GetKitchenObject().GetComponent<Rigidbody>().useGravity=false;
                delivered = true;
                player.GetKitchenObject().SetkitchenObjectParent(this);
                OnDelivered?.Invoke(this,EventArgs.Empty);
                Debug.Log("Thats refreshing hmm hmm!!!!");
            }
        }
    }
    private bool DeliverToCustomer(PlateKitchenObject plateKitchenObject){
        DeliveryRecipeSO waitingRecipeSO = orderState.Order;
        if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectList().Count){
            // Has same number of ingredients
            bool plateContentMatchesRecipe = true;
            foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList){
                // cycling through each ingredient in the recipe
                bool ingredientFound = false;
                foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectList()){
                    //cyclying through all the ingredients in the plate
                    if(plateKitchenObjectSO == recipeKitchenObjectSO){
                        //Ingredients matched
                        ingredientFound = true;
                        break;
                    }
                }
                if(!ingredientFound){
                    // this recipe was not found on the plate
                    plateContentMatchesRecipe = false;
                }
            }
            if(plateContentMatchesRecipe){
                Debug.Log("Player delivered the correct recipe");
                // OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                // OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                return true;
            }
        }
        // no matches found
        //player didnt deliver correct recipe
        Debug.Log("player didnt deliver correct recipe");
        return false;
        //OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    
   }
        
    }
    


