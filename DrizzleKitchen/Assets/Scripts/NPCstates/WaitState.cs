using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : State
{
   private float WaitingTime;
   private float WaitingTimeMax = 10f;

   public LeaveState leaveState;
   public override State RunCurrentState(){
        WaitingTime+=Time.deltaTime;
        if(WaitingTime >=WaitingTimeMax){
            WaitingTime=0f;
            return leaveState;
        }
        else{
            return this;
        }
   }
}
