using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for the trigger box
public class TestJoint : MonoBehaviour
{
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
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * -10;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += transform.forward * Time.deltaTime * 10;
        }
    }
}
