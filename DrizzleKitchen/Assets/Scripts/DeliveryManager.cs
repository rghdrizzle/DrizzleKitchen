using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryManager : MonoBehaviour
{
   public event EventHandler OnRecipeSpawned;
   public event EventHandler OnRecipeCompleted;
   public event EventHandler OnRecipeSuccess;
   public event EventHandler OnRecipeFailed;
   public static DeliveryManager Instance { get; private set; }
   [SerializeField]private RecipeListSO recipelistSO;
   private List<DeliveryRecipeSO> waitingRecipeSOList;

   private float spawnRecipeTimer;
   private float spawnRecipeTimerMax = 4f;
   private int waitingRecipeMax=4;

   private void Awake(){
    Instance = this;
    waitingRecipeSOList = new List<DeliveryRecipeSO>();
    
   }
   private void Update(){

    spawnRecipeTimer -= Time.deltaTime;
    if( spawnRecipeTimer <=0f){
        spawnRecipeTimer = spawnRecipeTimerMax;
        if(waitingRecipeSOList.Count < waitingRecipeMax){
            DeliveryRecipeSO waitingRecipeSO = recipelistSO.recipeSOList[UnityEngine.Random.Range(0,recipelistSO.recipeSOList.Count)];
            Debug.Log(waitingRecipeSO.recipeName);
            waitingRecipeSOList.Add(waitingRecipeSO);

            OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
        }
       
    }
   }
   public void DeliverRecipe(PlateKitchenObject plateKitchenObject){
    for( int i=0;i<waitingRecipeSOList.Count;i++){
        DeliveryRecipeSO waitingRecipeSO = waitingRecipeSOList[i];
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
                waitingRecipeSOList.RemoveAt(i);
                OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                return;
            }
        }
        // no matches found
        //player didnt deliver correct recipe
        Debug.Log("player didnt deliver correct recipe");
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }
   }
   public List<DeliveryRecipeSO> GetWaitingRecipeSOList(){
    return waitingRecipeSOList;
   }
}
