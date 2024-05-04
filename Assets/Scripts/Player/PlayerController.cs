using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Public properties
    public float movementSpeed = 10;
    public float jumpPower = 10;

    public float jumpMovimentVector = 1;
   
    //State MAchine
    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Walking walkingState; 
    [HideInInspector] public Jump jumpState;

    [HideInInspector] public Dead deadState;

    //Internal properties
    [HideInInspector] public Vector2 movementVector;
    [HideInInspector] public bool hasJumpInput;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public Rigidbody thisRigidbody;
    [HideInInspector] public Collider thisCollider;
    [HideInInspector] public Animator thisAnimator;


    void Awake(){
        thisRigidbody = GetComponent<Rigidbody>();
        thisAnimator = GetComponent<Animator>();
        thisCollider = GetComponent<Collider>();
    }


    void Start()
    {
        stateMachine = new StateMachine();
        idleState = new Idle(this);
        walkingState = new Walking(this);
        jumpState = new Jump(this);
        deadState = new Dead(this);
        stateMachine.ChangeState(idleState);

    }


    // Update is called once per frame
    void Update()
    {
        //Check Game Over
        if(GameManager.Instance.isGameOver){
            if(stateMachine.currentStateName != deadState.name){
                stateMachine.ChangeState(deadState);
            
            }
        }        

         //Read Input (Controls)
        bool isUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool isDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool isLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool isRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        float inputX = isRight ? 1 : isLeft ? -1 : 0;
        float inputZ = isUp ? 1 : isDown ? -1 : 0;

        movementVector = new Vector2(inputX, inputZ);
        
        hasJumpInput = Input.GetKey(KeyCode.Space);

        //pass velocity from 0 to 1 for Animator
        float velocity = thisRigidbody.velocity.magnitude;
        float velocityRate = velocity / movementSpeed; 
        thisAnimator.SetFloat("fVelocity", velocityRate);
        

        //Detect ground
        DetectGround();

        stateMachine.Update();
       
    }

    void LateUpdate(){
        stateMachine.LaterUpdate();
    }

    void FixedUpdate(){
        stateMachine.FixedUpdate();
    }

    public Quaternion GetForward(){
        Camera camera = Camera.main;
        return Quaternion.Euler(0,camera.transform.eulerAngles.y,0);
    }

    public void RotateBodytoFaceInput(){
        if(movementVector.IsZero()){
            return;
        }
        //Calculate rotation
        Camera camera = Camera.main;
        Vector3 inputVector = new Vector3(movementVector.x, 0, movementVector.y);
        Quaternion q1 = Quaternion.LookRotation(inputVector, Vector3.up);
        Quaternion q2 = Quaternion.Euler(0,camera.transform.eulerAngles.y,0);
        Quaternion toRotation = q1 * q2;
        Quaternion newRotation = Quaternion.LerpUnclamped(transform.rotation, toRotation, 0.15f);

        //Apply rotation
        thisRigidbody.MoveRotation(newRotation);
    }

    private void DetectGround(){
        //Reset flag
        isGrounded = false;
        
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;
        Bounds bounds = thisCollider.bounds;
        float radius = bounds.size.x * 0.33f;
        float maxDistance = bounds.size.y * 0.33f;
        if (Physics.SphereCast(origin, radius, direction, out var hitInfo, maxDistance)){
            GameObject hitObject = hitInfo.transform.gameObject;
            if(hitObject.CompareTag("Platform")){
                isGrounded = true;
                Debug.Log("Is grounded");
            }
        }
    }

    // void OnDrawGizmos(){
    //     Vector3 origin = transform.position;
    //     Vector3 direction = Vector3.down;
    //     Bounds bounds = thisCollider.bounds;
    //     float radius = bounds.size.x * 0.33f;
    //     float maxDistance = bounds.size.y * 0.33f;;
    //     Gizmos.color = isGrounded? Color.green : Color.red;
    //     Vector3 spherePosition = direction * maxDistance + origin;

    //     Gizmos.DrawSphere(spherePosition, radius);

    // }
    
    // void OnGUI(){
    //     Rect rect = new Rect(10, 10, 100, 50);
    //     string text = stateMachine.currentStateName;
    //     GUIStyle style = new GUIStyle();
    //     style.fontSize = (int)(50f * (Screen.width / 1920f));
    //     GUI.Label(rect, text, style);
    // }


}
