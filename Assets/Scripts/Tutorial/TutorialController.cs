using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Follow))]
[RequireComponent(typeof(Rigidbody))]
public class TutorialController : MonoBehaviour
{
    [HideInInspector]
    public Follow pathFollow;
    public GameObject carToSpawn;
    public FixedJoint fixedJoint;
    public TrailRenderer trail;
    [SerializeField]
    private float breakForce = 800;
    [SerializeField]
    private float breakTorque = 600;
    [SerializeField]
    private float acceleration = 0.2f;
    [SerializeField]
    private float decceleration = 0.05f;
    public float maxSpeed;
    public float minSpeed;
    float respawnTimer = 0f;

    private GameObject powerupToSpawn;

    PowerupClass powerup;
    private bool carAttached = true;
    private bool accelBool = false;
    public bool wait = true;

    //powerups
    private bool hasPowerup = false;
    private float powerupTimer = 0f;
    private bool powerActive = false;
    public GameObject powerUI;

    //cameras
    public Camera overheadCam;
    public Camera thirdPersonCam;

    // Start is called before the first frame update
    void Awake()
    {
        pathFollow = GetComponent<Follow>();
        powerup = new PowerupClass();
    }

    private void Start()
    {
        //set the cameras
        overheadCam.enabled = true;
        thirdPersonCam.enabled = false;
        //setup UI
        powerUI.GetComponent<Image>().color = new Vector4(.2f, 1, 1, .1f);
        powerUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/EmptyPower");//start with this one for now, todo change later
    }

    // Update is called once per frame
    void Update()
    {
        //use powerup
        if (hasPowerup && Input.GetKeyDown(KeyCode.F))
        {
            UsePowerup();
        }
        //powerup (phase) is active
        if (powerupTimer <= 0 && powerActive)
        {
            Debug.Log("powerStop");
            powerActive = false;
            powerup.StopEffect(carToSpawn);
        }
        else if (powerupTimer > 0 && powerActive)
        {
            powerupTimer -= Time.deltaTime;
        }
        //car is broken
        if (fixedJoint == null)
        {
            if (respawnTimer <= 0 && !carAttached)
            {
                ResetCar();
                carAttached = true;
                carToSpawn.GetComponent<MeshRenderer>().enabled = true;
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
        //move car
        if (!wait && (Input.GetKey(KeyCode.Space) || accelBool))
        {
            pathFollow.IncreaseSpeed(acceleration, minSpeed, maxSpeed);
            if (pathFollow.speed == maxSpeed)
            {
                //max width
                trail.startWidth = 1f;
            }
            else if (pathFollow.speed >= maxSpeed * .75f)
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
        else//slow car down
        {
            pathFollow.DecreaseSpeed(decceleration, minSpeed, maxSpeed);
        }
    }

    public void Accelerate()
    {
        if (carAttached && !fixedJoint)
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
    public void UsePowerup()
    {
        switch (powerup.power)
        {
            case PowerupClass.PowerType.Phase:
                powerupTimer = powerup.timer;
                hasPowerup = false;
                powerUI.GetComponent<Image>().color = new Vector4(.2f, 1, 1, .1f);
                powerUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/EmptyPower");
                powerActive = true;
                powerup.UseEffect(carToSpawn);
                powerup.power = PowerupClass.PowerType.None;
                break;
            case PowerupClass.PowerType.Split:
                GameObject miss = Instantiate(powerupToSpawn, transform.position, Quaternion.identity);
                miss.GetComponent<SplitShot>().ownerName = name;
                //TODO need a function to find target path later
                miss.GetComponent<SplitShot>().targetPath = pathFollow.pathCreator;
                miss.GetComponent<SplitShot>().carName = carToSpawn.name;
                miss.GetComponent<SplitShot>().ownerName = name;
                miss.transform.Rotate(90, 0, 0);
                miss.transform.localScale *= 2;
                hasPowerup = false;
                powerUI.GetComponent<Image>().color = new Vector4(.2f, 1, 1, .1f);
                powerUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/EmptyPower");
                powerup.power = PowerupClass.PowerType.None;
                break;
            case PowerupClass.PowerType.Mine:
                Vector3 spawnPos = carToSpawn.transform.position;
                GameObject mine = Instantiate(powerupToSpawn, spawnPos, Quaternion.identity);
                mine.GetComponent<Mine>().carName = carToSpawn.name;
                mine.GetComponent<Mine>().ownerName = name;
                hasPowerup = false;
                powerUI.GetComponent<Image>().color = new Vector4(.2f, 1, 1, .1f);
                powerUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/EmptyPower");
                powerup.power = PowerupClass.PowerType.None;
                break;
            default:
                break;
        }
    }
    public bool IsPowerActive()
    {
        return powerActive;
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
    public void SwapCam()
    {
        overheadCam.enabled = !overheadCam.enabled;
        thirdPersonCam.enabled = !thirdPersonCam.enabled;
    }
    void ActivatePowerButt()
    {
        powerUI.GetComponent<Image>().color = new Vector4(.2f, 1, 1, 1);
        switch (powerup.power)
        {
            case PowerupClass.PowerType.Mine:
                powerUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/MineButton");
                break;
            case PowerupClass.PowerType.Phase:
                powerUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PhaseButton");
                break;
            case PowerupClass.PowerType.Split:
                powerUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/MissileButton");
                break;
        }
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
        Debug.Log(gameObject.name + " falls off with force of : " + breakForce);
        carAttached = false;
        pathFollow.speed = 0;
        pathFollow.ResetCar();
        carToSpawn.GetComponent<Rigidbody>().useGravity = true;
        carToSpawn.GetComponent<MeshRenderer>().enabled = false;
        respawnTimer = 1f;
        carToSpawn.layer = 6;//fallen layer
        FindObjectOfType<TutManager>().Died();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("EnemyCar") && fixedJoint)
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
            ActivatePowerButt();
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
