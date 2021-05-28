using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFlying : MonoBehaviour
{

    public FixedJoint fixedJoint;
    //powerups
    bool powerup = true;
    float powerupTimer = 0f;
    public int lapTracker = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().materials[0].color = Color.grey;
    }

    // Update is called once per frame
    void Update()
    {
        if (powerupTimer <= 0 && powerup)
        {
            powerup = false;
            //GetComponent<BoxCollider>().enabled = true;
            fixedJoint.breakForce = 1000;//var
            fixedJoint.breakTorque = 650;//var
            //stop changing colors
            GetComponent<MeshRenderer>().materials[0].color = Color.grey;
        }
        else if (powerupTimer > 0 && powerup)
        {
            powerupTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit something");
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
    }
}
