using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    public float acceleration = 100f;
    public float maximumSpeed = .2f;
    
    private Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    public void AccelerateInDirection(Vector3 direction)
    {
        direction = Vector3.Normalize(direction);

        Vector3 newVelocity = myRigidbody.velocity + (direction * acceleration * Time.deltaTime);

        newVelocity.x = Mathf.Clamp(newVelocity.x, -maximumSpeed, maximumSpeed);
        newVelocity.y = Mathf.Clamp(newVelocity.y, -maximumSpeed, maximumSpeed);
        newVelocity.z = Mathf.Clamp(newVelocity.z, -maximumSpeed, maximumSpeed);

        myRigidbody.velocity = newVelocity;
    }

}
