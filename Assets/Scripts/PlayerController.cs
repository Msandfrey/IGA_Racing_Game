using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Follow))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Follow pathFollow;
    public GameObject carToSpawn;
    public FixedJoint fixedJoint;
    public GameObject GameStartUI;
    public TrailRenderer trail;
    [SerializeField]
    private float breakForce = 800;
    [SerializeField]
    private float breakTorque = 600;
    public float maxSpeed;
    public float minSpeed;
    public float delayToStart;
    float respawnTimer = 0f;

    private float timer;
    private bool carAttached = true;

    //powerups
    private bool powerup = false;
    private float powerupTimer = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        pathFollow = GetComponent<Follow>();
        trail = GetComponentInChildren<TrailRenderer>();
    }

    private void Start()
    {
        GameStartUI.SetActive(true);
        GameStartUI.GetComponentInChildren<TextMeshProUGUI>().text = "Ready...";
    }

    // Update is called once per frame
    void Update()
    {
        //put something here with a var if stop movement on win
        if(timer >= delayToStart - .2)
        {
            GameStartUI.GetComponentInChildren<TextMeshProUGUI>().text = "GO!!!!!";
        }
        if(timer <= delayToStart)
        {
            timer += Time.deltaTime;
            return;
        }
        if (GameStartUI.activeSelf)
        {
            GameStartUI.SetActive(false);
        }
        if(powerupTimer <= 0 && powerup)
        {
            powerup = false;
            //GetComponent<BoxCollider>().enabled = true;
            fixedJoint.breakForce = breakForce;//var
            fixedJoint.breakTorque = breakTorque;//var
            //stop changing colors
            carToSpawn.GetComponent<MeshRenderer>().materials[0].color = Color.grey;
        }
        else if(powerupTimer > 0 && powerup)
        {
            powerupTimer -= Time.deltaTime;
        }

        if(fixedJoint == null)
        {
            if (respawnTimer <= 0 && !carAttached)
            {
                ResetCar();
                carAttached = true;
            }
            else if (respawnTimer > 0)
            {
                respawnTimer -= Time.deltaTime;
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
            if(pathFollow.speed == maxSpeed)
            {
                //max width
                trail.startWidth = 1f;
            }
            else if(pathFollow.speed >= maxSpeed * .75f)
            {
                //less wisth
                trail.startWidth = .75f;
            }
            else if (pathFollow.speed >= maxSpeed * .5f)
            {
                //half width
                trail.startWidth = .5f;
            }
            else if (pathFollow.speed >= maxSpeed * .25f)
            {
                //little width
                trail.startWidth = .25f;
            }
            else
            {
                trail.startWidth = 0;
            }
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
        fixedJoint.breakForce = breakForce;//var
        fixedJoint.breakTorque = breakTorque;//var
        fixedJoint.enablePreprocessing = false;
        carToSpawn.GetComponent<CarFlying>().fixedJoint = fixedJoint;//for now it doesnt do anything with this var
        carToSpawn.layer = 0;
    }

    private void OnJointBreak(float breakForce)
    {
        Debug.Log(gameObject.name +" falls off with force of : " + breakForce);
        carAttached = false;
        pathFollow.speed = 0;
        carToSpawn.GetComponent<Rigidbody>().useGravity = true;
        respawnTimer = 1.5f;
        carToSpawn.layer = 6;//fallen layer
        //Vector3 tempV = carToSpawn.GetComponent<Rigidbody>().velocity;
        //carToSpawn.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(tempV.x * tempV.x, tempV.y * tempV.y, tempV.z * tempV.z));
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(gameObject.name + " hit something: " + other);
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

    private void OnCollisionEnter(Collision collision)
    {
        //carToSpawn.GetComponent<Rigidbody>().AddRelativeForce(collision.impulse);
    }
}
