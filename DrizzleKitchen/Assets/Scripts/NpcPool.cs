using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPool : MonoBehaviour
{
   public static NpcPool Instance;
   [SerializeField]private GameObject Npc;
   [SerializeField]private int poolSize;
   [SerializeField]private GameObject spawnPoint;

   private Queue<GameObject> npcPool= new Queue<GameObject>();

    private void Awake(){
        if(Instance==null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
        for(int i=0; i<poolSize;i++){
            GameObject npc = Instantiate(Npc,spawnPoint.transform.position,Quaternion.identity);
            npc.SetActive(false);
            npcPool.Enqueue(npc);
        }
    }
    public GameObject GetNpc()
    {
        if (npcPool.Count == 0)
            return null;

        GameObject npc = npcPool.Dequeue();
        npc.SetActive(true);
        return npc;
    }

    public void ReturnNpcToPool(GameObject npc)
    {
        npc.SetActive(false);
        npcPool.Enqueue(npc);
    }

}
