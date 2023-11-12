using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour , IkitchenObjectParent
{  
    public static Player Instance {get; private set;}
    public event EventHandler OnPick;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs{
          public BaseCounter selectedCounter;
    }

    [SerializeField]private Transform ObjectPoint;
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private Gameinputs Gameinputs;
    [SerializeField]private LayerMask countersLayerMask;
    [SerializeField]private LayerMask NpcLayerMask;
    [SerializeField]private Transform KitchenObjectHoldPoint;
    private bool isWalking;
    private Vector3 LastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;


    //fps testing
    public GameObject playerCamera;
    [SerializeField] private Transform playerTransform;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float mouseSensitivity = 100;
    public float lookXLimit = 90f;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    public bool canMove = true;
    public CharacterController characterController;

    [SerializeField] public OrderUI orderUI;
    

     private void Awake() {
          if(Instance!=null){
               Debug.LogError("More than one player instance");
          }
          Instance = this;
    }
    private void Start() {
          Gameinputs.OnInteractAction += Gameinputs_OnInteractAction;
          Gameinputs.OnInteractAlternateAction += Gameinputs_OnInteractAlternateAction;
          Gameinputs.OnInteractAction += Npc_OnInteractAction;
          Cursor.lockState= CursorLockMode.Locked;
          Cursor.visible = false;   
}
     private void Npc_OnInteractAction(object sender, System.EventArgs e){
        Collider[] colliderArray = Physics.OverlapSphere(transform.position,1.5f);
        foreach(Collider collider in colliderArray){
          if(collider.TryGetComponent(out NpcInteractable npcInteract)){
               npcInteract.Interact(this);
          }
          
        }
        
          
     }
     private void Gameinputs_OnInteractAlternateAction(object sender, System.EventArgs e){
          if(!GameManager.Instance.IsGamePlaying()) return ;
          if(selectedCounter!=null ){
          selectedCounter.InteractAlternate(this);
      }
     }
    private void Gameinputs_OnInteractAction(object sender, System.EventArgs e){
      if(!GameManager.Instance.IsGamePlaying()) return ;
      if( selectedCounter!=null ){
          selectedCounter.Interact(this);
          //Debug.Log("Interact");
      }
    }
    
    private void Update(){
         HandleInteractions();
         HandleMovement();
         HandleLookMovement();
    }

    public bool IsWalking(){
         return isWalking;
    }
     private void HandleLookMovement(){
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player's body left/right based on mouse X movement
        playerTransform.Rotate(Vector3.up * mouseX);

        // Rotate the camera up/down based on mouse Y movement
        rotationX  -= mouseY;
        rotationX  = Mathf.Clamp(rotationX , -90f, 90f);

     //    // Apply rotation to the camera
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX , 0f, 0f);
     }
    private void HandleMovement(){
     Vector2 inputVector= Gameinputs.GetMovementVectorNormalized();
     //Vector3 moveDir = new Vector3(inputVector.x, 0f,inputVector.y);
     Vector3 moveDir = transform.right*inputVector.x + transform.forward*inputVector.y ;
         float playerRadius =.5f;
         float playerheight =1.3f;
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
     //characterController.Move(moveDir* walkSpeed*Time.deltaTime);
           
         }
     //     float rotateSpeed = 10f;
     //     transform.forward= Vector3.Slerp(transform.forward, moveDir,Time.deltaTime* rotateSpeed);

        
         

    }
    private void HandleInteractions(){
     Collider[] colliderArray = Physics.OverlapSphere(transform.position,2f);
        foreach(Collider collider in colliderArray){
          if(collider.TryGetComponent(out NpcInteractable npcInteract)){
             if(collider.TryGetComponent(out StateManager stateManager)){
                     //orderUI.gameObject.SetActive(true);
               Transform orderUITransform = npcInteract.transform.Find("OrderUi");
               GameObject orderUI = orderUITransform.gameObject;

            if (orderUITransform != null)
            {   
                float distance = Vector3.Distance(gameObject.transform.position,npcInteract.transform.position);
                if(distance<=2f && (stateManager.CurrentState is WaitState || stateManager.CurrentState is MoveState)){
                    orderUI.SetActive(true); 
                }
                else{
                    orderUI.SetActive(false); 
                }
            }
             }
              
            


          }
          
          
        }
     Vector2 inputVector= Gameinputs.GetMovementVectorNormalized();
     Vector3 moveDir = new Vector3(inputVector.x, 0f,inputVector.y);
     if( moveDir!= Vector3.zero){
          LastInteractDir=moveDir;
     }
     float Interactdistance = 2f;
     if(Physics.Raycast(playerCamera.transform.position,playerCamera.transform.forward,out RaycastHit raycasthit ,Interactdistance,countersLayerMask)){
        if(raycasthit.transform.TryGetComponent(out BaseCounter baseCounter)){
          // Has counter
          if( baseCounter!=selectedCounter){
              SetSelectedCounter(baseCounter);
          }
          else{
               SetSelectedCounter(selectedCounter);
          }
        }
        else{
          SetSelectedCounter(null);
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
        if(kitchenObject!=null){
          OnPick?.Invoke(this, EventArgs.Empty);
        }
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
