using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarFlying : MonoBehaviour
{

    public FixedJoint fixedJoint;
    public GameObject LapTrackUI;
    [SerializeField]
    private float breakForce = 800;
    [SerializeField]
    private float breakTorque = 600;
    //powerups
    bool powerup = false;
    float powerupTimer = 0f;
    public int lapTracker = 0;
    public Color carColor;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().materials[0].color = carColor;
        Physics.IgnoreLayerCollision(0,6);
    }

    // Update is called once per frame
    void Update()
    {
        LapTrackUI.GetComponentInChildren<TextMeshProUGUI>().text = (lapTracker).ToString();
        if (powerupTimer <= 0 && powerup)
        {
            Debug.LogError("Car FLying Update powerup");
            powerup = false;
            //GetComponent<BoxCollider>().enabled = true;
            fixedJoint.breakForce = breakForce;//var
            fixedJoint.breakTorque = breakTorque;//var
            //stop changing colors
            GetComponent<MeshRenderer>().materials[0].color = carColor;
        }
        else if (powerupTimer > 0 && powerup)
        {
            powerupTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(gameObject.name + " hit something: " + other +",");
        if (other.tag == "Powerup")
        {
            //disable powerup
            other.gameObject.SetActive(false);
            //set timer
            powerup = true;
            powerupTimer = 4f;
            //disable collision
            //GetComponent<BoxCollider>().enabled = false;
            //force and torque break to inf
            fixedJoint.breakForce = Mathf.Infinity;
            fixedJoint.breakTorque = Mathf.Infinity;
            //change colors of car
            GetComponent<MeshRenderer>().materials[0].color = Color.blue;
            //starPowerColorChange();
        }
        //GetComponent<Rigidbody>().AddRelativeForce(0, 500, 500, ForceMode.Impulse);
    }

}
