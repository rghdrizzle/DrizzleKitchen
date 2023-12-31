using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
   [SerializeField]private StoveCounter stoveCounter;
   [SerializeField]private GameObject stoveOnGameObject;
   [SerializeField]private GameObject particleGameObject;

   private void Start(){
    stoveCounter.OnStateChanged += stoveCounter_OnStateChanged;

   }
   private void stoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e){
    bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
    stoveOnGameObject.SetActive(showVisual);
    particleGameObject.SetActive(showVisual);
   }

}
