using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupobject : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private Player player;
    float Interactdistance = 2f;

   private void Update(){
    if(Input.GetKeyDown(KeyCode.T)){
        if(player.HasKitchenObject()){
            player.GetKitchenObject().TryGetComponent(out Rigidbody objectRigid);
            player.GetKitchenObject().transform.parent=null;
            objectRigid.useGravity= true;
            objectRigid.velocity = playerCamera.transform.forward * 1000f * Time.deltaTime;

        }  
        else{
            Debug.Log("No mug in hand");
        }
        }
   }
}
