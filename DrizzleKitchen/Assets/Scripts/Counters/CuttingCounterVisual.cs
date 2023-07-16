using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT="Cut";
    [SerializeField]private CuttingCounter cuttingCounter;
    private Animator animator;

    private void Awake(){
        animator = GetComponent<Animator>();
    }

    private void Start(){
        cuttingCounter.OnCutting += cuttingCounter_OnCutting;

    }
    private void cuttingCounter_OnCutting(object sender, System.EventArgs e){
        animator.SetTrigger(CUT);
    }
}
