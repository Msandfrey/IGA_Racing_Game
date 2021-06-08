using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapTracker : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    [SerializeField]
    private int lapsToWin = 3;
    bool win = false;
    bool lose = false;

    // Start is called before the first frame update
    void Start()
    {
        winPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("PlayerCar"))
        {
            //check if win
            if(other.gameObject.GetComponent<CarFlying>().lapTracker >= lapsToWin && !lose)
            {
                winPanel.SetActive(true);
                win = true;
                other.gameObject.GetComponent<CarFlying>().fixedJoint.breakTorque = Mathf.Infinity;
                other.gameObject.GetComponent<CarFlying>().fixedJoint.breakForce = Mathf.Infinity;
                return;
            }
            //increase laps
            other.gameObject.GetComponent<CarFlying>().lapTracker += 1;
         
        }
        //do the same for enemies
        if (other.tag.Equals("EnemyCar"))
        {
            //check if win
            if (other.gameObject.GetComponent<CarFlying>().lapTracker >= lapsToWin && !win)
            {
                losePanel.SetActive(true);
                lose = true;
                return;
            }
            //increase laps
            other.gameObject.GetComponent<CarFlying>().lapTracker += 1;
        }
    }
}
