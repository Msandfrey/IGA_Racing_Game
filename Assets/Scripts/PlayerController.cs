using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BezierFollow))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public BezierFollow bezierFollow;
    public GameObject carToSpawn;
    public GameObject winPanel;
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

        winPanel.SetActive(false);

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
        if(winPanel.activeInHierarchy == true)
        {
            return;
        }
        if(timer <= delayToStart)
        {
            timer += Time.deltaTime;
            return;
        }
        if(fixedJoint == null)
        {
            bezierFollow.speedModifier = 0;
            return;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            bezierFollow.speedModifier += 0.05f;
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
                    winPanel.SetActive(true);
                    bezierFollow.speedModifier = 0;
                }
            }
    }

}
