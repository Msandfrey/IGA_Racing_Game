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
        if (other.tag == "Player")
        {
            //check if win
            if(other.gameObject.GetComponent<CarFlying>().lapTracker >= lapsToWin && !lose && other.gameObject.GetComponent<CarFlying>().fakeLapTracker /3 >= lapsToWin)
            {
                winPanel.SetActive(true);
                win = true;
                return;
            }
            //increase laps
            other.gameObject.GetComponent<CarFlying>().fakeLapTracker += 1;
            other.gameObject.GetComponent<CarFlying>().lapTracker = (other.gameObject.GetComponent<CarFlying>().fakeLapTracker - 1) / 3 + 1;
         
        }
        //do the same for enemies
        if (other.tag == "Enemy")
        {
            //check if win
            if (other.gameObject.GetComponent<CarFlying>().lapTracker >= lapsToWin && !win && other.gameObject.GetComponent<CarFlying>().fakeLapTracker /3 >= lapsToWin)
            {
                losePanel.SetActive(true);
                lose = true;
                return;
            }
            //increase laps
            other.gameObject.GetComponent<CarFlying>().lapTracker += 1;
            other.gameObject.GetComponent<CarFlying>().lapTracker = (other.gameObject.GetComponent<CarFlying>().fakeLapTracker - 1) / 3 + 1;

        }
    }
}
