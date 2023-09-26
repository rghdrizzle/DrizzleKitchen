using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Gameinputs : MonoBehaviour
{
    public static Gameinputs Instance {get;private set;}
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    private PlayerInputs playerInputs;
    private void Awake(){
        Instance = this;
        playerInputs = new PlayerInputs();
        playerInputs.player.Enable();
        
        playerInputs.player.Interact.performed +=  Interact_performed;
        playerInputs.player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputs.player.Pause.performed += Pause_performed;
    }
    public Vector2 GetMovementVectorNormalized(){
        Vector2 inputVector = playerInputs.player.move.ReadValue<Vector2>();
         inputVector = inputVector.normalized;

         return inputVector;
    }
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        if(OnInteractAction!=null){
            OnInteractAction?.Invoke(this,EventArgs.Empty);
            //Debug.Log("Event OnIntereactAction called");
        }
        

    }
    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnInteractAlternateAction?.Invoke(this,EventArgs.Empty);
         //Debug.Log("Event OnIntereactActionAlternate called");
    }
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnPauseAction?.Invoke(this,EventArgs.Empty);
    }
}
