using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for the trigger box
public class TestJoint : MonoBehaviour
{
    //public Transform target;
    public FixedJoint fj;
    // Start is called before the first frame update
    void Start()
    {

        //Physics.IgnoreLayerCollision(0, 6);
        //GetComponent<Rigidbody>().velocity = new Vector3(20, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody>().AddRelativeForce(target.position, ForceMode.Acceleration);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Enemy"))
        {
            Debug.LogWarning("i am working?");
            fj.breakForce = Mathf.Infinity;
            fj.breakTorque = Mathf.Infinity;
            //other.gameObject.GetComponent<Follow>().speed = 120;
            //
            //other.gameObject.GetComponent<Rigidbody>().AddRelativeForce(AttachedCar.GetComponent<Rigidbody>().velocity, ForceMode.Impulse);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Enemy"))
        {
            fj.breakTorque = 600;
            fj.breakForce = 800;
            //Vector3 newV = AttachedCar.GetComponent<Rigidbody>().velocity;
            //other.gameObject.GetComponent<Follow>().speed = 200;
            //newV = Vector3.Cross(newV, AttachedCar.GetComponent<Rigidbody>().angularVelocity);
            //other.gameObject.GetComponent<Rigidbody>().AddRelativeForce(newV, ForceMode.Impulse);
        }

    }

}
