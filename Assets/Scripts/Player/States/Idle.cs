using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Idle : State
{
    private PlayerController controller;
 public Idle(PlayerController controller) : base("Idle"){
    this.controller = controller;
 }

    public override void Enter() {
        base.Enter();

        

    }

    
    public override void Exit() {
        base.Exit();

    }

    public override void FixedUpdate() {
        base.FixedUpdate();
    }

    public override void Update() {
        base.Update();
       //switch to jump 
        if(controller.hasJumpInput){
            controller.stateMachine.ChangeState(controller.jumpState);
            return;
        }
        //switch to walking
        if(!controller.movementVector.IsZero()){
            controller.stateMachine.ChangeState(controller.walkingState);
            return;
        }
   }

    public override void LaterUpdate() {
        base.LaterUpdate();
    }

    

    


}