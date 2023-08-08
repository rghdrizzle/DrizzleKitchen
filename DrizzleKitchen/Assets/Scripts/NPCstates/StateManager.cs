using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    [SerializeField] private State CurrentState;
    [SerializeField] private MoveState move;

    private void Update()
    {
        RunStateMachine();
    }
    private void RunStateMachine(){
        State nextState = CurrentState?.RunCurrentState();
        if(nextState != null){
            SwitchState(nextState);
        }
    }
    private void SwitchState(State nextstate){
        CurrentState = nextstate;

    } 
}
