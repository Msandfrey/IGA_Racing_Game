using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    [SerializeField]
    private float timeToStayHidden;
    private float pickupTimer;
    private bool hidden = false;
    [SerializeField]
    private GameObject missile;
    [SerializeField]
    private GameObject mine;
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

    public PowerupClass ChoosePowerup()
    {
        PowerupClass power = new PowerupClass();
        int powerType = 3;
        //int powerType = Random.Range(1, System.Enum.GetValues(typeof(PowerupClass.PowerType)).Length);
        switch (powerType)
        {
            case 1://Phase shift
                power.power = (PowerupClass.PowerType)1;
                power.timer = 2f;//var
                power.UIImage = null;
                power.prefabToSpawn = null;
                break;
            case 2:
                power.power = (PowerupClass.PowerType)2;
                power.timer = -1f;
                power.UIImage = null;
                power.prefabToSpawn = missile;
                break;
            case 3:
                power.power = (PowerupClass.PowerType)3;
                power.timer = -1f;
                power.UIImage = null;
                power.prefabToSpawn = mine;
                break;
            default: 
                break;
        }
        return power;
    }
}
