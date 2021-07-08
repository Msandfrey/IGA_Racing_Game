using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LapTracker : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject endButtons;
    public TextMeshProUGUI playerPlaceText;
    public CarFlying playerCar;
    public CarFlying AICar1;
    public CarFlying AICar2;
    public CarFlying AICar3;
    [SerializeField]
    private int lapsToWin = 3;
    private int currentPlace;
    bool win = false;
    bool lose = false;

    //bools
    bool ahead1 = false;
    bool ahead2 = false;
    bool ahead3 = false;
    bool behind1 = false;
    bool behind2 = false;
    bool behind3 = false;

    // Start is called before the first frame update
    void Start()
    {
        winPanel.SetActive(false);
        currentPlace = AICar3 ? 4 : 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerCar) { playerCar = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().carToSpawn.GetComponent<CarFlying>(); }
        if(playerCar.lapTracker == 0)
        {
            return;
        }
        //is player ahead of car1
        if(playerCar.lapTracker > AICar1.lapTracker && !ahead1)
        {
            currentPlace--;
            ahead1 = true;
            behind1 = false;
        }else if(playerCar.lapTracker == AICar1.lapTracker)
        {
            //test difference in level completeness
            if (!ahead1 && playerCar.playerController.pathFollow.GetDistanceTraveled() / playerCar.playerController.pathFollow.pathCreator.path.length > AICar1.Controller.pathFollow.GetDistanceTraveled() / AICar1.Controller.pathFollow.pathCreator.path.length)
            {
                currentPlace--;
                ahead1 = true;
                behind1 = false;
            }
            else if (!behind1 && playerCar.playerController.pathFollow.GetDistanceTraveled() / playerCar.playerController.pathFollow.pathCreator.path.length < AICar1.Controller.pathFollow.GetDistanceTraveled() / AICar1.Controller.pathFollow.pathCreator.path.length) 
            {
                currentPlace++;
                ahead1 = false;
                behind1 = true;
            }
        }
        else if(!behind1 && playerCar.lapTracker < AICar1.lapTracker)
        {
            currentPlace++;
            ahead1 = false;
            behind1 = true;
        }
        //is player ahead of car 2
        if (playerCar.lapTracker > AICar2.lapTracker && !ahead2)
        {
            currentPlace--;
            ahead2 = true;
            behind2 = false;
        }
        else if (playerCar.lapTracker == AICar2.lapTracker)
        {
            //test difference in level completeness
            if (!ahead2 && playerCar.playerController.pathFollow.GetDistanceTraveled() / playerCar.playerController.pathFollow.pathCreator.path.length > AICar2.Controller.pathFollow.GetDistanceTraveled() / AICar2.Controller.pathFollow.pathCreator.path.length)
            {
                currentPlace--;
                ahead2 = true;
                behind2 = false;
            }
            else if (!behind2 && playerCar.playerController.pathFollow.GetDistanceTraveled() / playerCar.playerController.pathFollow.pathCreator.path.length < AICar2.Controller.pathFollow.GetDistanceTraveled() / AICar2.Controller.pathFollow.pathCreator.path.length)
            {
                currentPlace++;
                ahead2 = false;
                behind2 = true;
            }
        }
        else if (!behind2 && playerCar.lapTracker < AICar2.lapTracker)
        {
            currentPlace++;
            ahead2 = false;
            behind2 = true;
        }
        //is player car ahead of car 3
        if (AICar3)
        {
            if (playerCar.lapTracker > AICar3.lapTracker && !ahead3)
            {
                currentPlace--;
                ahead3 = true;
                behind3 = false;
            }
            else if (playerCar.lapTracker == AICar3.lapTracker)
            {
                //test difference in level completeness
                if (!ahead3 && playerCar.playerController.pathFollow.GetDistanceTraveled() / playerCar.playerController.pathFollow.pathCreator.path.length > AICar3.Controller.pathFollow.GetDistanceTraveled() / AICar3.Controller.pathFollow.pathCreator.path.length)
                {
                    currentPlace--;
                    ahead3 = true;
                    behind3 = false;
                }
                else if (!behind3 && playerCar.playerController.pathFollow.GetDistanceTraveled() / playerCar.playerController.pathFollow.pathCreator.path.length < AICar3.Controller.pathFollow.GetDistanceTraveled() / AICar3.Controller.pathFollow.pathCreator.path.length)
                {
                    currentPlace++;
                    ahead3 = false;
                    behind3 = true;
                }
            }
            else if (!behind3 && playerCar.lapTracker < AICar3.lapTracker)
            {
                currentPlace++;
                ahead3 = false;
                behind3 = true;
            }
        }
        currentPlace = Mathf.Clamp(currentPlace, 1, AICar3 ? 4 : 3);//clamp place to be between 1 & 3/4
        switch (currentPlace)
        {
            case 1:
                playerPlaceText.text = currentPlace.ToString() + "st";
                break;
            case 2:
                playerPlaceText.text = currentPlace.ToString() + "nd";
                break;
            case 3:
                playerPlaceText.text = currentPlace.ToString() + "rd";
                break;
            case 4:
                playerPlaceText.text = currentPlace.ToString() + "th";
                break;
            default:
                playerPlaceText.text = currentPlace.ToString() + "th";
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerCar.gameObject)
        {
            if (other.gameObject.GetComponent<CarFlying>().lapTracker >= lapsToWin && !lose)
            {
                winPanel.SetActive(true);
                win = true;
                endButtons.SetActive(true);
                other.gameObject.GetComponent<CarFlying>().fixedJoint.breakTorque = Mathf.Infinity;
                other.gameObject.GetComponent<CarFlying>().fixedJoint.breakForce = Mathf.Infinity;
                return;
            }
            //increase laps
            if (other.gameObject.GetComponent<CarFlying>().lapsToWin == 0)
            {
                other.gameObject.GetComponent<CarFlying>().lapsToWin = lapsToWin;
            }
            other.gameObject.GetComponent<CarFlying>().lapTracker += 1;
        }
        else if(other.gameObject == AICar1.gameObject || other.gameObject == AICar2.gameObject)
        {
            if (other.gameObject.GetComponent<CarFlying>().lapTracker >= lapsToWin && !win)
            {
                losePanel.SetActive(true);
                lose = true;
                endButtons.SetActive(true);
                return;
            }
            //increase laps
            other.gameObject.GetComponent<CarFlying>().lapTracker += 1;
        }
        else if (AICar3)
        {
            if(other.gameObject == AICar3.gameObject)
            {
                if (other.gameObject.GetComponent<CarFlying>().lapTracker >= lapsToWin && !win)
                {
                    losePanel.SetActive(true);
                    lose = true;
                    endButtons.SetActive(true);
                    return;
                }
                //increase laps
                other.gameObject.GetComponent<CarFlying>().lapTracker += 1;
            }
        }
        /*else if(other.gameObject == AICar2)
        {

        }
        else if (AICar3)
        {
            if(other.gameObject == AICar3)
            {

            }
        }*/
    }
}
