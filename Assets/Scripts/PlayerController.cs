using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Follow))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Follow pathFollow;
    public GameObject carToSpawn;
    public FixedJoint fixedJoint;
    public float maxSpeed;
    public float minSpeed;
    public float delayToStart;
    public int lapTracker = 0;

    private float timer;
    private bool carAttached = true;

    //powerups
    private bool powerup = false;
    private float powerupTimer = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        pathFollow = GetComponent<Follow>();

        //winPanel.SetActive(false);

        //GameObject spawnedObject = Instantiate(carToSpawn) as GameObject;
        //fixedJoint = spawnedObject.GetComponent<FixedJoint>();
        //if (fixedJoint != null)
        //{
        //    fixedJoint.connectedBody = GetComponent<Rigidbody>();
        //}
        //spawnedObject.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //put something here with a var if stop movement on win

        if(timer <= delayToStart)
        {
            timer += Time.deltaTime;
            return;
        }
        if(powerupTimer <= 0 && powerup)
        {
            powerup = false;
            //GetComponent<BoxCollider>().enabled = true;
            fixedJoint.breakForce = 1000;//var
            fixedJoint.breakTorque = 650;//var
            //stop changing colors
            carToSpawn.GetComponent<MeshRenderer>().materials[0].color = Color.grey;
        }
        else if(powerupTimer > 0 && powerup)
        {
            powerupTimer -= Time.deltaTime;
        }

        if(fixedJoint == null)
        {
            //pathFollow.speed = 0;
            if (Input.GetKeyDown(KeyCode.R))
            {
                //reset car
                ResetCar();
                Debug.Log("Resetting");
                carAttached = true;
            }
            if (Input.GetKeyDown(KeyCode.Space) && carAttached)
            {
                //set joint
                MakeNewJoint();
                Debug.Log("Joint set; continue driving");
            }
            return;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            pathFollow.IncreaseSpeed(.1f, minSpeed, maxSpeed);
        }
        else
        {
            pathFollow.DecreaseSpeed(.5f, minSpeed, maxSpeed);
        }
    }

    void ResetCar()
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
    }

    void MakeNewJoint()
    {
        fixedJoint = gameObject.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = carToSpawn.GetComponent<Rigidbody>();
        fixedJoint.breakForce = 1000;//var
        fixedJoint.breakTorque = 650;//var
        fixedJoint.enablePreprocessing = false;
        carToSpawn.GetComponent<CarFlying>().fixedJoint = fixedJoint;//for now it doesnt do anything with this var
    }

    private void OnJointBreak(float breakForce)
    {
        Debug.Log("Car falls off");
        carAttached = false;
        pathFollow.speed = 0;
        carToSpawn.GetComponent<Rigidbody>().useGravity = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit something");
        if(other.tag == "Powerup")
        {
            //disable powerup
            other.gameObject.SetActive(false);
            //set timer
            powerup = true;
            powerupTimer = 4f;
            //disable collision
            GetComponent<BoxCollider>().enabled = false;
            //force and torque break to inf
            fixedJoint.breakForce = Mathf.Infinity;
            fixedJoint.breakTorque = Mathf.Infinity;
            //change colors of car
            carToSpawn.GetComponent<MeshRenderer>().materials[0].color = Color.blue;
            //starPowerColorChange();
        }
    }

}
