using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject start;
    public GameObject level;
    public GameObject intro;
    public GameObject car;
    private bool hasIntroPlayer = false;
    private bool playIntro = false;
    private bool isPlaying = false;
    [SerializeField]
    private AudioSource introSound;

    private string SceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playIntro && !hasIntroPlayer)
        {
            introSound.Play();
            isPlaying = true;
            hasIntroPlayer = true;
            playIntro = false;
        }
        if (isPlaying && Input.GetKey(KeyCode.Space))
        {
            isPlaying = false;
            //stop playing sound
            introSound.Stop();
            LevelSelect();
        }
        if (!introSound.isPlaying && hasIntroPlayer)
        {
            isPlaying = false;
            intro.SetActive(false);
            LevelSelect();
            hasIntroPlayer = false;
        }
    }
    public void EnterLevelSelect()
    {
        playIntro = true;
        start.SetActive(false);
        intro.SetActive(true);
    }
    public void LevelSelect()
    {
        level.SetActive(true);
    }
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
        FindObjectOfType<InGameController>().racerType = 1;
        FindObjectOfType<InGameController>().playerCarColor = "red";
    }
    public void MattTest()
    {
        SceneToLoad = "Circle";
        level.SetActive(false);
        car.SetActive(true);
        //SceneManager.LoadScene("Circle");//rename later maybe
    }
    public void Butterfly()
    {
        SceneToLoad = "ButterflyTest";
        level.SetActive(false);
        car.SetActive(true);
        //SceneManager.LoadScene("ButterflyTest");//rename later maybe
    }
    public void Wave()
    {
        SceneToLoad = "WaveTest";
        level.SetActive(false);
        car.SetActive(true);
        //SceneManager.LoadScene("WaveTest");//rename later maybe
    }
    public void Dollar()
    {
        SceneToLoad = "DollarTest";
        level.SetActive(false);
        car.SetActive(true);
    }
    public void CarA(string color)
    {
        //save car to gamemanager
        FindObjectOfType<InGameController>().racerType = 1;
        FindObjectOfType<InGameController>().playerCarColor = color;
        SceneManager.LoadScene(SceneToLoad);
    }
    public void CarB(string color)
    {
        FindObjectOfType<InGameController>().racerType = 2;
        FindObjectOfType<InGameController>().playerCarColor = color;
        SceneManager.LoadScene(SceneToLoad);
    }
    public void CarC(string color)
    {
        FindObjectOfType<InGameController>().racerType = 3;
        FindObjectOfType<InGameController>().playerCarColor = color;
        SceneManager.LoadScene(SceneToLoad);
    }
    public void CarD(string color)
    {
        FindObjectOfType<InGameController>().racerType = 4;
        FindObjectOfType<InGameController>().playerCarColor = color;
        SceneManager.LoadScene(SceneToLoad);
    }
}
