using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Gameinputs : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    private PlayerInputs playerInputs;
    private void Awake(){
        playerInputs = new PlayerInputs();
        playerInputs.player.Enable();

        playerInputs.player.Interact.performed +=  Interact_performed;
        playerInputs.player.InteractAlternate.performed += InteractAlternate_performed;
    }
    public Vector2 GetMovementVectorNormalized(){
        Vector2 inputVector = playerInputs.player.move.ReadValue<Vector2>();
         inputVector = inputVector.normalized;

         return inputVector;
    }
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        if(OnInteractAction!=null){
            OnInteractAction(this,EventArgs.Empty);
        }
        

    }
    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnInteractAlternateAction?.Invoke(this,EventArgs.Empty);
    }
}
