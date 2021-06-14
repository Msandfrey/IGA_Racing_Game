using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarFlying : MonoBehaviour
{

    public FixedJoint fixedJoint;
    public GameObject LapTrackUI;
    public float breakForce = 800;
    public float breakTorque = 600;
    //powerups
    bool powerup = false;
    float powerupTimer = 0f;
    public int lapTracker = 0;
    public Color carColor;
    public GameObject Controller;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<MeshRenderer>().materials[0].color = carColor;
        Physics.IgnoreLayerCollision(0,6);
    }

    // Update is called once per frame
    void Update()
    {
        LapTrackUI.GetComponentInChildren<TextMeshProUGUI>().text = (lapTracker).ToString();
        if (powerupTimer <= 0 && powerup)
        {
            powerup = false;
            //GetComponent<MeshRenderer>().materials[0].color = carColor;
            //GetComponent<BoxCollider>().enabled = true;
            fixedJoint.breakForce = breakForce;//var
            fixedJoint.breakTorque = breakTorque;//var
            //stop changing colors
        }
        else if (powerupTimer > 0 && powerup)
        {
            powerupTimer -= Time.deltaTime;
        }
    }

}
