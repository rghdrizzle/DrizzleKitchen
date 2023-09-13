using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Throwobject : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private Player player;

    private float time=0;
    public event EventHandler OnThrowing;

   private void Update(){
;
    if(Input.GetKeyDown(KeyCode.T)){
        if(player.HasKitchenObject()){
 
            OnThrowing?.Invoke(this,EventArgs.Empty);
            player.GetKitchenObject().GetComponent<Rigidbody>().useGravity=true;
            player.GetKitchenObject().transform.parent=null;

            player.GetKitchenObject().GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward*350);
            player.SetKitchenObject(null);


        }  
        else{
            Debug.Log("No mug in hand");
        }
        }
   }
   
}
