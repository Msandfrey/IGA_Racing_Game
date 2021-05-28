using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapTracker : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    [SerializeField]
    private int lapsToWin = 6;//full lap times 3, so 2 full laps for 6
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
            if(other.gameObject.GetComponent<CarFlying>().lapTracker >= lapsToWin && !lose)
            {
                winPanel.SetActive(true);
                win = true;
            }
            //increase laps
            other.gameObject.GetComponent<CarFlying>().lapTracker += 1;
        }
        //do the same for enemies
        if (other.tag == "Enemy")
        {
            //check if win
            if (other.gameObject.GetComponent<CarFlying>().lapTracker >= lapsToWin && !win)
            {
                losePanel.SetActive(true);
                lose = true;
            }
            //increase laps
            other.gameObject.GetComponent<CarFlying>().lapTracker += 1;
        }
    }
}
