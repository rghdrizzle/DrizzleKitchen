using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceBarrel : BaseCounter
{
    [SerializeField] private GameObject Juice;
    [SerializeField] private GameObject playerPoint;
    private float Timer;
    private float TimeMax=10f;
    public override void Interact(Player player){
        if(!HasKitchenObject()){
            if(player.HasKitchenObject()){
                player.GetKitchenObject().SetkitchenObjectParent(this);
                Juice.SetActive(true);
            }
            else if(HasKitchenObject()){
                
                    GetKitchenObject().SetkitchenObjectParent(player);
                    Timer=0f;
                //player not carrying anything
            }
        }



        Debug.Log("JUICE BE GOIN BRRRR!!!!");
    }
    public void Update(){
        Timer += Time.deltaTime;
        if(Timer>=TimeMax){
            Juice.SetActive(false);
            Timer=0f;
            
        }
    }
    
}
