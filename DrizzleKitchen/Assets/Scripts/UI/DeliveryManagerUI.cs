using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField]private Transform container;
    [SerializeField]private Transform recipeTemplate;

    private void Start(){
        DeliveryManager.Instance.OnRecipeSpawned+= Delivery_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted +=Delivery_OnRecipeCompleted;
        UpdateVisual();
    }   

    private void Awake(){
        recipeTemplate.gameObject.SetActive(false);

    }
    private void Delivery_OnRecipeSpawned(object sender,System.EventArgs e){
        UpdateVisual();
    }
    private void Delivery_OnRecipeCompleted(object sender,System.EventArgs  e){
        UpdateVisual();
    }
    private void UpdateVisual(){
        foreach(Transform child in container){
            if(child ==recipeTemplate)continue;
            Destroy(child.gameObject);
        }
        foreach(DeliveryRecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList()){
            Transform recipeTransform = Instantiate(recipeTemplate,container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}
