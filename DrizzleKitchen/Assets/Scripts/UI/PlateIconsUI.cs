using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
   [SerializeField]private PlateKitchenObject plateKitchenObject;
   [SerializeField]private Transform IconTemplate;
   
   private void Awake(){
    IconTemplate.gameObject.SetActive(false);
   }
   private void Start() {
    plateKitchenObject.OnIngredientAdded += plateKitchenObject_OnIngredientAdded;   
   }
   private void plateKitchenObject_OnIngredientAdded(object sender,PlateKitchenObject.OnIngredientAddedEventArgs e){
        UpdateVisual();
   }
   private void UpdateVisual(){
    foreach(Transform child in transform){
        if(child==IconTemplate) continue;
        Destroy(child.gameObject);
    }
    foreach(KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectList()){
        Transform iconTransform = Instantiate(IconTemplate,transform);
        iconTransform.gameObject.SetActive(true);
        iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
    }
   }
}
