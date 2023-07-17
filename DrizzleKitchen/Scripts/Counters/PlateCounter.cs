using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateCounter :BaseCounter
{
    public event EventHandler OnplateSpawned;
    public event EventHandler OnplateRemoved;
    [SerializeField]private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax=4f;
    private int plateSpawnedAmount;
    private int platesSpawnedAmountMax=4;
    private void Update(){
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer>spawnPlateTimerMax){
            spawnPlateTimer =0f;
            if(plateSpawnedAmount< platesSpawnedAmountMax){
                plateSpawnedAmount++;

                OnplateSpawned?.Invoke(this, EventArgs.Empty);
            }
            //KitchenObject.SpawnKitchenObject(plateKitchenObjectSO,this);
        }
    }
    public override void Interact(Player player){
        Debug.Log("Interact");
        if(!player.HasKitchenObject()){
            //player is empty handed
            if(plateSpawnedAmount>0){
                //there is atleast one plate available
                plateSpawnedAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO,player);
                OnplateRemoved?.Invoke(this, EventArgs.Empty); 
            }
        }
    }

}
