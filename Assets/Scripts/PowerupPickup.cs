using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    [SerializeField]
    private float timeToStayHidden;
    private float pickupTimer;
    private bool hidden = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pickupTimer > 0)
        {
            pickupTimer -= Time.deltaTime;
        } 
        else if (pickupTimer <= 0 && hidden)
        {
            hidden = false;
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<BoxCollider>().enabled = true;
        }
    }
    //disappear and set timer
    public void PickedUp()
    {
        hidden = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        pickupTimer = timeToStayHidden;
    }
}
