using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnStateChanged;
    public static GameManager Instance {get;private set;}
    private enum State{
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float waiting =1f;
    private float countdown = 3f;
    private float gamePlayingTimer =10f;
    private void Awake(){
        Instance = this;
        state = State.WaitingToStart;
    }
    private void Update(){
        switch(state){
            case State.WaitingToStart:
                waiting -= Time.deltaTime;
                if( waiting <0f){
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this,EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                countdown -= Time.deltaTime;
                if(countdown<0f){
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this,EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if(gamePlayingTimer<0f){
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this,EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
    }      
    Debug.Log(state);

}
    public bool IsGamePlaying(){
                return state== State.GamePlaying;
            }
    public bool IsCountDownToStartActive(){
        return state==State.CountdownToStart;
    }
    public float GetCountdownTimer(){
        return countdown;
    }
    public bool IsGameOver(){
        return state==State.GameOver;
    }
}