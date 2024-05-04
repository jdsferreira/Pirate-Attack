using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jump : State
{
    private PlayerController controller;

    private bool hasJumped;
    private float cooldown;
 public Jump(PlayerController controller) : base("Jump"){
    this.controller = controller;
 }

    public override void Enter() {
        base.Enter();

        //reset variables
        hasJumped = false;
        cooldown = 0.5f;

        //Control animator
        controller.thisAnimator.SetBool("bJumping", true);
        

    }

    
    public override void Exit() {
        base.Exit();
        controller.thisAnimator.SetBool("bJumping", false);

    }

    public override void FixedUpdate() {
        base.FixedUpdate();

        Vector3 controllerVector = controller.movementVector; //x(left - right +) y(z in global up + down -)
        Vector3 walkVector = new Vector3(controllerVector.x, 0, controllerVector.y);
        
        walkVector = controller.GetForward() * walkVector;
        walkVector *= controller.movementSpeed * controller.jumpMovimentVector;

        //Apply input to character
       // controller.transform.Translate(walkVector);
        controller.thisRigidbody.AddForce(walkVector, ForceMode.Force);
        
        //Rotate character
        controller.RotateBodytoFaceInput();
    }

    public override void Update() {
        base.Update();

        cooldown -= Time.deltaTime;

        //switch to idle
        if(hasJumped && controller.isGrounded && cooldown <=0){
           Debug.Log("Entered Idle");
            controller.stateMachine.ChangeState(controller.idleState);
            return;
        }

   }

    public override void LaterUpdate() {
        base.LaterUpdate();

        if(!hasJumped){
            hasJumped = true;
            ApplyImpulse();
        }

    }

    private void ApplyImpulse(){
        Vector3 forceVector = Vector3.up * controller.jumpPower;
        controller.thisRigidbody.AddForce(forceVector, ForceMode.Impulse);
    }

    

    


}