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
    public float speed;
    public bool carAttached = true;
    public FixedJoint fixedJoint;
    public bool move = true;
    public float acceleration = 20;
    public float decceleration = 20;

    private GameObject powerupToSpawn;
    private bool hasPowerup = false;
    private bool powerActive = false;
    private float powerupTimer;
    PowerupClass powerup;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(180, 0, 0);
        pathFollow = GetComponent<Follow>();
        powerup = new PowerupClass();
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
        if (carAttached && !fixedJoint)
        {
            fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = carToSpawn.GetComponent<Rigidbody>();
            fixedJoint.breakTorque = breakTorque;//var
            fixedJoint.breakForce = breakForce;//var
            fixedJoint.enablePreprocessing = false;
            carToSpawn.GetComponent<CarFlying>().fixedJoint = fixedJoint;//for now it doesnt do anything with this var
            carToSpawn.layer = 0;
        }
        if (hasPowerup)
        {
            powerupTimer = powerup.timer;
            hasPowerup = false;
            powerActive = true;
            powerup.UseEffect(carToSpawn);
            powerup.power = PowerupClass.PowerType.None;
        }
        if (powerupTimer <= 0 && powerActive)
        {
            powerup.StopEffect(carToSpawn);
            powerActive = false;
        }
        else if (powerupTimer > 0 && powerActive)
        {
            powerupTimer -= Time.deltaTime;
        }
        //go
        if (carAttached && move)
        {
            pathFollow.IncreaseSpeed(acceleration * Time.deltaTime, 0, speed);
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
    public bool IsPowerActive()//todo fix this no work
    {
        return powerActive;
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
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag.Equals("EnemyCar") || other.tag.Equals("PlayerCar")) && fixedJoint && other.gameObject != carToSpawn)
        {
            fixedJoint.breakForce = Mathf.Infinity;
            fixedJoint.breakTorque = Mathf.Infinity;
        }
        else if (other.tag.Equals("Powerup"))
        {
            //disable the powerup
            other.gameObject.GetComponent<PowerupPickup>().PickedUp();
            //set powerup vals
            powerup = other.gameObject.GetComponent<PowerupPickup>().ChoosePowerup();
            powerupToSpawn = powerup.prefabToSpawn;
            hasPowerup = true;

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if ((other.tag.Equals("EnemyCar") || other.tag.Equals("PlayerCar")) && fixedJoint && other.gameObject != carToSpawn)
        {
            fixedJoint.breakForce = Mathf.Infinity;
            fixedJoint.breakTorque = Mathf.Infinity;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((other.tag.Equals("EnemyCar") || other.tag.Equals("PlayerCar")) && fixedJoint && other.gameObject != carToSpawn)
        {
            fixedJoint.breakTorque = breakTorque;
            fixedJoint.breakForce = breakForce;
        }

    }
}
