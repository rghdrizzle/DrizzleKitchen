using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateCompleteVisual : MonoBehaviour
{
 [Serializable]
 public struct KitchenObjectSO_GameObject{
    public KitchenObjectSO kitchenObjectSO;
    public GameObject gameObject;
 }
 [SerializeField]private PlateKitchenObject plateKitchenObject;
 [SerializeField]private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

 private void Start(){
    plateKitchenObject.OnIngredientAdded += plateKitchenObject_OnIngredient;
    foreach(KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList){

            kitchenObjectSOGameObject.gameObject.SetActive(false);
    }
 }
 private void plateKitchenObject_OnIngredient(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e){
    foreach(KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList){
        if(kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO){
            kitchenObjectSOGameObject.gameObject.SetActive(true);
        }
    }
    //e.kitchenObjectSO
 }
}
