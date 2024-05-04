using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine {
    
    //CurrentState
    private State currentState;
    public string currentStateName {get; private set;}
    //Update, LaterUpdate, FixedUpdate
    public void Update(){
        currentState?.Update();
    }
    public void LaterUpdate(){
        currentState?.LaterUpdate();
    }
    public void FixedUpdate(){
        currentState?.FixedUpdate();
    }
    //ChangeState
    public void ChangeState(State newState){
        currentState?.Exit(); //shortcut for if (currentState != null){currentState.Exit();}
        currentState = newState;
        currentStateName = newState.name;
        newState?.Enter(); //shortcut for if (newState != null){newState.Enter();}

    }
}
