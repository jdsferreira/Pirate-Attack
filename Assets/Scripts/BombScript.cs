using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float ExplosionDelay = 5f;
    public GameObject ExplosionPrefab;

    public GameObject woodBreakingPrefab;

    public float BlastRadius = 5;
    public int blastDamage = 10;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplosionCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ExplosionCoroutine(){
        //Wait
        yield return new WaitForSeconds(ExplosionDelay);
        //Explode
        Explode();

    }

    private void Explode(){
        //Create Explosion

        Instantiate(ExplosionPrefab, transform.position, ExplosionPrefab.transform.rotation);
        
        //Destroy platforms
        Collider[] colliders = Physics.OverlapSphere(transform.position, BlastRadius);
        foreach(Collider collider in colliders){
            GameObject hitObject = collider.gameObject;

            if(hitObject.CompareTag("Platform")){
                LifeScript lifeScript = hitObject.GetComponent<LifeScript>();
                if(lifeScript != null){
                    float distance = (hitObject.transform.position - transform.position).magnitude;
                    float distanceRate = Mathf.Clamp(distance / BlastRadius, 0, 1);
                    float damageRate = (float)(1f - Math.Pow(distanceRate, 4));
                    int damage = (int) Mathf.Ceil(damageRate * blastDamage);
                    lifeScript.health -= damage;
                    if (lifeScript.health <= 0){
                        Instantiate(woodBreakingPrefab, transform.position, woodBreakingPrefab.transform.rotation);
                        Destroy(hitObject);
                    }
                }
                
            }
            
        }
   

        //destroy bomb
        Destroy(gameObject);
    }

}
