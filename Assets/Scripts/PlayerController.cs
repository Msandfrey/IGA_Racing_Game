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
    public Camera overheadCam;
    public Camera thirdPersonCam;
    public GameObject lapUI;
    [SerializeField]
    private float breakForce = 800;
    [SerializeField]
    private float breakTorque = 600;
    [SerializeField]
    private float acceleration = 0.05f;
    [SerializeField]
    private float decceleration = 0.5f;
    public float maxSpeed;
    public float minSpeed;
    public float delayToStart;
    float respawnTimer = 0f;

    private GameObject powerupToSpawn;

    PowerupClass powerup;
    private float timer;
    private bool carAttached = true;
    private bool accelBool = false;

    //powerups
    private bool hasPowerup = false;
    private float powerupTimer = 0f;
    private bool powerActive = false;

    // Start is called before the first frame update
    void Awake()
    {
        pathFollow = GetComponent<Follow>();
        trail = GetComponentInChildren<TrailRenderer>();
        powerup = new PowerupClass();

    }

    private void Start()
    {
        carToSpawn = Instantiate(FindObjectOfType<InGameController>().playerCar, transform.position, transform.rotation);
        carToSpawn.transform.Rotate(0, 180, 0);
        carToSpawn.GetComponent<CarFlying>().LapTrackUI = lapUI;
        fixedJoint.connectedBody = carToSpawn.GetComponent<Rigidbody>();//use function, but to do that need to remove fixed joint from prefab
        fixedJoint.breakTorque = breakTorque;//var
        fixedJoint.breakForce = breakForce;//var
        fixedJoint.enablePreprocessing = false;
        carToSpawn.GetComponent<CarFlying>().fixedJoint = fixedJoint;
        carToSpawn.layer = 0;
        overheadCam.enabled = true;
        thirdPersonCam.enabled = false;
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
        if (hasPowerup && Input.GetKeyDown(KeyCode.F))
        {
            switch (powerup.power)
            {
                case PowerupClass.PowerType.Phase:
                    powerupTimer = powerup.timer;
                    hasPowerup = false;
                    powerActive = true;
                    powerup.UseEffect(carToSpawn);
                    powerup.power = PowerupClass.PowerType.None;
                    break;
                case PowerupClass.PowerType.Split:
                    GameObject miss = Instantiate(powerupToSpawn, transform.position, Quaternion.identity);
                    miss.GetComponent<SplitShot>().owner = gameObject;
                    //TODO need a function to find target path later
                    miss.GetComponent<SplitShot>().targetPath = pathFollow.pathCreator;
                    miss.transform.Rotate(90, 0, 0);
                    miss.transform.localScale *= 2;
                    hasPowerup = false;
                    powerup.power = PowerupClass.PowerType.None;
                    break;
                case PowerupClass.PowerType.Mine:
                    Vector3 spawnPos = carToSpawn.transform.position;
                    GameObject mine = Instantiate(powerupToSpawn, spawnPos, Quaternion.identity);
                    mine.GetComponent<Mine>().ownerTag = gameObject.tag;
                    hasPowerup = false;
                    powerup.power = PowerupClass.PowerType.None;
                    break;
                default:
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //SwapCam();
        }
        if(powerupTimer <= 0 && powerActive)
        {
            powerActive = false;
            powerup.StopEffect(carToSpawn);
        }
        else if(powerupTimer > 0 && powerActive)
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
        if (Input.GetKey(KeyCode.Space) || accelBool)
        {
            pathFollow.IncreaseSpeed(acceleration, minSpeed, maxSpeed); 
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
            pathFollow.DecreaseSpeed(decceleration, minSpeed, maxSpeed);
        }
    }

    public void Accelerate()
    {
        if(carAttached && !fixedJoint)
        {
            //set joint
            MakeNewJoint();
            Debug.Log("Joint set; continue driving");
        }
        accelBool = true;
    }

    public void Deccelerate()
    {
        accelBool = false;
    }

    public bool IsPowerActive()
    {
        return powerActive;
    }
    void SwapCam()
    {
        overheadCam.enabled = !overheadCam.enabled;
        thirdPersonCam.enabled = !thirdPersonCam.enabled;
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
        fixedJoint.breakTorque = breakTorque;//var
        fixedJoint.breakForce = breakForce;//var
        fixedJoint.enablePreprocessing = false;
        carToSpawn.GetComponent<CarFlying>().fixedJoint = fixedJoint;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("EnemyCar") && fixedJoint)
        {
            fixedJoint.breakForce = Mathf.Infinity;
            fixedJoint.breakTorque = Mathf.Infinity;
        }
        else if (other.tag.Equals("Powerup") && !hasPowerup)
        {
            //disable the powerup
            other.gameObject.GetComponent<PowerupPickup>().PickedUp();
            powerup = other.gameObject.GetComponent<PowerupPickup>().ChoosePowerup();
            powerupToSpawn = powerup.prefabToSpawn;
            hasPowerup = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("EnemyCar") && fixedJoint)
        {
            fixedJoint.breakForce = Mathf.Infinity;
            fixedJoint.breakTorque = Mathf.Infinity;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("EnemyCar") && fixedJoint)
        {
            fixedJoint.breakTorque = breakTorque;
            fixedJoint.breakForce = breakForce;
        }
    }
}
