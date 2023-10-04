using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseCounter: MonoBehaviour , IkitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlaced;
     public static void ResetStaticData(){
        OnAnyObjectPlaced = null;
    }
    [SerializeField]private Transform counterToppoint;
    
    private KitchenObject kitchenObject;
    public virtual void Interact(Player player){
        Debug.Log("BaseCounter.Interact()");
    }
    public virtual void InteractAlternate(Player player){
         //Debug.Log("BaseCounter.InteractAlternate()");
    }
    public Transform GetKitchenObjectfollowTransform(){
        return counterToppoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject =kitchenObject;
        if(kitchenObject!=null){
            OnAnyObjectPlaced?.Invoke(this,EventArgs.Empty);
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
