using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class SplitShot : MonoBehaviour
{
    public GameObject owner;
    public PathCreator targetPath;
    float speed = 110;
    [SerializeField]
    float deathTimer = 3f;
    Follow pathFollow;
    // Start is called before the first frame update
    void Start()
    {
        pathFollow = GetComponent<Follow>();
        pathFollow.pathCreator = targetPath;
        pathFollow.OnPathChanged();
        pathFollow.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(deathTimer > 0)
        {
            deathTimer -= Time.deltaTime;
        }
        else if(deathTimer <= 0)
        {
            Boom();
        }
    }

    void Boom()
    {
        //play effect
        //destroy self
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != owner && other.tag.Equals("Enemy"))
        {
            other.GetComponent<CarFlying>().fixedJoint.breakTorque = 0;
            Boom();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject != owner)
        {
            collision.gameObject.GetComponent<CarFlying>().fixedJoint.breakTorque /= 2;
            Boom();
        }
    }
}
