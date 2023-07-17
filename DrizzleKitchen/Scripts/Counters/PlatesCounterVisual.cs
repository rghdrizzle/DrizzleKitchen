using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField]private Transform counterToppoint;
    [SerializeField]private Transform PlatePrefab;
    [SerializeField]private PlateCounter plateCounter;

    private List<GameObject> plateVisualGameObjectList;
    private void Awake(){
        plateVisualGameObjectList = new List<GameObject>();
    }
    private void Start(){
        plateCounter.OnplateSpawned += plateCounter_OnplateSpawned;
        plateCounter.OnplateRemoved += plateCounter_OnplateRemoved;
    }
    private void plateCounter_OnplateSpawned(object sender, System.EventArgs e) {
        Transform plateVisualTransform = Instantiate(PlatePrefab,counterToppoint);
        float plateOffsetY= .1f;
        plateVisualTransform.localPosition = new Vector3(0,plateOffsetY*plateVisualGameObjectList.Count,0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
    private void plateCounter_OnplateRemoved(object sender, System.EventArgs e){
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count-1];  
        plateVisualGameObjectList.Remove(plateGameObject); 
        Destroy(plateGameObject);
    }
    
}
