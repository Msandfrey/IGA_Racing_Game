using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class SplitShot : MonoBehaviour
{
    [HideInInspector]
    public string carName;
    [HideInInspector]
    public string ownerName;
    public PathCreator targetPath;
    public GameObject explosion;
    public float speed = 110;
    [Range(0,1)]
    public float smoothBrainFactor = .5f;
    //for finding target car
    public float viewRadius = 10;
    [Range(0,360)]
    public float viewAngle = 30;
    public LayerMask searchLayerMask;
    public Transform targetCar;
    [SerializeField]
    float deathTimer = 3f;
    [SerializeField]
    float newTorque = 0f;
    Follow pathFollow;
    private bool targetLocked = false;
    //particle effect

    // Start is called before the first frame update
    void Start()
    {
        pathFollow = GetComponent<Follow>();
        pathFollow.pathCreator = targetPath;
        pathFollow.OnPathChanged();
        pathFollow.speed = speed;
        StartCoroutine("FindTargets", .01f);
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
    void FixedUpdate()
    {
        if (targetLocked)
        {
            Vector3 targetPos = targetCar.position;
            Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, smoothBrainFactor * Time.deltaTime * speed/2);
            transform.position = smoothPos;
            transform.LookAt(targetCar);
            smoothBrainFactor += .1f;
        }
    }

    IEnumerator FindTargets(float delay)
    {
        while (!targetLocked)
        {
            yield return new WaitForSeconds(delay);
            FindTargetCar();
        }
    }

    void Boom()
    {
        //play effect
        GameObject e = Instantiate(explosion, transform.position, Quaternion.identity);
        //destroy self
        Destroy(gameObject);
    }

    void FindTargetCar()
    {
        //quick sphere detection radius, 0 is the target mask
        Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, viewRadius, searchLayerMask);
        //go through any options in the radius
        float closestDist = 10000;
        for(int i = 0; i < targetsInRadius.Length; i++)
        {
            Transform target = targetsInRadius[i].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            //check if the angle of the target is in the angle of the detection
            if(Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                //check if they are an ok target
                if (!target.gameObject.name.Equals(carName)) 
                {
                    float distToTar = Vector3.Distance(transform.position, target.position);
                    //check if it is closest
                    if(distToTar < closestDist)
                    {
                        //set closest as the target
                        closestDist = distToTar;
                        targetCar = target;
                        targetLocked = true;
                        pathFollow.LeavePath();
                    }
                }
            }
        }
    }
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal) { angleInDegrees += transform.eulerAngles.y; }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.name.Equals(carName) && !other.name.Equals(ownerName) && other.tag.Contains("Car"))
        {
            if (other.GetComponent<CarFlying>().Controller)
            {
                if (!other.GetComponent<CarFlying>().Controller.GetComponent<AIController>().IsPowerActive())
                {
                    other.GetComponent<CarFlying>().fixedJoint.breakTorque = newTorque;
                    other.GetComponent<Rigidbody>().AddRelativeTorque(0, 0, -100, ForceMode.Impulse);
                    other.GetComponent<Rigidbody>().useGravity = true;
                    Boom();
                }
            }
            else if (other.GetComponent<CarFlying>().playerController)
            {
                if (!other.GetComponent<CarFlying>().playerController.GetComponent<PlayerController>().IsPowerActive())
                {
                    other.GetComponent<CarFlying>().fixedJoint.breakTorque = newTorque;
                    other.GetComponent<Rigidbody>().AddRelativeTorque(0, 0, -100, ForceMode.Impulse);
                    other.GetComponent<Rigidbody>().useGravity = true;
                    Boom();
                }
            }
            else
            {
                Boom();
            }
        }
    }
}
