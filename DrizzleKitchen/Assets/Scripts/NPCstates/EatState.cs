using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatState : State
{
   private float EatingTime;
   private float EatingTimeMax=40;
   public LeaveState leaveState;
   public override State RunCurrentState(){
        EatingTime+= Time.deltaTime;
        if(EatingTime>=EatingTimeMax){
            EatingTime=0;
            Debug.Log("Bubye , the food was awesome");
            return this;
        }
        return this;

   }
}
