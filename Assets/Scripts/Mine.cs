using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;
    [HideInInspector]
    public string carName;
    [HideInInspector]
    public string ownerName;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(.05f, .05f, .05f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.name.Equals(carName) && !other.name.Equals(ownerName) && other.tag.Contains("Car"))
        {
            if (other.GetComponent<CarFlying>().Controller)
            {
                if (!other.GetComponent<CarFlying>().Controller.GetComponent<AIController>().IsPowerActive())
                {
                    other.GetComponent<CarFlying>().fixedJoint.breakForce = 0;
                    other.GetComponent<Rigidbody>().AddRelativeForce(0, 0, -100, ForceMode.Impulse);
                    other.GetComponent<Rigidbody>().useGravity = true;
                    Boom();
                }
                else
                {
                    Boom();
                }
            }
            else if (other.GetComponent<CarFlying>().playerController)
            {
                if (!other.GetComponent<CarFlying>().playerController.GetComponent<PlayerController>().IsPowerActive() && !other.GetComponent<CarFlying>().playerController.GetComponent<PlayerController>().invulnerable)
                {
                    other.GetComponent<CarFlying>().fixedJoint.breakForce = 0;
                    other.GetComponent<Rigidbody>().AddRelativeForce(0, 0, -100, ForceMode.Impulse);
                    other.GetComponent<Rigidbody>().useGravity = true;
                    Boom();
                }
                else
                {
                    Boom();
                }
            }
        }
        else if (other.tag.Equals("Missile"))
        {
            Boom();
        }
    }
    void Boom()
    {
        //play effect
        GameObject e = Instantiate(explosion, transform.position, Quaternion.identity);
        //destroy self
        Destroy(gameObject);
    }
}
