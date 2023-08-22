using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceBarrel : BaseCounter
{
    [SerializeField] private GameObject Juice;
    [SerializeField] private GameObject playerPoint;
    private float Timer=0f;
    private float TimeMax=10f;
    public override void Interact(Player player){
        if(!HasKitchenObject()){
            if(player.HasKitchenObject()){
                player.GetKitchenObject().SetkitchenObjectParent(this);
                Juice.SetActive(true);
            }
            
        }
        else if(this.HasKitchenObject()){
            if(Timer>TimeMax){
                GetKitchenObject().SetkitchenObjectParent(player);
                
                Timer=0f;
            }
    

                //player not carrying anything
            }



        Debug.Log("JUICE BE GOIN BRRRR!!!!");
    }
    public void Update(){
        Timer += Time.deltaTime;
        if(Timer>=TimeMax){
            Juice.SetActive(false);
            Transform point = this.transform.Find("CounterToppoint");
            GameObject Counterpoint = point.gameObject;
            Transform mugTransform = Counterpoint.transform.Find("mug(Clone)");
            GameObject mug = mugTransform.gameObject;
   

            Transform juiceTransform = mug.transform.Find("Juice");
            GameObject juiceVisual = juiceTransform.gameObject;
            juiceVisual.SetActive(true);        
        }
    }
    
}
