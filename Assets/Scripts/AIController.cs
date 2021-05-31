using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Follow))]
public class AIController : MonoBehaviour
{
    [HideInInspector]
    public Follow pathFollow;
    public GameObject carToSpawn;
    [SerializeField]
    private float breakForce = 800;
    [SerializeField]
    private float breakTorque = 600;
    private float timer;
    public float delayToStart;
    float respawnTimer = 0f;
    private bool carAttached = true;
    public FixedJoint fixedJoint;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(180, 0, 0);
        pathFollow = GetComponent<Follow>();
    }

    // Update is called once per frame
    void Update()
    {
        //start timer
        if (timer <= delayToStart)
        {
            timer += Time.deltaTime;
            return;
        }
        //make sure car is fixed in 
        if(carAttached && !fixedJoint)
        {
            fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = carToSpawn.GetComponent<Rigidbody>();
            fixedJoint.breakForce = breakForce;//var
            fixedJoint.breakTorque = breakTorque;//var
            fixedJoint.enablePreprocessing = false;
            carToSpawn.GetComponent<CarFlying>().fixedJoint = fixedJoint;//for now it doesnt do anything with this var
            carToSpawn.layer = 0;
        }
        //go
        if (carAttached)
        {
            pathFollow.IncreaseSpeed(.001f, 30, 60);
        }
        //respawn after getting hit
        if (respawnTimer <= 0 && !carAttached)
        {
            //stop car from moving
            carToSpawn.GetComponent<Rigidbody>().useGravity = false;
            carToSpawn.GetComponent<Rigidbody>().velocity = Vector3.zero;
            carToSpawn.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            //move car to pos
            carToSpawn.transform.position = transform.position;
            //fix rotation
            carToSpawn.transform.rotation = transform.rotation;
            carToSpawn.transform.Rotate(0, 180, 0); 
            timer = 0.5f;
            carAttached = true;
        }
        else if (respawnTimer > 0 && !carAttached)
        {
            respawnTimer -= Time.deltaTime;
        }
    }
        private void OnJointBreak(float breakForce)
    {
        Debug.Log("Car falls off with force of : " + breakForce);
        carAttached = false;
        pathFollow.speed = 0;
        carToSpawn.GetComponent<Rigidbody>().useGravity = true;
        respawnTimer = 1.5f;
        carToSpawn.layer = 6;
        Vector3 tempV = carToSpawn.GetComponent<Rigidbody>().velocity;
        carToSpawn.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(tempV.x * tempV.x / .02f, tempV.y * tempV.y / .02f, tempV.z * tempV.z / .02f));
    }
}
