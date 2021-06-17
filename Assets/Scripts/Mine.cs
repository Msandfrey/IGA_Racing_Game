using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;
    public string ownerTag;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(.01f, .01f, .01f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals(ownerTag) && !other.tag.Contains("Car"))
        {
            
            if (!other.GetComponent<AIController>().IsPowerActive())
            {
                other.GetComponent<AIController>().fixedJoint.breakForce = 0;
                other.GetComponent<Rigidbody>().AddForce(Vector3.up);
                Boom();
            }
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
