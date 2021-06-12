using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutCollision : MonoBehaviour
{
    public GameObject lastGate;//change the way to do this later
    private AIController Ai;
    // Start is called before the first frame update
    void Start()
    {
        Ai = GetComponent<AIController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnJointBreak(float breakForce)
    {
        //do func for blowing up first car (later determine how many times the car died)
        //FindObjectOfType<TutManager>().FirstCarDied();
        //stop car from moving
        Ai.carToSpawn.GetComponent<Rigidbody>().useGravity = false;
        Ai.carToSpawn.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Ai.carToSpawn.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //move car to pos
        Ai.carToSpawn.transform.position = transform.position;
        //fix rotation
        Ai.carToSpawn.transform.rotation = transform.rotation;
        Ai.carToSpawn.transform.Rotate(0, 180, 0);
        //reattach things
        Ai.carAttached = true;
        Ai.fixedJoint = gameObject.AddComponent<FixedJoint>();
        Ai.fixedJoint.connectedBody = Ai.carToSpawn.GetComponent<Rigidbody>();
        Ai.fixedJoint.breakTorque = 600;//var
        Ai.fixedJoint.breakForce = 800;//var
        Ai.fixedJoint.enablePreprocessing = false;
        Ai.carToSpawn.GetComponent<CarFlying>().fixedJoint = Ai.fixedJoint;//for now it doesnt do anything with this var
        Ai.carToSpawn.layer = 0;
        //activate the last gate
        lastGate.SetActive(true);
        //hide car
        gameObject.SetActive(false);
    }
}
