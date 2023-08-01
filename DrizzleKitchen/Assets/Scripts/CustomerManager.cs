using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    
    public static CustomerManager Instance{get; private set;}
    [SerializeField]private GameObject Npc;
    [SerializeField]private GameObject[] pathPoints;
    [SerializeField]private GameObject[] LeavingPath;
    [SerializeField]private GameObject[] SittingPath;
    public int NoOfPointsEntering;
    public int NoOfPointsExiting;
    public int NoOfPointsForSitting;
    public float speed = 2f;

    private Vector3 CurrentPosition;
    public int i;
    private int x;
    private int y;
    
    public enum State{
        Idle,
        Move,
        Order,
        Wait,
        Sit,
        Leave
    }
    [SerializeField]private RecipeListSO recipelistSO;
    public State state;
    
    private float WaitingTime ;
    private float WaitingTimeMax = 20f;
    private float EatingTime;
    private float EatingTimeMax = 40f;
    
    private void Awake(){
        Instance = this;    
    }
    private void Start(){
        state= State.Move;
        i=1;
        x=1;
        y=1;
    }

    private DeliveryRecipeSO Order;
    [SerializeField]private DeliveryCounter deliveryCounter;
    public void Update(){
        switch(state){
            case State.Idle:
                break;
            case State.Move:
                CurrentPosition = Npc.transform.position;
                Vector3 newPos = Vector3.MoveTowards(CurrentPosition,pathPoints[i].transform.position,speed*Time.deltaTime);
                transform.position = newPos;
                if( CurrentPosition == pathPoints[i].transform.position && i!= NoOfPointsEntering){
                        i++;
                }
                 if(i== NoOfPointsEntering){
                        state = State.Order;
                }
                break;
            case State.Order:
                DeliveryRecipeSO waitingRecipeSO = recipelistSO.recipeSOList[UnityEngine.Random.Range(0,recipelistSO.recipeSOList.Count)];
                Debug.Log("Npc ordered"+ " "+ waitingRecipeSO.recipeName);
                Order = waitingRecipeSO;
                state = State.Wait;
                Debug.Log(state);
                break;
            case State.Wait:
                WaitingTime+= Time.deltaTime;
                if(WaitingTime>=WaitingTimeMax){
                    WaitingTime=0;
                   Debug.Log("OH I DONT NEED UR FOOD");
                    state = State.Leave;
                }
                if(deliveryCounter.delivered){
                    Debug.Log("Thank you");
                    WaitingTime=0;
                    deliveryCounter.delivered = false;
                    state = State.Sit;
                }
                break;
            case State.Sit:
                CurrentPosition = Npc.transform.position;
                Vector3 newPosSit = Vector3.MoveTowards(CurrentPosition,SittingPath[y].transform.position,speed*Time.deltaTime);
                transform.position = newPosSit;
                if( CurrentPosition == SittingPath[y].transform.position && y!= NoOfPointsForSitting-1){
                        y++;
                }
                EatingTime+= Time.deltaTime;
                if(EatingTime>=EatingTimeMax){
                    EatingTime=0;
                   Debug.Log("Bubye , the food was awesome");
                    state = State.Leave;
                }
                
                break;
            case State.Leave:
                CurrentPosition = Npc.transform.position;
                Vector3 newPosLeave = Vector3.MoveTowards(CurrentPosition,LeavingPath[x].transform.position,speed*Time.deltaTime);
                transform.position = newPosLeave;
                if( CurrentPosition == LeavingPath[x].transform.position && x!= NoOfPointsExiting){
                        x++;
                }
                if(x==NoOfPointsExiting){
                    state=State.Move;
                    if(gameObject.name == "Npc"){
                        gameObject.SetActive(false);
                        i=1;
                        y=1;
                        x=1;
                        state=State.Idle;
                    }
                    else{
                        //Destroy(gameObject);
                        gameObject.SetActive(false);
                        i=1;
                        y=1;
                        x=1;
                    }
                    
                    
                }
                
                
                break;
        }
        
    }
public bool DeliverToCustomer(PlateKitchenObject plateKitchenObject){
        DeliveryRecipeSO waitingRecipeSO = Order;
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

