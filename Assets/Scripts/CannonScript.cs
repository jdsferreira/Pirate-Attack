using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public List<GameObject> bombPrefabs;
    public Vector2 timeInterval = new Vector2(1, 1);
    public GameObject spawnPoint;
    public GameObject target;
    public float rangeInDegrees;
    public Vector2 force;

    public float arcDegree = 45;
    private float cooldown;

   
    //Reference origin bomb point(GameObject)
    //Reference to aim
    //Force (Vector2)
    //Range (float)    
    // Start is called before the first frame update
    void Start()
    {
        cooldown = Random.Range(timeInterval.x, timeInterval.y);
    }

    // Update is called once per frame
    void Update()
    {
        //Ignore if is Game Over
        if(GameManager.Instance.isGameOver) return;
        //update cooldown;
        cooldown -= Time.deltaTime;
        if(cooldown <= 0 ){
            cooldown = Random.Range(timeInterval.x, timeInterval.y);

            Fire();
        }
        //Timer (cooldown, interval, DeltaTime)
        
    }

    private void Fire(){
        //Get Prefab
        GameObject bombPrefab = bombPrefabs[Random.Range(0, bombPrefabs.Count)];

        //Create Bomb
        GameObject bomb = Instantiate(bombPrefab, spawnPoint.transform.position, bombPrefab.transform.rotation);
        Rigidbody bombRigidbody = bomb.GetComponent<Rigidbody>();
        Vector3 impulseVector = target.transform.position - spawnPoint.transform.position;
        impulseVector.Scale(new Vector3(1, 0, 1));
        impulseVector.Normalize();
        impulseVector += new Vector3(0, arcDegree / 45f, 0);
        impulseVector.Normalize();
        impulseVector = Quaternion.AngleAxis(Random.Range(-rangeInDegrees, rangeInDegrees), Vector3.up) * impulseVector;
        impulseVector *= Random.Range(force.x, force.y);
        bombRigidbody.AddForce(impulseVector, ForceMode.Impulse);
    }
}
