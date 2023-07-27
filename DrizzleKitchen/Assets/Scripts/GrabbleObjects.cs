using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbleObjects : MonoBehaviour
{
    private Transform ObjectPoint;
    private Rigidbody rigidBody;

    private void Awake(){
        rigidBody = GetComponent<Rigidbody>();
    } 
    public void Grab(){
        //this.ObjectPoint=ObjectPoint;
        rigidBody.useGravity= false;
    }
    // private void FixedUpdate()
    // {
    //     if(ObjectPoint!=null){
    //         rigidBody.MovePosition(ObjectPoint.position);
    //     }
        
    // }
}
