using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Buyoancy : MonoBehaviour{

    public float underwaterDrag = 3f;
    public float underwaterAngularDrag = 1f;
    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;
   // public GameObject oceanReference;
    public float buyoancyForce = 10;
    private Rigidbody thisRigidbody;
    private bool hasTouchedWater;
    void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        
    }
    
    void FixedUpdate()
    {
        //Check if underwater
        float diffY = transform.position.y;
        bool isUnderwater = diffY < 0;
        
        //ignore if never touched water
        if(isUnderwater){
            hasTouchedWater = true;
        }

        if(!hasTouchedWater) return;

        if(isUnderwater){
            Vector3 vector = Vector3.up * buyoancyForce * -diffY; 
            thisRigidbody.AddForce(vector, ForceMode.Acceleration);
        }

        thisRigidbody.drag = isUnderwater ? underwaterDrag : airDrag;
        thisRigidbody.angularDrag = isUnderwater ? underwaterAngularDrag : airAngularDrag;

    }

}