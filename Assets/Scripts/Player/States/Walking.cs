using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : State {

    private PlayerController controller;

    public Walking(PlayerController controller) : base("Walking"){
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

        Vector3 controllerVector = controller.movementVector; //x(left - right +) y(z in global up + down -)
        Vector3 walkVector = new Vector3(controllerVector.x, 0, controllerVector.y);
        
        walkVector = controller.GetForward() * walkVector;
        walkVector *= controller.movementSpeed;

        //Apply input to character
       // controller.transform.Translate(walkVector);
        controller.thisRigidbody.AddForce(walkVector, ForceMode.Force);
        
        //Rotate character
        controller.RotateBodytoFaceInput();
    }

    public override void Update() {
        base.Update();
        //switch to jump
        if(controller.hasJumpInput){
            controller.stateMachine.ChangeState(controller.jumpState);
            return;
        }
        //switch to idle
        if(controller.movementVector.IsZero()){
            controller.stateMachine.ChangeState(controller.idleState);
            return;
        }



    }

    public override void LaterUpdate() {
        base.LaterUpdate();
    }


}