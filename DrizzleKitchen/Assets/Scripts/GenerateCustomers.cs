using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCustomers : MonoBehaviour
{
    public static GenerateCustomers Instance;
    [SerializeField]private GameObject Npc;
    [SerializeField]private GameObject SpawnPoint;
    private int NoOfCustomer=1;
    private float Timer;
    //public GameObject npc;

    private void Start(){
        Timer= 0f;
        
    }
    
    private void Update(){
        Timer+= Time.deltaTime;
        if(Timer>=6f){
            if(NoOfCustomer<=16){

            
            // if(CustomerManager.Instance.state ==CustomerManager.State.Leave ||  CustomerManager.Instance.state ==CustomerManager.State.Sit){
                Timer=0f;
                StartCoroutine(SpawnNpcAfterDelay());
                // npc = NpcPool.Instance.GetNpc();
                // //npc.SetActive(true);
                // //CustomerManager.Instance.state = CustomerManager.State.Move;
                // Debug.Log(npc);
                // if(npc!=null){
                //     npc.SetActive(true);
                //     npc.transform.position= SpawnPoint.transform.position;
                //     npc.transform.rotation= Quaternion.identity;
                //     CustomerManager.Instance.i=1;
                //     CustomerManager.Instance.state = CustomerManager.State.Move;
                // }
                // GameObject npc = Instantiate(Npc, SpawnPoint.transform.position, Quaternion.identity);
                // npc.SetActive(true);
                // CustomerManager.Instance.i=1;
                // CustomerManager.Instance.state = CustomerManager.State.Move;
            }
        }
            
            
        
    }
    private IEnumerator SpawnNpcAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); 
        GameObject npc = NpcPool.Instance.GetNpc();
                //npc.SetActive(true);
                //CustomerManager.Instance.state = CustomerManager.State.Move;
                Debug.Log(npc);
                if(npc!=null){
                    npc.SetActive(true);
                    npc.transform.position= SpawnPoint.transform.position;
                    npc.transform.rotation= Quaternion.identity;
                    //CustomerManager.Instance.i=1;
                    //CustomerManager.Instance.state = CustomerManager.State.Move;
                }
        
       
    }
    
}
