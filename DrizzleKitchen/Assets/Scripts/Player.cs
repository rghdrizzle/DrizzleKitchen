using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour , IkitchenObjectParent
{  
    public static Player Instance {get; private set;}

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs{
          public BaseCounter selectedCounter;
    }

    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private Gameinputs Gameinputs;
    [SerializeField]private LayerMask countersLayerMask;
    [SerializeField]private Transform KitchenObjectHoldPoint;
    private bool isWalking;
    private Vector3 LastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

     private void Awake() {
          if(Instance!=null){
               Debug.LogError("More than one player instance");
          }
          Instance = this;
    }
    private void Start() {
          Gameinputs.OnInteractAction += Gameinputs_OnInteractAction;
    }
    private void Gameinputs_OnInteractAction(object sender, System.EventArgs e){
      if( selectedCounter!=null ){
          selectedCounter.Interact(this);
      }
    }
    
    private void Update(){
         HandleInteractions();
         HandleMovement();
    }

    public bool IsWalking(){
         return isWalking;
    }

    private void HandleMovement(){
     Vector2 inputVector= Gameinputs.GetMovementVectorNormalized();
         Vector3 moveDir = new Vector3(inputVector.x, 0f,inputVector.y);
         float playerRadius =.7f;
         float playerheight =2f;
         float moveDistance = moveSpeed* Time.deltaTime;
         bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight,playerRadius,moveDir,moveDistance);
      
         if(!canMove){
          Vector3 moveDirX = new Vector3(moveDir.x,0,0).normalized;
          canMove = moveDir.x!=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight,playerRadius,moveDirX,moveDistance);
          if(canMove){
               moveDir =moveDirX;
          }else{
               Vector3 moveDirZ = new Vector3(0,0,moveDir.z).normalized;
               canMove =moveDir.z!=0 &&!Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight,playerRadius,moveDirZ,moveDistance);
               if(canMove){
                    moveDir =moveDirZ;
               }else{
                    //cannot move in any direction
               }
          }
         }
         isWalking = moveDir != Vector3.zero;
         if(canMove){
            
            transform.position += moveDir * moveDistance;
         }

         float rotateSpeed = 10f;
         transform.forward= Vector3.Slerp(transform.forward, moveDir,Time.deltaTime* rotateSpeed);

        
         

    }
    private void HandleInteractions(){
     Vector2 inputVector= Gameinputs.GetMovementVectorNormalized();
     Vector3 moveDir = new Vector3(inputVector.x, 0f,inputVector.y);
     if( moveDir!= Vector3.zero){
          LastInteractDir=moveDir;
     }
     float Interactdistance = 2f;
     if(Physics.Raycast(transform.position, LastInteractDir,out RaycastHit raycasthit ,Interactdistance,countersLayerMask)){
        if(raycasthit.transform.TryGetComponent(out BaseCounter baseCounter)){
          // Has counter
          if( baseCounter!=selectedCounter){
              SetSelectedCounter(baseCounter);
          }
          else{
               SetSelectedCounter(null);
          }
        }
     }
     else{
          SetSelectedCounter(null);
     }
    }
    private void SetSelectedCounter(BaseCounter selectedCounter){
     this.selectedCounter= selectedCounter;
     OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{selectedCounter=selectedCounter});
    }

    public Transform GetKitchenObjectfollowTransform(){
        return KitchenObjectHoldPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject =kitchenObject;
    }
        
    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }
    public void  ClearKitchenObject(){
        kitchenObject = null;
    }
    public bool HasKitchenObject(){
        return kitchenObject != null;
    }
}
