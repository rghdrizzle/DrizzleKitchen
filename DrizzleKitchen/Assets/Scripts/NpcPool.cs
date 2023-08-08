using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPool : MonoBehaviour
{
   public static NpcPool Instance;

   [SerializeField]private GameObject Npc;
   private int NpcPoolSize=10;

   private List<GameObject> NpcPoolList = new List<GameObject>();

   private void Awake(){
    if(Instance == null){
        Instance = this;
    }
   }
   private void Start(){
    for(int i=0;i<NpcPoolSize;i++){
        GameObject npc =Instantiate(Npc);
        //CustomerManager.Instance.state = CustomerManager.State.Idle;
        npc.SetActive(false);
        NpcPoolList.Add(npc);

    }
   }
   public GameObject GetNpc(){
        for(int i=0;i<NpcPoolList.Count;i++){
            if(!NpcPoolList[i].activeInHierarchy){
                return NpcPoolList[i];
            }
           
        }
         return null;
   }


}
