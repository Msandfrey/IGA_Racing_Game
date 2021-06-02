using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for the non trigger box
public class GetHitTest : MonoBehaviour
{
    public FixedJoint fj;
    public float v;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = (new Vector3(v, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        fj.breakForce = Mathf.Infinity;
        fj.breakTorque = Mathf.Infinity;
        
    }
}
