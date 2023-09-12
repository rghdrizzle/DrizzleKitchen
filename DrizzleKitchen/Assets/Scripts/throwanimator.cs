using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwanimator : MonoBehaviour
{
    private const string THROW="isThrowing";
    [SerializeField]private Throwobject arms;
    private Animator animator;

    private void Awake(){
        animator = GetComponent<Animator>();
    }

    private void Start(){
        arms.OnThrowing += throwobject_OnThrowing;

    }
    private void throwobject_OnThrowing(object sender, System.EventArgs e){
        animator.SetTrigger(THROW);
    }
}
