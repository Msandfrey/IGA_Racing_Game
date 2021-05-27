using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public BezierFollow bezierFollow;
    public GameObject carToSpawn;
    public FixedJoint fixedJoint;
    public float maxSpeed;
    public float minSpeed;
    public float delayToStart;
    public int lapsToWin;

    private float timer;
    private int lapTracker;

    // Start is called before the first frame update
    void Awake()
    {
        bezierFollow = GetComponent<BezierFollow>();
        bezierFollow.speedModifier = 0;

        //GameObject spawnedObject = Instantiate(carToSpawn) as GameObject;
        //fixedJoint = spawnedObject.GetComponent<FixedJoint>();
        //if (fixedJoint != null)
        //{
        //    fixedJoint.connectedBody = GetComponent<Rigidbody>();
        //}
        //spawnedObject.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= delayToStart)
        {
            timer += Time.deltaTime;
            return;
        }
        if (lapTracker >= lapsToWin)
        {
            bezierFollow.speedModifier = 0;
            return;
        }
        if (fixedJoint == null)
        {
            bezierFollow.speedModifier = 0;
            return;
        }
        if (bezierFollow.routeToGo%2==1)
        {
            bezierFollow.speedModifier += 0.08f;
            bezierFollow.speedModifier = Mathf.Clamp(bezierFollow.speedModifier, minSpeed, maxSpeed);
        }
        else
        {
            bezierFollow.speedModifier -= 0.05f;
            bezierFollow.speedModifier = Mathf.Clamp(bezierFollow.speedModifier, minSpeed, maxSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            lapTracker += 1;
            Debug.Log(lapTracker);
            if (lapTracker >= lapsToWin)
            {
                bezierFollow.speedModifier = 0;
            }
        }
    }

}
