using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCustomers : MonoBehaviour
{
    [SerializeField]private GameObject Npc;
    [SerializeField]private GameObject SpawnPoint;
    private float Timer;

    private void Start(){
        Timer= 0f;
    }
    private void Update(){
        Timer += Time.deltaTime;
        if(Timer>5f){
            Timer=0f;
            if(CustomerManager.Instance.state ==CustomerManager.State.Leave ||  CustomerManager.Instance.state ==CustomerManager.State.Sit){
                //GameObject npc = NpcPool.Instance.GetNpc();
                Instantiate(Npc, SpawnPoint.transform.position, Quaternion.identity);
                CustomerManager.Instance.i=1;
                CustomerManager.Instance.state = CustomerManager.State.Move;
            }
            //Instantiate(Npc, SpawnPoint.transform.position, Quaternion.identity);
        }
    }
    
}
