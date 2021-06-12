using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutManager : MonoBehaviour
{
    //UI
    public GameObject startUI;
    public GameObject turnFailUI;
    public GameObject powerUI;
    
    //objects
    public GameObject playerCar;
    public GameObject enemyCar1;
    public GameObject enemyCar2;
    public GameObject powerMiss;
    public GameObject powerPhase;
    public GameObject powerMine;

    //aduio
    public AudioSource tutAudio;
    public AudioClip startClip;
    public AudioClip turnFailClip;
    public AudioClip turnPassClip;
    public AudioClip powerClip;
    public AudioClip endClip;

    //timers
    private float firstTimer;
    private float secondTimer;
    private float powerTimer;
    private float passTimer;

    //flags
    private bool firstTut = false;
    private bool failTut = false;
    private bool passTut = false;
    private bool powerTut = false;
    private bool endTut = false;

    private bool firstDeath = false;
    private float startUIDelay = 7f;
    private float failUIDelay = 16f;
    private float passDelay;
    private float waitForEnd;
    private float powerUIDelay = 9f;
    private int timesDied = 0;
    // Start is called before the first frame update
    void Start()
    {
        //keep racers from going
        playerCar.GetComponent<TutorialController>().wait = true;
        //start audio
        tutAudio.clip = startClip;
        tutAudio.Play();
        //set timer for first audio
        firstTut = true;
        passDelay = turnPassClip.length;
    }

    // Update is called once per frame
    void Update()
    {
        //first timer-----------------------------------------------------------------
        if(firstTimer >= startUIDelay && firstTut)
        {
            //show UI
            startUI.SetActive(true);
            //let players go
            playerCar.GetComponent<TutorialController>().wait = false;
            firstTut = false;
        }
        else if(firstTimer < startUIDelay && firstTut)
        {
            firstTimer += Time.deltaTime;
        }
        //second timer--------------------------------------------------------------
        if (secondTimer >= failUIDelay && failTut)
        {
            //show UI
            turnFailUI.SetActive(true);
            //let players go
            playerCar.GetComponent<TutorialController>().wait = false;
            failTut = false;
        }
        else if (secondTimer < failUIDelay && failTut)
        {
            secondTimer += Time.deltaTime;
        }
        //power timer--------------------------------------------------------------
        if (powerTimer >= powerUIDelay && powerTut)
        {
            //show UI
            powerUI.SetActive(true);
            //let players go
            playerCar.GetComponent<TutorialController>().wait = false;
            powerTut = false;
        }
        else if (powerTimer < powerUIDelay && powerTut)
        {
            powerTimer += Time.deltaTime;
        }
        //delay for passClip-----------------------------------------------------------
        if (passTimer >= passDelay && passTut)
        {
            //powerupUI active
            tutAudio.clip = powerClip;
            tutAudio.Play();
            //spawn powerup
            powerMiss.SetActive(true);
            powerPhase.SetActive(true);
            //spawn enemy car - not moving
            enemyCar1.SetActive(true);
            enemyCar2.SetActive(true);
            //set timer
            powerTut = true;
            passTut = false;
        }
        else if (passTimer < passDelay && passTut)
        {
            passTimer += Time.deltaTime;
        }
        //wait till last audio plays then go back to main menu
        if(waitForEnd > 0 && endTut)
        {
            waitForEnd -= Time.deltaTime;
        }
        else if(waitForEnd <= 0 && endTut)
        {
            SceneManager.LoadScene(0);
        }
        //if no more fixed joint on car------------------------------------------------
        if (!playerCar.GetComponent<FixedJoint>() && !firstDeath)
        {
            startUI.SetActive(false);
            //  pause player from going
            playerCar.GetComponent<TutorialController>().wait = true;
            //  play next audio
            tutAudio.clip = turnFailClip;
            tutAudio.Play();
            //  set timer for UI
            firstDeath = true;
            failTut = true;
        }
    }

    public void Died()
    {
        timesDied++;
        switch (timesDied)
        {
            case 2:
                //play audio
                break;
            case 3:
                //play audio
                break;
            case 10:
                //play audio
                //slow down accel
                break;
            default:
                break;
        }
    }

    public void PassTurn()
    {
        Debug.Log("Function in TutMan works");
        startUI.SetActive(false);
        turnFailUI.SetActive(false);
        //reset car at beginning
        firstDeath = true;
        playerCar.GetComponent<TutorialController>().pathFollow.speed = 0;
        playerCar.GetComponent<TutorialController>().pathFollow.ResetCar();
        playerCar.GetComponent<TutorialController>().wait = true;
        //play spiel about powerups
        tutAudio.clip = turnPassClip;
        tutAudio.Play();
        passTut = true;        
    }

    public void FirstCarDied()
    {

    }

    public void SecondCarDied()
    {

    }

    public void ThirdCarDied()
    {

    }
    public void EndTut()
    {
        powerUI.SetActive(false);
        tutAudio.clip = endClip;
        tutAudio.Play();
        endTut = true;
        waitForEnd = endClip.length;
    }
}
