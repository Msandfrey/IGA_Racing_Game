using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject title;
    //buttons
    public GameObject start;
    public GameObject creditButt;
    public GameObject instructionsButt;
    public GameObject tutorial;
    //intro scene
    public GameObject intro;
    //credits
    public GameObject credit;
    //controls
    public GameObject instructions;
    public GameObject instructions3DObject;
    //level page
    public GameObject level;
    public GameObject level3DObject;
    public GameObject circle;
    public GameObject butter;
    public GameObject dollar;
    public GameObject wave;
    private int currLevel = 1;
    //cars
    public GameObject car;
    public GameObject car3DObject;
    private bool hasIntroPlayer = false;
    private bool playIntro = false;
    private bool isPlaying = false;
    [SerializeField]
    private AudioSource introSound;
    [SerializeField]
    private AudioSource menuBGM;

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
            menuBGM.Pause();
            introSound.Play();
            isPlaying = true;
            hasIntroPlayer = true;
            playIntro = false;
        }
        if (isPlaying && (Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            isPlaying = false;
            //stop playing sound
            introSound.Stop();
            menuBGM.Play();
            LevelSelect();
        }
        if (!introSound.isPlaying && hasIntroPlayer)
        {
            isPlaying = false;
            intro.SetActive(false);
            menuBGM.Play();
            LevelSelect();
            hasIntroPlayer = false;
        }
    }
    public void Credits()
    {
        //show credits
        start.SetActive(false);
        creditButt.SetActive(false);
        tutorial.SetActive(false);
        instructionsButt.SetActive(false);
        credit.SetActive(true);
    }
    public void BackFromCredits()
    {
        //todo go back to previous
        //just go to level select for now
        credit.SetActive(false);
        creditButt.SetActive(true);
        tutorial.SetActive(true);
        instructionsButt.SetActive(true);
        start.SetActive(true);
    }
    public void BackToLevels()
    {
        start.SetActive(false);
        level.SetActive(true);
        level3DObject.SetActive(true);
        car.SetActive(false);
    }
    public void EnterLevelSelect()
    {
        playIntro = true;
        start.SetActive(false);
        creditButt.SetActive(false);
        tutorial.SetActive(false);
        instructionsButt.SetActive(false);
        intro.SetActive(true);
    }
    public void LevelSelect()
    {
        level.SetActive(true);
        level3DObject.SetActive(true);
    }
    public void BackToStart()
    {
        start.SetActive(true);
        creditButt.SetActive(true);
        tutorial.SetActive(true);
        instructions.SetActive(true);
        level.SetActive(false);
        level3DObject.SetActive(false);
        currLevel = 1;
    }
    public void EnterControls()
    {
        title.SetActive(false);
        start.SetActive(false);
        creditButt.SetActive(false);
        tutorial.SetActive(false);
        instructionsButt.SetActive(false);
        instructions.SetActive(true);
        instructions3DObject.SetActive(true);
    }
    public void LeaveControls()
    {
        title.SetActive(true);
        start.SetActive(true);
        creditButt.SetActive(true);
        tutorial.SetActive(true);
        instructionsButt.SetActive(true);
        instructions.SetActive(false);
        instructions3DObject.SetActive(false);
    }
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
        FindObjectOfType<InGameController>().racerType = 1;
        FindObjectOfType<InGameController>().playerCarColor = "red";
    }
    public void LeftLevel()
    {
        currLevel--;
        if(currLevel <= 0)
        {
            currLevel = 4;
        }
        DisplayLevel(currLevel);
    }
    public void RightLevel()
    {
        currLevel++;
        if (currLevel > 4)
        {
            currLevel = 1;
        }
        DisplayLevel(currLevel);
    }
    void DisplayLevel(int level)
    {
        switch (level)
        {
            case 1:
                level3DObject.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Models/Prototype_Track_1");
                level3DObject.transform.localScale = new Vector3(5, 5, 5);
                level3DObject.transform.localEulerAngles = new Vector3(180, 90, -90);
                //circle
                circle.SetActive(true);
                butter.SetActive(false);
                dollar.SetActive(false);
                wave.SetActive(false);
                break;
            case 2:
                level3DObject.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Models/Prototype_Butterfly_Track_1");
                level3DObject.transform.localScale = new Vector3(.06f, .06f, .06f);
                level3DObject.transform.localEulerAngles = new Vector3(180, 90, -90);
                //butter
                circle.SetActive(false);
                butter.SetActive(true);
                dollar.SetActive(false);
                wave.SetActive(false);
                break;
            case 3:
                level3DObject.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Models/Prototype_Dollar_Track_1");
                level3DObject.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
                level3DObject.transform.localEulerAngles = new Vector3(90, 90, -90);
                //dollar
                circle.SetActive(false);
                butter.SetActive(false);
                dollar.SetActive(true);
                wave.SetActive(false);
                break;
            case 4:
                level3DObject.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Models/Prototype_Wave_Track_1");
                level3DObject.transform.localScale = new Vector3(0.09f, 0.09f, 0.09f);
                level3DObject.transform.localEulerAngles = new Vector3(90, 90, -90);
                //wave
                circle.SetActive(false);
                butter.SetActive(false);
                dollar.SetActive(false);
                wave.SetActive(true);
                break;
            default:
                level3DObject.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Models/Prototype_Track_1");
                //circle
                circle.SetActive(true);
                butter.SetActive(false);
                dollar.SetActive(false);
                wave.SetActive(false);
                break;
        }
    }
    public void MattTest()
    {
        SceneToLoad = "Circle";
        level.SetActive(false);
        level3DObject.SetActive(false);
        car.SetActive(true);
        //SceneManager.LoadScene("Circle");//rename later maybe
    }
    public void Butterfly()
    {
        SceneToLoad = "ButterflyTest";
        level.SetActive(false);
        level3DObject.SetActive(false);
        car.SetActive(true);
        //SceneManager.LoadScene("ButterflyTest");//rename later maybe
    }
    public void Wave()
    {
        SceneToLoad = "WaveTest";
        level.SetActive(false);
        level3DObject.SetActive(false);
        car.SetActive(true);
        //SceneManager.LoadScene("WaveTest");//rename later maybe
    }
    public void Dollar()
    {
        SceneToLoad = "DollarTest";
        level.SetActive(false);
        level3DObject.SetActive(false);
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
